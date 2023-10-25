// Decompiled with JetBrains decompiler
// Type: Google.Apis.YouTube.v3.GuideCategoriesResource
// Assembly: Google.Apis.YouTube.v3, Version=1.29.2.1006, Culture=neutral, PublicKeyToken=4b01fa6e34db77ab
// MVID: 9E53E5C1-D9AB-4142-94A7-8A2435650008
// Assembly location: C:\Users\Admin\Desktop\re\YoutubeApp\Google.Apis.YouTube.v3.dll

using Google.Apis.Discovery;
using Google.Apis.Services;
using Google.Apis.Util;
using Google.Apis.YouTube.v3.Data;

namespace Google.Apis.YouTube.v3
{
  public class GuideCategoriesResource
  {
    private const string Resource = "guideCategories";
    private readonly IClientService service;

    public GuideCategoriesResource(IClientService service) => this.service = service;

    public virtual GuideCategoriesResource.ListRequest List(string part) => new GuideCategoriesResource.ListRequest(this.service, part);

    public class ListRequest : YouTubeBaseServiceRequest<GuideCategoryListResponse>
    {
      public ListRequest(IClientService service, string part)
        : base(service)
      {
        this.Part = part;
        this.InitParameters();
      }

      [RequestParameter("part", RequestParameterType.Query)]
      public virtual string Part { get; private set; }

      [RequestParameter("hl", RequestParameterType.Query)]
      public virtual string Hl { get; set; }

      [RequestParameter("id", RequestParameterType.Query)]
      public virtual string Id { get; set; }

      [RequestParameter("regionCode", RequestParameterType.Query)]
      public virtual string RegionCode { get; set; }

      public override string MethodName => "list";

      public override string HttpMethod => "GET";

      public override string RestPath => "guideCategories";

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
        this.RequestParameters.Add("hl", (IParameter) new Parameter()
        {
          Name = "hl",
          IsRequired = false,
          ParameterType = "query",
          DefaultValue = "en-US",
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
