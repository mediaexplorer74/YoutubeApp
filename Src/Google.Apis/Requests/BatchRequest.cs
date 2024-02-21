// Google.Apis.Requests.BatchRequest

using Google.Apis.Http;
using Google.Apis.Services;
using Google.Apis.Testing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Google.Apis.Requests
{
  public sealed class BatchRequest
  {
    private const string DefaultBatchUrl = "https://www.googleapis.com/batch";
    private const int QueueLimit = 1000;

    private readonly IList<BatchRequest.InnerRequest> allRequests 
            = (IList<BatchRequest.InnerRequest>) new List<BatchRequest.InnerRequest>();

    private readonly string batchUrl;
    private readonly IClientService service;

    internal string BatchUrl => this.batchUrl;

    public BatchRequest(IClientService service)
      : this(service, (service is BaseClientService baseClientService 
            ? baseClientService.BatchUri 
            : (string) null) ?? "https://www.googleapis.com/batch")
    {
    }

    public BatchRequest(IClientService service, string batchUrl)
    {
      this.batchUrl = batchUrl;
      this.service = service;
    }

    public int Count => this.allRequests.Count;

    public void Queue<TResponse>(
      IClientServiceRequest request,
      BatchRequest.OnResponse<TResponse> callback)
      where TResponse : class
    {
      if (this.Count > 1000)
        throw new InvalidOperationException("A batch request cannot contain more than 1000 single requests");

      IList<BatchRequest.InnerRequest> allRequests = this.allRequests;
      BatchRequest.InnerRequest<TResponse> innerRequest = new BatchRequest.InnerRequest<TResponse>();
      innerRequest.ClientRequest = request;
      innerRequest.ResponseType = typeof (TResponse);
      innerRequest.OnResponseCallback = callback;
      allRequests.Add((BatchRequest.InnerRequest) innerRequest);
    }

    public Task ExecuteAsync() => this.ExecuteAsync(CancellationToken.None);

    public async Task ExecuteAsync(CancellationToken cancellationToken)
    {
      if (this.Count < 1)
        return;

      ConfigurableHttpClient httpClient = this.service.HttpClient;

      HttpContent content1 = await BatchRequest.CreateOuterRequestContent(
          this.allRequests.Select<BatchRequest.InnerRequest, IClientServiceRequest>(
              (Func<BatchRequest.InnerRequest, IClientServiceRequest>) (r => r.ClientRequest)))
                .ConfigureAwait(false);

      HttpResponseMessage result = await httpClient.PostAsync(new Uri(this.batchUrl), 
          content1, cancellationToken).ConfigureAwait(false);
      
      result.EnsureSuccessStatusCode();

      ConfiguredTaskAwaitable<string> configuredTaskAwaitable 
                = result.Content.ReadAsStringAsync().ConfigureAwait(false);
      string fullContent = await configuredTaskAwaitable;
      string str = result.Content.Headers.GetValues("Content-Type").First<string>();
      string boundary = str.Substring(str.IndexOf("boundary=") + "boundary=".Length);
      int requestIndex = 0;
      while (true)
      {
        cancellationToken.ThrowIfCancellationRequested();
        int num = fullContent.IndexOf("--" + boundary);
        if (num != -1)
        {
          fullContent = fullContent.Substring(num + boundary.Length + 2);
          int endIndex = fullContent.IndexOf("--" + boundary);
          if (endIndex != -1)
          {
            HttpResponseMessage responseMessage = BatchRequest.ParseAsHttpResponse(fullContent.Substring(0, endIndex));
            if (responseMessage.IsSuccessStatusCode)
            {
              configuredTaskAwaitable = responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
              object content2 = this.service.Serializer.Deserialize(await configuredTaskAwaitable, this.allRequests[requestIndex].ResponseType);
              this.allRequests[requestIndex].OnResponse(content2, (RequestError) null, requestIndex, responseMessage);
            }
            else
            {
              RequestError error = await this.service.DeserializeError(responseMessage).ConfigureAwait(false);
              this.allRequests[requestIndex].OnResponse((object) null, error, requestIndex, responseMessage);
            }
            ++requestIndex;
            fullContent = fullContent.Substring(endIndex);
            responseMessage = (HttpResponseMessage) null;
          }
          else
            goto label_10;
        }
        else
          break;
      }
      return;
label_10:;
    }

    [VisibleForTestOnly]
    internal static HttpResponseMessage ParseAsHttpResponse(string content)
    {
      HttpResponseMessage asHttpResponse = new HttpResponseMessage();
      using (StringReader stringReader = new StringReader(content))
      {
        string str1 = stringReader.ReadLine();
        while (string.IsNullOrEmpty(str1))
          str1 = stringReader.ReadLine();
        while (!string.IsNullOrEmpty(str1))
          str1 = stringReader.ReadLine();
        string str2 = stringReader.ReadLine();
        while (string.IsNullOrEmpty(str2))
          str2 = stringReader.ReadLine();
        int num = int.Parse(str2.Split(' ')[1]);
        asHttpResponse.StatusCode = (HttpStatusCode) num;
        IDictionary<string, string> dictionary = (IDictionary<string, string>) new Dictionary<string, string>();
        string str3;
        while (!string.IsNullOrEmpty(str3 = stringReader.ReadLine()))
        {
          int length = str3.IndexOf(':');
          string key = str3.Substring(0, length).Trim();
          string str4 = str3.Substring(length + 1).Trim();
          if (dictionary.ContainsKey(key))
            dictionary[key] = dictionary[key] + ", " + str4;
          else
            dictionary.Add(key, str4);
        }
        string mediaType = (string) null;
        if (dictionary.ContainsKey("Content-Type"))
        {
          mediaType = dictionary["Content-Type"].Split(';', ' ')[0];
          dictionary.Remove("Content-Type");
        }
        asHttpResponse.Content = (HttpContent) new StringContent(stringReader.ReadToEnd(), Encoding.UTF8, mediaType);
        foreach (KeyValuePair<string, string> keyValuePair in (IEnumerable<KeyValuePair<string, string>>) dictionary)
        {
          HttpHeaders headers = (HttpHeaders) asHttpResponse.Headers;
          if ((object) TypeExtensions.GetProperty(typeof (HttpContentHeaders), keyValuePair.Key.Replace("-", "")) != null)
            headers = (HttpHeaders) asHttpResponse.Content.Headers;
          if (!headers.TryAddWithoutValidation(keyValuePair.Key, keyValuePair.Value))
            throw new FormatException(string.Format("Could not parse header {0} from batch reply", (object) keyValuePair.Key));
        }
      }
      return asHttpResponse;
    }

    [VisibleForTestOnly]
    internal static async Task<HttpContent> CreateOuterRequestContent(
      IEnumerable<IClientServiceRequest> requests)
    {
      MultipartContent mixedContent = new MultipartContent("mixed");
      foreach (IClientServiceRequest request in requests)
      {
        MultipartContent multipartContent = mixedContent;
        multipartContent.Add(await BatchRequest.CreateIndividualRequest(request).ConfigureAwait(false));
        multipartContent = (MultipartContent) null;
      }
      return (HttpContent) mixedContent;
    }

    [VisibleForTestOnly]
    internal static async Task<HttpContent> CreateIndividualRequest(IClientServiceRequest request)
    {
      StringContent individualRequest = new StringContent(await BatchRequest.CreateRequestContentString(request.CreateRequest(new bool?(false))).ConfigureAwait(false));
      individualRequest.Headers.ContentType = new MediaTypeHeaderValue("application/http");
      return (HttpContent) individualRequest;
    }

    [VisibleForTestOnly]
    internal static async Task<string> CreateRequestContentString(HttpRequestMessage requestMessage)
    {
      StringBuilder sb = new StringBuilder();
      sb.AppendFormat("{0} {1}", (object) requestMessage.Method, (object) requestMessage.RequestUri.AbsoluteUri);
      foreach (KeyValuePair<string, IEnumerable<string>> header in (HttpHeaders) requestMessage.Headers)
        sb.Append(Environment.NewLine).AppendFormat("{0}: {1}", (object) header.Key, (object) string.Join(", ", header.Value.ToArray<string>()));
      if (requestMessage.Content != null)
      {
        foreach (KeyValuePair<string, IEnumerable<string>> header in (HttpHeaders) requestMessage.Content.Headers)
          sb.Append(Environment.NewLine).AppendFormat("{0}: {1}", (object) header.Key, (object) string.Join(", ", header.Value.ToArray<string>()));
      }
      if (requestMessage.Content != null)
      {
        sb.Append(Environment.NewLine);
        string str = await requestMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
        sb.Append("Content-Length:  ").Append(str.Length);
        sb.Append(Environment.NewLine).Append(Environment.NewLine).Append(str);
      }
      return sb.Append(Environment.NewLine).ToString();
    }

    public delegate void OnResponse<in TResponse>(
      TResponse content,
      RequestError error,
      int index,
      HttpResponseMessage message)
      where TResponse : class;

    private class InnerRequest
    {
      public IClientServiceRequest ClientRequest { get; set; }

      public Type ResponseType { get; set; }

      public virtual void OnResponse(
        object content,
        RequestError error,
        int index,
        HttpResponseMessage message)
      {
        string tag = message.Headers.ETag != null ? message.Headers.ETag.Tag : (string) null;
        if (!(content is IDirectResponseSchema directResponseSchema) || directResponseSchema.ETag != null || tag == null)
          return;
        directResponseSchema.ETag = tag;
      }
    }

    private class InnerRequest<TResponse> : BatchRequest.InnerRequest where TResponse : class
    {
      public BatchRequest.OnResponse<TResponse> OnResponseCallback { get; set; }

      public override void OnResponse(
        object content,
        RequestError error,
        int index,
        HttpResponseMessage message)
      {
        base.OnResponse(content, error, index, message);
        if (this.OnResponseCallback == null)
          return;
        this.OnResponseCallback(content as TResponse, error, index, message);
      }
    }
  }
}
