// Decompiled with JetBrains decompiler
// Type: Google.Apis.YouTube.v3.Data.LiveChatMessageSnippet
// Assembly: Google.Apis.YouTube.v3, Version=1.29.2.1006, Culture=neutral, PublicKeyToken=4b01fa6e34db77ab
// MVID: 9E53E5C1-D9AB-4142-94A7-8A2435650008
// Assembly location: C:\Users\Admin\Desktop\re\YoutubeApp\Google.Apis.YouTube.v3.dll

using Google.Apis.Requests;
using Google.Apis.Util;
using Newtonsoft.Json;
using System;

namespace Google.Apis.YouTube.v3.Data
{
  public class LiveChatMessageSnippet : IDirectResponseSchema
  {
    [JsonProperty("authorChannelId")]
    public virtual string AuthorChannelId { get; set; }

    [JsonProperty("displayMessage")]
    public virtual string DisplayMessage { get; set; }

    [JsonProperty("fanFundingEventDetails")]
    public virtual LiveChatFanFundingEventDetails FanFundingEventDetails { get; set; }

    [JsonProperty("hasDisplayContent")]
    public virtual bool? HasDisplayContent { get; set; }

    [JsonProperty("liveChatId")]
    public virtual string LiveChatId { get; set; }

    [JsonProperty("messageDeletedDetails")]
    public virtual LiveChatMessageDeletedDetails MessageDeletedDetails { get; set; }

    [JsonProperty("messageRetractedDetails")]
    public virtual LiveChatMessageRetractedDetails MessageRetractedDetails { get; set; }

    [JsonProperty("pollClosedDetails")]
    public virtual LiveChatPollClosedDetails PollClosedDetails { get; set; }

    [JsonProperty("pollEditedDetails")]
    public virtual LiveChatPollEditedDetails PollEditedDetails { get; set; }

    [JsonProperty("pollOpenedDetails")]
    public virtual LiveChatPollOpenedDetails PollOpenedDetails { get; set; }

    [JsonProperty("pollVotedDetails")]
    public virtual LiveChatPollVotedDetails PollVotedDetails { get; set; }

    [JsonProperty("publishedAt")]
    public virtual string PublishedAtRaw { get; set; }

    [JsonIgnore]
    public virtual DateTime? PublishedAt
    {
      get => Utilities.GetDateTimeFromString(this.PublishedAtRaw);
      set => this.PublishedAtRaw = Utilities.GetStringFromDateTime(value);
    }

    [JsonProperty("superChatDetails")]
    public virtual LiveChatSuperChatDetails SuperChatDetails { get; set; }

    [JsonProperty("textMessageDetails")]
    public virtual LiveChatTextMessageDetails TextMessageDetails { get; set; }

    [JsonProperty("type")]
    public virtual string Type { get; set; }

    [JsonProperty("userBannedDetails")]
    public virtual LiveChatUserBannedMessageDetails UserBannedDetails { get; set; }

    public virtual string ETag { get; set; }
  }
}
