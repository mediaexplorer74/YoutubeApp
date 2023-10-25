// Decompiled with JetBrains decompiler
// Type: Google.Apis.Auth.OAuth2.UserCredential
// Assembly: Google.Apis.Auth, Version=1.30.0.0, Culture=neutral, PublicKeyToken=4b01fa6e34db77ab
// MVID: A0E91A90-D8FA-470E-9253-CAE205F78A22
// Assembly location: C:\Users\Admin\Desktop\re\YoutubeApp\Google.Apis.Auth.dll

using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2.Responses;
using Google.Apis.Http;
using Google.Apis.Logging;
using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Google.Apis.Auth.OAuth2
{
  public class UserCredential : 
    ICredential,
    IConfigurableHttpClientInitializer,
    ITokenAccess,
    IHttpExecuteInterceptor,
    IHttpUnsuccessfulResponseHandler
  {
    protected static readonly ILogger Logger = ApplicationContext.Logger.ForType<UserCredential>();
    private TokenResponse token;
    private object lockObject = new object();
    private readonly IAuthorizationCodeFlow flow;
    private readonly string userId;

    public TokenResponse Token
    {
      get
      {
        lock (this.lockObject)
          return this.token;
      }
      set
      {
        lock (this.lockObject)
          this.token = value;
      }
    }

    public IAuthorizationCodeFlow Flow => this.flow;

    public string UserId => this.userId;

    public UserCredential(IAuthorizationCodeFlow flow, string userId, TokenResponse token)
    {
      this.flow = flow;
      this.userId = userId;
      this.token = token;
    }

    public async Task InterceptAsync(
      HttpRequestMessage request,
      CancellationToken taskCancellationToken)
    {
      string str = await this.GetAccessTokenForRequestAsync(request.RequestUri.AbsoluteUri, taskCancellationToken).ConfigureAwait(false);
      this.flow.AccessMethod.Intercept(request, this.Token.AccessToken);
    }

    public async Task<bool> HandleResponseAsync(HandleUnsuccessfulResponseArgs args)
    {
      if (args.Response.StatusCode != HttpStatusCode.Unauthorized)
        return false;
      bool flag = !object.Equals((object) this.Token.AccessToken, (object) this.flow.AccessMethod.GetAccessToken(args.Request));
      if (!flag)
        flag = await this.RefreshTokenAsync(args.CancellationToken).ConfigureAwait(false);
      return flag;
    }

    public void Initialize(ConfigurableHttpClient httpClient)
    {
      httpClient.MessageHandler.AddExecuteInterceptor((IHttpExecuteInterceptor) this);
      httpClient.MessageHandler.AddUnsuccessfulResponseHandler((IHttpUnsuccessfulResponseHandler) this);
    }

    public virtual async Task<string> GetAccessTokenForRequestAsync(
      string authUri = null,
      CancellationToken cancellationToken = default (CancellationToken))
    {
      if (this.Token.IsExpired(this.flow.Clock))
      {
        UserCredential.Logger.Debug("Token has expired, trying to refresh it.");
        if (!await this.RefreshTokenAsync(cancellationToken).ConfigureAwait(false))
          throw new InvalidOperationException("The access token has expired but we can't refresh it");
      }
      return this.token.AccessToken;
    }

    public async Task<bool> RefreshTokenAsync(CancellationToken taskCancellationToken)
    {
      if (this.Token.RefreshToken == null)
      {
        UserCredential.Logger.Warning("Refresh token is null, can't refresh the token!");
        return false;
      }
      TokenResponse tokenResponse = await this.flow.RefreshTokenAsync(this.userId, this.Token.RefreshToken, taskCancellationToken).ConfigureAwait(false);
      UserCredential.Logger.Info("Access token was refreshed successfully");
      if (tokenResponse.RefreshToken == null)
        tokenResponse.RefreshToken = this.Token.RefreshToken;
      this.Token = tokenResponse;
      return true;
    }

    public async Task<bool> RevokeTokenAsync(CancellationToken taskCancellationToken)
    {
      if (this.Token == null)
      {
        UserCredential.Logger.Warning("Token is already null, no need to revoke it.");
        return false;
      }
      await this.flow.RevokeTokenAsync(this.userId, this.Token.AccessToken, taskCancellationToken).ConfigureAwait(false);
      UserCredential.Logger.Info("Access token was revoked successfully");
      return true;
    }
  }
}
