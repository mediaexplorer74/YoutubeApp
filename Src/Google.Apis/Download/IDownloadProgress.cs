// Decompiled with JetBrains decompiler
// Type: Google.Apis.Download.IDownloadProgress
// Assembly: Google.Apis, Version=1.30.0.0, Culture=neutral, PublicKeyToken=4b01fa6e34db77ab
// MVID: 6C44ABAE-71BD-4009-BDB7-D7E324A25671
// Assembly location: C:\Users\Admin\Desktop\re\YoutubeApp\Google.Apis.dll

using System;

namespace Google.Apis.Download
{
  public interface IDownloadProgress
  {
    DownloadStatus Status { get; }

    long BytesDownloaded { get; }

    Exception Exception { get; }
  }
}
