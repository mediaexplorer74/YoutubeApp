// Decompiled with JetBrains decompiler
// Type: Google.Apis.YouTube.v3.Data.CaptionSnippet
// Assembly: Google.Apis.YouTube.v3, Version=1.29.2.1006, Culture=neutral, PublicKeyToken=4b01fa6e34db77ab
// MVID: 9E53E5C1-D9AB-4142-94A7-8A2435650008
// Assembly location: C:\Users\Admin\Desktop\re\YoutubeApp\Google.Apis.YouTube.v3.dll

using Google.Apis.Requests;
using Google.Apis.Util;
using Newtonsoft.Json;
using System;

namespace Google.Apis.YouTube.v3.Data
{
  public class CaptionSnippet : IDirectResponseSchema
  {
    [JsonProperty("audioTrackType")]
    public virtual string AudioTrackType { get; set; }

    [JsonProperty("failureReason")]
    public virtual string FailureReason { get; set; }

    [JsonProperty("isAutoSynced")]
    public virtual bool? IsAutoSynced { get; set; }

    [JsonProperty("isCC")]
    public virtual bool? IsCC { get; set; }

    [JsonProperty("isDraft")]
    public virtual bool? IsDraft { get; set; }

    [JsonProperty("isEasyReader")]
    public virtual bool? IsEasyReader { get; set; }

    [JsonProperty("isLarge")]
    public virtual bool? IsLarge { get; set; }

    [JsonProperty("language")]
    public virtual string Language { get; set; }

    [JsonProperty("lastUpdated")]
    public virtual string LastUpdatedRaw { get; set; }

    [JsonIgnore]
    public virtual DateTime? LastUpdated
    {
      get => Utilities.GetDateTimeFromString(this.LastUpdatedRaw);
      set => this.LastUpdatedRaw = Utilities.GetStringFromDateTime(value);
    }

    [JsonProperty("name")]
    public virtual string Name { get; set; }

    [JsonProperty("status")]
    public virtual string Status { get; set; }

    [JsonProperty("trackKind")]
    public virtual string TrackKind { get; set; }

    [JsonProperty("videoId")]
    public virtual string VideoId { get; set; }

    public virtual string ETag { get; set; }
  }
}
