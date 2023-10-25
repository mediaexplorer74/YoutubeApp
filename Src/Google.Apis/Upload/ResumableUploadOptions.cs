// Decompiled with JetBrains decompiler
// Type: Google.Apis.Upload.ResumableUploadOptions
// Assembly: Google.Apis, Version=1.30.0.0, Culture=neutral, PublicKeyToken=4b01fa6e34db77ab
// MVID: 6C44ABAE-71BD-4009-BDB7-D7E324A25671
// Assembly location: C:\Users\Admin\Desktop\re\YoutubeApp\Google.Apis.dll

using Google.Apis.Http;
using System;
using System.Net.Http;

namespace Google.Apis.Upload
{
  public sealed class ResumableUploadOptions
  {
    public HttpClient HttpClient { get; set; }

    public Action<HttpRequestMessage> ModifySessionInitiationRequest { get; set; }

    public ISerializer Serializer { get; set; }

    public string ServiceName { get; set; }

    internal ConfigurableHttpClient ConfigurableHttpClient => this.HttpClient as ConfigurableHttpClient;
  }
}
