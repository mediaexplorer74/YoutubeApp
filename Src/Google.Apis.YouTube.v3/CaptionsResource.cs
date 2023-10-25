// Decompiled with JetBrains decompiler
// Type: Google.Apis.YouTube.v3.CaptionsResource
// Assembly: Google.Apis.YouTube.v3, Version=1.29.2.1006, Culture=neutral, PublicKeyToken=4b01fa6e34db77ab
// MVID: 9E53E5C1-D9AB-4142-94A7-8A2435650008
// Assembly location: C:\Users\Admin\Desktop\re\YoutubeApp\Google.Apis.YouTube.v3.dll

using Google.Apis.Discovery;
using Google.Apis.Download;
using Google.Apis.Services;
using Google.Apis.Upload;
using Google.Apis.Util;
using Google.Apis.YouTube.v3.Data;
using System.IO;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace Google.Apis.YouTube.v3
{
  public class CaptionsResource
  {
    private const string Resource = "captions";
    private readonly IClientService service;

    public CaptionsResource(IClientService service) => this.service = service;

    public virtual CaptionsResource.DeleteRequest Delete(string id) => new CaptionsResource.DeleteRequest(this.service, id);

    public virtual CaptionsResource.DownloadRequest Download(string id) => new CaptionsResource.DownloadRequest(this.service, id);

    public virtual CaptionsResource.InsertRequest Insert(Caption body, string part) => new CaptionsResource.InsertRequest(this.service, body, part);

    public virtual CaptionsResource.InsertMediaUpload Insert(
      Caption body,
      string part,
      Stream stream,
      string contentType)
    {
      return new CaptionsResource.InsertMediaUpload(this.service, body, part, stream, contentType);
    }

    public virtual CaptionsResource.ListRequest List(string part, string videoId) => new CaptionsResource.ListRequest(this.service, part, videoId);

    public virtual CaptionsResource.UpdateRequest Update(Caption body, string part) => new CaptionsResource.UpdateRequest(this.service, body, part);

    public virtual CaptionsResource.UpdateMediaUpload Update(
      Caption body,
      string part,
      Stream stream,
      string contentType)
    {
      return new CaptionsResource.UpdateMediaUpload(this.service, body, part, stream, contentType);
    }

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

      [RequestParameter("onBehalfOf", RequestParameterType.Query)]
      public virtual string OnBehalfOf { get; set; }

      [RequestParameter("onBehalfOfContentOwner", RequestParameterType.Query)]
      public virtual string OnBehalfOfContentOwner { get; set; }

      public override string MethodName => "delete";

      public override string HttpMethod => "DELETE";

      public override string RestPath => "captions";

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
        this.RequestParameters.Add("onBehalfOf", (IParameter) new Parameter()
        {
          Name = "onBehalfOf",
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
      }
    }

    public class DownloadRequest : YouTubeBaseServiceRequest<string>
    {
      public DownloadRequest(IClientService service, string id)
        : base(service)
      {
        this.Id = id;
        this.MediaDownloader = (IMediaDownloader) new MediaDownloader(service);
        this.InitParameters();
      }

      [RequestParameter("id", RequestParameterType.Path)]
      public virtual string Id { get; private set; }

      [RequestParameter("onBehalfOf", RequestParameterType.Query)]
      public virtual string OnBehalfOf { get; set; }

      [RequestParameter("onBehalfOfContentOwner", RequestParameterType.Query)]
      public virtual string OnBehalfOfContentOwner { get; set; }

      [RequestParameter("tfmt", RequestParameterType.Query)]
      public virtual CaptionsResource.DownloadRequest.TfmtEnum? Tfmt { get; set; }

      [RequestParameter("tlang", RequestParameterType.Query)]
      public virtual string Tlang { get; set; }

      public override string MethodName => "download";

      public override string HttpMethod => "GET";

      public override string RestPath => "captions/{id}";

      protected override void InitParameters()
      {
        base.InitParameters();
        this.RequestParameters.Add("id", (IParameter) new Parameter()
        {
          Name = "id",
          IsRequired = true,
          ParameterType = "path",
          DefaultValue = (string) null,
          Pattern = (string) null
        });
        this.RequestParameters.Add("onBehalfOf", (IParameter) new Parameter()
        {
          Name = "onBehalfOf",
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
        this.RequestParameters.Add("tfmt", (IParameter) new Parameter()
        {
          Name = "tfmt",
          IsRequired = false,
          ParameterType = "query",
          DefaultValue = (string) null,
          Pattern = (string) null
        });
        this.RequestParameters.Add("tlang", (IParameter) new Parameter()
        {
          Name = "tlang",
          IsRequired = false,
          ParameterType = "query",
          DefaultValue = (string) null,
          Pattern = (string) null
        });
      }

      public IMediaDownloader MediaDownloader { get; private set; }

      public virtual void Download(Stream stream) => this.MediaDownloader.Download(this.GenerateRequestUri(), stream);

      public virtual IDownloadProgress DownloadWithStatus(Stream stream) => this.MediaDownloader.Download(this.GenerateRequestUri(), stream);

      public virtual Task<IDownloadProgress> DownloadAsync(Stream stream) => this.MediaDownloader.DownloadAsync(this.GenerateRequestUri(), stream);

      public virtual Task<IDownloadProgress> DownloadAsync(
        Stream stream,
        CancellationToken cancellationToken)
      {
        return this.MediaDownloader.DownloadAsync(this.GenerateRequestUri(), stream, cancellationToken);
      }

      public virtual IDownloadProgress DownloadRange(Stream stream, RangeHeaderValue range) => new MediaDownloader(this.Service)
      {
        Range = range
      }.Download(this.GenerateRequestUri(), stream);

      public virtual Task<IDownloadProgress> DownloadRangeAsync(
        Stream stream,
        RangeHeaderValue range,
        CancellationToken cancellationToken = default (CancellationToken))
      {
        return new MediaDownloader(this.Service)
        {
          Range = range
        }.DownloadAsync(this.GenerateRequestUri(), stream, cancellationToken);
      }

      public enum TfmtEnum
      {
        [StringValue("sbv")] Sbv,
        [StringValue("scc")] Scc,
        [StringValue("srt")] Srt,
        [StringValue("ttml")] Ttml,
        [StringValue("vtt")] Vtt,
      }
    }

    public class InsertRequest : YouTubeBaseServiceRequest<Caption>
    {
      public InsertRequest(IClientService service, Caption body, string part)
        : base(service)
      {
        this.Part = part;
        this.Body = body;
        this.InitParameters();
      }

      [RequestParameter("part", RequestParameterType.Query)]
      public virtual string Part { get; private set; }

      [RequestParameter("onBehalfOf", RequestParameterType.Query)]
      public virtual string OnBehalfOf { get; set; }

      [RequestParameter("onBehalfOfContentOwner", RequestParameterType.Query)]
      public virtual string OnBehalfOfContentOwner { get; set; }

      [RequestParameter("sync", RequestParameterType.Query)]
      public virtual bool? Sync { get; set; }

      private Caption Body { get; set; }

      protected override object GetBody() => (object) this.Body;

      public override string MethodName => "insert";

      public override string HttpMethod => "POST";

      public override string RestPath => "captions";

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
        this.RequestParameters.Add("onBehalfOf", (IParameter) new Parameter()
        {
          Name = "onBehalfOf",
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
        this.RequestParameters.Add("sync", (IParameter) new Parameter()
        {
          Name = "sync",
          IsRequired = false,
          ParameterType = "query",
          DefaultValue = (string) null,
          Pattern = (string) null
        });
      }
    }

    public class InsertMediaUpload : ResumableUpload<Caption, Caption>
    {
      [RequestParameter("alt", RequestParameterType.Query)]
      public virtual CaptionsResource.InsertMediaUpload.AltEnum? Alt { get; set; }

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

      [RequestParameter("onBehalfOf", RequestParameterType.Query)]
      public virtual string OnBehalfOf { get; set; }

      [RequestParameter("onBehalfOfContentOwner", RequestParameterType.Query)]
      public virtual string OnBehalfOfContentOwner { get; set; }

      [RequestParameter("sync", RequestParameterType.Query)]
      public virtual bool? Sync { get; set; }

      public InsertMediaUpload(
        IClientService service,
        Caption body,
        string part,
        Stream stream,
        string contentType)
        : base(service, string.Format("/{0}/{1}{2}", (object) "upload", (object) service.BasePath, (object) "captions"), "POST", stream, contentType)
      {
        this.Part = part;
        this.Body = body;
      }

      public enum AltEnum
      {
        [StringValue("json")] Json,
      }
    }

    public class ListRequest : YouTubeBaseServiceRequest<CaptionListResponse>
    {
      public ListRequest(IClientService service, string part, string videoId)
        : base(service)
      {
        this.Part = part;
        this.VideoId = videoId;
        this.InitParameters();
      }

      [RequestParameter("part", RequestParameterType.Query)]
      public virtual string Part { get; private set; }

      [RequestParameter("videoId", RequestParameterType.Query)]
      public virtual string VideoId { get; private set; }

      [RequestParameter("id", RequestParameterType.Query)]
      public virtual string Id { get; set; }

      [RequestParameter("onBehalfOf", RequestParameterType.Query)]
      public virtual string OnBehalfOf { get; set; }

      [RequestParameter("onBehalfOfContentOwner", RequestParameterType.Query)]
      public virtual string OnBehalfOfContentOwner { get; set; }

      public override string MethodName => "list";

      public override string HttpMethod => "GET";

      public override string RestPath => "captions";

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
        this.RequestParameters.Add("videoId", (IParameter) new Parameter()
        {
          Name = "videoId",
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
        this.RequestParameters.Add("onBehalfOf", (IParameter) new Parameter()
        {
          Name = "onBehalfOf",
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
      }
    }

    public class UpdateRequest : YouTubeBaseServiceRequest<Caption>
    {
      public UpdateRequest(IClientService service, Caption body, string part)
        : base(service)
      {
        this.Part = part;
        this.Body = body;
        this.InitParameters();
      }

      [RequestParameter("part", RequestParameterType.Query)]
      public virtual string Part { get; private set; }

      [RequestParameter("onBehalfOf", RequestParameterType.Query)]
      public virtual string OnBehalfOf { get; set; }

      [RequestParameter("onBehalfOfContentOwner", RequestParameterType.Query)]
      public virtual string OnBehalfOfContentOwner { get; set; }

      [RequestParameter("sync", RequestParameterType.Query)]
      public virtual bool? Sync { get; set; }

      private Caption Body { get; set; }

      protected override object GetBody() => (object) this.Body;

      public override string MethodName => "update";

      public override string HttpMethod => "PUT";

      public override string RestPath => "captions";

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
        this.RequestParameters.Add("onBehalfOf", (IParameter) new Parameter()
        {
          Name = "onBehalfOf",
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
        this.RequestParameters.Add("sync", (IParameter) new Parameter()
        {
          Name = "sync",
          IsRequired = false,
          ParameterType = "query",
          DefaultValue = (string) null,
          Pattern = (string) null
        });
      }
    }

    public class UpdateMediaUpload : ResumableUpload<Caption, Caption>
    {
      [RequestParameter("alt", RequestParameterType.Query)]
      public virtual CaptionsResource.UpdateMediaUpload.AltEnum? Alt { get; set; }

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

      [RequestParameter("onBehalfOf", RequestParameterType.Query)]
      public virtual string OnBehalfOf { get; set; }

      [RequestParameter("onBehalfOfContentOwner", RequestParameterType.Query)]
      public virtual string OnBehalfOfContentOwner { get; set; }

      [RequestParameter("sync", RequestParameterType.Query)]
      public virtual bool? Sync { get; set; }

      public UpdateMediaUpload(
        IClientService service,
        Caption body,
        string part,
        Stream stream,
        string contentType)
        : base(service, string.Format("/{0}/{1}{2}", (object) "upload", (object) service.BasePath, (object) "captions"), "PUT", stream, contentType)
      {
        this.Part = part;
        this.Body = body;
      }

      public enum AltEnum
      {
        [StringValue("json")] Json,
      }
    }
  }
}
