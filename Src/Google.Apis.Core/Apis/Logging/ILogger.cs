// Decompiled with JetBrains decompiler
// Type: Google.Apis.Logging.ILogger
// Assembly: Google.Apis.Core, Version=1.30.0.0, Culture=neutral, PublicKeyToken=4b01fa6e34db77ab
// MVID: 4EC5AF78-9014-40C1-B780-6317D8DBC917
// Assembly location: C:\Users\Admin\Desktop\re\YoutubeApp\Google.Apis.Core.dll

using System;

namespace Google.Apis.Logging
{
  public interface ILogger
  {
    bool IsDebugEnabled { get; }

    ILogger ForType(Type type);

    ILogger ForType<T>();

    void Debug(string message, params object[] formatArgs);

    void Info(string message, params object[] formatArgs);

    void Warning(string message, params object[] formatArgs);

    void Error(Exception exception, string message, params object[] formatArgs);

    void Error(string message, params object[] formatArgs);
  }
}
