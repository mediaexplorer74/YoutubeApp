// Decompiled with JetBrains decompiler
// Type: Google.Apis.Auth.OAuth2.AuthorizationCodeInstalledApp
// Assembly: Google.Apis.Auth, Version=1.30.0.0, Culture=neutral, PublicKeyToken=4b01fa6e34db77ab
// MVID: A0E91A90-D8FA-470E-9253-CAE205F78A22
// Assembly location: C:\Users\Admin\Desktop\re\YoutubeApp\Google.Apis.Auth.dll

using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2.Responses;
using Google.Apis.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace Google.Apis.Auth.OAuth2
{
  public class AuthorizationCodeInstalledApp : IAuthorizationCodeInstalledApp
  {
    private static readonly ILogger Logger = ApplicationContext.Logger.ForType<AuthorizationCodeInstalledApp>();
    private readonly IAuthorizationCodeFlow flow;
    private readonly ICodeReceiver codeReceiver;

    public AuthorizationCodeInstalledApp(IAuthorizationCodeFlow flow, ICodeReceiver codeReceiver)
    {
      this.flow = flow;
      this.codeReceiver = codeReceiver;
    }

    public IAuthorizationCodeFlow Flow => this.flow;

    public ICodeReceiver CodeReceiver => this.codeReceiver;

    public async Task<UserCredential> AuthorizeAsync(
      string userId,
      CancellationToken taskCancellationToken)
    {
      TokenResponse token = await this.Flow.LoadTokenAsync(userId, taskCancellationToken).ConfigureAwait(false);
      if (this.ShouldRequestAuthorizationCode(token))
      {
        string redirectUri = this.CodeReceiver.RedirectUri;
        AuthorizationCodeResponseUrl authorizationCode = await this.CodeReceiver.ReceiveCodeAsync(this.Flow.CreateAuthorizationCodeRequest(redirectUri), taskCancellationToken).ConfigureAwait(false);
        if (string.IsNullOrEmpty(authorizationCode.Code))
        {
          TokenErrorResponse error = new TokenErrorResponse(authorizationCode);
          AuthorizationCodeInstalledApp.Logger.Info("Received an error. The response is: {0}", (object) error);
          throw new TokenResponseException(error);
        }
        AuthorizationCodeInstalledApp.Logger.Debug("Received \"{0}\" code", (object) authorizationCode.Code);
        token = await this.Flow.ExchangeCodeForTokenAsync(userId, authorizationCode.Code, redirectUri, taskCancellationToken).ConfigureAwait(false);
        redirectUri = (string) null;
      }
      return new UserCredential(this.flow, userId, token);
    }

    public bool ShouldRequestAuthorizationCode(TokenResponse token)
    {
      if (this.Flow.ShouldForceTokenRetrieval() || token == null)
        return true;
      return token.RefreshToken == null && token.IsExpired(this.flow.Clock);
    }
  }
}
