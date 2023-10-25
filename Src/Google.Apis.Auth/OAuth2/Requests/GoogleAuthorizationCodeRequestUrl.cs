// Decompiled with JetBrains decompiler
// Type: Google.Apis.Auth.OAuth2.Requests.GoogleAuthorizationCodeRequestUrl
// Assembly: Google.Apis.Auth, Version=1.30.0.0, Culture=neutral, PublicKeyToken=4b01fa6e34db77ab
// MVID: A0E91A90-D8FA-470E-9253-CAE205F78A22
// Assembly location: C:\Users\Admin\Desktop\re\YoutubeApp\Google.Apis.Auth.dll

using Google.Apis.Util;
using System;
using System.Collections.Generic;

namespace Google.Apis.Auth.OAuth2.Requests
{
  public class GoogleAuthorizationCodeRequestUrl : AuthorizationCodeRequestUrl
  {
    [RequestParameter("access_type", RequestParameterType.Query)]
    public string AccessType { get; set; }

    [RequestParameter("approval_prompt", RequestParameterType.Query)]
    public string ApprovalPrompt { get; set; }

    [RequestParameter("login_hint", RequestParameterType.Query)]
    public string LoginHint { get; set; }

    [RequestParameter("include_granted_scopes", RequestParameterType.Query)]
    public string IncludeGrantedScopes { get; set; }

    [RequestParameter("user_defined_query_params", RequestParameterType.UserDefinedQueries)]
    public IEnumerable<KeyValuePair<string, string>> UserDefinedQueryParams { get; set; }

    public GoogleAuthorizationCodeRequestUrl(Uri authorizationServerUrl)
      : base(authorizationServerUrl)
    {
      this.AccessType = "offline";
    }
  }
}
