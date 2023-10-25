// Decompiled with JetBrains decompiler
// Type: Google.Apis.Http.ConfigurableMessageHandler
// Assembly: Google.Apis.Core, Version=1.30.0.0, Culture=neutral, PublicKeyToken=4b01fa6e34db77ab
// MVID: 4EC5AF78-9014-40C1-B780-6317D8DBC917
// Assembly location: C:\Users\Admin\Desktop\re\YoutubeApp\Google.Apis.Core.dll

using Google.Apis.Logging;
using Google.Apis.Testing;
using Google.Apis.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Google.Apis.Http
{
  public class ConfigurableMessageHandler : DelegatingHandler
  {
    private static readonly ILogger Logger = ApplicationContext.Logger.ForType<ConfigurableMessageHandler>();
    [VisibleForTestOnly]
    public const int MaxAllowedNumTries = 20;
    private static readonly string ApiVersion = Utilities.GetLibraryVersion();
    private static readonly string UserAgentSuffix = "google-api-dotnet-client/" 
            + ConfigurableMessageHandler.ApiVersion + " (gzip)";
    private readonly object unsuccessfulResponseHandlersLock = new object();
    private readonly object exceptionHandlersLock = new object();
    private readonly object executeInterceptorsLock = new object();
    private readonly IList<IHttpUnsuccessfulResponseHandler> unsuccessfulResponseHandlers = (IList<IHttpUnsuccessfulResponseHandler>) new List<IHttpUnsuccessfulResponseHandler>();
    private readonly IList<IHttpExceptionHandler> exceptionHandlers = (IList<IHttpExceptionHandler>) new List<IHttpExceptionHandler>();
    private readonly IList<IHttpExecuteInterceptor> executeInterceptors = (IList<IHttpExecuteInterceptor>) new List<IHttpExecuteInterceptor>();
    private int _loggingRequestId;
    private ILogger _instanceLogger = ConfigurableMessageHandler.Logger;
    private int numTries = 3;
    private int numRedirects = 10;

    [Obsolete("Use AddUnsuccessfulResponseHandler or RemoveUnsuccessfulResponseHandler instead.")]
    public IList<IHttpUnsuccessfulResponseHandler> UnsuccessfulResponseHandlers => this.unsuccessfulResponseHandlers;

    public void AddUnsuccessfulResponseHandler(IHttpUnsuccessfulResponseHandler handler)
    {
      lock (this.unsuccessfulResponseHandlersLock)
        this.unsuccessfulResponseHandlers.Add(handler);
    }

    public void RemoveUnsuccessfulResponseHandler(IHttpUnsuccessfulResponseHandler handler)
    {
      lock (this.unsuccessfulResponseHandlersLock)
        this.unsuccessfulResponseHandlers.Remove(handler);
    }

    [Obsolete("Use AddExceptionHandler or RemoveExceptionHandler instead.")]
    public IList<IHttpExceptionHandler> ExceptionHandlers => this.exceptionHandlers;

    public void AddExceptionHandler(IHttpExceptionHandler handler)
    {
      lock (this.exceptionHandlersLock)
        this.exceptionHandlers.Add(handler);
    }

    public void RemoveExceptionHandler(IHttpExceptionHandler handler)
    {
      lock (this.exceptionHandlersLock)
        this.exceptionHandlers.Remove(handler);
    }

    [Obsolete("Use AddExecuteInterceptor or RemoveExecuteInterceptor instead.")]
    public IList<IHttpExecuteInterceptor> ExecuteInterceptors => this.executeInterceptors;

    public void AddExecuteInterceptor(IHttpExecuteInterceptor interceptor)
    {
      lock (this.executeInterceptorsLock)
        this.executeInterceptors.Add(interceptor);
    }

    public void RemoveExecuteInterceptor(IHttpExecuteInterceptor interceptor)
    {
      lock (this.executeInterceptorsLock)
        this.executeInterceptors.Remove(interceptor);
    }

    internal ILogger InstanceLogger
    {
      get => this._instanceLogger;
      set => this._instanceLogger = value.ForType<ConfigurableMessageHandler>();
    }

    public int NumTries
    {
      get => this.numTries;
      set => this.numTries = value <= 20 && value >= 1 ? value 
                : throw new ArgumentOutOfRangeException(nameof (NumTries));
    }

    public int NumRedirects
    {
      get => this.numRedirects;
      set => this.numRedirects = value <= 20 && value >= 1 ? value
                : throw new ArgumentOutOfRangeException(nameof (NumRedirects));
    }

    public bool FollowRedirect { get; set; }

    public bool IsLoggingEnabled { get; set; }

    public ConfigurableMessageHandler.LogEventType LogEvents { get; set; }

    public string ApplicationName { get; set; }

    public ConfigurableMessageHandler(HttpMessageHandler httpMessageHandler)
      : base(httpMessageHandler)
    {
      this.FollowRedirect = true;
      this.IsLoggingEnabled = true;
      this.LogEvents = ConfigurableMessageHandler.LogEventType.RequestUri | ConfigurableMessageHandler.LogEventType.ResponseStatus | ConfigurableMessageHandler.LogEventType.ResponseAbnormal;
    }

    private void LogHeaders(string initialText, HttpHeaders headers1, HttpHeaders headers2)
    {
      List<KeyValuePair<string, IEnumerable<string>>> list = ((IEnumerable<KeyValuePair<string, IEnumerable<string>>>) headers1 ?? Enumerable.Empty<KeyValuePair<string, IEnumerable<string>>>()).Concat<KeyValuePair<string, IEnumerable<string>>>((IEnumerable<KeyValuePair<string, IEnumerable<string>>>) headers2 ?? Enumerable.Empty<KeyValuePair<string, IEnumerable<string>>>()).ToList<KeyValuePair<string, IEnumerable<string>>>();
      object[] objArray1 = new object[list.Count * 2];
      StringBuilder stringBuilder1 = new StringBuilder(list.Count * 32);
      stringBuilder1.Append(initialText);
      StringBuilder stringBuilder2 = new StringBuilder();
      for (int index1 = 0; index1 < list.Count; ++index1)
      {
        stringBuilder1.Append(string.Format("\n  [{{{0}}}] '{{{1}}}'", (object) (index1 * 2), (object) (1 + index1 * 2)));
        object[] objArray2 = objArray1;
        int index2 = index1 * 2;
        KeyValuePair<string, IEnumerable<string>> keyValuePair = list[index1];
        string key = keyValuePair.Key;
        objArray2[index2] = (object) key;
        stringBuilder2.Clear();
        object[] objArray3 = objArray1;
        int index3 = 1 + index1 * 2;
        keyValuePair = list[index1];
        string str = string.Join("; ", keyValuePair.Value);
        objArray3[index3] = (object) str;
      }
      this.InstanceLogger.Debug(stringBuilder1.ToString(), objArray1);
    }

    private async Task LogBody(string fmtText, HttpContent content)
    {
      byte[] numArray1;
      if (content != null)
        numArray1 = await content.ReadAsByteArrayAsync();
      else
        numArray1 = new byte[0];
      byte[] numArray2 = numArray1;
      char[] chArray = new char[numArray2.Length];
      for (int index = 0; index < numArray2.Length; ++index)
      {
        byte num = numArray2[index];
        chArray[index] = num < (byte) 32 || num > (byte) 126 ? '.' : (char) num;
      }
      this.InstanceLogger.Debug(fmtText, (object) new string(chArray));
    }

    protected override async Task<HttpResponseMessage> SendAsync(
      HttpRequestMessage request,
      CancellationToken cancellationToken)
    {
      bool loggable = this.IsLoggingEnabled && this.InstanceLogger.IsDebugEnabled;
      string loggingRequestId = "";
      if (loggable)
        loggingRequestId = Interlocked.Increment(ref this._loggingRequestId).ToString("X8");
      int triesRemaining = this.NumTries;
      int redirectRemaining = this.NumRedirects;
      Exception lastException = (Exception) null;
      request.Headers.Add("User-Agent", (this.ApplicationName == null ? "" : this.ApplicationName + " ") 
          + ConfigurableMessageHandler.UserAgentSuffix);

      HttpResponseMessage response = (HttpResponseMessage) null;
      do
      {
        cancellationToken.ThrowIfCancellationRequested();
        if (response != null)
        {
          response.Dispose();
          response = (HttpResponseMessage) null;
        }
        lastException = (Exception) null;
        IEnumerable<IHttpExecuteInterceptor> list1;
        lock (this.executeInterceptorsLock)
          list1 = (IEnumerable<IHttpExecuteInterceptor>) this.executeInterceptors.ToList<IHttpExecuteInterceptor>();
        foreach (IHttpExecuteInterceptor executeInterceptor in list1)
          await executeInterceptor.InterceptAsync(request, cancellationToken).ConfigureAwait(false);
        if (loggable)
        {
          if ((this.LogEvents & ConfigurableMessageHandler.LogEventType.RequestUri) != ConfigurableMessageHandler.LogEventType.None)
            this.InstanceLogger.Debug("Request[{0}] (triesRemaining={1}) URI: '{2}'", (object) loggingRequestId, (object) triesRemaining, (object) request.RequestUri);
          if ((this.LogEvents & ConfigurableMessageHandler.LogEventType.RequestHeaders) != ConfigurableMessageHandler.LogEventType.None)
            this.LogHeaders(string.Format("Request[{0}] Headers:", (object) loggingRequestId), (HttpHeaders) request.Headers, (HttpHeaders) request.Content?.Headers);
          if ((this.LogEvents & ConfigurableMessageHandler.LogEventType.RequestBody) != ConfigurableMessageHandler.LogEventType.None)
            await this.LogBody(string.Format("Request[{0}] Body: '{{0}}'", (object) loggingRequestId), request.Content);
        }
        try
        {
          response = await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
          lastException = ex;
        }
        if (response == null || response.StatusCode >= HttpStatusCode.BadRequest || response.StatusCode < HttpStatusCode.OK)
          --triesRemaining;
        bool flag;
        if (response == null)
        {
          bool flag1 = false;
          IEnumerable<IHttpExceptionHandler> list2;
          lock (this.exceptionHandlersLock)
            list2 = (IEnumerable<IHttpExceptionHandler>) this.exceptionHandlers.ToList<IHttpExceptionHandler>();
          foreach (IHttpExceptionHandler exceptionHandler in list2)
          {
            flag = flag1;
            flag1 = flag | await exceptionHandler.HandleExceptionAsync(new HandleExceptionArgs()
            {
              Request = request,
              Exception = lastException,
              TotalTries = this.NumTries,
              CurrentFailedTry = this.NumTries - triesRemaining,
              CancellationToken = cancellationToken
            }).ConfigureAwait(false);
          }
          if (!flag1)
          {
            this.InstanceLogger.Error(lastException, "Response[{0}] Exception was thrown while executing a HTTP request and it wasn't handled", (object) loggingRequestId);
            throw lastException;
          }
          if (loggable && (this.LogEvents & ConfigurableMessageHandler.LogEventType.ResponseAbnormal) != ConfigurableMessageHandler.LogEventType.None)
            this.InstanceLogger.Debug("Response[{0}] Exception {1} was thrown, but it was handled by an exception handler", (object) loggingRequestId, (object) lastException.Message);
        }
        else
        {
          if (loggable)
          {
            if ((this.LogEvents & ConfigurableMessageHandler.LogEventType.ResponseStatus) != ConfigurableMessageHandler.LogEventType.None)
              this.InstanceLogger.Debug("Response[{0}] Response status: {1} '{2}'", (object) loggingRequestId, (object) response.StatusCode, (object) response.ReasonPhrase);
            if ((this.LogEvents & ConfigurableMessageHandler.LogEventType.ResponseHeaders) != ConfigurableMessageHandler.LogEventType.None)
              this.LogHeaders(string.Format("Response[{0}] Headers:", (object) loggingRequestId), (HttpHeaders) response.Headers, (HttpHeaders) response.Content?.Headers);
            if ((this.LogEvents & ConfigurableMessageHandler.LogEventType.ResponseBody) != ConfigurableMessageHandler.LogEventType.None)
              await this.LogBody(string.Format("Response[{0}] Body: '{{0}}'", (object) loggingRequestId), response.Content);
          }
          if (response.IsSuccessStatusCode)
          {
            triesRemaining = 0;
          }
          else
          {
            bool flag2 = false;
            IEnumerable<IHttpUnsuccessfulResponseHandler> list3;
            lock (this.unsuccessfulResponseHandlersLock)
              list3 = (IEnumerable<IHttpUnsuccessfulResponseHandler>) this.unsuccessfulResponseHandlers.ToList<IHttpUnsuccessfulResponseHandler>();
            foreach (IHttpUnsuccessfulResponseHandler unsuccessfulResponseHandler in list3)
            {
              flag = flag2;
              flag2 = flag | await unsuccessfulResponseHandler.HandleResponseAsync(new HandleUnsuccessfulResponseArgs()
              {
                Request = request,
                Response = response,
                TotalTries = this.NumTries,
                CurrentFailedTry = this.NumTries - triesRemaining,
                CancellationToken = cancellationToken
              }).ConfigureAwait(false);
            }
            if (!flag2)
            {
              if (this.FollowRedirect && this.HandleRedirect(response))
              {
                if (redirectRemaining-- == 0)
                  triesRemaining = 0;
                if (loggable && (this.LogEvents & ConfigurableMessageHandler.LogEventType.ResponseAbnormal) != ConfigurableMessageHandler.LogEventType.None)
                  this.InstanceLogger.Debug("Response[{0}] Redirect response was handled successfully. Redirect to {1}", (object) loggingRequestId, (object) response.Headers.Location);
              }
              else
              {
                if (loggable && (this.LogEvents & ConfigurableMessageHandler.LogEventType.ResponseAbnormal) != ConfigurableMessageHandler.LogEventType.None)
                  this.InstanceLogger.Debug("Response[{0}] An abnormal response wasn't handled. Status code is {1}", (object) loggingRequestId, (object) response.StatusCode);
                triesRemaining = 0;
              }
            }
            else if (loggable && (this.LogEvents & ConfigurableMessageHandler.LogEventType.ResponseAbnormal) != ConfigurableMessageHandler.LogEventType.None)
              this.InstanceLogger.Debug("Response[{0}] An abnormal response was handled by an unsuccessful response handler. Status Code is {1}", (object) loggingRequestId, (object) response.StatusCode);
          }
        }
      }
      while (triesRemaining > 0);
      if (response == null)
      {
        this.InstanceLogger.Error(lastException, "Request[{0}] Exception was thrown while executing a HTTP request", (object) loggingRequestId);
        throw lastException;
      }
      if (!response.IsSuccessStatusCode & loggable && (this.LogEvents & ConfigurableMessageHandler.LogEventType.ResponseAbnormal) != ConfigurableMessageHandler.LogEventType.None)
        this.InstanceLogger.Debug("Response[{0}] Abnormal response is being returned. Status Code is {1}", (object) loggingRequestId, (object) response.StatusCode);
      return response;
    }

    private bool HandleRedirect(HttpResponseMessage message)
    {
      Uri location = message.Headers.Location;
      if (!message.IsRedirectStatusCode() || location == (Uri) null)
        return false;
      HttpRequestMessage requestMessage = message.RequestMessage;
      requestMessage.RequestUri = new Uri(requestMessage.RequestUri, location);
      if (message.StatusCode == HttpStatusCode.SeeOther)
        requestMessage.Method = HttpMethod.Get;
      requestMessage.Headers.Remove("Authorization");
      requestMessage.Headers.IfMatch.Clear();
      requestMessage.Headers.IfNoneMatch.Clear();
      requestMessage.Headers.IfModifiedSince = new DateTimeOffset?();
      requestMessage.Headers.IfUnmodifiedSince = new DateTimeOffset?();
      requestMessage.Headers.Remove("If-Range");
      return true;
    }

    [Flags]
    public enum LogEventType
    {
      None = 0,
      RequestUri = 1,
      RequestHeaders = 2,
      RequestBody = 4,
      ResponseStatus = 8,
      ResponseHeaders = 16, // 0x00000010
      ResponseBody = 32, // 0x00000020
      ResponseAbnormal = 64, // 0x00000040
    }
  }
}
