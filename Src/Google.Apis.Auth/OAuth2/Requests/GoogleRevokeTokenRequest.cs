// Decompiled with JetBrains decompiler
// Type: Google.Apis.Auth.OAuth2.Requests.GoogleRevokeTokenRequest
// Assembly: Google.Apis.Auth, Version=1.30.0.0, Culture=neutral, PublicKeyToken=4b01fa6e34db77ab
// MVID: A0E91A90-D8FA-470E-9253-CAE205F78A22
// Assembly location: C:\Users\Admin\Desktop\re\YoutubeApp\Google.Apis.Auth.dll

using Google.Apis.Requests;
using Google.Apis.Requests.Parameters;
using Google.Apis.Util;
using System;

namespace Google.Apis.Auth.OAuth2.Requests
{
  internal class GoogleRevokeTokenRequest
  {
    private readonly Uri revokeTokenUrl;

    public Uri RevokeTokenUrl => this.revokeTokenUrl;

    [RequestParameter("token")]
    public string Token { get; set; }

    public GoogleRevokeTokenRequest(Uri revokeTokenUrl) => this.revokeTokenUrl = revokeTokenUrl;

    public Uri Build()
    {
      RequestBuilder builder = new RequestBuilder();
      builder.BaseUri = this.revokeTokenUrl;
      ParameterUtils.InitParameters(builder, (object) this);
      return builder.BuildUri();
    }
  }
}
