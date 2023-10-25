// Decompiled with JetBrains decompiler
// Type: Google.Apis.YouTube.v3.Data.VideoRecordingDetails
// Assembly: Google.Apis.YouTube.v3, Version=1.29.2.1006, Culture=neutral, PublicKeyToken=4b01fa6e34db77ab
// MVID: 9E53E5C1-D9AB-4142-94A7-8A2435650008
// Assembly location: C:\Users\Admin\Desktop\re\YoutubeApp\Google.Apis.YouTube.v3.dll

using Google.Apis.Requests;
using Google.Apis.Util;
using Newtonsoft.Json;
using System;

namespace Google.Apis.YouTube.v3.Data
{
  public class VideoRecordingDetails : IDirectResponseSchema
  {
    [JsonProperty("location")]
    public virtual GeoPoint Location { get; set; }

    [JsonProperty("locationDescription")]
    public virtual string LocationDescription { get; set; }

    [JsonProperty("recordingDate")]
    public virtual string RecordingDateRaw { get; set; }

    [JsonIgnore]
    public virtual DateTime? RecordingDate
    {
      get => Utilities.GetDateTimeFromString(this.RecordingDateRaw);
      set => this.RecordingDateRaw = Utilities.GetStringFromDateTime(value);
    }

    public virtual string ETag { get; set; }
  }
}
