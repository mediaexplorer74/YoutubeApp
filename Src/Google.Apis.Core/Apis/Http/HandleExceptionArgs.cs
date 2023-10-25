// Decompiled with JetBrains decompiler
// Type: Google.Apis.Http.HandleExceptionArgs
// Assembly: Google.Apis.Core, Version=1.30.0.0, Culture=neutral, PublicKeyToken=4b01fa6e34db77ab
// MVID: 4EC5AF78-9014-40C1-B780-6317D8DBC917
// Assembly location: C:\Users\Admin\Desktop\re\YoutubeApp\Google.Apis.Core.dll

using System;
using System.Net.Http;
using System.Threading;

namespace Google.Apis.Http
{
  public class HandleExceptionArgs
  {
    public HttpRequestMessage Request { get; set; }

    public Exception Exception { get; set; }

    public int TotalTries { get; set; }

    public int CurrentFailedTry { get; set; }

    public bool SupportsRetry => this.TotalTries - this.CurrentFailedTry > 0;

    public CancellationToken CancellationToken { get; set; }
  }
}
