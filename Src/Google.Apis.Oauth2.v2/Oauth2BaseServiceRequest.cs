// Decompiled with JetBrains decompiler
// Type: Google.Apis.Oauth2.v2.Oauth2BaseServiceRequest`1
// Assembly: Google.Apis.Oauth2.v2, Version=1.29.2.994, Culture=neutral, PublicKeyToken=4b01fa6e34db77ab
// MVID: E0ACF747-2877-47FE-945D-4D75D59EA56A
// Assembly location: C:\Users\Admin\Desktop\re\YoutubeApp\Google.Apis.Oauth2.v2.dll

using Google.Apis.Discovery;
using Google.Apis.Requests;
using Google.Apis.Services;
using Google.Apis.Util;

namespace Google.Apis.Oauth2.v2
{
  public abstract class Oauth2BaseServiceRequest<TResponse> : ClientServiceRequest<TResponse>
  {
    protected Oauth2BaseServiceRequest(IClientService service)
      : base(service)
    {
    }

    [RequestParameter("alt", RequestParameterType.Query)]
    public virtual Oauth2BaseServiceRequest<TResponse>.AltEnum? Alt { get; set; }

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

    protected override void InitParameters()
    {
      base.InitParameters();
      this.RequestParameters.Add("alt", (IParameter) new Parameter()
      {
        Name = "alt",
        IsRequired = false,
        ParameterType = "query",
        DefaultValue = "json",
        Pattern = (string) null
      });
      this.RequestParameters.Add("fields", (IParameter) new Parameter()
      {
        Name = "fields",
        IsRequired = false,
        ParameterType = "query",
        DefaultValue = (string) null,
        Pattern = (string) null
      });
      this.RequestParameters.Add("key", (IParameter) new Parameter()
      {
        Name = "key",
        IsRequired = false,
        ParameterType = "query",
        DefaultValue = (string) null,
        Pattern = (string) null
      });
      this.RequestParameters.Add("oauth_token", (IParameter) new Parameter()
      {
        Name = "oauth_token",
        IsRequired = false,
        ParameterType = "query",
        DefaultValue = (string) null,
        Pattern = (string) null
      });
      this.RequestParameters.Add("prettyPrint", (IParameter) new Parameter()
      {
        Name = "prettyPrint",
        IsRequired = false,
        ParameterType = "query",
        DefaultValue = "true",
        Pattern = (string) null
      });
      this.RequestParameters.Add("quotaUser", (IParameter) new Parameter()
      {
        Name = "quotaUser",
        IsRequired = false,
        ParameterType = "query",
        DefaultValue = (string) null,
        Pattern = (string) null
      });
      this.RequestParameters.Add("userIp", (IParameter) new Parameter()
      {
        Name = "userIp",
        IsRequired = false,
        ParameterType = "query",
        DefaultValue = (string) null,
        Pattern = (string) null
      });
    }

    public enum AltEnum
    {
      [StringValue("json")] Json,
    }
  }
}
