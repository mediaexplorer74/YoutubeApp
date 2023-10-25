// Decompiled with JetBrains decompiler
// Type: Google.Apis.YouTube.v3.Data.SubscriptionContentDetails
// Assembly: Google.Apis.YouTube.v3, Version=1.29.2.1006, Culture=neutral, PublicKeyToken=4b01fa6e34db77ab
// MVID: 9E53E5C1-D9AB-4142-94A7-8A2435650008
// Assembly location: C:\Users\Admin\Desktop\re\YoutubeApp\Google.Apis.YouTube.v3.dll

using Google.Apis.Requests;
using Newtonsoft.Json;

namespace Google.Apis.YouTube.v3.Data
{
  public class SubscriptionContentDetails : IDirectResponseSchema
  {
    [JsonProperty("activityType")]
    public virtual string ActivityType { get; set; }

    [JsonProperty("newItemCount")]
    public virtual long? NewItemCount { get; set; }

    [JsonProperty("totalItemCount")]
    public virtual long? TotalItemCount { get; set; }

    public virtual string ETag { get; set; }
  }
}
