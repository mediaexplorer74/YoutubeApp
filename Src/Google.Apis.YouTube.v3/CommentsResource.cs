// Decompiled with JetBrains decompiler
// Type: Google.Apis.YouTube.v3.CommentsResource
// Assembly: Google.Apis.YouTube.v3, Version=1.29.2.1006, Culture=neutral, PublicKeyToken=4b01fa6e34db77ab
// MVID: 9E53E5C1-D9AB-4142-94A7-8A2435650008
// Assembly location: C:\Users\Admin\Desktop\re\YoutubeApp\Google.Apis.YouTube.v3.dll

using Google.Apis.Discovery;
using Google.Apis.Services;
using Google.Apis.Util;
using Google.Apis.YouTube.v3.Data;

namespace Google.Apis.YouTube.v3
{
  public class CommentsResource
  {
    private const string Resource = "comments";
    private readonly IClientService service;

    public CommentsResource(IClientService service) => this.service = service;

    public virtual CommentsResource.DeleteRequest Delete(string id) => new CommentsResource.DeleteRequest(this.service, id);

    public virtual CommentsResource.InsertRequest Insert(Comment body, string part) => new CommentsResource.InsertRequest(this.service, body, part);

    public virtual CommentsResource.ListRequest List(string part) => new CommentsResource.ListRequest(this.service, part);

    public virtual CommentsResource.MarkAsSpamRequest MarkAsSpam(string id) => new CommentsResource.MarkAsSpamRequest(this.service, id);

    public virtual CommentsResource.SetModerationStatusRequest SetModerationStatus(
      string id,
      CommentsResource.SetModerationStatusRequest.ModerationStatusEnum moderationStatus)
    {
      return new CommentsResource.SetModerationStatusRequest(this.service, id, moderationStatus);
    }

    public virtual CommentsResource.UpdateRequest Update(Comment body, string part) => new CommentsResource.UpdateRequest(this.service, body, part);

    public class DeleteRequest : YouTubeBaseServiceRequest<string>
    {
      public DeleteRequest(IClientService service, string id)
        : base(service)
      {
        this.Id = id;
        this.InitParameters();
      }

      [RequestParameter("id", RequestParameterType.Query)]
      public virtual string Id { get; private set; }

      public override string MethodName => "delete";

      public override string HttpMethod => "DELETE";

      public override string RestPath => "comments";

      protected override void InitParameters()
      {
        base.InitParameters();
        this.RequestParameters.Add("id", (IParameter) new Parameter()
        {
          Name = "id",
          IsRequired = true,
          ParameterType = "query",
          DefaultValue = (string) null,
          Pattern = (string) null
        });
      }
    }

    public class InsertRequest : YouTubeBaseServiceRequest<Comment>
    {
      public InsertRequest(IClientService service, Comment body, string part)
        : base(service)
      {
        this.Part = part;
        this.Body = body;
        this.InitParameters();
      }

      [RequestParameter("part", RequestParameterType.Query)]
      public virtual string Part { get; private set; }

      private Comment Body { get; set; }

      protected override object GetBody() => (object) this.Body;

      public override string MethodName => "insert";

      public override string HttpMethod => "POST";

      public override string RestPath => "comments";

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

    public class ListRequest : YouTubeBaseServiceRequest<CommentListResponse>
    {
      public ListRequest(IClientService service, string part)
        : base(service)
      {
        this.Part = part;
        this.InitParameters();
      }

      [RequestParameter("part", RequestParameterType.Query)]
      public virtual string Part { get; private set; }

      [RequestParameter("id", RequestParameterType.Query)]
      public virtual string Id { get; set; }

      [RequestParameter("maxResults", RequestParameterType.Query)]
      public virtual long? MaxResults { get; set; }

      [RequestParameter("pageToken", RequestParameterType.Query)]
      public virtual string PageToken { get; set; }

      [RequestParameter("parentId", RequestParameterType.Query)]
      public virtual string ParentId { get; set; }

      [RequestParameter("textFormat", RequestParameterType.Query)]
      public virtual CommentsResource.ListRequest.TextFormatEnum? TextFormat { get; set; }

      public override string MethodName => "list";

      public override string HttpMethod => "GET";

      public override string RestPath => "comments";

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
        this.RequestParameters.Add("pageToken", (IParameter) new Parameter()
        {
          Name = "pageToken",
          IsRequired = false,
          ParameterType = "query",
          DefaultValue = (string) null,
          Pattern = (string) null
        });
        this.RequestParameters.Add("parentId", (IParameter) new Parameter()
        {
          Name = "parentId",
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
      }

      public enum TextFormatEnum
      {
        [StringValue("html")] Html,
        [StringValue("plainText")] PlainText,
      }
    }

    public class MarkAsSpamRequest : YouTubeBaseServiceRequest<string>
    {
      public MarkAsSpamRequest(IClientService service, string id)
        : base(service)
      {
        this.Id = id;
        this.InitParameters();
      }

      [RequestParameter("id", RequestParameterType.Query)]
      public virtual string Id { get; private set; }

      public override string MethodName => "markAsSpam";

      public override string HttpMethod => "POST";

      public override string RestPath => "comments/markAsSpam";

      protected override void InitParameters()
      {
        base.InitParameters();
        this.RequestParameters.Add("id", (IParameter) new Parameter()
        {
          Name = "id",
          IsRequired = true,
          ParameterType = "query",
          DefaultValue = (string) null,
          Pattern = (string) null
        });
      }
    }

    public class SetModerationStatusRequest : YouTubeBaseServiceRequest<string>
    {
      public SetModerationStatusRequest(
        IClientService service,
        string id,
        CommentsResource.SetModerationStatusRequest.ModerationStatusEnum moderationStatus)
        : base(service)
      {
        this.Id = id;
        this.ModerationStatus = moderationStatus;
        this.InitParameters();
      }

      [RequestParameter("id", RequestParameterType.Query)]
      public virtual string Id { get; private set; }

      [RequestParameter("moderationStatus", RequestParameterType.Query)]
      public virtual CommentsResource.SetModerationStatusRequest.ModerationStatusEnum ModerationStatus { get; private set; }

      [RequestParameter("banAuthor", RequestParameterType.Query)]
      public virtual bool? BanAuthor { get; set; }

      public override string MethodName => "setModerationStatus";

      public override string HttpMethod => "POST";

      public override string RestPath => "comments/setModerationStatus";

      protected override void InitParameters()
      {
        base.InitParameters();
        this.RequestParameters.Add("id", (IParameter) new Parameter()
        {
          Name = "id",
          IsRequired = true,
          ParameterType = "query",
          DefaultValue = (string) null,
          Pattern = (string) null
        });
        this.RequestParameters.Add("moderationStatus", (IParameter) new Parameter()
        {
          Name = "moderationStatus",
          IsRequired = true,
          ParameterType = "query",
          DefaultValue = (string) null,
          Pattern = (string) null
        });
        this.RequestParameters.Add("banAuthor", (IParameter) new Parameter()
        {
          Name = "banAuthor",
          IsRequired = false,
          ParameterType = "query",
          DefaultValue = "false",
          Pattern = (string) null
        });
      }

      public enum ModerationStatusEnum
      {
        [StringValue("heldForReview")] HeldForReview,
        [StringValue("published")] Published,
        [StringValue("rejected")] Rejected,
      }
    }

    public class UpdateRequest : YouTubeBaseServiceRequest<Comment>
    {
      public UpdateRequest(IClientService service, Comment body, string part)
        : base(service)
      {
        this.Part = part;
        this.Body = body;
        this.InitParameters();
      }

      [RequestParameter("part", RequestParameterType.Query)]
      public virtual string Part { get; private set; }

      private Comment Body { get; set; }

      protected override object GetBody() => (object) this.Body;

      public override string MethodName => "update";

      public override string HttpMethod => "PUT";

      public override string RestPath => "comments";

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
