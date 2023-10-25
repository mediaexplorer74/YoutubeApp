// Decompiled with JetBrains decompiler
// Type: Google.Apis.YouTube.v3.Data.SuperChatEventSnippet
// Assembly: Google.Apis.YouTube.v3, Version=1.29.2.1006, Culture=neutral, PublicKeyToken=4b01fa6e34db77ab
// MVID: 9E53E5C1-D9AB-4142-94A7-8A2435650008
// Assembly location: C:\Users\Admin\Desktop\re\YoutubeApp\Google.Apis.YouTube.v3.dll

using Google.Apis.Requests;
using Google.Apis.Util;
using Newtonsoft.Json;
using System;

namespace Google.Apis.YouTube.v3.Data
{
  public class SuperChatEventSnippet : IDirectResponseSchema
  {
    [JsonProperty("amountMicros")]
    public virtual ulong? AmountMicros { get; set; }

    [JsonProperty("channelId")]
    public virtual string ChannelId { get; set; }

    [JsonProperty("commentText")]
    public virtual string CommentText { get; set; }

    [JsonProperty("createdAt")]
    public virtual string CreatedAtRaw { get; set; }

    [JsonIgnore]
    public virtual DateTime? CreatedAt
    {
      get => Utilities.GetDateTimeFromString(this.CreatedAtRaw);
      set => this.CreatedAtRaw = Utilities.GetStringFromDateTime(value);
    }

    [JsonProperty("currency")]
    public virtual string Currency { get; set; }

    [JsonProperty("displayString")]
    public virtual string DisplayString { get; set; }

    [JsonProperty("messageType")]
    public virtual long? MessageType { get; set; }

    [JsonProperty("supporterDetails")]
    public virtual ChannelProfileDetails SupporterDetails { get; set; }

    public virtual string ETag { get; set; }
  }
}
