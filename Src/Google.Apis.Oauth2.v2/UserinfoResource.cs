// Decompiled with JetBrains decompiler
// Type: Google.Apis.Oauth2.v2.UserinfoResource
// Assembly: Google.Apis.Oauth2.v2, Version=1.29.2.994, Culture=neutral, PublicKeyToken=4b01fa6e34db77ab
// MVID: E0ACF747-2877-47FE-945D-4D75D59EA56A
// Assembly location: C:\Users\Admin\Desktop\re\YoutubeApp\Google.Apis.Oauth2.v2.dll

using Google.Apis.Oauth2.v2.Data;
using Google.Apis.Services;

namespace Google.Apis.Oauth2.v2
{
  public class UserinfoResource
  {
    private const string Resource = "userinfo";
    private readonly IClientService service;
    private readonly UserinfoResource.V2Resource v2;

    public UserinfoResource(IClientService service)
    {
      this.service = service;
      this.v2 = new UserinfoResource.V2Resource(service);
    }

    public virtual UserinfoResource.V2Resource V2 => this.v2;

    public virtual UserinfoResource.GetRequest Get() => new UserinfoResource.GetRequest(this.service);

    public class V2Resource
    {
      private const string Resource = "v2";
      private readonly IClientService service;
      private readonly UserinfoResource.V2Resource.MeResource me;

      public V2Resource(IClientService service)
      {
        this.service = service;
        this.me = new UserinfoResource.V2Resource.MeResource(service);
      }

      public virtual UserinfoResource.V2Resource.MeResource Me => this.me;

      public class MeResource
      {
        private const string Resource = "me";
        private readonly IClientService service;

        public MeResource(IClientService service) => this.service = service;

        public virtual UserinfoResource.V2Resource.MeResource.GetRequest Get() => new UserinfoResource.V2Resource.MeResource.GetRequest(this.service);

        public class GetRequest : Oauth2BaseServiceRequest<Userinfoplus>
        {
          public GetRequest(IClientService service)
            : base(service)
          {
            this.InitParameters();
          }

          public override string MethodName => "get";

          public override string HttpMethod => "GET";

          public override string RestPath => "userinfo/v2/me";

          protected override void InitParameters() => base.InitParameters();
        }
      }
    }

    public class GetRequest : Oauth2BaseServiceRequest<Userinfoplus>
    {
      public GetRequest(IClientService service)
        : base(service)
      {
        this.InitParameters();
      }

      public override string MethodName => "get";

      public override string HttpMethod => "GET";

      public override string RestPath => "oauth2/v2/userinfo";

      protected override void InitParameters() => base.InitParameters();
    }
  }
}
