// Decompiled with JetBrains decompiler
// Type: Google.Apis.YouTube.v3.Data.VideoContentDetails
// Assembly: Google.Apis.YouTube.v3, Version=1.29.2.1006, Culture=neutral, PublicKeyToken=4b01fa6e34db77ab
// MVID: 9E53E5C1-D9AB-4142-94A7-8A2435650008
// Assembly location: C:\Users\Admin\Desktop\re\YoutubeApp\Google.Apis.YouTube.v3.dll

using Google.Apis.Requests;
using Newtonsoft.Json;

namespace Google.Apis.YouTube.v3.Data
{
  public class VideoContentDetails : IDirectResponseSchema
  {
    [JsonProperty("caption")]
    public virtual string Caption { get; set; }

    [JsonProperty("contentRating")]
    public virtual ContentRating ContentRating { get; set; }

    [JsonProperty("countryRestriction")]
    public virtual AccessPolicy CountryRestriction { get; set; }

    [JsonProperty("definition")]
    public virtual string Definition { get; set; }

    [JsonProperty("dimension")]
    public virtual string Dimension { get; set; }

    [JsonProperty("duration")]
    public virtual string Duration { get; set; }

    [JsonProperty("hasCustomThumbnail")]
    public virtual bool? HasCustomThumbnail { get; set; }

    [JsonProperty("licensedContent")]
    public virtual bool? LicensedContent { get; set; }

    [JsonProperty("projection")]
    public virtual string Projection { get; set; }

    [JsonProperty("regionRestriction")]
    public virtual VideoContentDetailsRegionRestriction RegionRestriction { get; set; }

    public virtual string ETag { get; set; }
  }
}
