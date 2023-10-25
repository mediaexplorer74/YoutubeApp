// Decompiled with JetBrains decompiler
// Type: Google.Apis.Auth.OAuth2.Web.AuthorizationCodeWebApp
// Assembly: Google.Apis.Auth, Version=1.30.0.0, Culture=neutral, PublicKeyToken=4b01fa6e34db77ab
// MVID: A0E91A90-D8FA-470E-9253-CAE205F78A22
// Assembly location: C:\Users\Admin\Desktop\re\YoutubeApp\Google.Apis.Auth.dll

using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2.Requests;
using Google.Apis.Auth.OAuth2.Responses;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Google.Apis.Auth.OAuth2.Web
{
  public class AuthorizationCodeWebApp
  {
    public const string StateKey = "oauth_";
    public const int StateRandomLength = 8;
    private readonly IAuthorizationCodeFlow flow;
    private readonly string redirectUri;
    private readonly string state;

    public IAuthorizationCodeFlow Flow => this.flow;

    public string RedirectUri => this.redirectUri;

    public string State => this.state;

    public AuthorizationCodeWebApp(IAuthorizationCodeFlow flow, string redirectUri, string state)
    {
      this.flow = flow;
      this.redirectUri = redirectUri;
      this.state = state;
    }

    public async Task<AuthorizationCodeWebApp.AuthResult> AuthorizeAsync(
      string userId,
      CancellationToken taskCancellationToken)
    {
      TokenResponse token = await this.Flow.LoadTokenAsync(userId, taskCancellationToken).ConfigureAwait(false);
      if (this.ShouldRequestAuthorizationCode(token))
      {
        AuthorizationCodeRequestUrl codeRequest = this.Flow.CreateAuthorizationCodeRequest(this.redirectUri);
        string oauthState = this.state;
        if (this.Flow.DataStore != null)
        {
          oauthState += new Random().Next(int.Parse(new string('9', 8))).ToString("D" + (object) 8);
          await this.Flow.DataStore.StoreAsync<string>("oauth_" + userId, oauthState).ConfigureAwait(false);
        }
        codeRequest.State = oauthState;
        return new AuthorizationCodeWebApp.AuthResult()
        {
          RedirectUri = codeRequest.Build().AbsoluteUri
        };
      }
      return new AuthorizationCodeWebApp.AuthResult()
      {
        Credential = new UserCredential(this.flow, userId, token)
      };
    }

    public bool ShouldRequestAuthorizationCode(TokenResponse token)
    {
      if (this.Flow.ShouldForceTokenRetrieval() || token == null)
        return true;
      return token.RefreshToken == null && token.IsExpired(this.flow.Clock);
    }

    public class AuthResult
    {
      public UserCredential Credential { get; set; }

      public string RedirectUri { get; set; }
    }
  }
}
