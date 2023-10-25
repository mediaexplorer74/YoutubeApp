﻿// Decompiled with JetBrains decompiler
// Type: Google.Apis.YouTube.v3.Data.LiveChatSuperChatDetails
// Assembly: Google.Apis.YouTube.v3, Version=1.29.2.1006, Culture=neutral, PublicKeyToken=4b01fa6e34db77ab
// MVID: 9E53E5C1-D9AB-4142-94A7-8A2435650008
// Assembly location: C:\Users\Admin\Desktop\re\YoutubeApp\Google.Apis.YouTube.v3.dll

using Google.Apis.Requests;
using Newtonsoft.Json;

namespace Google.Apis.YouTube.v3.Data
{
  public class LiveChatSuperChatDetails : IDirectResponseSchema
  {
    [JsonProperty("amountDisplayString")]
    public virtual string AmountDisplayString { get; set; }

    [JsonProperty("amountMicros")]
    public virtual ulong? AmountMicros { get; set; }

    [JsonProperty("currency")]
    public virtual string Currency { get; set; }

    [JsonProperty("tier")]
    public virtual long? Tier { get; set; }

    [JsonProperty("userComment")]
    public virtual string UserComment { get; set; }

    public virtual string ETag { get; set; }
  }
}
