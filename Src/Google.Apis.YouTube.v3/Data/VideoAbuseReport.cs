﻿// Decompiled with JetBrains decompiler
// Type: Google.Apis.YouTube.v3.Data.VideoAbuseReport
// Assembly: Google.Apis.YouTube.v3, Version=1.29.2.1006, Culture=neutral, PublicKeyToken=4b01fa6e34db77ab
// MVID: 9E53E5C1-D9AB-4142-94A7-8A2435650008
// Assembly location: C:\Users\Admin\Desktop\re\YoutubeApp\Google.Apis.YouTube.v3.dll

using Google.Apis.Requests;
using Newtonsoft.Json;

namespace Google.Apis.YouTube.v3.Data
{
  public class VideoAbuseReport : IDirectResponseSchema
  {
    [JsonProperty("comments")]
    public virtual string Comments { get; set; }

    [JsonProperty("language")]
    public virtual string Language { get; set; }

    [JsonProperty("reasonId")]
    public virtual string ReasonId { get; set; }

    [JsonProperty("secondaryReasonId")]
    public virtual string SecondaryReasonId { get; set; }

    [JsonProperty("videoId")]
    public virtual string VideoId { get; set; }

    public virtual string ETag { get; set; }
  }
}
