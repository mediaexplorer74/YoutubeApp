// Decompiled with JetBrains decompiler
// Type: Google.Apis.Util.IClock
// Assembly: Google.Apis.Core, Version=1.30.0.0, Culture=neutral, PublicKeyToken=4b01fa6e34db77ab
// MVID: 4EC5AF78-9014-40C1-B780-6317D8DBC917
// Assembly location: C:\Users\Admin\Desktop\re\YoutubeApp\Google.Apis.Core.dll

using System;

namespace Google.Apis.Util
{
  public interface IClock
  {
    [Obsolete("System local time is almost always inappropriate to use. If you really need this, call UtcNow and then call ToLocalTime on the result")]
    DateTime Now { get; }

    DateTime UtcNow { get; }
  }
}
