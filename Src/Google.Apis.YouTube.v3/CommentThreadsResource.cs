// Decompiled with JetBrains decompiler
// Type: Google.Apis.YouTube.v3.CommentThreadsResource
// Assembly: Google.Apis.YouTube.v3, Version=1.29.2.1006, Culture=neutral, PublicKeyToken=4b01fa6e34db77ab
// MVID: 9E53E5C1-D9AB-4142-94A7-8A2435650008
// Assembly location: C:\Users\Admin\Desktop\re\YoutubeApp\Google.Apis.YouTube.v3.dll

using Google.Apis.Discovery;
using Google.Apis.Services;
using Google.Apis.Util;
using Google.Apis.YouTube.v3.Data;

namespace Google.Apis.YouTube.v3
{
  public class CommentThreadsResource
  {
    private const string Resource = "commentThreads";
    private readonly IClientService service;

    public CommentThreadsResource(IClientService service) => this.service = service;

    public virtual CommentThreadsResource.InsertRequest Insert(CommentThread body, string part) => new CommentThreadsResource.InsertRequest(this.service, body, part);

    public virtual CommentThreadsResource.ListRequest List(string part) => new CommentThreadsResource.ListRequest(this.service, part);

    public virtual CommentThreadsResource.UpdateRequest Update(CommentThread body, string part) => new CommentThreadsResource.UpdateRequest(this.service, body, part);

    public class InsertRequest : YouTubeBaseServiceRequest<CommentThread>
    {
      public InsertRequest(IClientService service, CommentThread body, string part)
        : base(service)
      {
        this.Part = part;
        this.Body = body;
        this.InitParameters();
      }

      [RequestParameter("part", RequestParameterType.Query)]
      public virtual string Part { get; private set; }

      private CommentThread Body { get; set; }

      protected override object GetBody() => (object) this.Body;

      public override string MethodName => "insert";

      public override string HttpMethod => "POST";

      public override string RestPath => "commentThreads";

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
      }
    }

    public class ListRequest : YouTubeBaseServiceRequest<CommentThreadListResponse>
    {
      public ListRequest(IClientService service, string part)
        : base(service)
      {
        this.Part = part;
        this.InitParameters();
      }

      [RequestParameter("part", RequestParameterType.Query)]
      public virtual string Part { get; private set; }

      [RequestParameter("allThreadsRelatedToChannelId", RequestParameterType.Query)]
      public virtual string AllThreadsRelatedToChannelId { get; set; }

      [RequestParameter("channelId", RequestParameterType.Query)]
      public virtual string ChannelId { get; set; }

      [RequestParameter("id", RequestParameterType.Query)]
      public virtual string Id { get; set; }

      [RequestParameter("maxResults", RequestParameterType.Query)]
      public virtual long? MaxResults { get; set; }

      [RequestParameter("moderationStatus", RequestParameterType.Query)]
      public virtual CommentThreadsResource.ListRequest.ModerationStatusEnum? ModerationStatus { get; set; }

      [RequestParameter("order", RequestParameterType.Query)]
      public virtual CommentThreadsResource.ListRequest.OrderEnum? Order { get; set; }

      [RequestParameter("pageToken", RequestParameterType.Query)]
      public virtual string PageToken { get; set; }

      [RequestParameter("searchTerms", RequestParameterType.Query)]
      public virtual string SearchTerms { get; set; }

      [RequestParameter("textFormat", RequestParameterType.Query)]
      public virtual CommentThreadsResource.ListRequest.TextFormatEnum? TextFormat { get; set; }

      [RequestParameter("videoId", RequestParameterType.Query)]
      public virtual string VideoId { get; set; }

      public override string MethodName => "list";

      public override string HttpMethod => "GET";

      public override string RestPath => "commentThreads";

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
        this.RequestParameters.Add("allThreadsRelatedToChannelId", (IParameter) new Parameter()
        {
          Name = "allThreadsRelatedToChannelId",
          IsRequired = false,
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
        this.RequestParameters.Add("id", (IParameter) new Parameter()
        {
          Name = "id",
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
          DefaultValue = "20",
          Pattern = (string) null
        });
        this.RequestParameters.Add("moderationStatus", (IParameter) new Parameter()
        {
          Name = "moderationStatus",
          IsRequired = false,
          ParameterType = "query",
          DefaultValue = "MODERATION_STATUS_PUBLISHED",
          Pattern = (string) null
        });
        this.RequestParameters.Add("order", (IParameter) new Parameter()
        {
          Name = "order",
          IsRequired = false,
          ParameterType = "query",
          DefaultValue = "true",
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
        this.RequestParameters.Add("searchTerms", (IParameter) new Parameter()
        {
          Name = "searchTerms",
          IsRequired = false,
          ParameterType = "query",
          DefaultValue = (string) null,
          Pattern = (string) null
        });
        this.RequestParameters.Add("textFormat", (IParameter) new Parameter()
        {
          Name = "textFormat",
          IsRequired = false,
          ParameterType = "query",
          DefaultValue = "FORMAT_HTML",
          Pattern = (string) null
        });
        this.RequestParameters.Add("videoId", (IParameter) new Parameter()
        {
          Name = "videoId",
          IsRequired = false,
          ParameterType = "query",
          DefaultValue = (string) null,
          Pattern = (string) null
        });
      }

      public enum ModerationStatusEnum
      {
        [StringValue("heldForReview")] HeldForReview,
        [StringValue("likelySpam")] LikelySpam,
        [StringValue("published")] Published,
      }

      public enum OrderEnum
      {
        [StringValue("relevance")] Relevance,
        [StringValue("time")] Time,
      }

      public enum TextFormatEnum
      {
        [StringValue("html")] Html,
        [StringValue("plainText")] PlainText,
      }
    }

    public class UpdateRequest : YouTubeBaseServiceRequest<CommentThread>
    {
      public UpdateRequest(IClientService service, CommentThread body, string part)
        : base(service)
      {
        this.Part = part;
        this.Body = body;
        this.InitParameters();
      }

      [RequestParameter("part", RequestParameterType.Query)]
      public virtual string Part { get; private set; }

      private CommentThread Body { get; set; }

      protected override object GetBody() => (object) this.Body;

      public override string MethodName => "update";

      public override string HttpMethod => "PUT";

      public override string RestPath => "commentThreads";

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
      }
    }
  }
}
