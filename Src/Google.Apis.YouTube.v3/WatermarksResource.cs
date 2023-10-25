// Decompiled with JetBrains decompiler
// Type: Google.Apis.YouTube.v3.WatermarksResource
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
  public class WatermarksResource
  {
    private const string Resource = "watermarks";
    private readonly IClientService service;

    public WatermarksResource(IClientService service) => this.service = service;

    public virtual WatermarksResource.SetRequest Set(InvideoBranding body, string channelId) => new WatermarksResource.SetRequest(this.service, body, channelId);

    public virtual WatermarksResource.SetMediaUpload Set(
      InvideoBranding body,
      string channelId,
      Stream stream,
      string contentType)
    {
      return new WatermarksResource.SetMediaUpload(this.service, body, channelId, stream, contentType);
    }

    public virtual WatermarksResource.UnsetRequest Unset(string channelId) => new WatermarksResource.UnsetRequest(this.service, channelId);

    public class SetRequest : YouTubeBaseServiceRequest<string>
    {
      public SetRequest(IClientService service, InvideoBranding body, string channelId)
        : base(service)
      {
        this.ChannelId = channelId;
        this.Body = body;
        this.InitParameters();
      }

      [RequestParameter("channelId", RequestParameterType.Query)]
      public virtual string ChannelId { get; private set; }

      [RequestParameter("onBehalfOfContentOwner", RequestParameterType.Query)]
      public virtual string OnBehalfOfContentOwner { get; set; }

      private InvideoBranding Body { get; set; }

      protected override object GetBody() => (object) this.Body;

      public override string MethodName => "set";

      public override string HttpMethod => "POST";

      public override string RestPath => "watermarks/set";

      protected override void InitParameters()
      {
        base.InitParameters();
        this.RequestParameters.Add("channelId", (IParameter) new Parameter()
        {
          Name = "channelId",
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

    public class SetMediaUpload : ResumableUpload<InvideoBranding>
    {
      [RequestParameter("alt", RequestParameterType.Query)]
      public virtual WatermarksResource.SetMediaUpload.AltEnum? Alt { get; set; }

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
      public virtual string ChannelId { get; private set; }

      [RequestParameter("onBehalfOfContentOwner", RequestParameterType.Query)]
      public virtual string OnBehalfOfContentOwner { get; set; }

      public SetMediaUpload(
        IClientService service,
        InvideoBranding body,
        string channelId,
        Stream stream,
        string contentType)
        : base(service, string.Format("/{0}/{1}{2}", (object) "upload", (object) service.BasePath, (object) "watermarks/set"), "POST", stream, contentType)
      {
        this.ChannelId = channelId;
        this.Body = body;
      }

      public enum AltEnum
      {
        [StringValue("json")] Json,
      }
    }

    public class UnsetRequest : YouTubeBaseServiceRequest<string>
    {
      public UnsetRequest(IClientService service, string channelId)
        : base(service)
      {
        this.ChannelId = channelId;
        this.InitParameters();
      }

      [RequestParameter("channelId", RequestParameterType.Query)]
      public virtual string ChannelId { get; private set; }

      [RequestParameter("onBehalfOfContentOwner", RequestParameterType.Query)]
      public virtual string OnBehalfOfContentOwner { get; set; }

      public override string MethodName => "unset";

      public override string HttpMethod => "POST";

      public override string RestPath => "watermarks/unset";

      protected override void InitParameters()
      {
        base.InitParameters();
        this.RequestParameters.Add("channelId", (IParameter) new Parameter()
        {
          Name = "channelId",
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
