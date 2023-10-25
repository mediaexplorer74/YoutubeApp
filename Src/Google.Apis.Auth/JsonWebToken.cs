// Decompiled with JetBrains decompiler
// Type: Google.Apis.Auth.JsonWebToken
// Assembly: Google.Apis.Auth, Version=1.30.0.0, Culture=neutral, PublicKeyToken=4b01fa6e34db77ab
// MVID: A0E91A90-D8FA-470E-9253-CAE205F78A22
// Assembly location: C:\Users\Admin\Desktop\re\YoutubeApp\Google.Apis.Auth.dll

using Newtonsoft.Json;
using System.Collections.Generic;

namespace Google.Apis.Auth
{
  public class JsonWebToken
  {
    public class Header
    {
      [JsonProperty("typ")]
      public string Type { get; set; }

      [JsonProperty("cty")]
      public string ContentType { get; set; }
    }

    public class Payload
    {
      [JsonProperty("iss")]
      public string Issuer { get; set; }

      [JsonProperty("sub")]
      public string Subject { get; set; }

      [JsonProperty("aud")]
      public object Audience { get; set; }

      [JsonProperty("exp")]
      public long? ExpirationTimeSeconds { get; set; }

      [JsonProperty("nbf")]
      public long? NotBeforeTimeSeconds { get; set; }

      [JsonProperty("iat")]
      public long? IssuedAtTimeSeconds { get; set; }

      [JsonProperty("jti")]
      public string JwtId { get; set; }

      [JsonProperty("typ")]
      public string Type { get; set; }

      [JsonIgnore]
      public IEnumerable<string> AudienceAsList
      {
        get
        {
          if (this.Audience is List<string> audience1)
            return (IEnumerable<string>) audience1;
          List<string> audienceAsList = new List<string>();
          if (this.Audience is string audience2)
            audienceAsList.Add(audience2);
          return (IEnumerable<string>) audienceAsList;
        }
      }
    }
  }
}
