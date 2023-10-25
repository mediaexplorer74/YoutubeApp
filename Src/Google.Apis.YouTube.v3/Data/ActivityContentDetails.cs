// Decompiled with JetBrains decompiler
// Type: Google.Apis.YouTube.v3.Data.ActivityContentDetails
// Assembly: Google.Apis.YouTube.v3, Version=1.29.2.1006, Culture=neutral, PublicKeyToken=4b01fa6e34db77ab
// MVID: 9E53E5C1-D9AB-4142-94A7-8A2435650008
// Assembly location: C:\Users\Admin\Desktop\re\YoutubeApp\Google.Apis.YouTube.v3.dll

using Google.Apis.Requests;
using Newtonsoft.Json;

namespace Google.Apis.YouTube.v3.Data
{
  public class ActivityContentDetails : IDirectResponseSchema
  {
    [JsonProperty("bulletin")]
    public virtual ActivityContentDetailsBulletin Bulletin { get; set; }

    [JsonProperty("channelItem")]
    public virtual ActivityContentDetailsChannelItem ChannelItem { get; set; }

    [JsonProperty("comment")]
    public virtual ActivityContentDetailsComment Comment { get; set; }

    [JsonProperty("favorite")]
    public virtual ActivityContentDetailsFavorite Favorite { get; set; }

    [JsonProperty("like")]
    public virtual ActivityContentDetailsLike Like { get; set; }

    [JsonProperty("playlistItem")]
    public virtual ActivityContentDetailsPlaylistItem PlaylistItem { get; set; }

    [JsonProperty("promotedItem")]
    public virtual ActivityContentDetailsPromotedItem PromotedItem { get; set; }

    [JsonProperty("recommendation")]
    public virtual ActivityContentDetailsRecommendation Recommendation { get; set; }

    [JsonProperty("social")]
    public virtual ActivityContentDetailsSocial Social { get; set; }

    [JsonProperty("subscription")]
    public virtual ActivityContentDetailsSubscription Subscription { get; set; }

    [JsonProperty("upload")]
    public virtual ActivityContentDetailsUpload Upload { get; set; }

    public virtual string ETag { get; set; }
  }
}
