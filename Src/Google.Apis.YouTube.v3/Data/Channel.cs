// Decompiled with JetBrains decompiler
// Type: Google.Apis.YouTube.v3.Data.Channel
// Assembly: Google.Apis.YouTube.v3, Version=1.29.2.1006, Culture=neutral, PublicKeyToken=4b01fa6e34db77ab
// MVID: 9E53E5C1-D9AB-4142-94A7-8A2435650008
// Assembly location: C:\Users\Admin\Desktop\re\YoutubeApp\Google.Apis.YouTube.v3.dll

using Google.Apis.Requests;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Google.Apis.YouTube.v3.Data
{
  public class Channel : IDirectResponseSchema
  {
    [JsonProperty("auditDetails")]
    public virtual ChannelAuditDetails AuditDetails { get; set; }

    [JsonProperty("brandingSettings")]
    public virtual ChannelBrandingSettings BrandingSettings { get; set; }

    [JsonProperty("contentDetails")]
    public virtual ChannelContentDetails ContentDetails { get; set; }

    [JsonProperty("contentOwnerDetails")]
    public virtual ChannelContentOwnerDetails ContentOwnerDetails { get; set; }

    [JsonProperty("conversionPings")]
    public virtual ChannelConversionPings ConversionPings { get; set; }

    [JsonProperty("etag")]
    public virtual string ETag { get; set; }

    [JsonProperty("id")]
    public virtual string Id { get; set; }

    [JsonProperty("invideoPromotion")]
    public virtual InvideoPromotion InvideoPromotion { get; set; }

    [JsonProperty("kind")]
    public virtual string Kind { get; set; }

    [JsonProperty("localizations")]
    public virtual IDictionary<string, ChannelLocalization> Localizations { get; set; }

    [JsonProperty("snippet")]
    public virtual ChannelSnippet Snippet { get; set; }

    [JsonProperty("statistics")]
    public virtual ChannelStatistics Statistics { get; set; }

    [JsonProperty("status")]
    public virtual ChannelStatus Status { get; set; }

    [JsonProperty("topicDetails")]
    public virtual ChannelTopicDetails TopicDetails { get; set; }
  }
}
