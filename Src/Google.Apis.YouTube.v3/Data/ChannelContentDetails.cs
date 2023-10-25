// Decompiled with JetBrains decompiler
// Type: Google.Apis.YouTube.v3.Data.ChannelContentDetails
// Assembly: Google.Apis.YouTube.v3, Version=1.29.2.1006, Culture=neutral, PublicKeyToken=4b01fa6e34db77ab
// MVID: 9E53E5C1-D9AB-4142-94A7-8A2435650008
// Assembly location: C:\Users\Admin\Desktop\re\YoutubeApp\Google.Apis.YouTube.v3.dll

using Google.Apis.Requests;
using Newtonsoft.Json;

namespace Google.Apis.YouTube.v3.Data
{
  public class ChannelContentDetails : IDirectResponseSchema
  {
    [JsonProperty("relatedPlaylists")]
    public virtual ChannelContentDetails.RelatedPlaylistsData RelatedPlaylists { get; set; }

    public virtual string ETag { get; set; }

    public class RelatedPlaylistsData
    {
      [JsonProperty("favorites")]
      public virtual string Favorites { get; set; }

      [JsonProperty("likes")]
      public virtual string Likes { get; set; }

      [JsonProperty("uploads")]
      public virtual string Uploads { get; set; }

      [JsonProperty("watchHistory")]
      public virtual string WatchHistory { get; set; }

      [JsonProperty("watchLater")]
      public virtual string WatchLater { get; set; }
    }
  }
}
