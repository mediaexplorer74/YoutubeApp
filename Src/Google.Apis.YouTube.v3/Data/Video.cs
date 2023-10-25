// Decompiled with JetBrains decompiler
// Type: Google.Apis.YouTube.v3.Data.Video
// Assembly: Google.Apis.YouTube.v3, Version=1.29.2.1006, Culture=neutral, PublicKeyToken=4b01fa6e34db77ab
// MVID: 9E53E5C1-D9AB-4142-94A7-8A2435650008
// Assembly location: C:\Users\Admin\Desktop\re\YoutubeApp\Google.Apis.YouTube.v3.dll

using Google.Apis.Requests;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Google.Apis.YouTube.v3.Data
{
  public class Video : IDirectResponseSchema
  {
    [JsonProperty("ageGating")]
    public virtual VideoAgeGating AgeGating { get; set; }

    [JsonProperty("contentDetails")]
    public virtual VideoContentDetails ContentDetails { get; set; }

    [JsonProperty("etag")]
    public virtual string ETag { get; set; }

    [JsonProperty("fileDetails")]
    public virtual VideoFileDetails FileDetails { get; set; }

    [JsonProperty("id")]
    public virtual string Id { get; set; }

    [JsonProperty("kind")]
    public virtual string Kind { get; set; }

    [JsonProperty("liveStreamingDetails")]
    public virtual VideoLiveStreamingDetails LiveStreamingDetails { get; set; }

    [JsonProperty("localizations")]
    public virtual IDictionary<string, VideoLocalization> Localizations { get; set; }

    [JsonProperty("monetizationDetails")]
    public virtual VideoMonetizationDetails MonetizationDetails { get; set; }

    [JsonProperty("player")]
    public virtual VideoPlayer Player { get; set; }

    [JsonProperty("processingDetails")]
    public virtual VideoProcessingDetails ProcessingDetails { get; set; }

    [JsonProperty("projectDetails")]
    public virtual VideoProjectDetails ProjectDetails { get; set; }

    [JsonProperty("recordingDetails")]
    public virtual VideoRecordingDetails RecordingDetails { get; set; }

    [JsonProperty("snippet")]
    public virtual VideoSnippet Snippet { get; set; }

    [JsonProperty("statistics")]
    public virtual VideoStatistics Statistics { get; set; }

    [JsonProperty("status")]
    public virtual VideoStatus Status { get; set; }

    [JsonProperty("suggestions")]
    public virtual VideoSuggestions Suggestions { get; set; }

    [JsonProperty("topicDetails")]
    public virtual VideoTopicDetails TopicDetails { get; set; }
  }
}
