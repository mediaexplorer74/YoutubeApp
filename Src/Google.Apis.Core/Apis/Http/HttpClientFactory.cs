// Decompiled with JetBrains decompiler
// Type: Google.Apis.Http.HttpClientFactory
// Assembly: Google.Apis.Core, Version=1.30.0.0, Culture=neutral, PublicKeyToken=4b01fa6e34db77ab
// MVID: 4EC5AF78-9014-40C1-B780-6317D8DBC917
// Assembly location: C:\Users\Admin\Desktop\re\YoutubeApp\Google.Apis.Core.dll

using Google.Apis.Logging;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;

namespace Google.Apis.Http
{
  public class HttpClientFactory : IHttpClientFactory
  {
    private static readonly ILogger Logger = ApplicationContext.Logger.ForType<HttpClientFactory>();

    public ConfigurableHttpClient CreateHttpClient(CreateHttpClientArgs args)
    {
      ConfigurableHttpClient httpClient = new ConfigurableHttpClient(new ConfigurableMessageHandler(this.CreateHandler(args))
      {
        ApplicationName = args.ApplicationName
      });
      foreach (IConfigurableHttpClientInitializer initializer in (IEnumerable<IConfigurableHttpClientInitializer>) args.Initializers)
        initializer.Initialize(httpClient);
      return httpClient;
    }

    protected virtual HttpMessageHandler CreateHandler(CreateHttpClientArgs args)
    {
      HttpClientHandler handler = new HttpClientHandler();
      if (handler.SupportsRedirectConfiguration)
        handler.AllowAutoRedirect = false;
      if (handler.SupportsAutomaticDecompression && args.GZipEnabled)
        handler.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
      HttpClientFactory.Logger.Debug("Handler was created. SupportsRedirectConfiguration={0}, SupportsAutomaticDecompression={1}", (object) handler.SupportsRedirectConfiguration, (object) handler.SupportsAutomaticDecompression);
      return (HttpMessageHandler) handler;
    }
  }
}
