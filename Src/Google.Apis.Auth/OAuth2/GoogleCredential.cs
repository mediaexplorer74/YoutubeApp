// Decompiled with JetBrains decompiler
// Type: Google.Apis.Auth.OAuth2.GoogleCredential
// Assembly: Google.Apis.Auth, Version=1.30.0.0, Culture=neutral, PublicKeyToken=4b01fa6e34db77ab
// MVID: A0E91A90-D8FA-470E-9253-CAE205F78A22
// Assembly location: C:\Users\Admin\Desktop\re\YoutubeApp\Google.Apis.Auth.dll

using Google.Apis.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Google.Apis.Auth.OAuth2
{
  public class GoogleCredential : ICredential, IConfigurableHttpClientInitializer, ITokenAccess
  {
    private static DefaultCredentialProvider defaultCredentialProvider = new DefaultCredentialProvider();
    protected readonly ICredential credential;

    internal GoogleCredential(ICredential credential) => this.credential = credential;

    public static Task<GoogleCredential> GetApplicationDefaultAsync() => GoogleCredential.defaultCredentialProvider.GetDefaultCredentialAsync();

    public static GoogleCredential GetApplicationDefault() => Task.Run<GoogleCredential>((Func<Task<GoogleCredential>>) (() => GoogleCredential.GetApplicationDefaultAsync())).Result;

    public static GoogleCredential FromStream(Stream stream) => GoogleCredential.defaultCredentialProvider.CreateDefaultCredentialFromStream(stream);

    public static GoogleCredential FromFile(string path)
    {
      using (FileStream fileStream = File.OpenRead(path))
        return GoogleCredential.FromStream((Stream) fileStream);
    }

    public static GoogleCredential FromJson(string json) => GoogleCredential.defaultCredentialProvider.CreateDefaultCredentialFromJson(json);

    public static GoogleCredential FromAccessToken(string accessToken, IAccessMethod accessMethod = null)
    {
      accessMethod = accessMethod ?? (IAccessMethod) new BearerToken.AuthorizationHeaderAccessMethod();
      return new GoogleCredential((ICredential) new GoogleCredential.AccessTokenCredential(accessToken, accessMethod));
    }

    public static GoogleCredential FromComputeCredential(ComputeCredential computeCredential = null) => new GoogleCredential((ICredential) (computeCredential ?? new ComputeCredential()));

    public virtual bool IsCreateScopedRequired => false;

    public virtual GoogleCredential CreateScoped(IEnumerable<string> scopes) => this;

    public virtual GoogleCredential CreateWithUser(string user) => throw new InvalidOperationException();

    public GoogleCredential CreateScoped(params string[] scopes) => this.CreateScoped((IEnumerable<string>) scopes);

    void IConfigurableHttpClientInitializer.Initialize(ConfigurableHttpClient httpClient) => this.credential.Initialize(httpClient);

    Task<string> ITokenAccess.GetAccessTokenForRequestAsync(
      string authUri,
      CancellationToken cancellationToken)
    {
      return this.credential.GetAccessTokenForRequestAsync(authUri, cancellationToken);
    }

    public ICredential UnderlyingCredential => this.credential;

    internal static GoogleCredential FromCredential(ServiceAccountCredential credential) => (GoogleCredential) new GoogleCredential.ServiceAccountGoogleCredential(credential);

    internal class AccessTokenCredential : 
      ICredential,
      IConfigurableHttpClientInitializer,
      ITokenAccess,
      IHttpExecuteInterceptor
    {
      private readonly string _accessToken;
      private readonly IAccessMethod _accessMethod;

      public AccessTokenCredential(string accessToken, IAccessMethod accessMethod)
      {
        this._accessToken = accessToken;
        this._accessMethod = accessMethod;
      }

      public void Initialize(ConfigurableHttpClient httpClient) => httpClient.MessageHandler.AddExecuteInterceptor((IHttpExecuteInterceptor) this);

      public Task<string> GetAccessTokenForRequestAsync(
        string authUri = null,
        CancellationToken cancellationToken = default (CancellationToken))
      {
        return Task.FromResult<string>(this._accessToken);
      }

      public Task InterceptAsync(
        HttpRequestMessage request,
        CancellationToken taskCancellationToken)
      {
        this._accessMethod.Intercept(request, this._accessToken);
        return (Task) Task.FromResult<int>(0);
      }
    }

    internal class ServiceAccountGoogleCredential : GoogleCredential
    {
      public ServiceAccountGoogleCredential(ServiceAccountCredential credential)
        : base((ICredential) credential)
      {
      }

      public override bool IsCreateScopedRequired => !(this.credential as ServiceAccountCredential).HasScopes;

      public override GoogleCredential CreateScoped(IEnumerable<string> scopes)
      {
        ServiceAccountCredential credential = this.credential as ServiceAccountCredential;
        return (GoogleCredential) new GoogleCredential.ServiceAccountGoogleCredential(new ServiceAccountCredential(new ServiceAccountCredential.Initializer(credential.Id)
        {
          User = credential.User,
          Key = credential.Key,
          Scopes = scopes
        }));
      }

      public override GoogleCredential CreateWithUser(string user)
      {
        ServiceAccountCredential credential = this.credential as ServiceAccountCredential;
        return (GoogleCredential) new GoogleCredential.ServiceAccountGoogleCredential(new ServiceAccountCredential(new ServiceAccountCredential.Initializer(credential.Id)
        {
          User = user,
          Key = credential.Key,
          Scopes = credential.Scopes
        }));
      }
    }
  }
}
