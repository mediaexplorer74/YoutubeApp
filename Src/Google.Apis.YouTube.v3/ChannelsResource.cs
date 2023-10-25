// Decompiled with JetBrains decompiler
// Type: Google.Apis.YouTube.v3.ChannelsResource
// Assembly: Google.Apis.YouTube.v3, Version=1.29.2.1006, Culture=neutral, PublicKeyToken=4b01fa6e34db77ab
// MVID: 9E53E5C1-D9AB-4142-94A7-8A2435650008
// Assembly location: C:\Users\Admin\Desktop\re\YoutubeApp\Google.Apis.YouTube.v3.dll

using Google.Apis.Discovery;
using Google.Apis.Services;
using Google.Apis.Util;
using Google.Apis.YouTube.v3.Data;

namespace Google.Apis.YouTube.v3
{
  public class ChannelsResource
  {
    private const string Resource = "channels";
    private readonly IClientService service;

    public ChannelsResource(IClientService service) => this.service = service;

    public virtual ChannelsResource.ListRequest List(string part) => new ChannelsResource.ListRequest(this.service, part);

    public virtual ChannelsResource.UpdateRequest Update(Channel body, string part) => new ChannelsResource.UpdateRequest(this.service, body, part);

    public class ListRequest : YouTubeBaseServiceRequest<ChannelListResponse>
    {
      public ListRequest(IClientService service, string part)
        : base(service)
      {
        this.Part = part;
        this.InitParameters();
      }

      [RequestParameter("part", RequestParameterType.Query)]
      public virtual string Part { get; private set; }

      [RequestParameter("categoryId", RequestParameterType.Query)]
      public virtual string CategoryId { get; set; }

      [RequestParameter("forUsername", RequestParameterType.Query)]
      public virtual string ForUsername { get; set; }

      [RequestParameter("hl", RequestParameterType.Query)]
      public virtual string Hl { get; set; }

      [RequestParameter("id", RequestParameterType.Query)]
      public virtual string Id { get; set; }

      [RequestParameter("managedByMe", RequestParameterType.Query)]
      public virtual bool? ManagedByMe { get; set; }

      [RequestParameter("maxResults", RequestParameterType.Query)]
      public virtual long? MaxResults { get; set; }

      [RequestParameter("mine", RequestParameterType.Query)]
      public virtual bool? Mine { get; set; }

      [RequestParameter("mySubscribers", RequestParameterType.Query)]
      public virtual bool? MySubscribers { get; set; }

      [RequestParameter("onBehalfOfContentOwner", RequestParameterType.Query)]
      public virtual string OnBehalfOfContentOwner { get; set; }

      [RequestParameter("pageToken", RequestParameterType.Query)]
      public virtual string PageToken { get; set; }

      public override string MethodName => "list";

      public override string HttpMethod => "GET";

      public override string RestPath => "channels";

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
        this.RequestParameters.Add("categoryId", (IParameter) new Parameter()
        {
          Name = "categoryId",
          IsRequired = false,
          ParameterType = "query",
          DefaultValue = (string) null,
          Pattern = (string) null
        });
        this.RequestParameters.Add("forUsername", (IParameter) new Parameter()
        {
          Name = "forUsername",
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
        this.RequestParameters.Add("managedByMe", (IParameter) new Parameter()
        {
          Name = "managedByMe",
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
        this.RequestParameters.Add("mine", (IParameter) new Parameter()
        {
          Name = "mine",
          IsRequired = false,
          ParameterType = "query",
          DefaultValue = (string) null,
          Pattern = (string) null
        });
        this.RequestParameters.Add("mySubscribers", (IParameter) new Parameter()
        {
          Name = "mySubscribers",
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
      }
    }

    public class UpdateRequest : YouTubeBaseServiceRequest<Channel>
    {
      public UpdateRequest(IClientService service, Channel body, string part)
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

      private Channel Body { get; set; }

      protected override object GetBody() => (object) this.Body;

      public override string MethodName => "update";

      public override string HttpMethod => "PUT";

      public override string RestPath => "channels";

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
