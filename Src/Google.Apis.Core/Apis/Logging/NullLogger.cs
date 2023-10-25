// Decompiled with JetBrains decompiler
// Type: Google.Apis.Logging.NullLogger
// Assembly: Google.Apis.Core, Version=1.30.0.0, Culture=neutral, PublicKeyToken=4b01fa6e34db77ab
// MVID: 4EC5AF78-9014-40C1-B780-6317D8DBC917
// Assembly location: C:\Users\Admin\Desktop\re\YoutubeApp\Google.Apis.Core.dll

using System;

namespace Google.Apis.Logging
{
  public class NullLogger : ILogger
  {
    public bool IsDebugEnabled => false;

    public ILogger ForType(Type type) => (ILogger) new NullLogger();

    public ILogger ForType<T>() => (ILogger) new NullLogger();

    public void Info(string message, params object[] formatArgs)
    {
    }

    public void Warning(string message, params object[] formatArgs)
    {
    }

    public void Debug(string message, params object[] formatArgs)
    {
    }

    public void Error(Exception exception, string message, params object[] formatArgs)
    {
    }

    public void Error(string message, params object[] formatArgs)
    {
    }
  }
}
