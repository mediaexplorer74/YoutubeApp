// Type: Google.Apis.Auth.OAuth2.Flows.AuthorizationCodeFlow
// Assembly: Google.Apis.Auth, Version=1.30.0.0, Culture=neutral, PublicKeyToken=4b01fa6e34db77ab

using Google.Apis.Auth.OAuth2.Requests;
using Google.Apis.Auth.OAuth2.Responses;
using Google.Apis.Http;
using Google.Apis.Logging;
using Google.Apis.Testing;
using Google.Apis.Util;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Google.Apis.Auth.OAuth2.Flows
{
  public class AuthorizationCodeFlow : IAuthorizationCodeFlow, IDisposable
  {
    private static readonly ILogger Logger = ApplicationContext.Logger.ForType<AuthorizationCodeFlow>();
    private readonly IAccessMethod accessMethod;
    private readonly string tokenServerUrl;
    private readonly string authorizationServerUrl;
    private readonly ClientSecrets clientSecrets;
    private readonly IDataStore dataStore;
    private readonly IEnumerable<string> scopes;
    private readonly ConfigurableHttpClient httpClient;
    private readonly IClock clock;

        public string TokenServerUrl
        {
            get
            {
                return this.tokenServerUrl;
            }
        }

        public string AuthorizationServerUrl
        {
            get
            {
                return this.authorizationServerUrl;
            }
        }

        public ClientSecrets ClientSecrets
        {
            get
            {
                return this.clientSecrets;
            }
        }

        public IDataStore DataStore
        {
            get
            {
                return this.dataStore;
            }
        }

        public IEnumerable<string> Scopes
        {
            get
            {
                return this.scopes;
            }
        }

        public ConfigurableHttpClient HttpClient
        {
            get
            {
                return this.httpClient;
            }
        }

        public AuthorizationCodeFlow(AuthorizationCodeFlow.Initializer initializer)
    {
      this.clientSecrets = initializer.ClientSecrets;
      if (this.clientSecrets == null)
      {
        if (initializer.ClientSecretsStream == null)
          throw new ArgumentException("You MUST set ClientSecret or ClientSecretStream on the initializer");

        using (initializer.ClientSecretsStream)
          this.clientSecrets = GoogleClientSecrets.Load(initializer.ClientSecretsStream).Secrets;
      }
      else if (initializer.ClientSecretsStream != null)
        throw new ArgumentException(
            "You CAN'T set both ClientSecrets AND ClientSecretStream on the initializer");

      this.accessMethod = initializer.AccessMethod.ThrowIfNull<IAccessMethod>("Initializer.AccessMethod");

      this.clock = initializer.Clock.ThrowIfNull<IClock>("Initializer.Clock");
      this.tokenServerUrl = initializer.TokenServerUrl.ThrowIfNullOrEmpty("Initializer.TokenServerUrl");

      this.authorizationServerUrl = initializer.AuthorizationServerUrl.ThrowIfNullOrEmpty(
          "Initializer.AuthorizationServerUrl");
      this.dataStore = initializer.DataStore;

      if (this.dataStore == null)
        AuthorizationCodeFlow.Logger.Warning(
            "Datastore is null, as a result the user's credential will not be stored");
      this.scopes = initializer.Scopes;
      CreateHttpClientArgs args = new CreateHttpClientArgs();

      if (initializer.DefaultExponentialBackOffPolicy != ExponentialBackOffPolicy.None)
        args.Initializers.Add((IConfigurableHttpClientInitializer) 
            new ExponentialBackOffInitializer(initializer.DefaultExponentialBackOffPolicy, 
            (Func<BackOffHandler>) (() => new BackOffHandler((IBackOff) new ExponentialBackOff()))));
      this.httpClient = (initializer.HttpClientFactory 
                ?? (IHttpClientFactory) new HttpClientFactory()).CreateHttpClient(args);
    }

    public IAccessMethod AccessMethod => this.accessMethod;

    public IClock Clock => this.clock;

    public async Task<TokenResponse> LoadTokenAsync(
      string userId,
      CancellationToken taskCancellationToken)
    {
      taskCancellationToken.ThrowIfCancellationRequested();

      //RnD
      //return this.DataStore == null
      //          ? (TokenResponse) null 
      //          : await this.DataStore.GetAsync<TokenResponse>(userId).ConfigureAwait(false);
      if (this.DataStore == null)
      {
         return (TokenResponse)null;
      }
      else
      {
        TokenResponse res = default;

        try
        {
            res = await this.DataStore.GetAsync<TokenResponse>(userId).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
             Debug.WriteLine("[ex] DataStore.GetAsync bug: " + ex.Message);
        }

        return res;
      }
    }

    public async Task DeleteTokenAsync(string userId, CancellationToken taskCancellationToken)
    {
      taskCancellationToken.ThrowIfCancellationRequested();
      if (this.DataStore == null)
        return;
      await this.DataStore.DeleteAsync<TokenResponse>(userId).ConfigureAwait(false);
    }

    public virtual AuthorizationCodeRequestUrl CreateAuthorizationCodeRequest(string redirectUri)
    {
      AuthorizationCodeRequestUrl authorizationCodeRequest = 
                new AuthorizationCodeRequestUrl(new Uri(this.AuthorizationServerUrl));
      authorizationCodeRequest.ClientId = this.ClientSecrets.ClientId;
      authorizationCodeRequest.Scope = string.Join(" ", this.Scopes);
      authorizationCodeRequest.RedirectUri = redirectUri;
      return authorizationCodeRequest;
    }

    public async Task<TokenResponse> ExchangeCodeForTokenAsync(
      string userId,
      string code,
      string redirectUri,
      CancellationToken taskCancellationToken)
    {
      AuthorizationCodeTokenRequest codeTokenRequest = new AuthorizationCodeTokenRequest();
      codeTokenRequest.Scope = string.Join(" ", this.Scopes);
      codeTokenRequest.RedirectUri = redirectUri;
      codeTokenRequest.Code = code;
      AuthorizationCodeTokenRequest request = codeTokenRequest;
      TokenResponse token = await this.FetchTokenAsync(userId, (TokenRequest) request, 
          taskCancellationToken).ConfigureAwait(false);
      await this.StoreTokenAsync(userId, token, taskCancellationToken).ConfigureAwait(false);
      return token;
    }

    public async Task<TokenResponse> RefreshTokenAsync(
      string userId,
      string refreshToken,
      CancellationToken taskCancellationToken)
    {
      RefreshTokenRequest request = new RefreshTokenRequest()
      {
        RefreshToken = refreshToken
      };

      TokenResponse token = 
                await this.FetchTokenAsync(userId, (TokenRequest) request, 
                taskCancellationToken).ConfigureAwait(false);

      if (token.RefreshToken == null)
        token.RefreshToken = refreshToken;

      await this.StoreTokenAsync(userId, token, taskCancellationToken).ConfigureAwait(false);
      return token;
    }

    public virtual Task RevokeTokenAsync(
      string userId,
      string token,
      CancellationToken taskCancellationToken)
    {
      throw new NotImplementedException("The OAuth 2.0 protocol does not support token revocation.");
    }

    public virtual bool ShouldForceTokenRetrieval() => false;

    private async Task StoreTokenAsync(
      string userId,
      TokenResponse token,
      CancellationToken taskCancellationToken)
    {
      taskCancellationToken.ThrowIfCancellationRequested();
      if (this.DataStore == null)
        return;
      await this.DataStore.StoreAsync<TokenResponse>(userId, token).ConfigureAwait(false);
    }

    [VisibleForTestOnly]
    public async Task<TokenResponse> FetchTokenAsync(
      string userId,
      TokenRequest request,
      CancellationToken taskCancellationToken)
    {
      request.ClientId = this.ClientSecrets.ClientId;
      request.ClientSecret = this.ClientSecrets.ClientSecret;
      try
      {
        return await request.ExecuteAsync((System.Net.Http.HttpClient) this.httpClient, 
            this.TokenServerUrl, 
            taskCancellationToken, this.Clock).ConfigureAwait(false);
      }
      catch (TokenResponseException ex)
      {
                int num = 0;
                try
                {
                    num = (int)ex.StatusCode;
                }
                catch { }

        if ((num < 500 ? 0 : (num < 600 ? 1 : 0)) == 0)
          await this.DeleteTokenAsync(userId, taskCancellationToken).ConfigureAwait(false);
        throw;
      }
      TokenResponse tokenResponse;
      return tokenResponse;
    }

    public void Dispose()
    {
      if (this.HttpClient == null)
        return;
      this.HttpClient.Dispose();
    }

    public class Initializer
    {
      public IAccessMethod AccessMethod { get; set; }

      public string TokenServerUrl { get; private set; }

      public string AuthorizationServerUrl { get; private set; }

      public ClientSecrets ClientSecrets { get; set; }

      public Stream ClientSecretsStream { get; set; }

      public IDataStore DataStore { get; set; }

      public IEnumerable<string> Scopes { get; set; }

      public IHttpClientFactory HttpClientFactory { get; set; }

      public ExponentialBackOffPolicy DefaultExponentialBackOffPolicy { get; set; }

      public IClock Clock { get; set; }

      public Initializer(string authorizationServerUrl, string tokenServerUrl)
      {
        this.AuthorizationServerUrl = authorizationServerUrl;
        this.TokenServerUrl = tokenServerUrl;
        this.Scopes = (IEnumerable<string>) new List<string>();
        this.AccessMethod = (IAccessMethod) new BearerToken.AuthorizationHeaderAccessMethod();
        this.DefaultExponentialBackOffPolicy = ExponentialBackOffPolicy.UnsuccessfulResponse503;
        this.Clock = SystemClock.Default;
      }
    }
  }
}
