// Decompiled with JetBrains decompiler
// Type: Google.Apis.Auth.OAuth2.Flows.GoogleAuthorizationCodeFlow
// Assembly: Google.Apis.Auth, Version=1.30.0.0, Culture=neutral, PublicKeyToken=4b01fa6e34db77ab
// MVID: A0E91A90-D8FA-470E-9253-CAE205F78A22
// Assembly location: C:\Users\Admin\Desktop\re\YoutubeApp\Google.Apis.Auth.dll

using Google.Apis.Auth.OAuth2.Requests;
using Google.Apis.Auth.OAuth2.Responses;
using Google.Apis.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Google.Apis.Auth.OAuth2.Flows
{
  public class GoogleAuthorizationCodeFlow : AuthorizationCodeFlow
  {
    private readonly string revokeTokenUrl;
    public readonly bool? includeGrantedScopes;
    private readonly IEnumerable<KeyValuePair<string, string>> userDefinedQueryParams;

    public string RevokeTokenUrl => this.revokeTokenUrl;

    public bool? IncludeGrantedScopes => this.includeGrantedScopes;

    public IEnumerable<KeyValuePair<string, string>> UserDefinedQueryParams => this.userDefinedQueryParams;

    public GoogleAuthorizationCodeFlow(
      GoogleAuthorizationCodeFlow.Initializer initializer)
      : base((AuthorizationCodeFlow.Initializer) initializer)
    {
      this.revokeTokenUrl = initializer.RevokeTokenUrl;
      this.includeGrantedScopes = initializer.IncludeGrantedScopes;
      this.userDefinedQueryParams = initializer.UserDefinedQueryParams;
    }

    public override AuthorizationCodeRequestUrl CreateAuthorizationCodeRequest(string redirectUri)
    {
      GoogleAuthorizationCodeRequestUrl authorizationCodeRequest = new GoogleAuthorizationCodeRequestUrl(new Uri(this.AuthorizationServerUrl));
      authorizationCodeRequest.ClientId = this.ClientSecrets.ClientId;
      authorizationCodeRequest.Scope = string.Join(" ", this.Scopes);
      authorizationCodeRequest.RedirectUri = redirectUri;
      authorizationCodeRequest.IncludeGrantedScopes = this.IncludeGrantedScopes.HasValue ? this.IncludeGrantedScopes.Value.ToString().ToLower() : (string) null;
      authorizationCodeRequest.UserDefinedQueryParams = this.UserDefinedQueryParams;
      return (AuthorizationCodeRequestUrl) authorizationCodeRequest;
    }

    public override async Task RevokeTokenAsync(
      string userId,
      string token,
      CancellationToken taskCancellationToken)
    {
      GoogleAuthorizationCodeFlow authorizationCodeFlow = this;
      HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, new GoogleRevokeTokenRequest(new Uri(authorizationCodeFlow.RevokeTokenUrl))
      {
        Token = token
      }.Build());
      HttpResponseMessage response = await authorizationCodeFlow.HttpClient.SendAsync(request, taskCancellationToken).ConfigureAwait(false);
      if (!response.IsSuccessStatusCode)
        throw new TokenResponseException(NewtonsoftJsonSerializer.Instance.Deserialize<TokenErrorResponse>(await response.Content.ReadAsStringAsync().ConfigureAwait(false)), new HttpStatusCode?(response.StatusCode));
      // ISSUE: explicit non-virtual call
      //await __nonvirtual
      await (authorizationCodeFlow.DeleteTokenAsync(userId, taskCancellationToken));
    }

    public override bool ShouldForceTokenRetrieval() => this.IncludeGrantedScopes.HasValue && this.IncludeGrantedScopes.Value;

    public new class Initializer : AuthorizationCodeFlow.Initializer
    {
      public string RevokeTokenUrl { get; set; }

      public bool? IncludeGrantedScopes { get; set; }

      public IEnumerable<KeyValuePair<string, string>> UserDefinedQueryParams { get; set; }

      public Initializer()
        : this("https://accounts.google.com/o/oauth2/v2/auth", "https://www.googleapis.com/oauth2/v4/token", "https://accounts.google.com/o/oauth2/revoke")
      {
      }

      protected Initializer(
        string authorizationServerUrl,
        string tokenServerUrl,
        string revokeTokenUrl)
        : base(authorizationServerUrl, tokenServerUrl)
      {
        this.RevokeTokenUrl = revokeTokenUrl;
      }
    }
  }
}
