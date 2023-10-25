// Decompiled with JetBrains decompiler
// Type: Google.Apis.YouTube.v3.Data.VideoStatistics
// Assembly: Google.Apis.YouTube.v3, Version=1.29.2.1006, Culture=neutral, PublicKeyToken=4b01fa6e34db77ab
// MVID: 9E53E5C1-D9AB-4142-94A7-8A2435650008
// Assembly location: C:\Users\Admin\Desktop\re\YoutubeApp\Google.Apis.YouTube.v3.dll

using Google.Apis.Requests;
using Newtonsoft.Json;

namespace Google.Apis.YouTube.v3.Data
{
  public class VideoStatistics : IDirectResponseSchema
  {
    [JsonProperty("commentCount")]
    public virtual ulong? CommentCount { get; set; }

    [JsonProperty("dislikeCount")]
    public virtual ulong? DislikeCount { get; set; }

    [JsonProperty("favoriteCount")]
    public virtual ulong? FavoriteCount { get; set; }

    [JsonProperty("likeCount")]
    public virtual ulong? LikeCount { get; set; }

    [JsonProperty("viewCount")]
    public virtual ulong? ViewCount { get; set; }

    public virtual string ETag { get; set; }
  }
}
