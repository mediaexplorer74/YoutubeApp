// Decompiled with JetBrains decompiler
// Type: Google.Apis.YouTube.v3.Data.ChannelSectionSnippet
// Assembly: Google.Apis.YouTube.v3, Version=1.29.2.1006, Culture=neutral, PublicKeyToken=4b01fa6e34db77ab
// MVID: 9E53E5C1-D9AB-4142-94A7-8A2435650008
// Assembly location: C:\Users\Admin\Desktop\re\YoutubeApp\Google.Apis.YouTube.v3.dll

using Google.Apis.Requests;
using Newtonsoft.Json;

namespace Google.Apis.YouTube.v3.Data
{
  public class ChannelSectionSnippet : IDirectResponseSchema
  {
    [JsonProperty("channelId")]
    public virtual string ChannelId { get; set; }

    [JsonProperty("defaultLanguage")]
    public virtual string DefaultLanguage { get; set; }

    [JsonProperty("localized")]
    public virtual ChannelSectionLocalization Localized { get; set; }

    [JsonProperty("position")]
    public virtual long? Position { get; set; }

    [JsonProperty("style")]
    public virtual string Style { get; set; }

    [JsonProperty("title")]
    public virtual string Title { get; set; }

    [JsonProperty("type")]
    public virtual string Type { get; set; }

    public virtual string ETag { get; set; }
  }
}
