// Decompiled with JetBrains decompiler
// Type: Google.Apis.Services.BaseClientService
// Assembly: Google.Apis, Version=1.30.0.0, Culture=neutral, PublicKeyToken=4b01fa6e34db77ab
// MVID: 6C44ABAE-71BD-4009-BDB7-D7E324A25671
// Assembly location: C:\Users\Admin\Desktop\re\YoutubeApp\Google.Apis.dll

using Google.Apis.Http;
using Google.Apis.Json;
using Google.Apis.Logging;
using Google.Apis.Requests;
using Google.Apis.Testing;
using Google.Apis.Util;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Google.Apis.Services
{
  public abstract class BaseClientService : IClientService, IDisposable
  {
    private static readonly ILogger Logger = ApplicationContext.Logger.ForType<BaseClientService>();
    [VisibleForTestOnly]
    public const uint DefaultMaxUrlLength = 2048;

    protected BaseClientService(BaseClientService.Initializer initializer)
    {
      initializer.Validate();
      this.GZipEnabled = initializer.GZipEnabled;
      this.Serializer = initializer.Serializer;
      this.ApiKey = initializer.ApiKey;
      this.ApplicationName = initializer.ApplicationName;
      if (this.ApplicationName == null)
        BaseClientService.Logger.Warning("Application name is not set. Please set Initializer.ApplicationName property");
      this.HttpClientInitializer = initializer.HttpClientInitializer;
      this.HttpClient = this.CreateHttpClient(initializer);
    }

    private bool HasFeature(Google.Apis.Discovery.Features feature) => this.Features.Contains(Utilities.GetEnumStringValue((Enum) feature));

    private ConfigurableHttpClient CreateHttpClient(BaseClientService.Initializer initializer)
    {
      IHttpClientFactory httpClientFactory = initializer.HttpClientFactory ?? (IHttpClientFactory) new HttpClientFactory();
      CreateHttpClientArgs args = new CreateHttpClientArgs()
      {
        GZipEnabled = this.GZipEnabled,
        ApplicationName = this.ApplicationName
      };
      if (this.HttpClientInitializer != null)
        args.Initializers.Add(this.HttpClientInitializer);
      if (initializer.DefaultExponentialBackOffPolicy != ExponentialBackOffPolicy.None)
        args.Initializers.Add((IConfigurableHttpClientInitializer) new ExponentialBackOffInitializer(initializer.DefaultExponentialBackOffPolicy, new Func<BackOffHandler>(this.CreateBackOffHandler)));
      ConfigurableHttpClient httpClient = httpClientFactory.CreateHttpClient(args);
      if (initializer.MaxUrlLength > 0U)
        httpClient.MessageHandler.AddExecuteInterceptor((IHttpExecuteInterceptor) new MaxUrlLengthInterceptor(initializer.MaxUrlLength));
      return httpClient;
    }

    protected virtual BackOffHandler CreateBackOffHandler() => new BackOffHandler((IBackOff) new ExponentialBackOff());

    public ConfigurableHttpClient HttpClient { get; private set; }

    public IConfigurableHttpClientInitializer HttpClientInitializer { get; private set; }

    public bool GZipEnabled { get; private set; }

    public string ApiKey { get; private set; }

    public string ApplicationName { get; private set; }

    public void SetRequestSerailizedContent(HttpRequestMessage request, object body) => request.SetRequestSerailizedContent((IClientService) this, body, this.GZipEnabled);

    public ISerializer Serializer { get; private set; }

    public virtual string SerializeObject(object obj) => this.Serializer.Serialize(obj);

    public virtual async Task<T> DeserializeResponse<T>(HttpResponseMessage response)
    {
      T obj1 = default(T);
      var input = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

      if (object.Equals((object)typeof(T), (object)typeof(string)))
      {
         return obj1;//(Task<T>)input;
      }

      if (this.HasFeature(Google.Apis.Discovery.Features.LegacyDataResponse))
      {
        StandardResponse<T> standardResponse;
        try
        {
          standardResponse = this.Serializer.Deserialize<StandardResponse<T>>(input);
        }
        catch (JsonReaderException ex)
        {
          throw new GoogleApiException(this.Name, "Failed to parse response from server as json [" + input + "]", (Exception) ex);
        }
        if (standardResponse.Error != null)
          throw new GoogleApiException(this.Name, "Server error - " + (object) standardResponse.Error)
          {
            Error = standardResponse.Error
          };
        return (object) standardResponse.Data != null ? standardResponse.Data : throw new GoogleApiException(this.Name, "The response could not be deserialized.");
      }
      
      T obj2 = default(T); 
      try
      {
        obj2 = this.Serializer.Deserialize<T>(input);
      }
      catch (JsonReaderException ex)
      {
        throw new GoogleApiException(this.Name, "Failed to parse response from server as json [" + input + "]", (Exception) ex);
      }
      string tag = response.Headers.ETag != null ? response.Headers.ETag.Tag : (string) null;
      if ((object) obj2 is IDirectResponseSchema && tag != null)
        ((object) obj2 as IDirectResponseSchema).ETag = tag;
      return obj2;
    }

    public virtual async Task<RequestError> DeserializeError(HttpResponseMessage response)
    {
      StandardResponse<object> errorResponse = (StandardResponse<object>) null;
      try
      {
        errorResponse = this.Serializer.Deserialize<StandardResponse<object>>(await response.Content.ReadAsStringAsync().ConfigureAwait(false));
        if (errorResponse.Error == null)
          throw new GoogleApiException(this.Name, "error response is null");
      }
      catch (Exception ex)
      {
        throw new GoogleApiException(this.Name, "An Error occurred, but the error response could not be deserialized", ex);
      }
      return errorResponse.Error;
    }

    public abstract string Name { get; }

    public abstract string BaseUri { get; }

    public abstract string BasePath { get; }

    public virtual string BatchUri => (string) null;

    public virtual string BatchPath => (string) null;

    public abstract IList<string> Features { get; }

    public virtual void Dispose()
    {
      if (this.HttpClient == null)
        return;
      this.HttpClient.Dispose();
    }

    public class Initializer
    {
      private const string InvalidApplicationNameCharacters = "\"(),:;<=>?@[\\]{}";

      public IHttpClientFactory HttpClientFactory { get; set; }

      public IConfigurableHttpClientInitializer HttpClientInitializer { get; set; }

      public ExponentialBackOffPolicy DefaultExponentialBackOffPolicy { get; set; }

      public bool GZipEnabled { get; set; }

      public ISerializer Serializer { get; set; }

      public string ApiKey { get; set; }

      public string ApplicationName { get; set; }

      public uint MaxUrlLength { get; set; }

      public Initializer()
      {
        this.GZipEnabled = true;
        this.Serializer = (ISerializer) new NewtonsoftJsonSerializer();
        this.DefaultExponentialBackOffPolicy = ExponentialBackOffPolicy.UnsuccessfulResponse503;
        this.MaxUrlLength = 2048U;
      }

      internal void Validate()
      {
        if (this.ApplicationName != null && this.ApplicationName.Any<char>((Func<char, bool>) (c => "\"(),:;<=>?@[\\]{}".Contains<char>(c))))
          throw new ArgumentException("Invalid Application name", "ApplicationName");
      }
    }
  }
}
