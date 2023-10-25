// Decompiled with JetBrains decompiler
// Type: Google.Apis.Auth.OAuth2.Responses.TokenErrorResponse
// Assembly: Google.Apis.Auth, Version=1.30.0.0, Culture=neutral, PublicKeyToken=4b01fa6e34db77ab
// MVID: A0E91A90-D8FA-470E-9253-CAE205F78A22
// Assembly location: C:\Users\Admin\Desktop\re\YoutubeApp\Google.Apis.Auth.dll

using Newtonsoft.Json;

namespace Google.Apis.Auth.OAuth2.Responses
{
  public class TokenErrorResponse
  {
    [JsonProperty("error")]
    public string Error { get; set; }

    [JsonProperty("error_description")]
    public string ErrorDescription { get; set; }

    [JsonProperty("error_uri")]
    public string ErrorUri { get; set; }

    public override string ToString() => string.Format("Error:\"{0}\", Description:\"{1}\", Uri:\"{2}\"", (object) this.Error, (object) this.ErrorDescription, (object) this.ErrorUri);

    public TokenErrorResponse()
    {
    }

    public TokenErrorResponse(AuthorizationCodeResponseUrl authorizationCode)
    {
      this.Error = authorizationCode.Error;
      this.ErrorDescription = authorizationCode.ErrorDescription;
      this.ErrorUri = authorizationCode.ErrorUri;
    }
  }
}
