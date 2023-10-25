// Decompiled with JetBrains decompiler
// Type: Google.Apis.YouTube.v3.Data.VideoProcessingDetails
// Assembly: Google.Apis.YouTube.v3, Version=1.29.2.1006, Culture=neutral, PublicKeyToken=4b01fa6e34db77ab
// MVID: 9E53E5C1-D9AB-4142-94A7-8A2435650008
// Assembly location: C:\Users\Admin\Desktop\re\YoutubeApp\Google.Apis.YouTube.v3.dll

using Google.Apis.Requests;
using Newtonsoft.Json;

namespace Google.Apis.YouTube.v3.Data
{
  public class VideoProcessingDetails : IDirectResponseSchema
  {
    [JsonProperty("editorSuggestionsAvailability")]
    public virtual string EditorSuggestionsAvailability { get; set; }

    [JsonProperty("fileDetailsAvailability")]
    public virtual string FileDetailsAvailability { get; set; }

    [JsonProperty("processingFailureReason")]
    public virtual string ProcessingFailureReason { get; set; }

    [JsonProperty("processingIssuesAvailability")]
    public virtual string ProcessingIssuesAvailability { get; set; }

    [JsonProperty("processingProgress")]
    public virtual VideoProcessingDetailsProcessingProgress ProcessingProgress { get; set; }

    [JsonProperty("processingStatus")]
    public virtual string ProcessingStatus { get; set; }

    [JsonProperty("tagSuggestionsAvailability")]
    public virtual string TagSuggestionsAvailability { get; set; }

    [JsonProperty("thumbnailsAvailability")]
    public virtual string ThumbnailsAvailability { get; set; }

    public virtual string ETag { get; set; }
  }
}
