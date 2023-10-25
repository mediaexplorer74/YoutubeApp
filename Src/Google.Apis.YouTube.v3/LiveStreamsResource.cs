// Decompiled with JetBrains decompiler
// Type: Google.Apis.YouTube.v3.LiveStreamsResource
// Assembly: Google.Apis.YouTube.v3, Version=1.29.2.1006, Culture=neutral, PublicKeyToken=4b01fa6e34db77ab
// MVID: 9E53E5C1-D9AB-4142-94A7-8A2435650008
// Assembly location: C:\Users\Admin\Desktop\re\YoutubeApp\Google.Apis.YouTube.v3.dll

using Google.Apis.Discovery;
using Google.Apis.Services;
using Google.Apis.Util;
using Google.Apis.YouTube.v3.Data;

namespace Google.Apis.YouTube.v3
{
  public class LiveStreamsResource
  {
    private const string Resource = "liveStreams";
    private readonly IClientService service;

    public LiveStreamsResource(IClientService service) => this.service = service;

    public virtual LiveStreamsResource.DeleteRequest Delete(string id) => new LiveStreamsResource.DeleteRequest(this.service, id);

    public virtual LiveStreamsResource.InsertRequest Insert(LiveStream body, string part) => new LiveStreamsResource.InsertRequest(this.service, body, part);

    public virtual LiveStreamsResource.ListRequest List(string part) => new LiveStreamsResource.ListRequest(this.service, part);

    public virtual LiveStreamsResource.UpdateRequest Update(LiveStream body, string part) => new LiveStreamsResource.UpdateRequest(this.service, body, part);

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

      [RequestParameter("onBehalfOfContentOwnerChannel", RequestParameterType.Query)]
      public virtual string OnBehalfOfContentOwnerChannel { get; set; }

      public override string MethodName => "delete";

      public override string HttpMethod => "DELETE";

      public override string RestPath => "liveStreams";

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
        this.RequestParameters.Add("onBehalfOfContentOwnerChannel", (IParameter) new Parameter()
        {
          Name = "onBehalfOfContentOwnerChannel",
          IsRequired = false,
          ParameterType = "query",
          DefaultValue = (string) null,
          Pattern = (string) null
        });
      }
    }

    public class InsertRequest : YouTubeBaseServiceRequest<LiveStream>
    {
      public InsertRequest(IClientService service, LiveStream body, string part)
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

      [RequestParameter("onBehalfOfContentOwnerChannel", RequestParameterType.Query)]
      public virtual string OnBehalfOfContentOwnerChannel { get; set; }

      private LiveStream Body { get; set; }

      protected override object GetBody() => (object) this.Body;

      public override string MethodName => "insert";

      public override string HttpMethod => "POST";

      public override string RestPath => "liveStreams";

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
        this.RequestParameters.Add("onBehalfOfContentOwnerChannel", (IParameter) new Parameter()
        {
          Name = "onBehalfOfContentOwnerChannel",
          IsRequired = false,
          ParameterType = "query",
          DefaultValue = (string) null,
          Pattern = (string) null
        });
      }
    }

    public class ListRequest : YouTubeBaseServiceRequest<LiveStreamListResponse>
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

      [RequestParameter("mine", RequestParameterType.Query)]
      public virtual bool? Mine { get; set; }

      [RequestParameter("onBehalfOfContentOwner", RequestParameterType.Query)]
      public virtual string OnBehalfOfContentOwner { get; set; }

      [RequestParameter("onBehalfOfContentOwnerChannel", RequestParameterType.Query)]
      public virtual string OnBehalfOfContentOwnerChannel { get; set; }

      [RequestParameter("pageToken", RequestParameterType.Query)]
      public virtual string PageToken { get; set; }

      public override string MethodName => "list";

      public override string HttpMethod => "GET";

      public override string RestPath => "liveStreams";

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

    public class UpdateRequest : YouTubeBaseServiceRequest<LiveStream>
    {
      public UpdateRequest(IClientService service, LiveStream body, string part)
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

      [RequestParameter("onBehalfOfContentOwnerChannel", RequestParameterType.Query)]
      public virtual string OnBehalfOfContentOwnerChannel { get; set; }

      private LiveStream Body { get; set; }

      protected override object GetBody() => (object) this.Body;

      public override string MethodName => "update";

      public override string HttpMethod => "PUT";

      public override string RestPath => "liveStreams";

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
        this.RequestParameters.Add("onBehalfOfContentOwnerChannel", (IParameter) new Parameter()
        {
          Name = "onBehalfOfContentOwnerChannel",
          IsRequired = false,
          ParameterType = "query",
          DefaultValue = (string) null,
          Pattern = (string) null
        });
      }
    }
  }
}
