// Decompiled with JetBrains decompiler
// Type: Google.Apis.Logging.MemoryLogger
// Assembly: Google.Apis.Core, Version=1.30.0.0, Culture=neutral, PublicKeyToken=4b01fa6e34db77ab
// MVID: 4EC5AF78-9014-40C1-B780-6317D8DBC917
// Assembly location: C:\Users\Admin\Desktop\re\YoutubeApp\Google.Apis.Core.dll

using Google.Apis.Util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Google.Apis.Logging
{
  public sealed class MemoryLogger : BaseLogger, ILogger
  {
    private readonly int _maximumEntryCount;
    private readonly List<string> _logEntries;

    public MemoryLogger(LogLevel minimumLogLevel, int maximumEntryCount = 1000, IClock clock = null)
      : this(minimumLogLevel, maximumEntryCount, clock, new List<string>(), (Type) null)
    {
    }

    private MemoryLogger(
      LogLevel minimumLogLevel,
      int maximumEntryCount,
      IClock clock,
      List<string> logEntries,
      Type forType)
      : base(minimumLogLevel, clock, forType)
    {
      this._logEntries = logEntries;
      this.LogEntries = (IList<string>) new ReadOnlyCollection<string>((IList<string>) this._logEntries);
      this._maximumEntryCount = maximumEntryCount;
    }

    public IList<string> LogEntries { get; }

    protected override ILogger BuildNewLogger(Type type) => (ILogger) new MemoryLogger(this.MinimumLogLevel, this._maximumEntryCount, this.Clock, this._logEntries, type);

    protected override void Log(LogLevel logLevel, string formattedMessage)
    {
      lock (this._logEntries)
      {
        if (this._logEntries.Count >= this._maximumEntryCount)
          return;
        this._logEntries.Add(formattedMessage);
      }
    }
  }
}
