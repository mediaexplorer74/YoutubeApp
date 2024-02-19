﻿// Decompiled with JetBrains decompiler
// Type: Google.Apis.YouTube.v3.Data.SubscriptionSubscriberSnippet
// Assembly: Google.Apis.YouTube.v3, Version=1.29.2.1006, Culture=neutral, PublicKeyToken=4b01fa6e34db77ab
// MVID: 9E53E5C1-D9AB-4142-94A7-8A2435650008
// Assembly location: C:\Users\Admin\Desktop\re\YoutubeApp\Google.Apis.YouTube.v3.dll

using Google.Apis.Requests;
using Newtonsoft.Json;

namespace Google.Apis.YouTube.v3.Data
{
  public class SubscriptionSubscriberSnippet : IDirectResponseSchema
  {
    [JsonProperty("channelId")]
    public virtual string ChannelId { get; set; }

    [JsonProperty("description")]
    public virtual string Description { get; set; }

    [JsonProperty("thumbnails")]
    public virtual ThumbnailDetails Thumbnails { get; set; }

    [JsonProperty("title")]
    public virtual string Title { get; set; }

    public virtual string ETag { get; set; }
  }
}