// Decompiled with JetBrains decompiler
// Type: Google.Apis.Requests.IClientServiceRequest
// Assembly: Google.Apis, Version=1.30.0.0, Culture=neutral, PublicKeyToken=4b01fa6e34db77ab
// MVID: 6C44ABAE-71BD-4009-BDB7-D7E324A25671
// Assembly location: C:\Users\Admin\Desktop\re\YoutubeApp\Google.Apis.dll

using Google.Apis.Discovery;
using Google.Apis.Services;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Google.Apis.Requests
{
  public interface IClientServiceRequest
  {
    string MethodName { get; }

    string RestPath { get; }

    string HttpMethod { get; }

    IDictionary<string, IParameter> RequestParameters { get; }

    IClientService Service { get; }

    HttpRequestMessage CreateRequest(bool? overrideGZipEnabled = null);

    Task<Stream> ExecuteAsStreamAsync();

    Task<Stream> ExecuteAsStreamAsync(CancellationToken cancellationToken);

    Stream ExecuteAsStream();
  }
}
