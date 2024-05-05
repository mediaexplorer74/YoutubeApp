// Type: Google.Apis.Auth.OAuth2.AuthorizationCodeInstalledApp
// Assembly: Google.Apis.Auth, Version=1.30.0.0, Culture=neutral, PublicKeyToken=4b01fa6e34db77ab

using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2.Responses;
using Google.Apis.Logging;
using System;
using System.Diagnostics;
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

        Requests.AuthorizationCodeRequestUrl floww = this.Flow.CreateAuthorizationCodeRequest(redirectUri);

        AuthorizationCodeResponseUrl authorizationCode = 
             await this.CodeReceiver.ReceiveCodeAsync(floww, taskCancellationToken).ConfigureAwait(false);

                //Dirty hack
                authorizationCode.code = authorizationCode.approvalCode;

                if (string.IsNullOrEmpty(authorizationCode.code))
                {
                    TokenErrorResponse error = new TokenErrorResponse(authorizationCode);
                    AuthorizationCodeInstalledApp.Logger.Info("Received an error. The response is: {0}", 
                        (object)error);
                    throw new TokenResponseException(error);
                }
                else
                {
                    authorizationCode.code = Uri.UnescapeDataString(authorizationCode.code);

                    Debug.WriteLine("[i] Code=" + authorizationCode.code);
                    
                    //authorizationCode.code = authorizationCode.code.Substring(3);
                }

        //AuthorizationCodeInstalledApp.Logger.Debug("Received \"{0}\" code", authorizationCode.Code);
        //Debug.WriteLine("Received \"{0}\" code", authorizationCode.Code);

        token = await this.Flow.ExchangeCodeForTokenAsync(userId, authorizationCode.code, 
            redirectUri, taskCancellationToken).ConfigureAwait(false);

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
