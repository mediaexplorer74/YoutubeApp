// Decompiled with JetBrains decompiler
// Type: Google.Apis.YouTube.v3.Data.LiveChatMessageAuthorDetails
// Assembly: Google.Apis.YouTube.v3, Version=1.29.2.1006, Culture=neutral, PublicKeyToken=4b01fa6e34db77ab
// MVID: 9E53E5C1-D9AB-4142-94A7-8A2435650008
// Assembly location: C:\Users\Admin\Desktop\re\YoutubeApp\Google.Apis.YouTube.v3.dll

using Google.Apis.Requests;
using Newtonsoft.Json;

namespace Google.Apis.YouTube.v3.Data
{
  public class LiveChatMessageAuthorDetails : IDirectResponseSchema
  {
    [JsonProperty("channelId")]
    public virtual string ChannelId { get; set; }

    [JsonProperty("channelUrl")]
    public virtual string ChannelUrl { get; set; }

    [JsonProperty("displayName")]
    public virtual string DisplayName { get; set; }

    [JsonProperty("isChatModerator")]
    public virtual bool? IsChatModerator { get; set; }

    [JsonProperty("isChatOwner")]
    public virtual bool? IsChatOwner { get; set; }

    [JsonProperty("isChatSponsor")]
    public virtual bool? IsChatSponsor { get; set; }

    [JsonProperty("isVerified")]
    public virtual bool? IsVerified { get; set; }

    [JsonProperty("profileImageUrl")]
    public virtual string ProfileImageUrl { get; set; }

    public virtual string ETag { get; set; }
  }
}
