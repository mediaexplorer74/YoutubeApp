// Decompiled with JetBrains decompiler
// Type: Google.Apis.Upload.ResumableUpload`1
// Assembly: Google.Apis, Version=1.30.0.0, Culture=neutral, PublicKeyToken=4b01fa6e34db77ab
// MVID: 6C44ABAE-71BD-4009-BDB7-D7E324A25671
// Assembly location: C:\Users\Admin\Desktop\re\YoutubeApp\Google.Apis.dll

using Google.Apis.Requests;
using Google.Apis.Services;
using Google.Apis.Util;
using System;
using System.Collections;
using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Google.Apis.Upload
{
  public class ResumableUpload<TRequest> : ResumableUpload
  {
    private const string PayloadContentTypeHeader = "X-Upload-Content-Type";
    private const string PayloadContentLengthHeader = "X-Upload-Content-Length";
    private const string UploadType = "uploadType";
    private const string Resumable = "resumable";

    protected ResumableUpload(
      IClientService service,
      string path,
      string httpMethod,
      Stream contentStream,
      string contentType)
      : base(contentStream, new ResumableUploadOptions()
      {
        HttpClient = (System.Net.Http.HttpClient) service.HttpClient,
        Serializer = service.Serializer,
        ServiceName = service.Name
      })
    {
      service.ThrowIfNull<IClientService>(nameof (service));
      path.ThrowIfNull<string>(nameof (path));
      httpMethod.ThrowIfNullOrEmpty(nameof (httpMethod));
      contentStream.ThrowIfNull<Stream>(nameof (contentStream));
      this.Service = service;
      this.Path = path;
      this.HttpMethod = httpMethod;
      this.ContentType = contentType;
    }

    public IClientService Service { get; private set; }

    public string Path { get; private set; }

    public string HttpMethod { get; private set; }

    public string ContentType { get; private set; }

    public TRequest Body { get; set; }

    public override async Task<Uri> InitiateSessionAsync(CancellationToken cancellationToken = default (CancellationToken))
    {
      ResumableUpload<TRequest> resumableUpload = this;
      HttpRequestMessage initializeRequest = resumableUpload.CreateInitializeRequest();
      ResumableUploadOptions options = resumableUpload.Options;
      if (options != null)
      {
        Action<HttpRequestMessage> initiationRequest = options.ModifySessionInitiationRequest;
        if (initiationRequest != null)
          initiationRequest(initializeRequest);
      }
      HttpResponseMessage response = await resumableUpload.Service.HttpClient.SendAsync(initializeRequest, cancellationToken).ConfigureAwait(false);
      if (!response.IsSuccessStatusCode)
        throw await resumableUpload.ExceptionForResponseAsync(response).ConfigureAwait(false);
      return response.Headers.Location;
    }

    private HttpRequestMessage CreateInitializeRequest()
    {
      RequestBuilder requestBuilder = new RequestBuilder()
      {
        BaseUri = new Uri(this.Service.BaseUri),
        Path = this.Path,
        Method = this.HttpMethod
      };
      requestBuilder.AddParameter(RequestParameterType.Query, "key", this.Service.ApiKey);
      requestBuilder.AddParameter(RequestParameterType.Query, "uploadType", "resumable");
      this.SetAllPropertyValues(requestBuilder);
      HttpRequestMessage request = requestBuilder.CreateRequest();
      if (this.ContentType != null)
        request.Headers.Add("X-Upload-Content-Type", this.ContentType);
      if (this.ContentStream.CanSeek)
        request.Headers.Add("X-Upload-Content-Length", this.StreamLength.ToString());
      this.Service.SetRequestSerailizedContent(request, (object) this.Body);
      return request;
    }

    private void SetAllPropertyValues(RequestBuilder requestBuilder)
    {
      foreach (PropertyInfo property in TypeExtensions.GetProperties(this.GetType()))
      {
        RequestParameterAttribute customAttribute = Utilities.GetCustomAttribute<RequestParameterAttribute>(property);
        if (customAttribute != null)
        {
          string name = customAttribute.Name ?? property.Name.ToLower();
          object o1 = property.GetValue((object) this, (object[]) null);
          if (o1 != null)
          {
            IEnumerable enumerable = o1 as IEnumerable;
            if (!(o1 is string) && enumerable != null)
            {
              foreach (object o2 in enumerable)
                requestBuilder.AddParameter(customAttribute.Type, name, Utilities.ConvertToString(o2));
            }
            else
              requestBuilder.AddParameter(customAttribute.Type, name, Utilities.ConvertToString(o1));
          }
        }
      }
    }
  }
}
