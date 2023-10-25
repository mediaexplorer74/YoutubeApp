// Decompiled with JetBrains decompiler
// Type: Google.Apis.YouTube.v3.Data.VideoCategorySnippet
// Assembly: Google.Apis.YouTube.v3, Version=1.29.2.1006, Culture=neutral, PublicKeyToken=4b01fa6e34db77ab
// MVID: 9E53E5C1-D9AB-4142-94A7-8A2435650008
// Assembly location: C:\Users\Admin\Desktop\re\YoutubeApp\Google.Apis.YouTube.v3.dll

using Google.Apis.Requests;
using Newtonsoft.Json;

namespace Google.Apis.YouTube.v3.Data
{
  public class VideoCategorySnippet : IDirectResponseSchema
  {
    [JsonProperty("assignable")]
    public virtual bool? Assignable { get; set; }

    [JsonProperty("channelId")]
    public virtual string ChannelId { get; set; }

    [JsonProperty("title")]
    public virtual string Title { get; set; }

    public virtual string ETag { get; set; }
  }
}
