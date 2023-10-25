// Decompiled with JetBrains decompiler
// Type: Google.Apis.YouTube.v3.Data.VideoFileDetailsVideoStream
// Assembly: Google.Apis.YouTube.v3, Version=1.29.2.1006, Culture=neutral, PublicKeyToken=4b01fa6e34db77ab
// MVID: 9E53E5C1-D9AB-4142-94A7-8A2435650008
// Assembly location: C:\Users\Admin\Desktop\re\YoutubeApp\Google.Apis.YouTube.v3.dll

using Google.Apis.Requests;
using Newtonsoft.Json;

namespace Google.Apis.YouTube.v3.Data
{
  public class VideoFileDetailsVideoStream : IDirectResponseSchema
  {
    [JsonProperty("aspectRatio")]
    public virtual double? AspectRatio { get; set; }

    [JsonProperty("bitrateBps")]
    public virtual ulong? BitrateBps { get; set; }

    [JsonProperty("codec")]
    public virtual string Codec { get; set; }

    [JsonProperty("frameRateFps")]
    public virtual double? FrameRateFps { get; set; }

    [JsonProperty("heightPixels")]
    public virtual long? HeightPixels { get; set; }

    [JsonProperty("rotation")]
    public virtual string Rotation { get; set; }

    [JsonProperty("vendor")]
    public virtual string Vendor { get; set; }

    [JsonProperty("widthPixels")]
    public virtual long? WidthPixels { get; set; }

    public virtual string ETag { get; set; }
  }
}
