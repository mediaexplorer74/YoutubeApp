// Decompiled with JetBrains decompiler
// Type: Google.Apis.Auth.OAuth2.Requests.AuthorizationCodeRequestUrl
// Assembly: Google.Apis.Auth, Version=1.30.0.0, Culture=neutral, PublicKeyToken=4b01fa6e34db77ab
// MVID: A0E91A90-D8FA-470E-9253-CAE205F78A22
// Assembly location: C:\Users\Admin\Desktop\re\YoutubeApp\Google.Apis.Auth.dll

using Google.Apis.Requests;
using Google.Apis.Requests.Parameters;
using System;

namespace Google.Apis.Auth.OAuth2.Requests
{
  public class AuthorizationCodeRequestUrl : AuthorizationRequestUrl
  {
    public AuthorizationCodeRequestUrl(Uri authorizationServerUrl)
      : base(authorizationServerUrl)
    {
      this.ResponseType = "code";
    }

    public Uri Build()
    {
      RequestBuilder builder = new RequestBuilder();
      builder.BaseUri = this.AuthorizationServerUrl;
      ParameterUtils.InitParameters(builder, (object) this);
      return builder.BuildUri();
    }
  }
}
