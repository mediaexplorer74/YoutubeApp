// Decompiled with JetBrains decompiler
// Type: Google.Apis.YouTube.v3.Data.VideoStatus
// Assembly: Google.Apis.YouTube.v3, Version=1.29.2.1006, Culture=neutral, PublicKeyToken=4b01fa6e34db77ab
// MVID: 9E53E5C1-D9AB-4142-94A7-8A2435650008
// Assembly location: C:\Users\Admin\Desktop\re\YoutubeApp\Google.Apis.YouTube.v3.dll

using Google.Apis.Requests;
using Google.Apis.Util;
using Newtonsoft.Json;
using System;

namespace Google.Apis.YouTube.v3.Data
{
  public class VideoStatus : IDirectResponseSchema
  {
    [JsonProperty("embeddable")]
    public virtual bool? Embeddable { get; set; }

    [JsonProperty("failureReason")]
    public virtual string FailureReason { get; set; }

    [JsonProperty("license")]
    public virtual string License { get; set; }

    [JsonProperty("privacyStatus")]
    public virtual string PrivacyStatus { get; set; }

    [JsonProperty("publicStatsViewable")]
    public virtual bool? PublicStatsViewable { get; set; }

    [JsonProperty("publishAt")]
    public virtual string PublishAtRaw { get; set; }

    [JsonIgnore]
    public virtual DateTime? PublishAt
    {
      get => Utilities.GetDateTimeFromString(this.PublishAtRaw);
      set => this.PublishAtRaw = Utilities.GetStringFromDateTime(value);
    }

    [JsonProperty("rejectionReason")]
    public virtual string RejectionReason { get; set; }

    [JsonProperty("uploadStatus")]
    public virtual string UploadStatus { get; set; }

    public virtual string ETag { get; set; }
  }
}
