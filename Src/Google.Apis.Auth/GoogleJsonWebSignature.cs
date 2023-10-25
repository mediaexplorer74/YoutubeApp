// Decompiled with JetBrains decompiler
// Type: Google.Apis.Auth.GoogleJsonWebSignature
// Assembly: Google.Apis.Auth, Version=1.30.0.0, Culture=neutral, PublicKeyToken=4b01fa6e34db77ab
// MVID: A0E91A90-D8FA-470E-9253-CAE205F78A22
// Assembly location: C:\Users\Admin\Desktop\re\YoutubeApp\Google.Apis.Auth.dll

using Google.Apis.Json;
using Google.Apis.Util;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Google.Apis.Auth
{
  public class GoogleJsonWebSignature
  {
    internal const int MaxJwtLength = 10000;
    internal static readonly TimeSpan CertCacheRefreshInterval = TimeSpan.FromHours(1.0);
    private const string Sha256Oid = "2.16.840.1.101.3.4.2.1";
    private const string SupportedJwtAlgorithm = "RS256";
    private static readonly IEnumerable<string> ValidJwtIssuers = (IEnumerable<string>) new string[2]
    {
      "https://accounts.google.com",
      "accounts.google.com"
    };
    private static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
    private static SemaphoreSlim _certCacheLock = new SemaphoreSlim(1);
    internal static DateTime _certCacheDownloadTime;
    private static List<RSA> _certCache;

    public static Task<GoogleJsonWebSignature.Payload> ValidateAsync(
      string jwt,
      IClock clock = null,
      bool forceGoogleCertRefresh = false)
    {
      return GoogleJsonWebSignature.ValidateAsync(jwt, new GoogleJsonWebSignature.ValidationSettings()
      {
        Clock = clock,
        ForceGoogleCertRefresh = forceGoogleCertRefresh
      });
    }

    public static Task<GoogleJsonWebSignature.Payload> ValidateAsync(
      string jwt,
      GoogleJsonWebSignature.ValidationSettings validationSettings)
    {
      return GoogleJsonWebSignature.ValidateInternalAsync(jwt, validationSettings, (string) null, false);
    }

    internal static async Task<GoogleJsonWebSignature.Payload> ValidateInternalAsync(
      string jwt,
      GoogleJsonWebSignature.ValidationSettings validationSettings,
      string certsJson,
      bool ignoreCertCheck)
    {
      jwt.ThrowIfNull<string>(nameof (jwt));
      jwt.ThrowIfNullOrEmpty(nameof (jwt));
      GoogleJsonWebSignature.ValidationSettings settings = validationSettings.ThrowIfNull<GoogleJsonWebSignature.ValidationSettings>(nameof (validationSettings)).Clone();
      IClock clock = settings.Clock ?? SystemClock.Default;
      string[] strArray = jwt.Length <= 10000 ? jwt.Split('.') : throw new InvalidJwtException(string.Format("JWT exceeds maximum allowed length of {0}", (object) 10000));
      GoogleJsonWebSignature.Header header = strArray.Length == 3 ? NewtonsoftJsonSerializer.Instance.Deserialize<GoogleJsonWebSignature.Header>(GoogleJsonWebSignature.Base64UrlToString(strArray[0])) : throw new InvalidJwtException("JWT must consist of Header, Payload, and Signature");
      GoogleJsonWebSignature.Payload payload = NewtonsoftJsonSerializer.Instance.Deserialize<GoogleJsonWebSignature.Payload>(GoogleJsonWebSignature.Base64UrlToString(strArray[1]));
      byte[] signature = GoogleJsonWebSignature.Base64UrlDecode(strArray[2]);
      if (header.Algorithm != "RS256")
        throw new InvalidJwtException(string.Format("JWT algorithm must be '{0}'", (object) "RS256"));
      if (!ignoreCertCheck)
      {
        byte[] hash;
        using (SHA256 shA256 = SHA256.Create())
          hash = shA256.ComputeHash(Encoding.ASCII.GetBytes(string.Format("{0}.{1}", (object) strArray[0], (object) strArray[1])));
        bool verifiedOk = false;
        foreach (RSA rsa in await GoogleJsonWebSignature.GetGoogleCertsAsync(clock, settings.ForceGoogleCertRefresh, certsJson))
        {
          verifiedOk = rsa.VerifyHash(hash, signature, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
          if (verifiedOk)
            break;
        }
        if (!verifiedOk)
          throw new InvalidJwtException("JWT invalid, unable to verify signature.");
        hash = (byte[]) null;
      }
      if (!GoogleJsonWebSignature.ValidJwtIssuers.Contains<string>(payload.Issuer))
        throw new InvalidJwtException(string.Format("JWT issuer incorrect. Must be one of: {0}", (object) string.Join(", ", GoogleJsonWebSignature.ValidJwtIssuers.Select<string, string>((Func<string, string>) (x => string.Format("'{0}'", (object) x))))));
      if (settings.Audience != null && payload.AudienceAsList.Except<string>(settings.Audience).Any<string>())
        throw new InvalidJwtException("JWT contains untrusted 'aud' claim.");
      long? nullable = payload.IssuedAtTimeSeconds;
      nullable = nullable.HasValue ? payload.ExpirationTimeSeconds : throw new InvalidJwtException("JWT must contain 'iat' and 'exp' claims");
      if (nullable.HasValue)
      {
        double totalSeconds = (clock.UtcNow - GoogleJsonWebSignature.UnixEpoch).TotalSeconds;
        nullable = payload.IssuedAtTimeSeconds;
        if (totalSeconds < (double) nullable.Value)
          throw new InvalidJwtException("JWT is not yet valid.");
        nullable = payload.ExpirationTimeSeconds;
        if (totalSeconds > (double) nullable.Value)
          throw new InvalidJwtException("JWT has expired.");
        if (settings.HostedDomain != null && payload.HostedDomain != settings.HostedDomain)
          throw new InvalidJwtException("JWT contains invalid 'hd' claim.");
        return payload;
      }
      return default;
    }

    private static string Base64UrlToString(string base64Url) => Encoding.UTF8.GetString(GoogleJsonWebSignature.Base64UrlDecode(base64Url));

    private static byte[] Base64UrlDecode(string base64Url)
    {
      string s = base64Url.Replace('-', '+').Replace('_', '/');
      switch (s.Length % 4)
      {
        case 2:
          s += "==";
          break;
        case 3:
          s += "=";
          break;
      }
      return Convert.FromBase64String(s);
    }

    internal static async Task<List<RSA>> GetGoogleCertsAsync(
      IClock clock,
      bool forceGoogleCertRefresh,
      string certsJson)
    {
      DateTime now = clock.UtcNow;
      await GoogleJsonWebSignature._certCacheLock.WaitAsync();
      List<RSA> certCache;
      try
      {
        if (forceGoogleCertRefresh || GoogleJsonWebSignature._certCache == null || GoogleJsonWebSignature._certCacheDownloadTime + GoogleJsonWebSignature.CertCacheRefreshInterval < now)
        {
          using (HttpClient httpClient = new HttpClient())
          {
            if (certsJson == null)
              certsJson = await httpClient.GetStringAsync("https://www.googleapis.com/oauth2/v3/certs");
          }
          GoogleJsonWebSignature._certCache = GoogleJsonWebSignature.GetGoogleCertsFromJson(certsJson);
          GoogleJsonWebSignature._certCacheDownloadTime = now;
        }
        certCache = GoogleJsonWebSignature._certCache;
      }
      finally
      {
        GoogleJsonWebSignature._certCacheLock.Release();
      }
      return certCache;
    }

    private static List<RSA> GetGoogleCertsFromJson(string json) => JToken.Parse(json)[(object) "keys"].AsEnumerable<JToken>().Select<JToken, RSA>((Func<JToken, RSA>) (key =>
    {
      RSA googleCertsFromJson = RSA.Create();
      googleCertsFromJson.ImportParameters(new RSAParameters()
      {
        Modulus = GoogleJsonWebSignature.Base64UrlDecode((string) key[(object) "n"]),
        Exponent = GoogleJsonWebSignature.Base64UrlDecode((string) key[(object) "e"])
      });
      return googleCertsFromJson;
    })).ToList<RSA>();

    public sealed class ValidationSettings
    {
      public ValidationSettings()
      {
      }

      private ValidationSettings(GoogleJsonWebSignature.ValidationSettings other)
      {
        IEnumerable<string> audience = other.Audience;
        this.Audience = audience != null ? (IEnumerable<string>) audience.ToArray<string>() : (IEnumerable<string>) (string[]) null;
        this.HostedDomain = other.HostedDomain;
        this.Clock = other.Clock;
        this.ForceGoogleCertRefresh = other.ForceGoogleCertRefresh;
      }

      public IEnumerable<string> Audience { get; set; }

      public string HostedDomain { get; set; }

      public IClock Clock { get; set; }

      public bool ForceGoogleCertRefresh { get; set; }

      internal GoogleJsonWebSignature.ValidationSettings Clone() => new GoogleJsonWebSignature.ValidationSettings(this);
    }

    public class Header : JsonWebSignature.Header
    {
    }

    public class Payload : JsonWebSignature.Payload
    {
      [JsonProperty("scope")]
      public string Scope { get; set; }

      [JsonProperty("prn")]
      public string Prn { get; set; }

      [JsonProperty("hd")]
      public string HostedDomain { get; set; }

      [JsonProperty("email")]
      public string Email { get; set; }

      [JsonProperty("email_verified")]
      public bool EmailVerified { get; set; }

      [JsonProperty("name")]
      public string Name { get; set; }

      [JsonProperty("given_name")]
      public string GivenName { get; set; }

      [JsonProperty("family_name")]
      public string FamilyName { get; set; }

      [JsonProperty("picture")]
      public string Picture { get; set; }

      [JsonProperty("locale")]
      public string Locale { get; set; }
    }
  }
}
