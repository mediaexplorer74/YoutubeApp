// Decompiled with JetBrains decompiler
// Type: Google.Apis.YouTube.v3.ChannelBannersResource
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
  public class ChannelBannersResource
  {
    private const string Resource = "channelBanners";
    private readonly IClientService service;

    public ChannelBannersResource(IClientService service) => this.service = service;

    public virtual ChannelBannersResource.InsertRequest Insert(ChannelBannerResource body) => new ChannelBannersResource.InsertRequest(this.service, body);

    public virtual ChannelBannersResource.InsertMediaUpload Insert(
      ChannelBannerResource body,
      Stream stream,
      string contentType)
    {
      return new ChannelBannersResource.InsertMediaUpload(this.service, body, stream, contentType);
    }

    public class InsertRequest : YouTubeBaseServiceRequest<ChannelBannerResource>
    {
      public InsertRequest(IClientService service, ChannelBannerResource body)
        : base(service)
      {
        this.Body = body;
        this.InitParameters();
      }

      [RequestParameter("channelId", RequestParameterType.Query)]
      public virtual string ChannelId { get; set; }

      [RequestParameter("onBehalfOfContentOwner", RequestParameterType.Query)]
      public virtual string OnBehalfOfContentOwner { get; set; }

      private ChannelBannerResource Body { get; set; }

      protected override object GetBody() => (object) this.Body;

      public override string MethodName => "insert";

      public override string HttpMethod => "POST";

      public override string RestPath => "channelBanners/insert";

      protected override void InitParameters()
      {
        base.InitParameters();
        this.RequestParameters.Add("channelId", (IParameter) new Parameter()
        {
          Name = "channelId",
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

    public class InsertMediaUpload : ResumableUpload<ChannelBannerResource, ChannelBannerResource>
    {
      [RequestParameter("alt", RequestParameterType.Query)]
      public virtual ChannelBannersResource.InsertMediaUpload.AltEnum? Alt { get; set; }

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

      [RequestParameter("channelId", RequestParameterType.Query)]
      public virtual string ChannelId { get; set; }

      [RequestParameter("onBehalfOfContentOwner", RequestParameterType.Query)]
      public virtual string OnBehalfOfContentOwner { get; set; }

      public InsertMediaUpload(
        IClientService service,
        ChannelBannerResource body,
        Stream stream,
        string contentType)
        : base(service, string.Format("/{0}/{1}{2}", (object) "upload", (object) service.BasePath, (object) "channelBanners/insert"), "POST", stream, contentType)
      {
        this.Body = body;
      }

      public enum AltEnum
      {
        [StringValue("json")] Json,
      }
    }
  }
}
