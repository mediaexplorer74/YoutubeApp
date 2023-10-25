// Decompiled with JetBrains decompiler
// Type: Google.Apis.YouTube.v3.Data.Playlist
// Assembly: Google.Apis.YouTube.v3, Version=1.29.2.1006, Culture=neutral, PublicKeyToken=4b01fa6e34db77ab
// MVID: 9E53E5C1-D9AB-4142-94A7-8A2435650008
// Assembly location: C:\Users\Admin\Desktop\re\YoutubeApp\Google.Apis.YouTube.v3.dll

using Google.Apis.Requests;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Google.Apis.YouTube.v3.Data
{
  public class Playlist : IDirectResponseSchema
  {
    [JsonProperty("contentDetails")]
    public virtual PlaylistContentDetails ContentDetails { get; set; }

    [JsonProperty("etag")]
    public virtual string ETag { get; set; }

    [JsonProperty("id")]
    public virtual string Id { get; set; }

    [JsonProperty("kind")]
    public virtual string Kind { get; set; }

    [JsonProperty("localizations")]
    public virtual IDictionary<string, PlaylistLocalization> Localizations { get; set; }

    [JsonProperty("player")]
    public virtual PlaylistPlayer Player { get; set; }

    [JsonProperty("snippet")]
    public virtual PlaylistSnippet Snippet { get; set; }

    [JsonProperty("status")]
    public virtual PlaylistStatus Status { get; set; }
  }
}
