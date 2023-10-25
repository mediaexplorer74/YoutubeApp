// Decompiled with JetBrains decompiler
// Type: Google.Apis.YouTube.v3.Data.ActivityContentDetailsPromotedItem
// Assembly: Google.Apis.YouTube.v3, Version=1.29.2.1006, Culture=neutral, PublicKeyToken=4b01fa6e34db77ab
// MVID: 9E53E5C1-D9AB-4142-94A7-8A2435650008
// Assembly location: C:\Users\Admin\Desktop\re\YoutubeApp\Google.Apis.YouTube.v3.dll

using Google.Apis.Requests;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Google.Apis.YouTube.v3.Data
{
  public class ActivityContentDetailsPromotedItem : IDirectResponseSchema
  {
    [JsonProperty("adTag")]
    public virtual string AdTag { get; set; }

    [JsonProperty("clickTrackingUrl")]
    public virtual string ClickTrackingUrl { get; set; }

    [JsonProperty("creativeViewUrl")]
    public virtual string CreativeViewUrl { get; set; }

    [JsonProperty("ctaType")]
    public virtual string CtaType { get; set; }

    [JsonProperty("customCtaButtonText")]
    public virtual string CustomCtaButtonText { get; set; }

    [JsonProperty("descriptionText")]
    public virtual string DescriptionText { get; set; }

    [JsonProperty("destinationUrl")]
    public virtual string DestinationUrl { get; set; }

    [JsonProperty("forecastingUrl")]
    public virtual IList<string> ForecastingUrl { get; set; }

    [JsonProperty("impressionUrl")]
    public virtual IList<string> ImpressionUrl { get; set; }

    [JsonProperty("videoId")]
    public virtual string VideoId { get; set; }

    public virtual string ETag { get; set; }
  }
}
