// Decompiled with JetBrains decompiler
// Type: Google.Apis.Auth.OAuth2.Requests.GoogleAssertionTokenRequest
// Assembly: Google.Apis.Auth, Version=1.30.0.0, Culture=neutral, PublicKeyToken=4b01fa6e34db77ab
// MVID: A0E91A90-D8FA-470E-9253-CAE205F78A22
// Assembly location: C:\Users\Admin\Desktop\re\YoutubeApp\Google.Apis.Auth.dll

using Google.Apis.Util;

namespace Google.Apis.Auth.OAuth2.Requests
{
  public class GoogleAssertionTokenRequest : TokenRequest
  {
    [RequestParameter("assertion")]
    public string Assertion { get; set; }

    public GoogleAssertionTokenRequest() => this.GrantType = "urn:ietf:params:oauth:grant-type:jwt-bearer";
  }
}
