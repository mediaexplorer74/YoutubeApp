// Decompiled with JetBrains decompiler
// Type: Google.Apis.Download.MediaDownloader
// Assembly: Google.Apis, Version=1.30.0.0, Culture=neutral, PublicKeyToken=4b01fa6e34db77ab
// MVID: 6C44ABAE-71BD-4009-BDB7-D7E324A25671
// Assembly location: C:\Users\Admin\Desktop\re\YoutubeApp\Google.Apis.dll

using Google.Apis.Logging;
using Google.Apis.Media;
using Google.Apis.Services;
using Google.Apis.Util;
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace Google.Apis.Download
{
  public class MediaDownloader : IMediaDownloader
  {
    private static readonly ILogger Logger = ApplicationContext.Logger.ForType<MediaDownloader>();
    private readonly IClientService service;
    private const int MB = 1048576;
    public const int MaximumChunkSize = 10485760;
    private int chunkSize = 10485760;

    static MediaDownloader() => UriPatcher.PatchUriQuirks();

    public int ChunkSize
    {
      get => this.chunkSize;
      set => this.chunkSize = value <= 10485760 ? value : throw new ArgumentOutOfRangeException(nameof (ChunkSize));
    }

    public RangeHeaderValue Range { get; set; }

    private void UpdateProgress(IDownloadProgress progress)
    {
      Action<IDownloadProgress> progressChanged = this.ProgressChanged;
      if (progressChanged == null)
        return;
      progressChanged(progress);
    }

    public MediaDownloader(IClientService service) => this.service = service;

    public Action<HttpRequestMessage> ModifyRequest { get; set; }

    public event Action<IDownloadProgress> ProgressChanged;

    public IDownloadProgress Download(string url, Stream stream) => this.DownloadCoreAsync(url, stream, CancellationToken.None).Result;

    public async Task<IDownloadProgress> DownloadAsync(string url, Stream stream) => await this.DownloadAsync(url, stream, CancellationToken.None).ConfigureAwait(false);

    public async Task<IDownloadProgress> DownloadAsync(
      string url,
      Stream stream,
      CancellationToken cancellationToken)
    {
      return await this.DownloadCoreAsync(url, stream, cancellationToken).ConfigureAwait(false);
    }

    private async Task<IDownloadProgress> DownloadCoreAsync(
      string url,
      Stream stream,
      CancellationToken cancellationToken)
    {
      url.ThrowIfNull<string>(nameof (url));
      stream.ThrowIfNull<Stream>(nameof (stream));
      if (!stream.CanWrite)
        throw new ArgumentException("stream doesn't support write operations");
      UriBuilder uriBuilder = new UriBuilder(url);
      uriBuilder.Query = uriBuilder.Query == null || uriBuilder.Query.Length <= 1 ? "alt=media" : uriBuilder.Query.Substring(1) + "&alt=media";
      HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, uriBuilder.ToString());
      request.Headers.Range = this.Range;
      Action<HttpRequestMessage> modifyRequest = this.ModifyRequest;
      if (modifyRequest != null)
        modifyRequest(request);
      long bytesReturned = 0;
      try
      {
        HttpCompletionOption completionOption = HttpCompletionOption.ResponseHeadersRead;
        using (HttpResponseMessage response = await this.service.HttpClient.SendAsync(request, completionOption, cancellationToken).ConfigureAwait(false))
        {
          if (!response.IsSuccessStatusCode)
            throw await MediaApiErrorHandling.ExceptionForResponseAsync(this.service, response).ConfigureAwait(false);
          this.OnResponseReceived(response);
          using (Stream responseStream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false))
          {
            MediaDownloader.CountedBuffer buffer = new MediaDownloader.CountedBuffer(this.ChunkSize + 1);
            while (true)
            {
              await buffer.Fill(responseStream, cancellationToken).ConfigureAwait(false);
              int bytesToReturn = Math.Min(this.ChunkSize, buffer.Count);
              this.OnDataReceived(buffer.Data, bytesToReturn);
              await stream.WriteAsync(buffer.Data, 0, bytesToReturn, cancellationToken).ConfigureAwait(false);
              bytesReturned += (long) bytesToReturn;
              buffer.RemoveFromFront(this.ChunkSize);
              if (!buffer.IsEmpty)
                this.UpdateProgress((IDownloadProgress) new MediaDownloader.DownloadProgress(DownloadStatus.Downloading, bytesReturned));
              else
                break;
            }
            buffer = (MediaDownloader.CountedBuffer) null;
          }
          this.OnDownloadCompleted();
          MediaDownloader.DownloadProgress progress = new MediaDownloader.DownloadProgress(DownloadStatus.Completed, bytesReturned);
          this.UpdateProgress((IDownloadProgress) progress);
          return (IDownloadProgress) progress;
        }
      }
      catch (TaskCanceledException ex)
      {
        MediaDownloader.Logger.Error((Exception) ex, "Download media was canceled");
        this.UpdateProgress((IDownloadProgress) new MediaDownloader.DownloadProgress((Exception) ex, bytesReturned));
        throw;
      }
      catch (Exception ex)
      {
        MediaDownloader.Logger.Error(ex, "Exception occurred while downloading media");
        MediaDownloader.DownloadProgress progress = new MediaDownloader.DownloadProgress(ex, bytesReturned);
        this.UpdateProgress((IDownloadProgress) progress);
        return (IDownloadProgress) progress;
      }
    }

    protected virtual void OnResponseReceived(HttpResponseMessage response)
    {
    }

    protected virtual void OnDataReceived(byte[] data, int length)
    {
    }

    protected virtual void OnDownloadCompleted()
    {
    }

    private class DownloadProgress : IDownloadProgress
    {
      public DownloadProgress(DownloadStatus status, long bytes)
      {
        this.Status = status;
        this.BytesDownloaded = bytes;
      }

      public DownloadProgress(Exception exception, long bytes)
      {
        this.Status = DownloadStatus.Failed;
        this.BytesDownloaded = bytes;
        this.Exception = exception;
      }

      public DownloadStatus Status { get; private set; }

      public long BytesDownloaded { get; private set; }

      public Exception Exception { get; private set; }
    }

    private class CountedBuffer
    {
      public byte[] Data { get; set; }

      public int Count { get; private set; }

      public CountedBuffer(int size)
      {
        this.Data = new byte[size];
        this.Count = 0;
      }

      public bool IsEmpty => this.Count == 0;

      public async Task Fill(Stream stream, CancellationToken cancellationToken)
      {
        int num;
        for (; this.Count < this.Data.Length; this.Count += num)
        {
          num = await stream.ReadAsync(this.Data, this.Count, this.Data.Length - this.Count, cancellationToken).ConfigureAwait(false);
          if (num == 0)
            break;
        }
      }

      public void RemoveFromFront(int n)
      {
        if (n >= this.Count)
        {
          this.Count = 0;
        }
        else
        {
          Array.Copy((Array) this.Data, n, (Array) this.Data, 0, this.Count - n);
          this.Count -= n;
        }
      }
    }
  }
}
