// Decompiled with JetBrains decompiler
// Type: Google.Apis.YouTube.v3.Data.LiveBroadcastSnippet
// Assembly: Google.Apis.YouTube.v3, Version=1.29.2.1006, Culture=neutral, PublicKeyToken=4b01fa6e34db77ab
// MVID: 9E53E5C1-D9AB-4142-94A7-8A2435650008
// Assembly location: C:\Users\Admin\Desktop\re\YoutubeApp\Google.Apis.YouTube.v3.dll

using Google.Apis.Requests;
using Google.Apis.Util;
using Newtonsoft.Json;
using System;

namespace Google.Apis.YouTube.v3.Data
{
  public class LiveBroadcastSnippet : IDirectResponseSchema
  {
    [JsonProperty("actualEndTime")]
    public virtual string ActualEndTimeRaw { get; set; }

    [JsonIgnore]
    public virtual DateTime? ActualEndTime
    {
      get => Utilities.GetDateTimeFromString(this.ActualEndTimeRaw);
      set => this.ActualEndTimeRaw = Utilities.GetStringFromDateTime(value);
    }

    [JsonProperty("actualStartTime")]
    public virtual string ActualStartTimeRaw { get; set; }

    [JsonIgnore]
    public virtual DateTime? ActualStartTime
    {
      get => Utilities.GetDateTimeFromString(this.ActualStartTimeRaw);
      set => this.ActualStartTimeRaw = Utilities.GetStringFromDateTime(value);
    }

    [JsonProperty("channelId")]
    public virtual string ChannelId { get; set; }

    [JsonProperty("description")]
    public virtual string Description { get; set; }

    [JsonProperty("isDefaultBroadcast")]
    public virtual bool? IsDefaultBroadcast { get; set; }

    [JsonProperty("liveChatId")]
    public virtual string LiveChatId { get; set; }

    [JsonProperty("publishedAt")]
    public virtual string PublishedAtRaw { get; set; }

    [JsonIgnore]
    public virtual DateTime? PublishedAt
    {
      get => Utilities.GetDateTimeFromString(this.PublishedAtRaw);
      set => this.PublishedAtRaw = Utilities.GetStringFromDateTime(value);
    }

    [JsonProperty("scheduledEndTime")]
    public virtual string ScheduledEndTimeRaw { get; set; }

    [JsonIgnore]
    public virtual DateTime? ScheduledEndTime
    {
      get => Utilities.GetDateTimeFromString(this.ScheduledEndTimeRaw);
      set => this.ScheduledEndTimeRaw = Utilities.GetStringFromDateTime(value);
    }

    [JsonProperty("scheduledStartTime")]
    public virtual string ScheduledStartTimeRaw { get; set; }

    [JsonIgnore]
    public virtual DateTime? ScheduledStartTime
    {
      get => Utilities.GetDateTimeFromString(this.ScheduledStartTimeRaw);
      set => this.ScheduledStartTimeRaw = Utilities.GetStringFromDateTime(value);
    }

    [JsonProperty("thumbnails")]
    public virtual ThumbnailDetails Thumbnails { get; set; }

    [JsonProperty("title")]
    public virtual string Title { get; set; }

    public virtual string ETag { get; set; }
  }
}
