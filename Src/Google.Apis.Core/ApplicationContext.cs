// Decompiled with JetBrains decompiler
// Type: Google.ApplicationContext
// Assembly: Google.Apis.Core, Version=1.30.0.0, Culture=neutral, PublicKeyToken=4b01fa6e34db77ab
// MVID: 4EC5AF78-9014-40C1-B780-6317D8DBC917
// Assembly location: C:\Users\Admin\Desktop\re\YoutubeApp\Google.Apis.Core.dll

using Google.Apis.Logging;
using System;

namespace Google
{
  public static class ApplicationContext
  {
    private static ILogger logger;

    internal static void Reset() => ApplicationContext.logger = (ILogger) null;

    public static ILogger Logger => ApplicationContext.logger ?? (ApplicationContext.logger = (ILogger) new NullLogger());

    public static void RegisterLogger(ILogger loggerToRegister)
    {
      switch (ApplicationContext.logger)
      {
        case null:
        case NullLogger _:
          ApplicationContext.logger = loggerToRegister;
          break;
        default:
          throw new InvalidOperationException("A logger was already registered with this context.");
      }
    }
  }
}
