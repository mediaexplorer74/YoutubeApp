// Decompiled with JetBrains decompiler
// Type: Google.Apis.Services.IClientService
// Assembly: Google.Apis, Version=1.30.0.0, Culture=neutral, PublicKeyToken=4b01fa6e34db77ab
// MVID: 6C44ABAE-71BD-4009-BDB7-D7E324A25671
// Assembly location: C:\Users\Admin\Desktop\re\YoutubeApp\Google.Apis.dll

using Google.Apis.Http;
using Google.Apis.Requests;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Google.Apis.Services
{
  public interface IClientService : IDisposable
  {
    ConfigurableHttpClient HttpClient { get; }

    IConfigurableHttpClientInitializer HttpClientInitializer { get; }

    string Name { get; }

    string BaseUri { get; }

    string BasePath { get; }

    IList<string> Features { get; }

    bool GZipEnabled { get; }

    string ApiKey { get; }

    string ApplicationName { get; }

    void SetRequestSerailizedContent(HttpRequestMessage request, object body);

    ISerializer Serializer { get; }

    string SerializeObject(object data);

    Task<T> DeserializeResponse<T>(HttpResponseMessage response);

    Task<RequestError> DeserializeError(HttpResponseMessage response);
  }
}
