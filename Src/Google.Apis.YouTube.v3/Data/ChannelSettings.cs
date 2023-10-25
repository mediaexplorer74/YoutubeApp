// Decompiled with JetBrains decompiler
// Type: Google.Apis.YouTube.v3.Data.ChannelSettings
// Assembly: Google.Apis.YouTube.v3, Version=1.29.2.1006, Culture=neutral, PublicKeyToken=4b01fa6e34db77ab
// MVID: 9E53E5C1-D9AB-4142-94A7-8A2435650008
// Assembly location: C:\Users\Admin\Desktop\re\YoutubeApp\Google.Apis.YouTube.v3.dll

using Google.Apis.Requests;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Google.Apis.YouTube.v3.Data
{
  public class ChannelSettings : IDirectResponseSchema
  {
    [JsonProperty("country")]
    public virtual string Country { get; set; }

    [JsonProperty("defaultLanguage")]
    public virtual string DefaultLanguage { get; set; }

    [JsonProperty("defaultTab")]
    public virtual string DefaultTab { get; set; }

    [JsonProperty("description")]
    public virtual string Description { get; set; }

    [JsonProperty("featuredChannelsTitle")]
    public virtual string FeaturedChannelsTitle { get; set; }

    [JsonProperty("featuredChannelsUrls")]
    public virtual IList<string> FeaturedChannelsUrls { get; set; }

    [JsonProperty("keywords")]
    public virtual string Keywords { get; set; }

    [JsonProperty("moderateComments")]
    public virtual bool? ModerateComments { get; set; }

    [JsonProperty("profileColor")]
    public virtual string ProfileColor { get; set; }

    [JsonProperty("showBrowseView")]
    public virtual bool? ShowBrowseView { get; set; }

    [JsonProperty("showRelatedChannels")]
    public virtual bool? ShowRelatedChannels { get; set; }

    [JsonProperty("title")]
    public virtual string Title { get; set; }

    [JsonProperty("trackingAnalyticsAccountId")]
    public virtual string TrackingAnalyticsAccountId { get; set; }

    [JsonProperty("unsubscribedTrailer")]
    public virtual string UnsubscribedTrailer { get; set; }

    public virtual string ETag { get; set; }
  }
}
