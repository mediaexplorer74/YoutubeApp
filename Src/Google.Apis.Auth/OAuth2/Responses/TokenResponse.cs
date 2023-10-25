// Decompiled with JetBrains decompiler
// Type: Google.Apis.Auth.OAuth2.Responses.TokenResponse
// Assembly: Google.Apis.Auth, Version=1.30.0.0, Culture=neutral, PublicKeyToken=4b01fa6e34db77ab
// MVID: A0E91A90-D8FA-470E-9253-CAE205F78A22
// Assembly location: C:\Users\Admin\Desktop\re\YoutubeApp\Google.Apis.Auth.dll

using Google.Apis.Json;
using Google.Apis.Logging;
using Google.Apis.Util;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Google.Apis.Auth.OAuth2.Responses
{
  public class TokenResponse
  {
    private const int TokenExpiryTimeWindowSeconds = 300;

    [JsonProperty("access_token")]
    public string AccessToken { get; set; }

    [JsonProperty("token_type")]
    public string TokenType { get; set; }

    [JsonProperty("expires_in")]
    public long? ExpiresInSeconds { get; set; }

    [JsonProperty("refresh_token")]
    public string RefreshToken { get; set; }

    [JsonProperty("scope")]
    public string Scope { get; set; }

    [JsonProperty("id_token")]
    public string IdToken { get; set; }

    [Obsolete("Use IssuedUtc instead")]
    [JsonProperty(Order = 1)]
    public DateTime Issued
    {
      get => this.IssuedUtc.ToLocalTime();
      set => this.IssuedUtc = value.ToUniversalTime();
    }

    [JsonProperty(Order = 2)]
    public DateTime IssuedUtc { get; set; }

    public bool IsExpired(IClock clock) => this.AccessToken == null || !this.ExpiresInSeconds.HasValue || this.IssuedUtc.AddSeconds((double) (this.ExpiresInSeconds.Value - 300L)) <= clock.UtcNow;

    public static async Task<TokenResponse> FromHttpResponseAsync(
      HttpResponseMessage response,
      IClock clock,
      ILogger logger)
    {
      string input = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
      string str = "";
      TokenResponse tokenResponse1;
      try
      {
        if (!response.IsSuccessStatusCode)
        {
          str = "TokenErrorResponse";
          throw new TokenResponseException(NewtonsoftJsonSerializer.Instance.Deserialize<TokenErrorResponse>(input), new HttpStatusCode?(response.StatusCode));
        }
        str = nameof (TokenResponse);
        TokenResponse tokenResponse2 = NewtonsoftJsonSerializer.Instance.Deserialize<TokenResponse>(input);
        tokenResponse2.IssuedUtc = clock.UtcNow;
        tokenResponse1 = tokenResponse2;
      }
      catch (JsonException ex)
      {
        logger.Error((Exception) ex, string.Format("Exception was caught when deserializing {0}. Content is: {1}", (object) str, (object) input));
        throw new TokenResponseException(new TokenErrorResponse()
        {
          Error = "Server response does not contain a JSON object. Status code is: " + (object) response.StatusCode
        }, new HttpStatusCode?(response.StatusCode));
      }
      return tokenResponse1;
    }
  }
}
