// Decompiled with JetBrains decompiler
// Type: Google.Apis.Upload.ResumableUpload
// Assembly: Google.Apis, Version=1.30.0.0, Culture=neutral, PublicKeyToken=4b01fa6e34db77ab
// MVID: 6C44ABAE-71BD-4009-BDB7-D7E324A25671
// Assembly location: C:\Users\Admin\Desktop\re\YoutubeApp\Google.Apis.dll

using Google.Apis.Http;
using Google.Apis.Logging;
using Google.Apis.Media;
using Google.Apis.Requests;
using Google.Apis.Testing;
using Google.Apis.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Google.Apis.Upload
{
  public abstract class ResumableUpload
  {
    private static readonly ILogger Logger = ApplicationContext.Logger.ForType<ResumableUpload>();
    private const int KB = 1024;
    private const int MB = 1048576;
    public const int MinimumChunkSize = 262144;
    public const int DefaultChunkSize = 10485760;
    internal int BufferSize = 4096;
    private const int UnknownSize = -1;
    private const string ZeroByteContentRangeHeader = "bytes */0";
    [VisibleForTestOnly]
    protected int chunkSize = 10485760;

    protected ResumableUpload(Stream contentStream, ResumableUploadOptions options)
    {
      contentStream.ThrowIfNull<Stream>(nameof (contentStream));
      this.ContentStream = contentStream;
      this.StreamLength = this.ContentStream.CanSeek ? this.ContentStream.Length : -1L;
      ConfigurableHttpClient configurableHttpClient = options?.ConfigurableHttpClient;
      if (configurableHttpClient == null)
        configurableHttpClient = new HttpClientFactory().CreateHttpClient(new CreateHttpClientArgs()
        {
          ApplicationName = nameof (ResumableUpload),
          GZipEnabled = true
        });
      this.HttpClient = configurableHttpClient;
      this.Options = options;
    }

    public static ResumableUpload CreateFromUploadUri(
      Uri uploadUri,
      Stream contentStream,
      ResumableUploadOptions options = null)
    {
      uploadUri.ThrowIfNull<Uri>(nameof (uploadUri));
      return (ResumableUpload) new ResumableUpload.InitiatedResumableUpload(uploadUri, contentStream, options);
    }

    protected ResumableUploadOptions Options { get; }

    internal ConfigurableHttpClient HttpClient { get; }

    public Stream ContentStream { get; }

    internal long StreamLength { get; set; }

    private byte[] LastMediaRequest { get; set; }

    private int LastMediaLength { get; set; }

    private Uri UploadUri { get; set; }

    private long BytesServerReceived { get; set; }

    private long BytesClientSent { get; set; }

    public int ChunkSize
    {
      get => this.chunkSize;
      set => this.chunkSize = value >= 262144 ? value : throw new ArgumentOutOfRangeException(nameof (ChunkSize));
    }

    public event Action<IUploadProgress> ProgressChanged;

    private ResumableUpload.ResumableUploadProgress Progress { get; set; }

    private void UpdateProgress(ResumableUpload.ResumableUploadProgress progress)
    {
      this.Progress = progress;
      Action<IUploadProgress> progressChanged = this.ProgressChanged;
      if (progressChanged == null)
        return;
      progressChanged((IUploadProgress) progress);
    }

    public IUploadProgress GetProgress() => (IUploadProgress) this.Progress;

    public event Action<IUploadSessionData> UploadSessionData;

    private void SendUploadSessionData(
      ResumableUpload.ResumeableUploadSessionData sessionData)
    {
      Action<IUploadSessionData> uploadSessionData = this.UploadSessionData;
      if (uploadSessionData == null)
        return;
      uploadSessionData((IUploadSessionData) sessionData);
    }

    public IUploadProgress Upload() => this.UploadAsync(CancellationToken.None).Result;

    public Task<IUploadProgress> UploadAsync() => this.UploadAsync(CancellationToken.None);

    public async Task<IUploadProgress> UploadAsync(CancellationToken cancellationToken)
    {
      this.BytesServerReceived = 0L;
      this.UpdateProgress(new ResumableUpload.ResumableUploadProgress(UploadStatus.Starting, 0L));
      try
      {
        this.UploadUri = await this.InitiateSessionAsync(cancellationToken).ConfigureAwait(false);
        if (this.ContentStream.CanSeek)
          this.SendUploadSessionData(new ResumableUpload.ResumeableUploadSessionData(this.UploadUri));
        ResumableUpload.Logger.Debug("MediaUpload[{0}] - Start uploading...", (object) this.UploadUri);
      }
      catch (Exception ex)
      {
        ResumableUpload.Logger.Error(ex, "MediaUpload - Exception occurred while initializing the upload");
        this.UpdateProgress(new ResumableUpload.ResumableUploadProgress(ex, this.BytesServerReceived));
        return (IUploadProgress) this.Progress;
      }
      return await this.UploadCoreAsync(cancellationToken).ConfigureAwait(false);
    }

    public IUploadProgress Resume() => this.ResumeAsync((Uri) null, CancellationToken.None).Result;

    public IUploadProgress Resume(Uri uploadUri) => this.ResumeAsync(uploadUri, CancellationToken.None).Result;

    public Task<IUploadProgress> ResumeAsync() => this.ResumeAsync((Uri) null, CancellationToken.None);

    public Task<IUploadProgress> ResumeAsync(CancellationToken cancellationToken) => this.ResumeAsync((Uri) null, cancellationToken);

    public Task<IUploadProgress> ResumeAsync(Uri uploadUri) => this.ResumeAsync(uploadUri, CancellationToken.None);

    public async Task<IUploadProgress> ResumeAsync(
      Uri uploadUri,
      CancellationToken cancellationToken)
    {
      ResumableUpload resumable = this;
      if (uploadUri != (Uri) null)
      {
        if (!resumable.ContentStream.CanSeek)
          throw new NotImplementedException("Resume after program restart not allowed when ContentStream.CanSeek is false");
        ResumableUpload.Logger.Info("Resuming after program restart: UploadUri={0}", (object) uploadUri);
        resumable.UploadUri = uploadUri;
      }
      if (resumable.UploadUri == (Uri) null)
      {
        ResumableUpload.Logger.Info("There isn't any upload in progress, so starting to upload again");
        return await resumable.UploadAsync(cancellationToken).ConfigureAwait(false);
      }
      string str = string.Format("bytes */{0}", resumable.StreamLength < 0L ? (object) "*" : (object) resumable.StreamLength.ToString());
      HttpRequestMessage request = new RequestBuilder()
      {
        BaseUri = resumable.UploadUri,
        Method = "PUT"
      }.CreateRequest();
      request.SetEmptyContent().Headers.Add("Content-Range", str);
      try
      {
        HttpResponseMessage response;
        using (new ResumableUpload.ServerErrorCallback(resumable))
          response = await resumable.HttpClient.SendAsync(request, cancellationToken).ConfigureAwait(false);
        if (await resumable.HandleResponse(response).ConfigureAwait(false))
        {
          resumable.UpdateProgress(new ResumableUpload.ResumableUploadProgress(UploadStatus.Completed, resumable.BytesServerReceived));
          return (IUploadProgress) resumable.Progress;
        }
      }
      catch (TaskCanceledException ex)
      {
        ResumableUpload.Logger.Error((Exception) ex, "MediaUpload[{0}] - Task was canceled", (object) resumable.UploadUri);
        resumable.UpdateProgress(new ResumableUpload.ResumableUploadProgress((Exception) ex, resumable.BytesServerReceived));
        throw ex;
      }
      catch (Exception ex)
      {
        ResumableUpload.Logger.Error(ex, "MediaUpload[{0}] - Exception occurred while resuming uploading media", (object) resumable.UploadUri);
        resumable.UpdateProgress(new ResumableUpload.ResumableUploadProgress(ex, resumable.BytesServerReceived));
        return (IUploadProgress) resumable.Progress;
      }
      return await resumable.UploadCoreAsync(cancellationToken).ConfigureAwait(false);
    }

    private async Task<IUploadProgress> UploadCoreAsync(CancellationToken cancellationToken)
    {
      ResumableUpload resumable = this;
      try
      {
        using (new ResumableUpload.ServerErrorCallback(resumable))
        {
          while (true)
          {
            if (!await resumable.SendNextChunkAsync(resumable.ContentStream, cancellationToken).ConfigureAwait(false))
              resumable.UpdateProgress(new ResumableUpload.ResumableUploadProgress(UploadStatus.Uploading, resumable.BytesServerReceived));
            else
              break;
          }
          resumable.UpdateProgress(new ResumableUpload.ResumableUploadProgress(UploadStatus.Completed, resumable.BytesServerReceived));
        }
      }
      catch (TaskCanceledException ex)
      {
        ResumableUpload.Logger.Error((Exception) ex, "MediaUpload[{0}] - Task was canceled", (object) resumable.UploadUri);
        resumable.UpdateProgress(new ResumableUpload.ResumableUploadProgress((Exception) ex, resumable.BytesServerReceived));
        throw ex;
      }
      catch (Exception ex)
      {
        ResumableUpload.Logger.Error(ex, "MediaUpload[{0}] - Exception occurred while uploading media", (object) resumable.UploadUri);
        resumable.UpdateProgress(new ResumableUpload.ResumableUploadProgress(ex, resumable.BytesServerReceived));
      }
      return (IUploadProgress) resumable.Progress;
    }

    public abstract Task<Uri> InitiateSessionAsync(CancellationToken cancellationToken = default (CancellationToken));

    protected virtual void ProcessResponse(HttpResponseMessage httpResponse)
    {
    }

    protected async Task<bool> SendNextChunkAsync(
      Stream stream,
      CancellationToken cancellationToken)
    {
      cancellationToken.ThrowIfCancellationRequested();
      HttpRequestMessage request = new RequestBuilder()
      {
        BaseUri = this.UploadUri,
        Method = "PUT"
      }.CreateRequest();
      this.BytesClientSent = this.BytesServerReceived + (this.ContentStream.CanSeek ? (long) this.PrepareNextChunkKnownSize(request, stream, cancellationToken) : (long) this.PrepareNextChunkUnknownSize(request, stream, cancellationToken));
      ResumableUpload.Logger.Debug("MediaUpload[{0}] - Sending bytes={1}-{2}", (object) this.UploadUri, (object) this.BytesServerReceived, (object) (this.BytesClientSent - 1L));
      return await this.HandleResponse(await this.HttpClient.SendAsync(request, cancellationToken).ConfigureAwait(false)).ConfigureAwait(false);
    }

    private async Task<bool> HandleResponse(HttpResponseMessage response)
    {
      if (response.IsSuccessStatusCode)
      {
        this.MediaCompleted(response);
        return true;
      }
      if (response.StatusCode != (HttpStatusCode) 308)
        throw await this.ExceptionForResponseAsync(response).ConfigureAwait(false);
      IEnumerable<string> source = response.Headers.FirstOrDefault<KeyValuePair<string, IEnumerable<string>>>((Func<KeyValuePair<string, IEnumerable<string>>, bool>) (x => x.Key == "Range")).Value;
      this.BytesServerReceived = this.GetNextByte(source != null ? source.First<string>() : (string) null);
      ResumableUpload.Logger.Debug("MediaUpload[{0}] - {1} Bytes were sent successfully", (object) this.UploadUri, (object) this.BytesServerReceived);
      return false;
    }

    protected Task<GoogleApiException> ExceptionForResponseAsync(HttpResponseMessage response) => MediaApiErrorHandling.ExceptionForResponseAsync(this.Options?.Serializer, this.Options?.ServiceName, response);

    private void MediaCompleted(HttpResponseMessage response)
    {
      ResumableUpload.Logger.Debug("MediaUpload[{0}] - media was uploaded successfully", (object) this.UploadUri);
      this.ProcessResponse(response);
      this.BytesServerReceived = this.StreamLength;
      this.LastMediaRequest = (byte[]) null;
    }

    private int PrepareNextChunkUnknownSize(
      HttpRequestMessage request,
      Stream stream,
      CancellationToken cancellationToken)
    {
      if (this.LastMediaRequest == null)
      {
        this.LastMediaRequest = new byte[this.ChunkSize + 1];
        this.LastMediaLength = 0;
      }
      int count1 = (int) (this.BytesClientSent - this.BytesServerReceived) + Math.Max(0, this.LastMediaLength - this.ChunkSize);
      if (this.LastMediaLength != count1)
      {
        Buffer.BlockCopy((Array) this.LastMediaRequest, this.LastMediaLength - count1, (Array) this.LastMediaRequest, 0, count1);
        this.LastMediaLength = count1;
      }
      while (this.LastMediaLength < this.ChunkSize + 1 && this.StreamLength == -1L)
      {
        cancellationToken.ThrowIfCancellationRequested();
        int count2 = Math.Min(this.BufferSize, this.ChunkSize + 1 - this.LastMediaLength);
        int num = stream.Read(this.LastMediaRequest, this.LastMediaLength, count2);
        this.LastMediaLength += num;
        if (num == 0)
          this.StreamLength = this.BytesServerReceived + (long) this.LastMediaLength;
      }
      int num1 = Math.Min(this.ChunkSize, this.LastMediaLength);
      ByteArrayContent byteArrayContent = new ByteArrayContent(this.LastMediaRequest, 0, num1);
      byteArrayContent.Headers.Add("Content-Range", this.GetContentRangeHeader(this.BytesServerReceived, (long) num1));
      request.Content = (HttpContent) byteArrayContent;
      return num1;
    }

    private int PrepareNextChunkKnownSize(
      HttpRequestMessage request,
      Stream stream,
      CancellationToken cancellationToken)
    {
      int num1 = (int) Math.Min(this.StreamLength - this.BytesServerReceived, (long) this.ChunkSize);
      byte[] buffer = new byte[Math.Min(num1, this.BufferSize)];
      if (stream.Position != this.BytesServerReceived)
        stream.Position = this.BytesServerReceived;
      MemoryStream content = new MemoryStream(num1);
      int num2 = 0;
      while (true)
      {
        cancellationToken.ThrowIfCancellationRequested();
        int count = stream.Read(buffer, 0, Math.Min(buffer.Length, num1 - num2));
        if (count != 0)
        {
          content.Write(buffer, 0, count);
          num2 += count;
        }
        else
          break;
      }
      content.Position = 0L;
      request.Content = (HttpContent) new StreamContent((Stream) content);
      request.Content.Headers.Add("Content-Range", this.GetContentRangeHeader(this.BytesServerReceived, (long) num1));
      return num1;
    }

    private long GetNextByte(string range) => range != null ? long.Parse(range.Substring(range.IndexOf('-') + 1)) + 1L : 0L;

    private string GetContentRangeHeader(long chunkStart, long chunkSize)
    {
      string str = this.StreamLength < 0L ? "*" : this.StreamLength.ToString();
      if (chunkStart == 0L && chunkSize == 0L && this.StreamLength == 0L)
        return "bytes */0";
      long num = chunkStart + chunkSize - 1L;
      return string.Format("bytes {0}-{1}/{2}", (object) chunkStart, (object) num, (object) str);
    }

    private sealed class InitiatedResumableUpload : ResumableUpload
    {
      private Uri _initiatedUploadUri;

      public InitiatedResumableUpload(
        Uri uploadUri,
        Stream contentStream,
        ResumableUploadOptions options)
        : base(contentStream, options)
      {
        this._initiatedUploadUri = uploadUri;
      }

      public override Task<Uri> InitiateSessionAsync(CancellationToken cancellationToken = default (CancellationToken)) => Task.FromResult<Uri>(this._initiatedUploadUri);
    }

    private class ServerErrorCallback : 
      IHttpUnsuccessfulResponseHandler,
      IHttpExceptionHandler,
      IDisposable
    {
      private ResumableUpload Owner { get; set; }

      public ServerErrorCallback(ResumableUpload resumable)
      {
        this.Owner = resumable;
        this.Owner.HttpClient.MessageHandler.AddUnsuccessfulResponseHandler((IHttpUnsuccessfulResponseHandler) this);
        this.Owner.HttpClient.MessageHandler.AddExceptionHandler((IHttpExceptionHandler) this);
      }

      public Task<bool> HandleResponseAsync(HandleUnsuccessfulResponseArgs args)
      {
        bool result = false;
        int statusCode = (int) args.Response.StatusCode;
        if (args.SupportsRetry && args.Request.RequestUri.Equals((object) this.Owner.UploadUri) && statusCode / 100 == 5)
          result = this.OnServerError(args.Request);
        TaskCompletionSource<bool> completionSource = new TaskCompletionSource<bool>();
        completionSource.SetResult(result);
        return completionSource.Task;
      }

      public Task<bool> HandleExceptionAsync(HandleExceptionArgs args)
      {
        bool result = args.SupportsRetry && !args.CancellationToken.IsCancellationRequested && args.Request.RequestUri.Equals((object) this.Owner.UploadUri) && this.OnServerError(args.Request);
        TaskCompletionSource<bool> completionSource = new TaskCompletionSource<bool>();
        completionSource.SetResult(result);
        return completionSource.Task;
      }

      private bool OnServerError(HttpRequestMessage request)
      {
        string str = string.Format("bytes */{0}", this.Owner.StreamLength < 0L ? (object) "*" : (object) this.Owner.StreamLength.ToString());
        request.Headers.Clear();
        request.Method = HttpMethod.Put;
        request.SetEmptyContent().Headers.Add("Content-Range", str);
        return true;
      }

      public void Dispose()
      {
        this.Owner.HttpClient.MessageHandler.RemoveUnsuccessfulResponseHandler((IHttpUnsuccessfulResponseHandler) this);
        this.Owner.HttpClient.MessageHandler.RemoveExceptionHandler((IHttpExceptionHandler) this);
      }
    }

    private class ResumableUploadProgress : IUploadProgress
    {
      public ResumableUploadProgress(UploadStatus status, long bytesSent)
      {
        this.Status = status;
        this.BytesSent = bytesSent;
      }

      public ResumableUploadProgress(Exception exception, long bytesSent)
      {
        this.Status = UploadStatus.Failed;
        this.BytesSent = bytesSent;
        this.Exception = exception;
      }

      public UploadStatus Status { get; private set; }

      public long BytesSent { get; private set; }

      public Exception Exception { get; private set; }
    }

    private class ResumeableUploadSessionData : IUploadSessionData
    {
      public ResumeableUploadSessionData(Uri uploadUri) => this.UploadUri = uploadUri;

      public Uri UploadUri { get; private set; }
    }
  }
}
