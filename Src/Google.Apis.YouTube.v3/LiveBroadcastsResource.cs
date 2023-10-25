// Decompiled with JetBrains decompiler
// Type: Google.Apis.YouTube.v3.LiveBroadcastsResource
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
  public class LiveBroadcastsResource
  {
    private const string Resource = "liveBroadcasts";
    private readonly IClientService service;

    public LiveBroadcastsResource(IClientService service) => this.service = service;

    public virtual LiveBroadcastsResource.BindRequest Bind(string id, string part) => new LiveBroadcastsResource.BindRequest(this.service, id, part);

    public virtual LiveBroadcastsResource.ControlRequest Control(string id, string part) => new LiveBroadcastsResource.ControlRequest(this.service, id, part);

    public virtual LiveBroadcastsResource.DeleteRequest Delete(string id) => new LiveBroadcastsResource.DeleteRequest(this.service, id);

    public virtual LiveBroadcastsResource.InsertRequest Insert(LiveBroadcast body, string part) => new LiveBroadcastsResource.InsertRequest(this.service, body, part);

    public virtual LiveBroadcastsResource.ListRequest List(string part) => new LiveBroadcastsResource.ListRequest(this.service, part);

    public virtual LiveBroadcastsResource.TransitionRequest Transition(
      LiveBroadcastsResource.TransitionRequest.BroadcastStatusEnum broadcastStatus,
      string id,
      string part)
    {
      return new LiveBroadcastsResource.TransitionRequest(this.service, broadcastStatus, id, part);
    }

    public virtual LiveBroadcastsResource.UpdateRequest Update(LiveBroadcast body, string part) => new LiveBroadcastsResource.UpdateRequest(this.service, body, part);

    public class BindRequest : YouTubeBaseServiceRequest<LiveBroadcast>
    {
      public BindRequest(IClientService service, string id, string part)
        : base(service)
      {
        this.Id = id;
        this.Part = part;
        this.InitParameters();
      }

      [RequestParameter("id", RequestParameterType.Query)]
      public virtual string Id { get; private set; }

      [RequestParameter("part", RequestParameterType.Query)]
      public virtual string Part { get; private set; }

      [RequestParameter("onBehalfOfContentOwner", RequestParameterType.Query)]
      public virtual string OnBehalfOfContentOwner { get; set; }

      [RequestParameter("onBehalfOfContentOwnerChannel", RequestParameterType.Query)]
      public virtual string OnBehalfOfContentOwnerChannel { get; set; }

      [RequestParameter("streamId", RequestParameterType.Query)]
      public virtual string StreamId { get; set; }

      public override string MethodName => "bind";

      public override string HttpMethod => "POST";

      public override string RestPath => "liveBroadcasts/bind";

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
        this.RequestParameters.Add("streamId", (IParameter) new Parameter()
        {
          Name = "streamId",
          IsRequired = false,
          ParameterType = "query",
          DefaultValue = (string) null,
          Pattern = (string) null
        });
      }
    }

    public class ControlRequest : YouTubeBaseServiceRequest<LiveBroadcast>
    {
      public ControlRequest(IClientService service, string id, string part)
        : base(service)
      {
        this.Id = id;
        this.Part = part;
        this.InitParameters();
      }

      [RequestParameter("id", RequestParameterType.Query)]
      public virtual string Id { get; private set; }

      [RequestParameter("part", RequestParameterType.Query)]
      public virtual string Part { get; private set; }

      [RequestParameter("displaySlate", RequestParameterType.Query)]
      public virtual bool? DisplaySlate { get; set; }

      [RequestParameter("offsetTimeMs", RequestParameterType.Query)]
      public virtual ulong? OffsetTimeMs { get; set; }

      [RequestParameter("onBehalfOfContentOwner", RequestParameterType.Query)]
      public virtual string OnBehalfOfContentOwner { get; set; }

      [RequestParameter("onBehalfOfContentOwnerChannel", RequestParameterType.Query)]
      public virtual string OnBehalfOfContentOwnerChannel { get; set; }

      [RequestParameter("walltime", RequestParameterType.Query)]
      public virtual DateTime? Walltime { get; set; }

      public override string MethodName => "control";

      public override string HttpMethod => "POST";

      public override string RestPath => "liveBroadcasts/control";

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
        this.RequestParameters.Add("part", (IParameter) new Parameter()
        {
          Name = "part",
          IsRequired = true,
          ParameterType = "query",
          DefaultValue = (string) null,
          Pattern = (string) null
        });
        this.RequestParameters.Add("displaySlate", (IParameter) new Parameter()
        {
          Name = "displaySlate",
          IsRequired = false,
          ParameterType = "query",
          DefaultValue = (string) null,
          Pattern = (string) null
        });
        this.RequestParameters.Add("offsetTimeMs", (IParameter) new Parameter()
        {
          Name = "offsetTimeMs",
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
        this.RequestParameters.Add("walltime", (IParameter) new Parameter()
        {
          Name = "walltime",
          IsRequired = false,
          ParameterType = "query",
          DefaultValue = (string) null,
          Pattern = (string) null
        });
      }
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

      [RequestParameter("onBehalfOfContentOwner", RequestParameterType.Query)]
      public virtual string OnBehalfOfContentOwner { get; set; }

      [RequestParameter("onBehalfOfContentOwnerChannel", RequestParameterType.Query)]
      public virtual string OnBehalfOfContentOwnerChannel { get; set; }

      public override string MethodName => "delete";

      public override string HttpMethod => "DELETE";

      public override string RestPath => "liveBroadcasts";

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

    public class InsertRequest : YouTubeBaseServiceRequest<LiveBroadcast>
    {
      public InsertRequest(IClientService service, LiveBroadcast body, string part)
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

      private LiveBroadcast Body { get; set; }

      protected override object GetBody() => (object) this.Body;

      public override string MethodName => "insert";

      public override string HttpMethod => "POST";

      public override string RestPath => "liveBroadcasts";

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

    public class ListRequest : YouTubeBaseServiceRequest<LiveBroadcastListResponse>
    {
      public ListRequest(IClientService service, string part)
        : base(service)
      {
        this.Part = part;
        this.InitParameters();
      }

      [RequestParameter("part", RequestParameterType.Query)]
      public virtual string Part { get; private set; }

      [RequestParameter("broadcastStatus", RequestParameterType.Query)]
      public virtual LiveBroadcastsResource.ListRequest.BroadcastStatusEnum? BroadcastStatus { get; set; }

      [RequestParameter("broadcastType", RequestParameterType.Query)]
      public virtual LiveBroadcastsResource.ListRequest.BroadcastTypeEnum? BroadcastType { get; set; }

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

      public override string RestPath => "liveBroadcasts";

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
        this.RequestParameters.Add("broadcastStatus", (IParameter) new Parameter()
        {
          Name = "broadcastStatus",
          IsRequired = false,
          ParameterType = "query",
          DefaultValue = (string) null,
          Pattern = (string) null
        });
        this.RequestParameters.Add("broadcastType", (IParameter) new Parameter()
        {
          Name = "broadcastType",
          IsRequired = false,
          ParameterType = "query",
          DefaultValue = "BROADCAST_TYPE_FILTER_EVENT",
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

      public enum BroadcastStatusEnum
      {
        [StringValue("active")] Active,
        [StringValue("all")] All,
        [StringValue("completed")] Completed,
        [StringValue("upcoming")] Upcoming,
      }

      public enum BroadcastTypeEnum
      {
        [StringValue("all")] All,
        [StringValue("event")] Event__,
        [StringValue("persistent")] Persistent,
      }
    }

    public class TransitionRequest : YouTubeBaseServiceRequest<LiveBroadcast>
    {
      public TransitionRequest(
        IClientService service,
        LiveBroadcastsResource.TransitionRequest.BroadcastStatusEnum broadcastStatus,
        string id,
        string part)
        : base(service)
      {
        this.BroadcastStatus = broadcastStatus;
        this.Id = id;
        this.Part = part;
        this.InitParameters();
      }

      [RequestParameter("broadcastStatus", RequestParameterType.Query)]
      public virtual LiveBroadcastsResource.TransitionRequest.BroadcastStatusEnum BroadcastStatus { get; private set; }

      [RequestParameter("id", RequestParameterType.Query)]
      public virtual string Id { get; private set; }

      [RequestParameter("part", RequestParameterType.Query)]
      public virtual string Part { get; private set; }

      [RequestParameter("onBehalfOfContentOwner", RequestParameterType.Query)]
      public virtual string OnBehalfOfContentOwner { get; set; }

      [RequestParameter("onBehalfOfContentOwnerChannel", RequestParameterType.Query)]
      public virtual string OnBehalfOfContentOwnerChannel { get; set; }

      public override string MethodName => "transition";

      public override string HttpMethod => "POST";

      public override string RestPath => "liveBroadcasts/transition";

      protected override void InitParameters()
      {
        base.InitParameters();
        this.RequestParameters.Add("broadcastStatus", (IParameter) new Parameter()
        {
          Name = "broadcastStatus",
          IsRequired = true,
          ParameterType = "query",
          DefaultValue = (string) null,
          Pattern = (string) null
        });
        this.RequestParameters.Add("id", (IParameter) new Parameter()
        {
          Name = "id",
          IsRequired = true,
          ParameterType = "query",
          DefaultValue = (string) null,
          Pattern = (string) null
        });
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

      public enum BroadcastStatusEnum
      {
        [StringValue("complete")] Complete,
        [StringValue("live")] Live,
        [StringValue("testing")] Testing,
      }
    }

    public class UpdateRequest : YouTubeBaseServiceRequest<LiveBroadcast>
    {
      public UpdateRequest(IClientService service, LiveBroadcast body, string part)
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

      private LiveBroadcast Body { get; set; }

      protected override object GetBody() => (object) this.Body;

      public override string MethodName => "update";

      public override string HttpMethod => "PUT";

      public override string RestPath => "liveBroadcasts";

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
