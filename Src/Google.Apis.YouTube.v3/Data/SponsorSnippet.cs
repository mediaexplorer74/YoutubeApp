// Decompiled with JetBrains decompiler
// Type: Google.Apis.YouTube.v3.Data.SponsorSnippet
// Assembly: Google.Apis.YouTube.v3, Version=1.29.2.1006, Culture=neutral, PublicKeyToken=4b01fa6e34db77ab
// MVID: 9E53E5C1-D9AB-4142-94A7-8A2435650008
// Assembly location: C:\Users\Admin\Desktop\re\YoutubeApp\Google.Apis.YouTube.v3.dll

using Google.Apis.Requests;
using Google.Apis.Util;
using Newtonsoft.Json;
using System;

namespace Google.Apis.YouTube.v3.Data
{
  public class SponsorSnippet : IDirectResponseSchema
  {
    [JsonProperty("channelId")]
    public virtual string ChannelId { get; set; }

    [JsonProperty("sponsorDetails")]
    public virtual ChannelProfileDetails SponsorDetails { get; set; }

    [JsonProperty("sponsorSince")]
    public virtual string SponsorSinceRaw { get; set; }

    [JsonIgnore]
    public virtual DateTime? SponsorSince
    {
      get => Utilities.GetDateTimeFromString(this.SponsorSinceRaw);
      set => this.SponsorSinceRaw = Utilities.GetStringFromDateTime(value);
    }

    public virtual string ETag { get; set; }
  }
}
