// Decompiled with JetBrains decompiler
// Type: Google.Apis.Upload.ResumableUpload`2
// Assembly: Google.Apis, Version=1.30.0.0, Culture=neutral, PublicKeyToken=4b01fa6e34db77ab
// MVID: 6C44ABAE-71BD-4009-BDB7-D7E324A25671
// Assembly location: C:\Users\Admin\Desktop\re\YoutubeApp\Google.Apis.dll

using Google.Apis.Services;
using System;
using System.IO;
using System.Net.Http;

namespace Google.Apis.Upload
{
  public class ResumableUpload<TRequest, TResponse> : ResumableUpload<TRequest>
  {
    protected ResumableUpload(
      IClientService service,
      string path,
      string httpMethod,
      Stream contentStream,
      string contentType)
      : base(service, path, httpMethod, contentStream, contentType)
    {
    }

    public TResponse ResponseBody { get; private set; }

    public event Action<TResponse> ResponseReceived;

    protected override void ProcessResponse(HttpResponseMessage response)
    {
      base.ProcessResponse(response);
      this.ResponseBody = this.Service.DeserializeResponse<TResponse>(response).Result;
      Action<TResponse> responseReceived = this.ResponseReceived;
      if (responseReceived == null)
        return;
      responseReceived(this.ResponseBody);
    }
  }
}
