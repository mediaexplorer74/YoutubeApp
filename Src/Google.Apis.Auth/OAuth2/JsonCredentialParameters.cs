// Decompiled with JetBrains decompiler
// Type: Google.Apis.Auth.OAuth2.JsonCredentialParameters
// Assembly: Google.Apis.Auth, Version=1.30.0.0, Culture=neutral, PublicKeyToken=4b01fa6e34db77ab
// MVID: A0E91A90-D8FA-470E-9253-CAE205F78A22
// Assembly location: C:\Users\Admin\Desktop\re\YoutubeApp\Google.Apis.Auth.dll

using Newtonsoft.Json;

namespace Google.Apis.Auth.OAuth2
{
  public class JsonCredentialParameters
  {
    public const string AuthorizedUserCredentialType = "authorized_user";
    public const string ServiceAccountCredentialType = "service_account";

    [JsonProperty("type")]
    public string Type { get; set; }

    [JsonProperty("client_id")]
    public string ClientId { get; set; }

    [JsonProperty("client_secret")]
    public string ClientSecret { get; set; }

    [JsonProperty("client_email")]
    public string ClientEmail { get; set; }

    [JsonProperty("private_key")]
    public string PrivateKey { get; set; }

    [JsonProperty("refresh_token")]
    public string RefreshToken { get; set; }
  }
}
