// Decompiled with JetBrains decompiler
// Type: Google.Apis.Logging.ConsoleLogger
// Assembly: Google.Apis.Core, Version=1.30.0.0, Culture=neutral, PublicKeyToken=4b01fa6e34db77ab
// MVID: 4EC5AF78-9014-40C1-B780-6317D8DBC917
// Assembly location: C:\Users\Admin\Desktop\re\YoutubeApp\Google.Apis.Core.dll

using Google.Apis.Util;
using System;

namespace Google.Apis.Logging
{
  public sealed class ConsoleLogger : BaseLogger, ILogger
  {
    public ConsoleLogger(LogLevel minimumLogLevel, bool logToStdOut = false, IClock clock = null)
      : this(minimumLogLevel, logToStdOut, clock, (Type) null)
    {
    }

    private ConsoleLogger(LogLevel minimumLogLevel, bool logToStdOut, IClock clock, Type forType)
      : base(minimumLogLevel, clock, forType)
    {
      this.LogToStdOut = logToStdOut;
    }

    public bool LogToStdOut { get; }

    protected override ILogger BuildNewLogger(Type type) => (ILogger) new ConsoleLogger(this.MinimumLogLevel, this.LogToStdOut, this.Clock, type);

    protected override void Log(LogLevel logLevel, string formattedMessage) => (this.LogToStdOut ? Console.Out : Console.Error).WriteLine(formattedMessage);
  }
}
