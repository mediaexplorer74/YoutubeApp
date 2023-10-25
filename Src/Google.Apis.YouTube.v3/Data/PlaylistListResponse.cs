// Decompiled with JetBrains decompiler
// Type: Google.Apis.YouTube.v3.Data.PlaylistListResponse
// Assembly: Google.Apis.YouTube.v3, Version=1.29.2.1006, Culture=neutral, PublicKeyToken=4b01fa6e34db77ab
// MVID: 9E53E5C1-D9AB-4142-94A7-8A2435650008
// Assembly location: C:\Users\Admin\Desktop\re\YoutubeApp\Google.Apis.YouTube.v3.dll

using Google.Apis.Requests;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Google.Apis.YouTube.v3.Data
{
  public class PlaylistListResponse : IDirectResponseSchema
  {
    [JsonProperty("etag")]
    public virtual string ETag { get; set; }

    [JsonProperty("eventId")]
    public virtual string EventId { get; set; }

    [JsonProperty("items")]
    public virtual IList<Playlist> Items { get; set; }

    [JsonProperty("kind")]
    public virtual string Kind { get; set; }

    [JsonProperty("nextPageToken")]
    public virtual string NextPageToken { get; set; }

    [JsonProperty("pageInfo")]
    public virtual PageInfo PageInfo { get; set; }

    [JsonProperty("prevPageToken")]
    public virtual string PrevPageToken { get; set; }

    [JsonProperty("tokenPagination")]
    public virtual TokenPagination TokenPagination { get; set; }

    [JsonProperty("visitorId")]
    public virtual string VisitorId { get; set; }
  }
}
