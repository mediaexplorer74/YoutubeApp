// Decompiled with JetBrains decompiler
// Type: Google.Apis.Auth.OAuth2.Flows.IAuthorizationCodeFlow
// Assembly: Google.Apis.Auth, Version=1.30.0.0, Culture=neutral, PublicKeyToken=4b01fa6e34db77ab
// MVID: A0E91A90-D8FA-470E-9253-CAE205F78A22
// Assembly location: C:\Users\Admin\Desktop\re\YoutubeApp\Google.Apis.Auth.dll

using Google.Apis.Auth.OAuth2.Requests;
using Google.Apis.Auth.OAuth2.Responses;
using Google.Apis.Util;
using Google.Apis.Util.Store;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Google.Apis.Auth.OAuth2.Flows
{
  public interface IAuthorizationCodeFlow : IDisposable
  {
    IAccessMethod AccessMethod { get; }

    IClock Clock { get; }

    IDataStore DataStore { get; }

    Task<TokenResponse> LoadTokenAsync(string userId, CancellationToken taskCancellationToken);

    Task DeleteTokenAsync(string userId, CancellationToken taskCancellationToken);

    AuthorizationCodeRequestUrl CreateAuthorizationCodeRequest(string redirectUri);

    Task<TokenResponse> ExchangeCodeForTokenAsync(
      string userId,
      string code,
      string redirectUri,
      CancellationToken taskCancellationToken);

    Task<TokenResponse> RefreshTokenAsync(
      string userId,
      string refreshToken,
      CancellationToken taskCancellationToken);

    Task RevokeTokenAsync(string userId, string token, CancellationToken taskCancellationToken);

    bool ShouldForceTokenRetrieval();
  }
}
