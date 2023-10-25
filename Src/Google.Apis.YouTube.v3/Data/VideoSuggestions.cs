// Decompiled with JetBrains decompiler
// Type: Google.Apis.YouTube.v3.Data.VideoSuggestions
// Assembly: Google.Apis.YouTube.v3, Version=1.29.2.1006, Culture=neutral, PublicKeyToken=4b01fa6e34db77ab
// MVID: 9E53E5C1-D9AB-4142-94A7-8A2435650008
// Assembly location: C:\Users\Admin\Desktop\re\YoutubeApp\Google.Apis.YouTube.v3.dll

using Google.Apis.Requests;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Google.Apis.YouTube.v3.Data
{
  public class VideoSuggestions : IDirectResponseSchema
  {
    [JsonProperty("editorSuggestions")]
    public virtual IList<string> EditorSuggestions { get; set; }

    [JsonProperty("processingErrors")]
    public virtual IList<string> ProcessingErrors { get; set; }

    [JsonProperty("processingHints")]
    public virtual IList<string> ProcessingHints { get; set; }

    [JsonProperty("processingWarnings")]
    public virtual IList<string> ProcessingWarnings { get; set; }

    [JsonProperty("tagSuggestions")]
    public virtual IList<VideoSuggestionsTagSuggestion> TagSuggestions { get; set; }

    public virtual string ETag { get; set; }
  }
}
