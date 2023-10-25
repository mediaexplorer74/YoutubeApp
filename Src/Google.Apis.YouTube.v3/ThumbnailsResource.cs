// Decompiled with JetBrains decompiler
// Type: Google.Apis.YouTube.v3.ThumbnailsResource
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
  public class ThumbnailsResource
  {
    private const string Resource = "thumbnails";
    private readonly IClientService service;

    public ThumbnailsResource(IClientService service) => this.service = service;

    public virtual ThumbnailsResource.SetRequest Set(string videoId) => new ThumbnailsResource.SetRequest(this.service, videoId);

    public virtual ThumbnailsResource.SetMediaUpload Set(
      string videoId,
      Stream stream,
      string contentType)
    {
      return new ThumbnailsResource.SetMediaUpload(this.service, videoId, stream, contentType);
    }

    public class SetRequest : YouTubeBaseServiceRequest<ThumbnailSetResponse>
    {
      public SetRequest(IClientService service, string videoId)
        : base(service)
      {
        this.VideoId = videoId;
        this.InitParameters();
      }

      [RequestParameter("videoId", RequestParameterType.Query)]
      public virtual string VideoId { get; private set; }

      [RequestParameter("onBehalfOfContentOwner", RequestParameterType.Query)]
      public virtual string OnBehalfOfContentOwner { get; set; }

      public override string MethodName => "set";

      public override string HttpMethod => "POST";

      public override string RestPath => "thumbnails/set";

      protected override void InitParameters()
      {
        base.InitParameters();
        this.RequestParameters.Add("videoId", (IParameter) new Parameter()
        {
          Name = "videoId",
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

    public class SetMediaUpload : ResumableUpload<string, ThumbnailSetResponse>
    {
      [RequestParameter("alt", RequestParameterType.Query)]
      public virtual ThumbnailsResource.SetMediaUpload.AltEnum? Alt { get; set; }

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

      [RequestParameter("videoId", RequestParameterType.Query)]
      public virtual string VideoId { get; private set; }

      [RequestParameter("onBehalfOfContentOwner", RequestParameterType.Query)]
      public virtual string OnBehalfOfContentOwner { get; set; }

      public SetMediaUpload(
        IClientService service,
        string videoId,
        Stream stream,
        string contentType)
        : base(service, string.Format("/{0}/{1}{2}", (object) "upload", (object) service.BasePath, (object) "thumbnails/set"), "POST", stream, contentType)
      {
        this.VideoId = videoId;
      }

      public enum AltEnum
      {
        [StringValue("json")] Json,
      }
    }
  }
}
