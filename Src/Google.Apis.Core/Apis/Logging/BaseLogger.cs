// Decompiled with JetBrains decompiler
// Type: Google.Apis.Logging.BaseLogger
// Assembly: Google.Apis.Core, Version=1.30.0.0, Culture=neutral, PublicKeyToken=4b01fa6e34db77ab
// MVID: 4EC5AF78-9014-40C1-B780-6317D8DBC917
// Assembly location: C:\Users\Admin\Desktop\re\YoutubeApp\Google.Apis.Core.dll

using Google.Apis.Util;
using System;
using System.Globalization;

namespace Google.Apis.Logging
{
  public abstract class BaseLogger : ILogger
  {
    private const string DateTimeFormatString = "yyyy-MM-dd HH:mm:ss.ffffff";
    private readonly string _loggerForTypeString;

    protected BaseLogger(LogLevel minimumLogLevel, IClock clock, Type forType)
    {
      this.MinimumLogLevel = minimumLogLevel;
      this.IsDebugEnabled = minimumLogLevel <= LogLevel.Debug;
      this.IsInfoEnabled = minimumLogLevel <= LogLevel.Info;
      this.IsWarningEnabled = minimumLogLevel <= LogLevel.Warning;
      this.IsErrorEnabled = minimumLogLevel <= LogLevel.Error;
      this.Clock = clock ?? SystemClock.Default;
      this.LoggerForType = forType;
      if ((object) forType != null)
      {
        string str = forType.Namespace ?? "";
        if (str.Length > 0)
          str += ".";
        this._loggerForTypeString = str + forType.Name + " ";
      }
      else
        this._loggerForTypeString = "";
    }

    public IClock Clock { get; }

    public Type LoggerForType { get; }

    public LogLevel MinimumLogLevel { get; }

    public bool IsDebugEnabled { get; }

    public bool IsInfoEnabled { get; }

    public bool IsWarningEnabled { get; }

    public bool IsErrorEnabled { get; }

    protected abstract ILogger BuildNewLogger(Type type);

    public ILogger ForType<T>() => this.ForType(typeof (T));

    public ILogger ForType(Type type) => (object) type != (object) this.LoggerForType ? this.BuildNewLogger(type) : (ILogger) this;

    protected abstract void Log(LogLevel logLevel, string formattedMessage);

    private string FormatLogEntry(
      string severityString,
      string message,
      params object[] formatArgs)
    {
      string str1 = string.Format(message, formatArgs);
      string str2 = this.Clock.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.ffffff", (IFormatProvider) CultureInfo.InvariantCulture);
      return string.Format("{0}{1} {2}{3}", (object) severityString, (object) str2, (object) this._loggerForTypeString, (object) str1);
    }

    public void Debug(string message, params object[] formatArgs)
    {
      if (!this.IsDebugEnabled)
        return;
      this.Log(LogLevel.Debug, this.FormatLogEntry("D", message, formatArgs));
    }

    public void Info(string message, params object[] formatArgs)
    {
      if (!this.IsInfoEnabled)
        return;
      this.Log(LogLevel.Info, this.FormatLogEntry("I", message, formatArgs));
    }

    public void Warning(string message, params object[] formatArgs)
    {
      if (!this.IsWarningEnabled)
        return;
      this.Log(LogLevel.Warning, this.FormatLogEntry("W", message, formatArgs));
    }

    public void Error(Exception exception, string message, params object[] formatArgs)
    {
      if (!this.IsErrorEnabled)
        return;
      this.Log(LogLevel.Error, string.Format("{0} {1}", (object) this.FormatLogEntry("E", message, formatArgs), (object) exception));
    }

    public void Error(string message, params object[] formatArgs)
    {
      if (!this.IsErrorEnabled)
        return;
      this.Log(LogLevel.Error, this.FormatLogEntry("E", message, formatArgs));
    }
  }
}
