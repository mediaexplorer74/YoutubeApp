// Decompiled with JetBrains decompiler
// Type: Google.Apis.Auth.OAuth2.ServiceAccountCredential
// Assembly: Google.Apis.Auth, Version=1.30.0.0, Culture=neutral, PublicKeyToken=4b01fa6e34db77ab
// MVID: A0E91A90-D8FA-470E-9253-CAE205F78A22
// Assembly location: C:\Users\Admin\Desktop\re\YoutubeApp\Google.Apis.Auth.dll

using Google.Apis.Auth.OAuth2.Requests;
using Google.Apis.Auth.OAuth2.Responses;
using Google.Apis.Json;
using Google.Apis.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Google.Apis.Auth.OAuth2
{
  public class ServiceAccountCredential : ServiceCredential
  {
    private const string Sha256Oid = "2.16.840.1.101.3.4.2.1";
    protected static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
    private readonly string id;
    private readonly string user;
    private readonly IEnumerable<string> scopes;
    private readonly RSA key;

    public string Id => this.id;

    public string User => this.user;

    public IEnumerable<string> Scopes => this.scopes;

    public RSA Key => this.key;

    internal bool HasScopes => this.scopes != null && this.scopes.Any<string>();

    public ServiceAccountCredential(ServiceAccountCredential.Initializer initializer)
      : base((ServiceCredential.Initializer) initializer)
    {
      this.id = initializer.Id.ThrowIfNullOrEmpty("initializer.Id");
      this.user = initializer.User;
      this.scopes = initializer.Scopes;
      this.key = initializer.Key.ThrowIfNull<RSA>("initializer.Key");
    }

    public static ServiceAccountCredential FromServiceAccountData(Stream credentialData)
    {
      if (GoogleCredential.FromStream(credentialData).UnderlyingCredential is ServiceAccountCredential underlyingCredential)
        return underlyingCredential;
      throw new InvalidOperationException("JSON data does not represent a valid service account credential.");
    }

    public override async Task<bool> RequestAccessTokenAsync(CancellationToken taskCancellationToken)
    {
      ServiceAccountCredential accountCredential = this;
      GoogleAssertionTokenRequest request = new GoogleAssertionTokenRequest()
      {
        Assertion = accountCredential.CreateAssertionFromPayload((JsonWebSignature.Payload) accountCredential.CreatePayload())
      };
      ServiceCredential.Logger.Debug("Request a new access token. Assertion data is: " + request.Assertion);
      TokenResponse tokenResponse = await request.ExecuteAsync((System.Net.Http.HttpClient) accountCredential.HttpClient, accountCredential.TokenServerUrl, taskCancellationToken, accountCredential.Clock).ConfigureAwait(false);
      accountCredential.Token = tokenResponse;
      return true;
    }

    public override async Task<string> GetAccessTokenForRequestAsync(
      string authUri = null,
      CancellationToken cancellationToken = default (CancellationToken))
    {
      return !this.HasScopes && authUri != null ? this.CreateJwtAccessToken(authUri) : await base.GetAccessTokenForRequestAsync(authUri, cancellationToken).ConfigureAwait(false);
    }

    private string CreateJwtAccessToken(string authUri)
    {
      int totalSeconds = (int) (this.Clock.UtcNow - ServiceAccountCredential.UnixEpoch).TotalSeconds;
      JsonWebSignature.Payload payload = new JsonWebSignature.Payload();
      payload.Issuer = this.Id;
      payload.Subject = this.Id;
      payload.Audience = (object) authUri;
      payload.IssuedAtTimeSeconds = new long?((long) totalSeconds);
      payload.ExpirationTimeSeconds = new long?((long) (totalSeconds + 3600));
      return this.CreateAssertionFromPayload(payload);
    }

    private string CreateAssertionFromPayload(JsonWebSignature.Payload payload)
    {
      string serializedHeader = ServiceAccountCredential.CreateSerializedHeader();
      string str = NewtonsoftJsonSerializer.Instance.Serialize((object) payload);
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append(this.UrlSafeBase64Encode(serializedHeader)).Append('.').Append(this.UrlSafeBase64Encode(str));
      string signature = this.CreateSignature(Encoding.ASCII.GetBytes(stringBuilder.ToString()));
      stringBuilder.Append('.').Append(this.UrlSafeEncode(signature));
      return stringBuilder.ToString();
    }

    public string CreateSignature(byte[] data)
    {
      data.ThrowIfNull<byte[]>(nameof (data));
      using (SHA256 shA256 = SHA256.Create())
        return Convert.ToBase64String(this.key.SignHash(shA256.ComputeHash(data), HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1));
    }

    private static string CreateSerializedHeader()
    {
      GoogleJsonWebSignature.Header header = new GoogleJsonWebSignature.Header();
      header.Algorithm = "RS256";
      header.Type = "JWT";
      return NewtonsoftJsonSerializer.Instance.Serialize((object) header);
    }

    private GoogleJsonWebSignature.Payload CreatePayload()
    {
      int totalSeconds = (int) (this.Clock.UtcNow - ServiceAccountCredential.UnixEpoch).TotalSeconds;
      GoogleJsonWebSignature.Payload payload = new GoogleJsonWebSignature.Payload();
      payload.Issuer = this.Id;
      payload.Audience = (object) this.TokenServerUrl;
      payload.IssuedAtTimeSeconds = new long?((long) totalSeconds);
      payload.ExpirationTimeSeconds = new long?((long) (totalSeconds + 3600));
      payload.Subject = this.User;
      payload.Scope = string.Join(" ", this.Scopes);
      return payload;
    }

    private string UrlSafeBase64Encode(string value) => this.UrlSafeBase64Encode(Encoding.UTF8.GetBytes(value));

    private string UrlSafeBase64Encode(byte[] bytes) => this.UrlSafeEncode(Convert.ToBase64String(bytes));

    private string UrlSafeEncode(string base64Value) => base64Value.Replace("=", string.Empty).Replace('+', '-').Replace('/', '_');

    public new class Initializer : ServiceCredential.Initializer
    {
      public string Id { get; private set; }

      public string User { get; set; }

      public IEnumerable<string> Scopes { get; set; }

      public RSA Key { get; set; }

      public Initializer(string id)
        : this(id, "https://www.googleapis.com/oauth2/v4/token")
      {
      }

      public Initializer(string id, string tokenServerUrl)
        : base(tokenServerUrl)
      {
        this.Id = id;
        this.Scopes = (IEnumerable<string>) new List<string>();
      }

      public ServiceAccountCredential.Initializer FromPrivateKey(string privateKey)
      {
        RSAParameters parameters = Pkcs8.DecodeRsaParameters(privateKey);
        this.Key = RSA.Create();
        this.Key.ImportParameters(parameters);
        return this;
      }

      public ServiceAccountCredential.Initializer FromCertificate(X509Certificate2 certificate)
      {
        this.Key = certificate.GetRSAPrivateKey();
        return this;
      }
    }
  }
}
