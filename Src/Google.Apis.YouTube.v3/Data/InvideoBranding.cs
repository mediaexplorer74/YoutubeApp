// Decompiled with JetBrains decompiler
// Type: Google.Apis.YouTube.v3.Data.InvideoBranding
// Assembly: Google.Apis.YouTube.v3, Version=1.29.2.1006, Culture=neutral, PublicKeyToken=4b01fa6e34db77ab
// MVID: 9E53E5C1-D9AB-4142-94A7-8A2435650008
// Assembly location: C:\Users\Admin\Desktop\re\YoutubeApp\Google.Apis.YouTube.v3.dll

using Google.Apis.Requests;
using Newtonsoft.Json;

namespace Google.Apis.YouTube.v3.Data
{
  public class InvideoBranding : IDirectResponseSchema
  {
    [JsonProperty("imageBytes")]
    public virtual string ImageBytes { get; set; }

    [JsonProperty("imageUrl")]
    public virtual string ImageUrl { get; set; }

    [JsonProperty("position")]
    public virtual InvideoPosition Position { get; set; }

    [JsonProperty("targetChannelId")]
    public virtual string TargetChannelId { get; set; }

    [JsonProperty("timing")]
    public virtual InvideoTiming Timing { get; set; }

    public virtual string ETag { get; set; }
  }
}
