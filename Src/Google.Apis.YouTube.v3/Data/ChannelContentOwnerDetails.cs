// Decompiled with JetBrains decompiler
// Type: Google.Apis.YouTube.v3.Data.ChannelContentOwnerDetails
// Assembly: Google.Apis.YouTube.v3, Version=1.29.2.1006, Culture=neutral, PublicKeyToken=4b01fa6e34db77ab
// MVID: 9E53E5C1-D9AB-4142-94A7-8A2435650008
// Assembly location: C:\Users\Admin\Desktop\re\YoutubeApp\Google.Apis.YouTube.v3.dll

using Google.Apis.Requests;
using Google.Apis.Util;
using Newtonsoft.Json;
using System;

namespace Google.Apis.YouTube.v3.Data
{
  public class ChannelContentOwnerDetails : IDirectResponseSchema
  {
    [JsonProperty("contentOwner")]
    public virtual string ContentOwner { get; set; }

    [JsonProperty("timeLinked")]
    public virtual string TimeLinkedRaw { get; set; }

    [JsonIgnore]
    public virtual DateTime? TimeLinked
    {
      get => Utilities.GetDateTimeFromString(this.TimeLinkedRaw);
      set => this.TimeLinkedRaw = Utilities.GetStringFromDateTime(value);
    }

    public virtual string ETag { get; set; }
  }
}
