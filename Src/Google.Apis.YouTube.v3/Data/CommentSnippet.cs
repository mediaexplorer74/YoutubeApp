// Decompiled with JetBrains decompiler
// Type: Google.Apis.YouTube.v3.Data.CommentSnippet
// Assembly: Google.Apis.YouTube.v3, Version=1.29.2.1006, Culture=neutral, PublicKeyToken=4b01fa6e34db77ab
// MVID: 9E53E5C1-D9AB-4142-94A7-8A2435650008
// Assembly location: C:\Users\Admin\Desktop\re\YoutubeApp\Google.Apis.YouTube.v3.dll

using Google.Apis.Requests;
using Google.Apis.Util;
using Newtonsoft.Json;
using System;

namespace Google.Apis.YouTube.v3.Data
{
  public class CommentSnippet : IDirectResponseSchema
  {
    [JsonProperty("authorChannelId")]
    public virtual object AuthorChannelId { get; set; }

    [JsonProperty("authorChannelUrl")]
    public virtual string AuthorChannelUrl { get; set; }

    [JsonProperty("authorDisplayName")]
    public virtual string AuthorDisplayName { get; set; }

    [JsonProperty("authorProfileImageUrl")]
    public virtual string AuthorProfileImageUrl { get; set; }

    [JsonProperty("canRate")]
    public virtual bool? CanRate { get; set; }

    [JsonProperty("channelId")]
    public virtual string ChannelId { get; set; }

    [JsonProperty("likeCount")]
    public virtual long? LikeCount { get; set; }

    [JsonProperty("moderationStatus")]
    public virtual string ModerationStatus { get; set; }

    [JsonProperty("parentId")]
    public virtual string ParentId { get; set; }

    [JsonProperty("publishedAt")]
    public virtual string PublishedAtRaw { get; set; }

    [JsonIgnore]
    public virtual DateTime? PublishedAt
    {
      get => Utilities.GetDateTimeFromString(this.PublishedAtRaw);
      set => this.PublishedAtRaw = Utilities.GetStringFromDateTime(value);
    }

    [JsonProperty("textDisplay")]
    public virtual string TextDisplay { get; set; }

    [JsonProperty("textOriginal")]
    public virtual string TextOriginal { get; set; }

    [JsonProperty("updatedAt")]
    public virtual string UpdatedAtRaw { get; set; }

    [JsonIgnore]
    public virtual DateTime? UpdatedAt
    {
      get => Utilities.GetDateTimeFromString(this.UpdatedAtRaw);
      set => this.UpdatedAtRaw = Utilities.GetStringFromDateTime(value);
    }

    [JsonProperty("videoId")]
    public virtual string VideoId { get; set; }

    [JsonProperty("viewerRating")]
    public virtual string ViewerRating { get; set; }

    public virtual string ETag { get; set; }
  }
}
