// Decompiled with JetBrains decompiler
// Type: Google.Apis.YouTube.v3.SponsorsResource
// Assembly: Google.Apis.YouTube.v3, Version=1.29.2.1006, Culture=neutral, PublicKeyToken=4b01fa6e34db77ab
// MVID: 9E53E5C1-D9AB-4142-94A7-8A2435650008
// Assembly location: C:\Users\Admin\Desktop\re\YoutubeApp\Google.Apis.YouTube.v3.dll

using Google.Apis.Discovery;
using Google.Apis.Services;
using Google.Apis.Util;
using Google.Apis.YouTube.v3.Data;

namespace Google.Apis.YouTube.v3
{
  public class SponsorsResource
  {
    private const string Resource = "sponsors";
    private readonly IClientService service;

    public SponsorsResource(IClientService service) => this.service = service;

    public virtual SponsorsResource.ListRequest List(string part) => new SponsorsResource.ListRequest(this.service, part);

    public class ListRequest : YouTubeBaseServiceRequest<SponsorListResponse>
    {
      public ListRequest(IClientService service, string part)
        : base(service)
      {
        this.Part = part;
        this.InitParameters();
      }

      [RequestParameter("part", RequestParameterType.Query)]
      public virtual string Part { get; private set; }

      [RequestParameter("filter", RequestParameterType.Query)]
      public virtual SponsorsResource.ListRequest.FilterEnum? Filter { get; set; }

      [RequestParameter("maxResults", RequestParameterType.Query)]
      public virtual long? MaxResults { get; set; }

      [RequestParameter("pageToken", RequestParameterType.Query)]
      public virtual string PageToken { get; set; }

      public override string MethodName => "list";

      public override string HttpMethod => "GET";

      public override string RestPath => "sponsors";

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
        this.RequestParameters.Add("filter", (IParameter) new Parameter()
        {
          Name = "filter",
          IsRequired = false,
          ParameterType = "query",
          DefaultValue = "POLL_NEWEST",
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
        this.RequestParameters.Add("pageToken", (IParameter) new Parameter()
        {
          Name = "pageToken",
          IsRequired = false,
          ParameterType = "query",
          DefaultValue = (string) null,
          Pattern = (string) null
        });
      }

      public enum FilterEnum
      {
        [StringValue("all")] All,
        [StringValue("newest")] Newest,
      }
    }
  }
}
