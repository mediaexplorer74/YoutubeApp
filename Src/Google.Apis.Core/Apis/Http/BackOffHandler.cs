// Decompiled with JetBrains decompiler
// Type: Google.Apis.Http.BackOffHandler
// Assembly: Google.Apis.Core, Version=1.30.0.0, Culture=neutral, PublicKeyToken=4b01fa6e34db77ab
// MVID: 4EC5AF78-9014-40C1-B780-6317D8DBC917
// Assembly location: C:\Users\Admin\Desktop\re\YoutubeApp\Google.Apis.Core.dll

using Google.Apis.Logging;
using Google.Apis.Util;
using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Google.Apis.Http
{
  public class BackOffHandler : IHttpUnsuccessfulResponseHandler, IHttpExceptionHandler
  {
    private static readonly ILogger Logger = ApplicationContext.Logger.ForType<BackOffHandler>();

    public IBackOff BackOff { get; private set; }

    public TimeSpan MaxTimeSpan { get; private set; }

    public Func<HttpResponseMessage, bool> HandleUnsuccessfulResponseFunc { get; private set; }

    public Func<Exception, bool> HandleExceptionFunc { get; private set; }

    public BackOffHandler(IBackOff backOff)
      : this(new BackOffHandler.Initializer(backOff))
    {
    }

    public BackOffHandler(BackOffHandler.Initializer initializer)
    {
      this.BackOff = initializer.BackOff;
      this.MaxTimeSpan = initializer.MaxTimeSpan;
      this.HandleExceptionFunc = initializer.HandleExceptionFunc;
      this.HandleUnsuccessfulResponseFunc = initializer.HandleUnsuccessfulResponseFunc;
    }

    public virtual async Task<bool> HandleResponseAsync(HandleUnsuccessfulResponseArgs args) => this.HandleUnsuccessfulResponseFunc != null && this.HandleUnsuccessfulResponseFunc(args.Response) && await this.HandleAsync(args.SupportsRetry, args.CurrentFailedTry, args.CancellationToken).ConfigureAwait(false);

    public virtual async Task<bool> HandleExceptionAsync(HandleExceptionArgs args) => this.HandleExceptionFunc != null && this.HandleExceptionFunc(args.Exception) && await this.HandleAsync(args.SupportsRetry, args.CurrentFailedTry, args.CancellationToken).ConfigureAwait(false);

    private async Task<bool> HandleAsync(
      bool supportsRetry,
      int currentFailedTry,
      CancellationToken cancellationToken)
    {
      if (!supportsRetry || this.BackOff.MaxNumOfRetries < currentFailedTry)
        return false;
      TimeSpan ts = this.BackOff.GetNextBackOff(currentFailedTry);
      if (ts > this.MaxTimeSpan || ts < TimeSpan.Zero)
        return false;
      await this.Wait(ts, cancellationToken).ConfigureAwait(false);
      BackOffHandler.Logger.Debug("Back-Off handled the error. Waited {0}ms before next retry...", (object) ts.TotalMilliseconds);
      return true;
    }

    protected virtual async Task Wait(TimeSpan ts, CancellationToken cancellationToken) => await Task.Delay(ts, cancellationToken).ConfigureAwait(false);

    public class Initializer
    {
      public static readonly Func<HttpResponseMessage, bool> DefaultHandleUnsuccessfulResponseFunc = (Func<HttpResponseMessage, bool>) (r => r.StatusCode == HttpStatusCode.ServiceUnavailable);
      public static readonly Func<Exception, bool> DefaultHandleExceptionFunc = (Func<Exception, bool>) (ex => !(ex is TaskCanceledException) && !(ex is OperationCanceledException));

      public IBackOff BackOff { get; private set; }

      public TimeSpan MaxTimeSpan { get; set; }

      public Func<HttpResponseMessage, bool> HandleUnsuccessfulResponseFunc { get; set; }

      public Func<Exception, bool> HandleExceptionFunc { get; set; }

      public Initializer(IBackOff backOff)
      {
        this.BackOff = backOff;
        this.HandleExceptionFunc = BackOffHandler.Initializer.DefaultHandleExceptionFunc;
        this.HandleUnsuccessfulResponseFunc = BackOffHandler.Initializer.DefaultHandleUnsuccessfulResponseFunc;
        this.MaxTimeSpan = TimeSpan.FromSeconds(16.0);
      }
    }
  }
}
