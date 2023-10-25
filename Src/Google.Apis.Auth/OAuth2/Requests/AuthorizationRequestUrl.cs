// Decompiled with JetBrains decompiler
// Type: Google.Apis.Auth.OAuth2.Requests.AuthorizationRequestUrl
// Assembly: Google.Apis.Auth, Version=1.30.0.0, Culture=neutral, PublicKeyToken=4b01fa6e34db77ab
// MVID: A0E91A90-D8FA-470E-9253-CAE205F78A22
// Assembly location: C:\Users\Admin\Desktop\re\YoutubeApp\Google.Apis.Auth.dll

using Google.Apis.Util;
using System;

namespace Google.Apis.Auth.OAuth2.Requests
{
  public class AuthorizationRequestUrl
  {
    private readonly Uri authorizationServerUrl;

    [RequestParameter("response_type", RequestParameterType.Query)]
    public string ResponseType { get; set; }

    [RequestParameter("client_id", RequestParameterType.Query)]
    public string ClientId { get; set; }

    [RequestParameter("redirect_uri", RequestParameterType.Query)]
    public string RedirectUri { get; set; }

    [RequestParameter("scope", RequestParameterType.Query)]
    public string Scope { get; set; }

    [RequestParameter("state", RequestParameterType.Query)]
    public string State { get; set; }

    public Uri AuthorizationServerUrl => this.authorizationServerUrl;

    public AuthorizationRequestUrl(Uri authorizationServerUrl) => this.authorizationServerUrl = authorizationServerUrl;
  }
}
