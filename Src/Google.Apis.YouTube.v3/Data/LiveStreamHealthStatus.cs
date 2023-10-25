// Decompiled with JetBrains decompiler
// Type: Google.Apis.YouTube.v3.Data.LiveStreamHealthStatus
// Assembly: Google.Apis.YouTube.v3, Version=1.29.2.1006, Culture=neutral, PublicKeyToken=4b01fa6e34db77ab
// MVID: 9E53E5C1-D9AB-4142-94A7-8A2435650008
// Assembly location: C:\Users\Admin\Desktop\re\YoutubeApp\Google.Apis.YouTube.v3.dll

using Google.Apis.Requests;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Google.Apis.YouTube.v3.Data
{
  public class LiveStreamHealthStatus : IDirectResponseSchema
  {
    [JsonProperty("configurationIssues")]
    public virtual IList<LiveStreamConfigurationIssue> ConfigurationIssues { get; set; }

    [JsonProperty("lastUpdateTimeSeconds")]
    public virtual ulong? LastUpdateTimeSeconds { get; set; }

    [JsonProperty("status")]
    public virtual string Status { get; set; }

    public virtual string ETag { get; set; }
  }
}
