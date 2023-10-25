// Decompiled with JetBrains decompiler
// Type: Google.Apis.YouTube.v3.ActivitiesResource
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
  public class ActivitiesResource
  {
    private const string Resource = "activities";
    private readonly IClientService service;

    public ActivitiesResource(IClientService service) => this.service = service;

    public virtual ActivitiesResource.InsertRequest Insert(Activity body, string part) => new ActivitiesResource.InsertRequest(this.service, body, part);

    public virtual ActivitiesResource.ListRequest List(string part) => new ActivitiesResource.ListRequest(this.service, part);

    public class InsertRequest : YouTubeBaseServiceRequest<Activity>
    {
      public InsertRequest(IClientService service, Activity body, string part)
        : base(service)
      {
        this.Part = part;
        this.Body = body;
        this.InitParameters();
      }

      [RequestParameter("part", RequestParameterType.Query)]
      public virtual string Part { get; private set; }

      private Activity Body { get; set; }

      protected override object GetBody() => (object) this.Body;

      public override string MethodName => "insert";

      public override string HttpMethod => "POST";

      public override string RestPath => "activities";

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

    public class ListRequest : YouTubeBaseServiceRequest<ActivityListResponse>
    {
      public ListRequest(IClientService service, string part)
        : base(service)
      {
        this.Part = part;
        this.InitParameters();
      }

      [RequestParameter("part", RequestParameterType.Query)]
      public virtual string Part { get; private set; }

      [RequestParameter("channelId", RequestParameterType.Query)]
      public virtual string ChannelId { get; set; }

      [RequestParameter("home", RequestParameterType.Query)]
      public virtual bool? Home { get; set; }

      [RequestParameter("maxResults", RequestParameterType.Query)]
      public virtual long? MaxResults { get; set; }

      [RequestParameter("mine", RequestParameterType.Query)]
      public virtual bool? Mine { get; set; }

      [RequestParameter("pageToken", RequestParameterType.Query)]
      public virtual string PageToken { get; set; }

      [RequestParameter("publishedAfter", RequestParameterType.Query)]
      public virtual DateTime? PublishedAfter { get; set; }

      [RequestParameter("publishedBefore", RequestParameterType.Query)]
      public virtual DateTime? PublishedBefore { get; set; }

      [RequestParameter("regionCode", RequestParameterType.Query)]
      public virtual string RegionCode { get; set; }

      public override string MethodName => "list";

      public override string HttpMethod => "GET";

      public override string RestPath => "activities";

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
        this.RequestParameters.Add("channelId", (IParameter) new Parameter()
        {
          Name = "channelId",
          IsRequired = false,
          ParameterType = "query",
          DefaultValue = (string) null,
          Pattern = (string) null
        });
        this.RequestParameters.Add("home", (IParameter) new Parameter()
        {
          Name = "home",
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
        this.RequestParameters.Add("pageToken", (IParameter) new Parameter()
        {
          Name = "pageToken",
          IsRequired = false,
          ParameterType = "query",
          DefaultValue = (string) null,
          Pattern = (string) null
        });
        this.RequestParameters.Add("publishedAfter", (IParameter) new Parameter()
        {
          Name = "publishedAfter",
          IsRequired = false,
          ParameterType = "query",
          DefaultValue = (string) null,
          Pattern = (string) null
        });
        this.RequestParameters.Add("publishedBefore", (IParameter) new Parameter()
        {
          Name = "publishedBefore",
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
      }
    }
  }
}
