// Decompiled with JetBrains decompiler
// Type: Google.Apis.Auth.OAuth2.ServiceCredential
// Assembly: Google.Apis.Auth, Version=1.30.0.0, Culture=neutral, PublicKeyToken=4b01fa6e34db77ab
// MVID: A0E91A90-D8FA-470E-9253-CAE205F78A22
// Assembly location: C:\Users\Admin\Desktop\re\YoutubeApp\Google.Apis.Auth.dll

using Google.Apis.Auth.OAuth2.Responses;
using Google.Apis.Http;
using Google.Apis.Logging;
using Google.Apis.Util;
using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Google.Apis.Auth.OAuth2
{
  public abstract class ServiceCredential : 
    ICredential,
    IConfigurableHttpClientInitializer,
    ITokenAccess,
    IHttpExecuteInterceptor,
    IHttpUnsuccessfulResponseHandler
  {
    protected static readonly ILogger Logger = ApplicationContext.Logger.ForType<ServiceCredential>();
    private readonly string tokenServerUrl;
    private readonly IClock clock;
    private readonly IAccessMethod accessMethod;
    private readonly ConfigurableHttpClient httpClient;
    private TokenResponse token;
    private object lockObject = new object();

    public string TokenServerUrl => this.tokenServerUrl;

    public IClock Clock => this.clock;

    public IAccessMethod AccessMethod => this.accessMethod;

    public ConfigurableHttpClient HttpClient => this.httpClient;

    public TokenResponse Token
    {
      get
      {
        lock (this.lockObject)
          return this.token;
      }
      protected set
      {
        lock (this.lockObject)
          this.token = value;
      }
    }

    public ServiceCredential(ServiceCredential.Initializer initializer)
    {
      this.tokenServerUrl = initializer.TokenServerUrl;
      this.accessMethod = initializer.AccessMethod.ThrowIfNull<IAccessMethod>("initializer.AccessMethod");
      this.clock = initializer.Clock.ThrowIfNull<IClock>("initializer.Clock");
      CreateHttpClientArgs args = new CreateHttpClientArgs();
      if (initializer.DefaultExponentialBackOffPolicy != ExponentialBackOffPolicy.None)
        args.Initializers.Add((IConfigurableHttpClientInitializer) new ExponentialBackOffInitializer(initializer.DefaultExponentialBackOffPolicy, (Func<BackOffHandler>) (() => new BackOffHandler((IBackOff) new ExponentialBackOff()))));
      this.httpClient = (initializer.HttpClientFactory ?? (IHttpClientFactory) new HttpClientFactory()).CreateHttpClient(args);
    }

    public void Initialize(ConfigurableHttpClient httpClient)
    {
      httpClient.MessageHandler.AddExecuteInterceptor((IHttpExecuteInterceptor) this);
      httpClient.MessageHandler.AddUnsuccessfulResponseHandler((IHttpUnsuccessfulResponseHandler) this);
    }

    public async Task InterceptAsync(
      HttpRequestMessage request,
      CancellationToken cancellationToken)
    {
      string accessToken = await this.GetAccessTokenForRequestAsync(request.RequestUri.AbsoluteUri, cancellationToken).ConfigureAwait(false);
      this.AccessMethod.Intercept(request, accessToken);
    }

    public async Task<bool> HandleResponseAsync(HandleUnsuccessfulResponseArgs args)
    {
      if (args.Response.StatusCode != HttpStatusCode.Unauthorized)
        return false;
      bool flag1 = false;
      if (this.Token != null)
        flag1 = object.Equals((object) this.Token.AccessToken, (object) this.AccessMethod.GetAccessToken(args.Request));
      bool flag2 = !flag1;
      if (!flag2)
        flag2 = await this.RequestAccessTokenAsync(args.CancellationToken).ConfigureAwait(false);
      return flag2;
    }

    public virtual async Task<string> GetAccessTokenForRequestAsync(
      string authUri = null,
      CancellationToken cancellationToken = default (CancellationToken))
    {
      if (this.Token == null || this.Token.IsExpired(this.Clock))
      {
        ServiceCredential.Logger.Debug("Token has expired, trying to get a new one.");
        if (!await this.RequestAccessTokenAsync(cancellationToken).ConfigureAwait(false))
          throw new InvalidOperationException("The access token has expired but we can't refresh it");
        ServiceCredential.Logger.Info("New access token was received successfully");
      }
      return this.Token.AccessToken;
    }

    public abstract Task<bool> RequestAccessTokenAsync(CancellationToken taskCancellationToken);

    public class Initializer
    {
      public string TokenServerUrl { get; private set; }

      public IClock Clock { get; set; }

      public IAccessMethod AccessMethod { get; set; }

      public IHttpClientFactory HttpClientFactory { get; set; }

      public ExponentialBackOffPolicy DefaultExponentialBackOffPolicy { get; set; }

      public Initializer(string tokenServerUrl)
      {
        this.TokenServerUrl = tokenServerUrl;
        this.AccessMethod = (IAccessMethod) new BearerToken.AuthorizationHeaderAccessMethod();
        this.Clock = SystemClock.Default;
        this.DefaultExponentialBackOffPolicy = ExponentialBackOffPolicy.UnsuccessfulResponse503;
      }
    }
  }
}
