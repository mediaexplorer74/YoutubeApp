// Decompiled with JetBrains decompiler
// Type: Google.Apis.Auth.JsonWebSignature
// Assembly: Google.Apis.Auth, Version=1.30.0.0, Culture=neutral, PublicKeyToken=4b01fa6e34db77ab
// MVID: A0E91A90-D8FA-470E-9253-CAE205F78A22
// Assembly location: C:\Users\Admin\Desktop\re\YoutubeApp\Google.Apis.Auth.dll

using Newtonsoft.Json;
using System.Collections.Generic;

namespace Google.Apis.Auth
{
  public class JsonWebSignature
  {
    public class Header : JsonWebToken.Header
    {
      [JsonProperty("alg")]
      public string Algorithm { get; set; }

      [JsonProperty("jku")]
      public string JwkUrl { get; set; }

      [JsonProperty("jwk")]
      public string Jwk { get; set; }

      [JsonProperty("kid")]
      public string KeyId { get; set; }

      [JsonProperty("x5u")]
      public string X509Url { get; set; }

      [JsonProperty("x5t")]
      public string X509Thumbprint { get; set; }

      [JsonProperty("x5c")]
      public string X509Certificate { get; set; }

      [JsonProperty("crit")]
      public IList<string> critical { get; set; }
    }

    public class Payload : JsonWebToken.Payload
    {
    }
  }
}
