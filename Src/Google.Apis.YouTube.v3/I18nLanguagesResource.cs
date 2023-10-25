// Decompiled with JetBrains decompiler
// Type: Google.Apis.YouTube.v3.I18nLanguagesResource
// Assembly: Google.Apis.YouTube.v3, Version=1.29.2.1006, Culture=neutral, PublicKeyToken=4b01fa6e34db77ab
// MVID: 9E53E5C1-D9AB-4142-94A7-8A2435650008
// Assembly location: C:\Users\Admin\Desktop\re\YoutubeApp\Google.Apis.YouTube.v3.dll

using Google.Apis.Discovery;
using Google.Apis.Services;
using Google.Apis.Util;
using Google.Apis.YouTube.v3.Data;

namespace Google.Apis.YouTube.v3
{
  public class I18nLanguagesResource
  {
    private const string Resource = "i18nLanguages";
    private readonly IClientService service;

    public I18nLanguagesResource(IClientService service) => this.service = service;

    public virtual I18nLanguagesResource.ListRequest List(string part) => new I18nLanguagesResource.ListRequest(this.service, part);

    public class ListRequest : YouTubeBaseServiceRequest<I18nLanguageListResponse>
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

      public override string MethodName => "list";

      public override string HttpMethod => "GET";

      public override string RestPath => "i18nLanguages";

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
          DefaultValue = "en_US",
          Pattern = (string) null
        });
      }
    }
  }
}
