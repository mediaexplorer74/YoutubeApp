// Decompiled with JetBrains decompiler
// Type: Google.Apis.YouTube.v3.SearchResource
// Assembly: Google.Apis.YouTube.v3, Version=1.29.2.1006, Culture=neutral, PublicKeyToken=4b01fa6e34db77ab
// MVID: 9E53E5C1-D9AB-4142-94A7-8A2435650008
// Assembly location: C:\Users\Admin\Desktop\re\YoutubeApp\Google.Apis.YouTube.v3.dll

using Google.Apis.Discovery;
using Google.Apis.Services;
using Google.Apis.Util;
using Google.Apis.YouTube.v3.Data;
using System;

namespace Google.Apis.YouTube.v3
{
  public class SearchResource
  {
    private const string Resource = "search";
    private readonly IClientService service;

    public SearchResource(IClientService service) => this.service = service;

    public virtual SearchResource.ListRequest List(string part) => new SearchResource.ListRequest(this.service, part);

    public class ListRequest : YouTubeBaseServiceRequest<SearchListResponse>
    {
      public ListRequest(IClientService service, string part)
        : base(service)
      {
        this.Part = part;
        this.InitParameters();
      }

      [RequestParameter("part", RequestParameterType.Query)]
      public virtual string Part { get; private set; }

      [RequestParameter("channelId", RequestParameterType.Query)]
      public virtual string ChannelId { get; set; }

      [RequestParameter("channelType", RequestParameterType.Query)]
      public virtual SearchResource.ListRequest.ChannelTypeEnum? ChannelType { get; set; }

      [RequestParameter("eventType", RequestParameterType.Query)]
      public virtual SearchResource.ListRequest.EventTypeEnum? EventType { get; set; }

      [RequestParameter("forContentOwner", RequestParameterType.Query)]
      public virtual bool? ForContentOwner { get; set; }

      [RequestParameter("forDeveloper", RequestParameterType.Query)]
      public virtual bool? ForDeveloper { get; set; }

      [RequestParameter("forMine", RequestParameterType.Query)]
      public virtual bool? ForMine { get; set; }

      [RequestParameter("location", RequestParameterType.Query)]
      public virtual string Location { get; set; }

      [RequestParameter("locationRadius", RequestParameterType.Query)]
      public virtual string LocationRadius { get; set; }

      [RequestParameter("maxResults", RequestParameterType.Query)]
      public virtual long? MaxResults { get; set; }

      [RequestParameter("onBehalfOfContentOwner", RequestParameterType.Query)]
      public virtual string OnBehalfOfContentOwner { get; set; }

      [RequestParameter("order", RequestParameterType.Query)]
      public virtual SearchResource.ListRequest.OrderEnum? Order { get; set; }

      [RequestParameter("pageToken", RequestParameterType.Query)]
      public virtual string PageToken { get; set; }

      [RequestParameter("publishedAfter", RequestParameterType.Query)]
      public virtual DateTime? PublishedAfter { get; set; }

      [RequestParameter("publishedBefore", RequestParameterType.Query)]
      public virtual DateTime? PublishedBefore { get; set; }

      [RequestParameter("q", RequestParameterType.Query)]
      public virtual string Q { get; set; }

      [RequestParameter("regionCode", RequestParameterType.Query)]
      public virtual string RegionCode { get; set; }

      [RequestParameter("relatedToVideoId", RequestParameterType.Query)]
      public virtual string RelatedToVideoId { get; set; }

      [RequestParameter("relevanceLanguage", RequestParameterType.Query)]
      public virtual string RelevanceLanguage { get; set; }

      [RequestParameter("safeSearch", RequestParameterType.Query)]
      public virtual SearchResource.ListRequest.SafeSearchEnum? SafeSearch { get; set; }

      [RequestParameter("topicId", RequestParameterType.Query)]
      public virtual string TopicId { get; set; }

      [RequestParameter("type", RequestParameterType.Query)]
      public virtual string Type { get; set; }

      [RequestParameter("videoCaption", RequestParameterType.Query)]
      public virtual SearchResource.ListRequest.VideoCaptionEnum? VideoCaption { get; set; }

      [RequestParameter("videoCategoryId", RequestParameterType.Query)]
      public virtual string VideoCategoryId { get; set; }

      [RequestParameter("videoDefinition", RequestParameterType.Query)]
      public virtual SearchResource.ListRequest.VideoDefinitionEnum? VideoDefinition { get; set; }

      [RequestParameter("videoDimension", RequestParameterType.Query)]
      public virtual SearchResource.ListRequest.VideoDimensionEnum? VideoDimension { get; set; }

      [RequestParameter("videoDuration", RequestParameterType.Query)]
      public virtual SearchResource.ListRequest.VideoDurationEnum? VideoDuration { get; set; }

      [RequestParameter("videoEmbeddable", RequestParameterType.Query)]
      public virtual SearchResource.ListRequest.VideoEmbeddableEnum? VideoEmbeddable { get; set; }

      [RequestParameter("videoLicense", RequestParameterType.Query)]
      public virtual SearchResource.ListRequest.VideoLicenseEnum? VideoLicense { get; set; }

      [RequestParameter("videoSyndicated", RequestParameterType.Query)]
      public virtual SearchResource.ListRequest.VideoSyndicatedEnum? VideoSyndicated { get; set; }

      [RequestParameter("videoType", RequestParameterType.Query)]
      public virtual SearchResource.ListRequest.VideoTypeEnum? VideoType { get; set; }

      public override string MethodName => "list";

      public override string HttpMethod => "GET";

      public override string RestPath => "search";

      protected override void InitParameters()
      {
        base.InitParameters();
        this.RequestParameters.Add("part", (IParameter) new Parameter()
        {
          Name = "part",
          IsRequired = true,
          ParameterType = "query",
          DefaultValue = (string) null,
          Pattern = (string) null
        });
        this.RequestParameters.Add("channelId", (IParameter) new Parameter()
        {
          Name = "channelId",
          IsRequired = false,
          ParameterType = "query",
          DefaultValue = (string) null,
          Pattern = (string) null
        });
        this.RequestParameters.Add("channelType", (IParameter) new Parameter()
        {
          Name = "channelType",
          IsRequired = false,
          ParameterType = "query",
          DefaultValue = (string) null,
          Pattern = (string) null
        });
        this.RequestParameters.Add("eventType", (IParameter) new Parameter()
        {
          Name = "eventType",
          IsRequired = false,
          ParameterType = "query",
          DefaultValue = (string) null,
          Pattern = (string) null
        });
        this.RequestParameters.Add("forContentOwner", (IParameter) new Parameter()
        {
          Name = "forContentOwner",
          IsRequired = false,
          ParameterType = "query",
          DefaultValue = (string) null,
          Pattern = (string) null
        });
        this.RequestParameters.Add("forDeveloper", (IParameter) new Parameter()
        {
          Name = "forDeveloper",
          IsRequired = false,
          ParameterType = "query",
          DefaultValue = (string) null,
          Pattern = (string) null
        });
        this.RequestParameters.Add("forMine", (IParameter) new Parameter()
        {
          Name = "forMine",
          IsRequired = false,
          ParameterType = "query",
          DefaultValue = (string) null,
          Pattern = (string) null
        });
        this.RequestParameters.Add("location", (IParameter) new Parameter()
        {
          Name = "location",
          IsRequired = false,
          ParameterType = "query",
          DefaultValue = (string) null,
          Pattern = (string) null
        });
        this.RequestParameters.Add("locationRadius", (IParameter) new Parameter()
        {
          Name = "locationRadius",
          IsRequired = false,
          ParameterType = "query",
          DefaultValue = (string) null,
          Pattern = (string) null
        });
        this.RequestParameters.Add("maxResults", (IParameter) new Parameter()
        {
          Name = "maxResults",
          IsRequired = false,
          ParameterType = "query",
          DefaultValue = "5",
          Pattern = (string) null
        });
        this.RequestParameters.Add("onBehalfOfContentOwner", (IParameter) new Parameter()
        {
          Name = "onBehalfOfContentOwner",
          IsRequired = false,
          ParameterType = "query",
          DefaultValue = (string) null,
          Pattern = (string) null
        });
        this.RequestParameters.Add("order", (IParameter) new Parameter()
        {
          Name = "order",
          IsRequired = false,
          ParameterType = "query",
          DefaultValue = "SEARCH_SORT_RELEVANCE",
          Pattern = (string) null
        });
        this.RequestParameters.Add("pageToken", (IParameter) new Parameter()
        {
          Name = "pageToken",
          IsRequired = false,
          ParameterType = "query",
          DefaultValue = (string) null,
          Pattern = (string) null
        });
        this.RequestParameters.Add("publishedAfter", (IParameter) new Parameter()
        {
          Name = "publishedAfter",
          IsRequired = false,
          ParameterType = "query",
          DefaultValue = (string) null,
          Pattern = (string) null
        });
        this.RequestParameters.Add("publishedBefore", (IParameter) new Parameter()
        {
          Name = "publishedBefore",
          IsRequired = false,
          ParameterType = "query",
          DefaultValue = (string) null,
          Pattern = (string) null
        });
        this.RequestParameters.Add("q", (IParameter) new Parameter()
        {
          Name = "q",
          IsRequired = false,
          ParameterType = "query",
          DefaultValue = (string) null,
          Pattern = (string) null
        });
        this.RequestParameters.Add("regionCode", (IParameter) new Parameter()
        {
          Name = "regionCode",
          IsRequired = false,
          ParameterType = "query",
          DefaultValue = (string) null,
          Pattern = (string) null
        });
        this.RequestParameters.Add("relatedToVideoId", (IParameter) new Parameter()
        {
          Name = "relatedToVideoId",
          IsRequired = false,
          ParameterType = "query",
          DefaultValue = (string) null,
          Pattern = (string) null
        });
        this.RequestParameters.Add("relevanceLanguage", (IParameter) new Parameter()
        {
          Name = "relevanceLanguage",
          IsRequired = false,
          ParameterType = "query",
          DefaultValue = (string) null,
          Pattern = (string) null
        });
        this.RequestParameters.Add("safeSearch", (IParameter) new Parameter()
        {
          Name = "safeSearch",
          IsRequired = false,
          ParameterType = "query",
          DefaultValue = (string) null,
          Pattern = (string) null
        });
        this.RequestParameters.Add("topicId", (IParameter) new Parameter()
        {
          Name = "topicId",
          IsRequired = false,
          ParameterType = "query",
          DefaultValue = (string) null,
          Pattern = (string) null
        });
        this.RequestParameters.Add("type", (IParameter) new Parameter()
        {
          Name = "type",
          IsRequired = false,
          ParameterType = "query",
          DefaultValue = "video,channel,playlist",
          Pattern = (string) null
        });
        this.RequestParameters.Add("videoCaption", (IParameter) new Parameter()
        {
          Name = "videoCaption",
          IsRequired = false,
          ParameterType = "query",
          DefaultValue = (string) null,
          Pattern = (string) null
        });
        this.RequestParameters.Add("videoCategoryId", (IParameter) new Parameter()
        {
          Name = "videoCategoryId",
          IsRequired = false,
          ParameterType = "query",
          DefaultValue = (string) null,
          Pattern = (string) null
        });
        this.RequestParameters.Add("videoDefinition", (IParameter) new Parameter()
        {
          Name = "videoDefinition",
          IsRequired = false,
          ParameterType = "query",
          DefaultValue = (string) null,
          Pattern = (string) null
        });
        this.RequestParameters.Add("videoDimension", (IParameter) new Parameter()
        {
          Name = "videoDimension",
          IsRequired = false,
          ParameterType = "query",
          DefaultValue = (string) null,
          Pattern = (string) null
        });
        this.RequestParameters.Add("videoDuration", (IParameter) new Parameter()
        {
          Name = "videoDuration",
          IsRequired = false,
          ParameterType = "query",
          DefaultValue = (string) null,
          Pattern = (string) null
        });
        this.RequestParameters.Add("videoEmbeddable", (IParameter) new Parameter()
        {
          Name = "videoEmbeddable",
          IsRequired = false,
          ParameterType = "query",
          DefaultValue = (string) null,
          Pattern = (string) null
        });
        this.RequestParameters.Add("videoLicense", (IParameter) new Parameter()
        {
          Name = "videoLicense",
          IsRequired = false,
          ParameterType = "query",
          DefaultValue = (string) null,
          Pattern = (string) null
        });
        this.RequestParameters.Add("videoSyndicated", (IParameter) new Parameter()
        {
          Name = "videoSyndicated",
          IsRequired = false,
          ParameterType = "query",
          DefaultValue = (string) null,
          Pattern = (string) null
        });
        this.RequestParameters.Add("videoType", (IParameter) new Parameter()
        {
          Name = "videoType",
          IsRequired = false,
          ParameterType = "query",
          DefaultValue = (string) null,
          Pattern = (string) null
        });
      }

      public enum ChannelTypeEnum
      {
        [StringValue("any")] Any,
        [StringValue("show")] Show,
      }

      public enum EventTypeEnum
      {
        [StringValue("completed")] Completed,
        [StringValue("live")] Live,
        [StringValue("upcoming")] Upcoming,
      }

      public enum OrderEnum
      {
        [StringValue("date")] Date,
        [StringValue("rating")] Rating,
        [StringValue("relevance")] Relevance,
        [StringValue("title")] Title,
        [StringValue("videoCount")] VideoCount,
        [StringValue("viewCount")] ViewCount,
      }

      public enum SafeSearchEnum
      {
        [StringValue("moderate")] Moderate,
        [StringValue("none")] None,
        [StringValue("strict")] Strict,
      }

      public enum VideoCaptionEnum
      {
        [StringValue("any")] Any,
        [StringValue("closedCaption")] ClosedCaption,
        [StringValue("none")] None,
      }

      public enum VideoDefinitionEnum
      {
        [StringValue("any")] Any,
        [StringValue("high")] High,
        [StringValue("standard")] Standard,
      }

      public enum VideoDimensionEnum
      {
        [StringValue("2d")] Value2d,
        [StringValue("3d")] Value3d,
        [StringValue("any")] Any,
      }

      public enum VideoDurationEnum
      {
        [StringValue("any")] Any,
        [StringValue("long")] Long__,
        [StringValue("medium")] Medium,
        [StringValue("short")] Short__,
      }

      public enum VideoEmbeddableEnum
      {
        [StringValue("any")] Any,
        [StringValue("true")] True__,
      }

      public enum VideoLicenseEnum
      {
        [StringValue("any")] Any,
        [StringValue("creativeCommon")] CreativeCommon,
        [StringValue("youtube")] Youtube,
      }

      public enum VideoSyndicatedEnum
      {
        [StringValue("any")] Any,
        [StringValue("true")] True__,
      }

      public enum VideoTypeEnum
      {
        [StringValue("any")] Any,
        [StringValue("episode")] Episode,
        [StringValue("movie")] Movie,
      }
    }
  }
}
