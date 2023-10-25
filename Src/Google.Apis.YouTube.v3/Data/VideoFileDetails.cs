// Decompiled with JetBrains decompiler
// Type: Google.Apis.YouTube.v3.Data.VideoFileDetails
// Assembly: Google.Apis.YouTube.v3, Version=1.29.2.1006, Culture=neutral, PublicKeyToken=4b01fa6e34db77ab
// MVID: 9E53E5C1-D9AB-4142-94A7-8A2435650008
// Assembly location: C:\Users\Admin\Desktop\re\YoutubeApp\Google.Apis.YouTube.v3.dll

using Google.Apis.Requests;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Google.Apis.YouTube.v3.Data
{
  public class VideoFileDetails : IDirectResponseSchema
  {
    [JsonProperty("audioStreams")]
    public virtual IList<VideoFileDetailsAudioStream> AudioStreams { get; set; }

    [JsonProperty("bitrateBps")]
    public virtual ulong? BitrateBps { get; set; }

    [JsonProperty("container")]
    public virtual string Container { get; set; }

    [JsonProperty("creationTime")]
    public virtual string CreationTime { get; set; }

    [JsonProperty("durationMs")]
    public virtual ulong? DurationMs { get; set; }

    [JsonProperty("fileName")]
    public virtual string FileName { get; set; }

    [JsonProperty("fileSize")]
    public virtual ulong? FileSize { get; set; }

    [JsonProperty("fileType")]
    public virtual string FileType { get; set; }

    [JsonProperty("videoStreams")]
    public virtual IList<VideoFileDetailsVideoStream> VideoStreams { get; set; }

    public virtual string ETag { get; set; }
  }
}
