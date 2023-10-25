// Decompiled with JetBrains decompiler
// Type: Google.Apis.YouTube.v3.Data.LiveBroadcastContentDetails
// Assembly: Google.Apis.YouTube.v3, Version=1.29.2.1006, Culture=neutral, PublicKeyToken=4b01fa6e34db77ab
// MVID: 9E53E5C1-D9AB-4142-94A7-8A2435650008
// Assembly location: C:\Users\Admin\Desktop\re\YoutubeApp\Google.Apis.YouTube.v3.dll

using Google.Apis.Requests;
using Google.Apis.Util;
using Newtonsoft.Json;
using System;

namespace Google.Apis.YouTube.v3.Data
{
  public class LiveBroadcastContentDetails : IDirectResponseSchema
  {
    [JsonProperty("boundStreamId")]
    public virtual string BoundStreamId { get; set; }

    [JsonProperty("boundStreamLastUpdateTimeMs")]
    public virtual string BoundStreamLastUpdateTimeMsRaw { get; set; }

    [JsonIgnore]
    public virtual DateTime? BoundStreamLastUpdateTimeMs
    {
      get => Utilities.GetDateTimeFromString(this.BoundStreamLastUpdateTimeMsRaw);
      set => this.BoundStreamLastUpdateTimeMsRaw = Utilities.GetStringFromDateTime(value);
    }

    [JsonProperty("closedCaptionsType")]
    public virtual string ClosedCaptionsType { get; set; }

    [JsonProperty("enableClosedCaptions")]
    public virtual bool? EnableClosedCaptions { get; set; }

    [JsonProperty("enableContentEncryption")]
    public virtual bool? EnableContentEncryption { get; set; }

    [JsonProperty("enableDvr")]
    public virtual bool? EnableDvr { get; set; }

    [JsonProperty("enableEmbed")]
    public virtual bool? EnableEmbed { get; set; }

    [JsonProperty("enableLowLatency")]
    public virtual bool? EnableLowLatency { get; set; }

    [JsonProperty("latencyPreference")]
    public virtual string LatencyPreference { get; set; }

    [JsonProperty("mesh")]
    public virtual string Mesh { get; set; }

    [JsonProperty("monitorStream")]
    public virtual MonitorStreamInfo MonitorStream { get; set; }

    [JsonProperty("projection")]
    public virtual string Projection { get; set; }

    [JsonProperty("recordFromStart")]
    public virtual bool? RecordFromStart { get; set; }

    [JsonProperty("startWithSlate")]
    public virtual bool? StartWithSlate { get; set; }

    public virtual string ETag { get; set; }
  }
}
