// Decompiled with JetBrains decompiler
// Type: Google.Apis.Util.ExponentialBackOff
// Assembly: Google.Apis.Core, Version=1.30.0.0, Culture=neutral, PublicKeyToken=4b01fa6e34db77ab
// MVID: 4EC5AF78-9014-40C1-B780-6317D8DBC917
// Assembly location: C:\Users\Admin\Desktop\re\YoutubeApp\Google.Apis.Core.dll

using System;

namespace Google.Apis.Util
{
  public class ExponentialBackOff : IBackOff
  {
    private const int MaxAllowedNumRetries = 20;
    private readonly TimeSpan deltaBackOff;
    private readonly int maxNumOfRetries;
    private Random random = new Random();

    public TimeSpan DeltaBackOff => this.deltaBackOff;

    public int MaxNumOfRetries => this.maxNumOfRetries;

    public ExponentialBackOff()
      : this(TimeSpan.FromMilliseconds(250.0))
    {
    }

    public ExponentialBackOff(TimeSpan deltaBackOff, int maximumNumOfRetries = 10)
    {
      if (deltaBackOff < TimeSpan.Zero || deltaBackOff > TimeSpan.FromSeconds(1.0))
        throw new ArgumentOutOfRangeException(nameof (deltaBackOff));
      if (maximumNumOfRetries < 0 || maximumNumOfRetries > 20)
        throw new ArgumentOutOfRangeException(nameof (deltaBackOff));
      this.deltaBackOff = deltaBackOff;
      this.maxNumOfRetries = maximumNumOfRetries;
    }

    public TimeSpan GetNextBackOff(int currentRetry)
    {
      if (currentRetry <= 0)
        throw new ArgumentOutOfRangeException(nameof (currentRetry));
      if (currentRetry > this.MaxNumOfRetries)
        return TimeSpan.MinValue;
      Random random = this.random;
      TimeSpan deltaBackOff = this.DeltaBackOff;
      int minValue = (int) (deltaBackOff.TotalMilliseconds * -1.0);
      deltaBackOff = this.DeltaBackOff;
      int maxValue = (int) (deltaBackOff.TotalMilliseconds * 1.0);
      double num = (double) random.Next(minValue, maxValue);
      return TimeSpan.FromMilliseconds((double) (int) (Math.Pow(2.0, (double) currentRetry - 1.0) * 1000.0 + num));
    }
  }
}
