// Decompiled with JetBrains decompiler
// Type: Google.Apis.YouTube.v3.Data.PlaylistSnippet
// Assembly: Google.Apis.YouTube.v3, Version=1.29.2.1006, Culture=neutral, PublicKeyToken=4b01fa6e34db77ab
// MVID: 9E53E5C1-D9AB-4142-94A7-8A2435650008
// Assembly location: C:\Users\Admin\Desktop\re\YoutubeApp\Google.Apis.YouTube.v3.dll

using Google.Apis.Requests;
using Google.Apis.Util;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Google.Apis.YouTube.v3.Data
{
  public class PlaylistSnippet : IDirectResponseSchema
  {
    [JsonProperty("channelId")]
    public virtual string ChannelId { get; set; }

    [JsonProperty("channelTitle")]
    public virtual string ChannelTitle { get; set; }

    [JsonProperty("defaultLanguage")]
    public virtual string DefaultLanguage { get; set; }

    [JsonProperty("description")]
    public virtual string Description { get; set; }

    [JsonProperty("localized")]
    public virtual PlaylistLocalization Localized { get; set; }

    [JsonProperty("publishedAt")]
    public virtual string PublishedAtRaw { get; set; }

    [JsonIgnore]
    public virtual DateTime? PublishedAt
    {
      get => Utilities.GetDateTimeFromString(this.PublishedAtRaw);
      set => this.PublishedAtRaw = Utilities.GetStringFromDateTime(value);
    }

    [JsonProperty("tags")]
    public virtual IList<string> Tags { get; set; }

    [JsonProperty("thumbnails")]
    public virtual ThumbnailDetails Thumbnails { get; set; }

    [JsonProperty("title")]
    public virtual string Title { get; set; }

    public virtual string ETag { get; set; }
  }
}
