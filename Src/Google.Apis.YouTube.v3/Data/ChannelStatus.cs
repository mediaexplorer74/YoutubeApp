// Decompiled with JetBrains decompiler
// Type: Google.Apis.YouTube.v3.Data.ChannelStatus
// Assembly: Google.Apis.YouTube.v3, Version=1.29.2.1006, Culture=neutral, PublicKeyToken=4b01fa6e34db77ab
// MVID: 9E53E5C1-D9AB-4142-94A7-8A2435650008
// Assembly location: C:\Users\Admin\Desktop\re\YoutubeApp\Google.Apis.YouTube.v3.dll

using Google.Apis.Requests;
using Newtonsoft.Json;

namespace Google.Apis.YouTube.v3.Data
{
  public class ChannelStatus : IDirectResponseSchema
  {
    [JsonProperty("isLinked")]
    public virtual bool? IsLinked { get; set; }

    [JsonProperty("longUploadsStatus")]
    public virtual string LongUploadsStatus { get; set; }

    [JsonProperty("privacyStatus")]
    public virtual string PrivacyStatus { get; set; }

    public virtual string ETag { get; set; }
  }
}
