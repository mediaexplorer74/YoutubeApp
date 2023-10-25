// Decompiled with JetBrains decompiler
// Type: Google.Apis.YouTube.v3.VideosResource
// Assembly: Google.Apis.YouTube.v3, Version=1.29.2.1006, Culture=neutral, PublicKeyToken=4b01fa6e34db77ab
// MVID: 9E53E5C1-D9AB-4142-94A7-8A2435650008
// Assembly location: C:\Users\Admin\Desktop\re\YoutubeApp\Google.Apis.YouTube.v3.dll

using Google.Apis.Discovery;
using Google.Apis.Services;
using Google.Apis.Upload;
using Google.Apis.Util;
using Google.Apis.YouTube.v3.Data;
using System.IO;

namespace Google.Apis.YouTube.v3
{
  public class VideosResource
  {
    private const string Resource = "videos";
    private readonly IClientService service;

    public VideosResource(IClientService service) => this.service = service;

    public virtual VideosResource.DeleteRequest Delete(string id) => new VideosResource.DeleteRequest(this.service, id);

    public virtual VideosResource.GetRatingRequest GetRating(string id) => new VideosResource.GetRatingRequest(this.service, id);

    public virtual VideosResource.InsertRequest Insert(Video body, string part) => new VideosResource.InsertRequest(this.service, body, part);

    public virtual VideosResource.InsertMediaUpload Insert(
      Video body,
      string part,
      Stream stream,
      string contentType)
    {
      return new VideosResource.InsertMediaUpload(this.service, body, part, stream, contentType);
    }

    public virtual VideosResource.ListRequest List(string part) => new VideosResource.ListRequest(this.service, part);

    public virtual VideosResource.RateRequest Rate(
      string id,
      VideosResource.RateRequest.RatingEnum rating)
    {
      return new VideosResource.RateRequest(this.service, id, rating);
    }

    public virtual VideosResource.ReportAbuseRequest ReportAbuse(VideoAbuseReport body) => new VideosResource.ReportAbuseRequest(this.service, body);

    public virtual VideosResource.UpdateRequest Update(Video body, string part) => new VideosResource.UpdateRequest(this.service, body, part);

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

      [RequestParameter("onBehalfOfContentOwner", RequestParameterType.Query)]
      public virtual string OnBehalfOfContentOwner { get; set; }

      public override string MethodName => "delete";

      public override string HttpMethod => "DELETE";

      public override string RestPath => "videos";

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
        this.RequestParameters.Add("onBehalfOfContentOwner", (IParameter) new Parameter()
        {
          Name = "onBehalfOfContentOwner",
          IsRequired = false,
          ParameterType = "query",
          DefaultValue = (string) null,
          Pattern = (string) null
        });
      }
    }

    public class GetRatingRequest : YouTubeBaseServiceRequest<VideoGetRatingResponse>
    {
      public GetRatingRequest(IClientService service, string id)
        : base(service)
      {
        this.Id = id;
        this.InitParameters();
      }

      [RequestParameter("id", RequestParameterType.Query)]
      public virtual string Id { get; private set; }

      [RequestParameter("onBehalfOfContentOwner", RequestParameterType.Query)]
      public virtual string OnBehalfOfContentOwner { get; set; }

      public override string MethodName => "getRating";

      public override string HttpMethod => "GET";

      public override string RestPath => "videos/getRating";

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
        this.RequestParameters.Add("onBehalfOfContentOwner", (IParameter) new Parameter()
        {
          Name = "onBehalfOfContentOwner",
          IsRequired = false,
          ParameterType = "query",
          DefaultValue = (string) null,
          Pattern = (string) null
        });
      }
    }

    public class InsertRequest : YouTubeBaseServiceRequest<Video>
    {
      public InsertRequest(IClientService service, Video body, string part)
        : base(service)
      {
        this.Part = part;
        this.Body = body;
        this.InitParameters();
      }

      [RequestParameter("part", RequestParameterType.Query)]
      public virtual string Part { get; private set; }

      [RequestParameter("autoLevels", RequestParameterType.Query)]
      public virtual bool? AutoLevels { get; set; }

      [RequestParameter("notifySubscribers", RequestParameterType.Query)]
      public virtual bool? NotifySubscribers { get; set; }

      [RequestParameter("onBehalfOfContentOwner", RequestParameterType.Query)]
      public virtual string OnBehalfOfContentOwner { get; set; }

      [RequestParameter("onBehalfOfContentOwnerChannel", RequestParameterType.Query)]
      public virtual string OnBehalfOfContentOwnerChannel { get; set; }

      [RequestParameter("stabilize", RequestParameterType.Query)]
      public virtual bool? Stabilize { get; set; }

      private Video Body { get; set; }

      protected override object GetBody() => (object) this.Body;

      public override string MethodName => "insert";

      public override string HttpMethod => "POST";

      public override string RestPath => "videos";

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
        this.RequestParameters.Add("autoLevels", (IParameter) new Parameter()
        {
          Name = "autoLevels",
          IsRequired = false,
          ParameterType = "query",
          DefaultValue = (string) null,
          Pattern = (string) null
        });
        this.RequestParameters.Add("notifySubscribers", (IParameter) new Parameter()
        {
          Name = "notifySubscribers",
          IsRequired = false,
          ParameterType = "query",
          DefaultValue = "true",
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
        this.RequestParameters.Add("onBehalfOfContentOwnerChannel", (IParameter) new Parameter()
        {
          Name = "onBehalfOfContentOwnerChannel",
          IsRequired = false,
          ParameterType = "query",
          DefaultValue = (string) null,
          Pattern = (string) null
        });
        this.RequestParameters.Add("stabilize", (IParameter) new Parameter()
        {
          Name = "stabilize",
          IsRequired = false,
          ParameterType = "query",
          DefaultValue = (string) null,
          Pattern = (string) null
        });
      }
    }

    public class InsertMediaUpload : ResumableUpload<Video, Video>
    {
      [RequestParameter("alt", RequestParameterType.Query)]
      public virtual VideosResource.InsertMediaUpload.AltEnum? Alt { get; set; }

      [RequestParameter("fields", RequestParameterType.Query)]
      public virtual string Fields { get; set; }

      [RequestParameter("key", RequestParameterType.Query)]
      public virtual string Key { get; set; }

      [RequestParameter("oauth_token", RequestParameterType.Query)]
      public virtual string OauthToken { get; set; }

      [RequestParameter("prettyPrint", RequestParameterType.Query)]
      public virtual bool? PrettyPrint { get; set; }

      [RequestParameter("quotaUser", RequestParameterType.Query)]
      public virtual string QuotaUser { get; set; }

      [RequestParameter("userIp", RequestParameterType.Query)]
      public virtual string UserIp { get; set; }

      [RequestParameter("part", RequestParameterType.Query)]
      public virtual string Part { get; private set; }

      [RequestParameter("autoLevels", RequestParameterType.Query)]
      public virtual bool? AutoLevels { get; set; }

      [RequestParameter("notifySubscribers", RequestParameterType.Query)]
      public virtual bool? NotifySubscribers { get; set; }

      [RequestParameter("onBehalfOfContentOwner", RequestParameterType.Query)]
      public virtual string OnBehalfOfContentOwner { get; set; }

      [RequestParameter("onBehalfOfContentOwnerChannel", RequestParameterType.Query)]
      public virtual string OnBehalfOfContentOwnerChannel { get; set; }

      [RequestParameter("stabilize", RequestParameterType.Query)]
      public virtual bool? Stabilize { get; set; }

      public InsertMediaUpload(
        IClientService service,
        Video body,
        string part,
        Stream stream,
        string contentType)
        : base(service, string.Format("/{0}/{1}{2}", (object) "upload", (object) service.BasePath, (object) "videos"), "POST", stream, contentType)
      {
        this.Part = part;
        this.Body = body;
      }

      public enum AltEnum
      {
        [StringValue("json")] Json,
      }
    }

    public class ListRequest : YouTubeBaseServiceRequest<VideoListResponse>
    {
      public ListRequest(IClientService service, string part)
        : base(service)
      {
        this.Part = part;
        this.InitParameters();
      }

      [RequestParameter("part", RequestParameterType.Query)]
      public virtual string Part { get; private set; }

      [RequestParameter("chart", RequestParameterType.Query)]
      public virtual VideosResource.ListRequest.ChartEnum? Chart { get; set; }

      [RequestParameter("hl", RequestParameterType.Query)]
      public virtual string Hl { get; set; }

      [RequestParameter("id", RequestParameterType.Query)]
      public virtual string Id { get; set; }

      [RequestParameter("locale", RequestParameterType.Query)]
      public virtual string Locale { get; set; }

      [RequestParameter("maxHeight", RequestParameterType.Query)]
      public virtual long? MaxHeight { get; set; }

      [RequestParameter("maxResults", RequestParameterType.Query)]
      public virtual long? MaxResults { get; set; }

      [RequestParameter("maxWidth", RequestParameterType.Query)]
      public virtual long? MaxWidth { get; set; }

      [RequestParameter("myRating", RequestParameterType.Query)]
      public virtual VideosResource.ListRequest.MyRatingEnum? MyRating { get; set; }

      [RequestParameter("onBehalfOfContentOwner", RequestParameterType.Query)]
      public virtual string OnBehalfOfContentOwner { get; set; }

      [RequestParameter("pageToken", RequestParameterType.Query)]
      public virtual string PageToken { get; set; }

      [RequestParameter("regionCode", RequestParameterType.Query)]
      public virtual string RegionCode { get; set; }

      [RequestParameter("videoCategoryId", RequestParameterType.Query)]
      public virtual string VideoCategoryId { get; set; }

      public override string MethodName => "list";

      public override string HttpMethod => "GET";

      public override string RestPath => "videos";

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
        this.RequestParameters.Add("chart", (IParameter) new Parameter()
        {
          Name = "chart",
          IsRequired = false,
          ParameterType = "query",
          DefaultValue = (string) null,
          Pattern = (string) null
        });
        this.RequestParameters.Add("hl", (IParameter) new Parameter()
        {
          Name = "hl",
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
        this.RequestParameters.Add("locale", (IParameter) new Parameter()
        {
          Name = "locale",
          IsRequired = false,
          ParameterType = "query",
          DefaultValue = (string) null,
          Pattern = (string) null
        });
        this.RequestParameters.Add("maxHeight", (IParameter) new Parameter()
        {
          Name = "maxHeight",
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
        this.RequestParameters.Add("maxWidth", (IParameter) new Parameter()
        {
          Name = "maxWidth",
          IsRequired = false,
          ParameterType = "query",
          DefaultValue = (string) null,
          Pattern = (string) null
        });
        this.RequestParameters.Add("myRating", (IParameter) new Parameter()
        {
          Name = "myRating",
          IsRequired = false,
          ParameterType = "query",
          DefaultValue = (string) null,
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
        this.RequestParameters.Add("pageToken", (IParameter) new Parameter()
        {
          Name = "pageToken",
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
        this.RequestParameters.Add("videoCategoryId", (IParameter) new Parameter()
        {
          Name = "videoCategoryId",
          IsRequired = false,
          ParameterType = "query",
          DefaultValue = "0",
          Pattern = (string) null
        });
      }

      public enum ChartEnum
      {
        [StringValue("mostPopular")] MostPopular,
      }

      public enum MyRatingEnum
      {
        [StringValue("dislike")] Dislike,
        [StringValue("like")] Like,
      }
    }

    public class RateRequest : YouTubeBaseServiceRequest<string>
    {
      public RateRequest(
        IClientService service,
        string id,
        VideosResource.RateRequest.RatingEnum rating)
        : base(service)
      {
        this.Id = id;
        this.Rating = rating;
        this.InitParameters();
      }

      [RequestParameter("id", RequestParameterType.Query)]
      public virtual string Id { get; private set; }

      [RequestParameter("rating", RequestParameterType.Query)]
      public virtual VideosResource.RateRequest.RatingEnum Rating { get; private set; }

      public override string MethodName => "rate";

      public override string HttpMethod => "POST";

      public override string RestPath => "videos/rate";

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
        this.RequestParameters.Add("rating", (IParameter) new Parameter()
        {
          Name = "rating",
          IsRequired = true,
          ParameterType = "query",
          DefaultValue = (string) null,
          Pattern = (string) null
        });
      }

      public enum RatingEnum
      {
        [StringValue("dislike")] Dislike,
        [StringValue("like")] Like,
        [StringValue("none")] None,
      }
    }

    public class ReportAbuseRequest : YouTubeBaseServiceRequest<string>
    {
      public ReportAbuseRequest(IClientService service, VideoAbuseReport body)
        : base(service)
      {
        this.Body = body;
        this.InitParameters();
      }

      [RequestParameter("onBehalfOfContentOwner", RequestParameterType.Query)]
      public virtual string OnBehalfOfContentOwner { get; set; }

      private VideoAbuseReport Body { get; set; }

      protected override object GetBody() => (object) this.Body;

      public override string MethodName => "reportAbuse";

      public override string HttpMethod => "POST";

      public override string RestPath => "videos/reportAbuse";

      protected override void InitParameters()
      {
        base.InitParameters();
        this.RequestParameters.Add("onBehalfOfContentOwner", (IParameter) new Parameter()
        {
          Name = "onBehalfOfContentOwner",
          IsRequired = false,
          ParameterType = "query",
          DefaultValue = (string) null,
          Pattern = (string) null
        });
      }
    }

    public class UpdateRequest : YouTubeBaseServiceRequest<Video>
    {
      public UpdateRequest(IClientService service, Video body, string part)
        : base(service)
      {
        this.Part = part;
        this.Body = body;
        this.InitParameters();
      }

      [RequestParameter("part", RequestParameterType.Query)]
      public virtual string Part { get; private set; }

      [RequestParameter("onBehalfOfContentOwner", RequestParameterType.Query)]
      public virtual string OnBehalfOfContentOwner { get; set; }

      private Video Body { get; set; }

      protected override object GetBody() => (object) this.Body;

      public override string MethodName => "update";

      public override string HttpMethod => "PUT";

      public override string RestPath => "videos";

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
        this.RequestParameters.Add("onBehalfOfContentOwner", (IParameter) new Parameter()
        {
          Name = "onBehalfOfContentOwner",
          IsRequired = false,
          ParameterType = "query",
          DefaultValue = (string) null,
          Pattern = (string) null
        });
      }
    }
  }
}
