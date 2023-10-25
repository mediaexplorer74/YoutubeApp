// Decompiled with JetBrains decompiler
// Type: Google.Apis.Oauth2.v2.Oauth2Service
// Assembly: Google.Apis.Oauth2.v2, Version=1.29.2.994, Culture=neutral, PublicKeyToken=4b01fa6e34db77ab
// MVID: E0ACF747-2877-47FE-945D-4D75D59EA56A
// Assembly location: C:\Users\Admin\Desktop\re\YoutubeApp\Google.Apis.Oauth2.v2.dll

using Google.Apis.Discovery;
using Google.Apis.Oauth2.v2.Data;
using Google.Apis.Services;
using Google.Apis.Util;
using System.Collections.Generic;

namespace Google.Apis.Oauth2.v2
{
  public class Oauth2Service : BaseClientService
  {
    public const string Version = "v2";
    public static DiscoveryVersion DiscoveryVersionUsed;
    private readonly UserinfoResource userinfo;

    public Oauth2Service()
      : this(new BaseClientService.Initializer())
    {
    }

    public Oauth2Service(BaseClientService.Initializer initializer)
      : base(initializer)
    {
      this.userinfo = new UserinfoResource((IClientService) this);
    }

    public override IList<string> Features => (IList<string>) new string[0];

    public override string Name => "oauth2";

    public override string BaseUri => "https://www.googleapis.com/";

    public override string BasePath => "";

    public override string BatchUri => "https://www.googleapis.com/batch/oauth2/v2";

    public override string BatchPath => "batch/oauth2/v2";

    public virtual Oauth2Service.GetCertForOpenIdConnectRequest GetCertForOpenIdConnect()
    {
        return new Oauth2Service.GetCertForOpenIdConnectRequest((IClientService)this);
    }

    public virtual Oauth2Service.TokeninfoRequest Tokeninfo()
    {
        return new Oauth2Service.TokeninfoRequest((IClientService)this);
    }

    public virtual UserinfoResource Userinfo
    {
        get
        {
            return this.userinfo;
        }
    }

    public class Scope
    {
      public static string PlusLogin = "https://www.googleapis.com/auth/plus.login";
      public static string PlusMe = "https://www.googleapis.com/auth/plus.me";
      public static string UserinfoEmail = "https://www.googleapis.com/auth/userinfo.email";
      public static string UserinfoProfile = "https://www.googleapis.com/auth/userinfo.profile";
    }

    public class GetCertForOpenIdConnectRequest : Oauth2BaseServiceRequest<Jwk>
    {
      public GetCertForOpenIdConnectRequest(IClientService service)
        : base(service)
      {
        this.InitParameters();
      }

      public override string MethodName => "getCertForOpenIdConnect";

      public override string HttpMethod => "GET";

      public override string RestPath => "oauth2/v2/certs";

      protected override void InitParameters() => base.InitParameters();
    }

    public class TokeninfoRequest : Oauth2BaseServiceRequest<Google.Apis.Oauth2.v2.Data.Tokeninfo>
    {
      public TokeninfoRequest(IClientService service)
        : base(service)
      {
        this.InitParameters();
      }

      [RequestParameter("access_token", RequestParameterType.Query)]
      public virtual string AccessToken { get; set; }

      [RequestParameter("id_token", RequestParameterType.Query)]
      public virtual string IdToken { get; set; }

      [RequestParameter("token_handle", RequestParameterType.Query)]
      public virtual string TokenHandle { get; set; }

      public override string MethodName => "tokeninfo";

      public override string HttpMethod => "POST";

      public override string RestPath => "oauth2/v2/tokeninfo";

      protected override void InitParameters()
      {
        base.InitParameters();
        this.RequestParameters.Add("access_token", (IParameter) new Parameter()
        {
          Name = "access_token",
          IsRequired = false,
          ParameterType = "query",
          DefaultValue = (string) null,
          Pattern = (string) null
        });
        this.RequestParameters.Add("id_token", (IParameter) new Parameter()
        {
          Name = "id_token",
          IsRequired = false,
          ParameterType = "query",
          DefaultValue = (string) null,
          Pattern = (string) null
        });
        this.RequestParameters.Add("token_handle", (IParameter) new Parameter()
        {
          Name = "token_handle",
          IsRequired = false,
          ParameterType = "query",
          DefaultValue = (string) null,
          Pattern = (string) null
        });
      }
    }
  }
}
