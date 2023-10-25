// Decompiled with JetBrains decompiler
// Type: Google.Apis.YouTube.v3.Data.PlaylistItemContentDetails
// Assembly: Google.Apis.YouTube.v3, Version=1.29.2.1006, Culture=neutral, PublicKeyToken=4b01fa6e34db77ab
// MVID: 9E53E5C1-D9AB-4142-94A7-8A2435650008
// Assembly location: C:\Users\Admin\Desktop\re\YoutubeApp\Google.Apis.YouTube.v3.dll

using Google.Apis.Requests;
using Google.Apis.Util;
using Newtonsoft.Json;
using System;

namespace Google.Apis.YouTube.v3.Data
{
  public class PlaylistItemContentDetails : IDirectResponseSchema
  {
    [JsonProperty("endAt")]
    public virtual string EndAt { get; set; }

    [JsonProperty("note")]
    public virtual string Note { get; set; }

    [JsonProperty("startAt")]
    public virtual string StartAt { get; set; }

    [JsonProperty("videoId")]
    public virtual string VideoId { get; set; }

    [JsonProperty("videoPublishedAt")]
    public virtual string VideoPublishedAtRaw { get; set; }

    [JsonIgnore]
    public virtual DateTime? VideoPublishedAt
    {
      get => Utilities.GetDateTimeFromString(this.VideoPublishedAtRaw);
      set => this.VideoPublishedAtRaw = Utilities.GetStringFromDateTime(value);
    }

    public virtual string ETag { get; set; }
  }
}
