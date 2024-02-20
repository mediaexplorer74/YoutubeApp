// Decompiled with JetBrains decompiler
// Type: Google.Apis.Requests.ClientServiceRequest`1
// Assembly: Google.Apis, Version=1.30.0.0, Culture=neutral, PublicKeyToken=4b01fa6e34db77ab
// MVID: 6C44ABAE-71BD-4009-BDB7-D7E324A25671
// Assembly location: C:\Users\Admin\Desktop\re\YoutubeApp\Google.Apis.dll

using Google.Apis.Discovery;
using Google.Apis.Logging;
using Google.Apis.Requests.Parameters;
using Google.Apis.Services;
using Google.Apis.Testing;
using Google.Apis.Util;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Google.Apis.Requests
{
  public abstract class ClientServiceRequest<TResponse> : 
    IClientServiceRequest<TResponse>,
    IClientServiceRequest
  {
    private static readonly ILogger Logger 
            = ApplicationContext.Logger.ForType<ClientServiceRequest<TResponse>>();
    
    private readonly IClientService service;

    public ETagAction ETagAction { get; set; }

    public Action<HttpRequestMessage> ModifyRequest { get; set; }

    public abstract string MethodName { get; }

    public abstract string RestPath { get; }

    public abstract string HttpMethod { get; }

    public IDictionary<string, IParameter> RequestParameters { get; private set; }

        public IClientService Service
        {
            get
            {
                return this.service;
            }
        }

    protected ClientServiceRequest(IClientService service) => this.service = service;

    protected virtual void InitParameters()
    {
        this.RequestParameters = (IDictionary<string, IParameter>)new Dictionary<string, IParameter>();
    }

    public TResponse Execute()
    {
      try
      {
        using (HttpResponseMessage result = this.ExecuteUnparsedAsync(CancellationToken.None).Result)
          return this.ParseResponse(result).Result;
      }
      catch (AggregateException ex)
      {
                //throw ex.InnerException;

                Debug.WriteLine("[ex] ClientServiceRequest - ex.: " + ex.Message);

                return default;
      }
      catch (Exception ex)
      {
                //throw ex;
                Debug.WriteLine("[ex] ClientServiceRequest - ex.: " + ex.Message);

                return default;
      }
    }

    public Stream ExecuteAsStream()
    {
      try
      {
        return this.ExecuteUnparsedAsync(CancellationToken.None).Result.Content.ReadAsStreamAsync().Result;
      }
      catch (AggregateException ex)
      {
        throw ex.InnerException;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public async Task<TResponse> ExecuteAsync()
    {
        return await this.ExecuteAsync(CancellationToken.None).ConfigureAwait(false);
    }

    public async Task<TResponse> ExecuteAsync(CancellationToken cancellationToken)
    {
      TResponse response1;
      using (HttpResponseMessage response2 = await this.ExecuteUnparsedAsync(cancellationToken).ConfigureAwait(false))
      {
        cancellationToken.ThrowIfCancellationRequested();
        response1 = await this.ParseResponse(response2).ConfigureAwait(false);
      }
      return response1;
    }

        public async Task<Stream> ExecuteAsStreamAsync()
        {
            return await this.ExecuteAsStreamAsync(CancellationToken.None).ConfigureAwait(false);
        }

        public async Task<Stream> ExecuteAsStreamAsync(CancellationToken cancellationToken)
    {
      HttpResponseMessage httpResponseMessage = await this.ExecuteUnparsedAsync(cancellationToken).ConfigureAwait(false);
      cancellationToken.ThrowIfCancellationRequested();
      return await httpResponseMessage.Content.ReadAsStreamAsync().ConfigureAwait(false);
    }

    private async Task<HttpResponseMessage> ExecuteUnparsedAsync(CancellationToken cancellationToken)
    {
      HttpResponseMessage httpResponseMessage;
      using (HttpRequestMessage request = this.CreateRequest())
        httpResponseMessage = await this.service.HttpClient.SendAsync(request, cancellationToken).ConfigureAwait(false);
      return httpResponseMessage;
    }

    private async Task<TResponse> ParseResponse(HttpResponseMessage response)
    {
      if (response.IsSuccessStatusCode)
        return await this.service.DeserializeResponse<TResponse>(response).ConfigureAwait(false);
      RequestError requestError = await this.service.DeserializeError(response).ConfigureAwait(false);
      throw new GoogleApiException(this.service.Name, requestError.ToString())
      {
        Error = requestError,
        HttpStatusCode = response.StatusCode
      };
    }

    public HttpRequestMessage CreateRequest(bool? overrideGZipEnabled = null)
    {
      HttpRequestMessage request = this.CreateBuilder().CreateRequest();
      object body = this.GetBody();
      
      request.SetRequestSerailizedContent
      (
          this.service, body, overrideGZipEnabled.HasValue 
          ? overrideGZipEnabled.Value 
          : this.service.GZipEnabled
      );

      this.AddETag(request);
      Action<HttpRequestMessage> modifyRequest = this.ModifyRequest;
      if (modifyRequest != null)
        modifyRequest(request);
      return request;
    }

    private RequestBuilder CreateBuilder()
    {
      RequestBuilder requestBuilder = new RequestBuilder()
      {
        BaseUri = new Uri(this.Service.BaseUri),
        Path = this.RestPath,
        Method = this.HttpMethod
      };
      if (this.service.ApiKey != null)
        requestBuilder.AddParameter(RequestParameterType.Query, "key", this.service.ApiKey);
      IDictionary<string, object> parameterDictionary = ParameterUtils.CreateParameterDictionary((object) this);
      this.AddParameters(requestBuilder, ParameterCollection.FromDictionary(parameterDictionary));
      return requestBuilder;
    }

    protected string GenerateRequestUri()
    {
        return this.CreateBuilder().BuildUri().ToString();
    }

    protected virtual object GetBody()
    {
        return (object)null;
    }

    private void AddETag(HttpRequestMessage request)
    {
      if (!(this.GetBody() is IDirectResponseSchema body) || string.IsNullOrEmpty(body.ETag))
        return;
      string etag = body.ETag;

      switch ( this.ETagAction == ETagAction.Default 
                ? ClientServiceRequest<TResponse>.GetDefaultETagAction(this.HttpMethod) 
                : this.ETagAction )
      {
        case ETagAction.IfMatch:
          request.Headers.TryAddWithoutValidation("If-Match", etag);
          break;
        case ETagAction.IfNoneMatch:
          request.Headers.TryAddWithoutValidation("If-None-Match", etag);
          break;
      }
    }

    [VisibleForTestOnly]
    public static ETagAction GetDefaultETagAction(string httpMethod)
    {
      switch (httpMethod)
      {
        case "GET":
          return ETagAction.IfNoneMatch;
        case "PUT":
        case "POST":
        case "PATCH":
        case "DELETE":
          return ETagAction.IfMatch;
        default:
          return ETagAction.Ignore;
      }
    }

    private void AddParameters(RequestBuilder requestBuilder, ParameterCollection inputParameters)
    {
      foreach (KeyValuePair<string, string> inputParameter in (List<KeyValuePair<string, string>>) inputParameters)
      {
        IParameter parameter;
        if (!this.RequestParameters.TryGetValue(inputParameter.Key, out parameter))
          throw new GoogleApiException(this.Service.Name, string.Format("Invalid parameter \"{0}\" was specified", (object) inputParameter.Key));
        string defaultValue = inputParameter.Value;
        if (!ParameterValidator.ValidateParameter(parameter, defaultValue))
          throw new GoogleApiException(this.Service.Name, string.Format("Parameter validation failed for \"{0}\"", (object) parameter.Name));
        if (defaultValue == null)
          defaultValue = parameter.DefaultValue;
        switch (parameter.ParameterType)
        {
          case "path":
            requestBuilder.AddParameter(RequestParameterType.Path, inputParameter.Key, defaultValue);
            continue;
          case "query":
            if (!object.Equals((object) defaultValue, (object) parameter.DefaultValue) || parameter.IsRequired)
            {
              requestBuilder.AddParameter(RequestParameterType.Query, inputParameter.Key, defaultValue);
              continue;
            }
            continue;
          default:
            throw new GoogleApiException(this.service.Name, string.Format(
                "Unsupported parameter type \"{0}\" for \"{1}\"", (object) parameter.ParameterType,
                (object) parameter.Name));
        }
      }
      foreach (IParameter parameter in (IEnumerable<IParameter>) this.RequestParameters.Values)
      {
        if (parameter.IsRequired && !inputParameters.ContainsKey(parameter.Name))
          throw new GoogleApiException(this.service.Name, string.Format("Parameter \"{0}\" is missing", 
              (object) parameter.Name));
      }
    }
  }
}
