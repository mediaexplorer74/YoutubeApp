// Decompiled with JetBrains decompiler
// Type: Google.Apis.Auth.OAuth2.Requests.TokenRequestExtenstions
// Assembly: Google.Apis.Auth, Version=1.30.0.0, Culture=neutral, PublicKeyToken=4b01fa6e34db77ab
// MVID: A0E91A90-D8FA-470E-9253-CAE205F78A22
// Assembly location: C:\Users\Admin\Desktop\re\YoutubeApp\Google.Apis.Auth.dll

using Google.Apis.Auth.OAuth2.Responses;
using Google.Apis.Json;
using Google.Apis.Requests.Parameters;
using Google.Apis.Util;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Google.Apis.Auth.OAuth2.Requests
{
  public static class TokenRequestExtenstions
  {
    public static async Task<TokenResponse> ExecuteAsync(
      this TokenRequest request,
      HttpClient httpClient,
      string tokenServerUrl,
      CancellationToken taskCancellationToken,
      IClock clock)
    {
      HttpResponseMessage response = await httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Post, tokenServerUrl)
      {
        Content = (HttpContent) ParameterUtils.CreateFormUrlEncodedContent((object) request)
      }, taskCancellationToken).ConfigureAwait(false);
      string input = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
      if (!response.IsSuccessStatusCode)
        throw new TokenResponseException(NewtonsoftJsonSerializer.Instance.Deserialize<TokenErrorResponse>(input), new HttpStatusCode?(response.StatusCode));
      TokenResponse tokenResponse = NewtonsoftJsonSerializer.Instance.Deserialize<TokenResponse>(input);
      tokenResponse.IssuedUtc = clock.UtcNow;
      return tokenResponse;
    }
  }
}
