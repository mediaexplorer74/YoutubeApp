﻿// Decompiled with JetBrains decompiler
// Type: Google.Apis.Util.IBackOff
// Assembly: Google.Apis.Core, Version=1.30.0.0, Culture=neutral, PublicKeyToken=4b01fa6e34db77ab
// MVID: 4EC5AF78-9014-40C1-B780-6317D8DBC917
// Assembly location: C:\Users\Admin\Desktop\re\YoutubeApp\Google.Apis.Core.dll

using System;

namespace Google.Apis.Util
{
  public interface IBackOff
  {
    TimeSpan GetNextBackOff(int currentRetry);

    int MaxNumOfRetries { get; }
  }
}
