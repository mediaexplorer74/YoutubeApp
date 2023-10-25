// Decompiled with JetBrains decompiler
// Type: Google.Apis.Oauth2.v2.Data.Jwk
// Assembly: Google.Apis.Oauth2.v2, Version=1.29.2.994, Culture=neutral, PublicKeyToken=4b01fa6e34db77ab
// MVID: E0ACF747-2877-47FE-945D-4D75D59EA56A
// Assembly location: C:\Users\Admin\Desktop\re\YoutubeApp\Google.Apis.Oauth2.v2.dll

using Google.Apis.Requests;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Google.Apis.Oauth2.v2.Data
{
  public class Jwk : IDirectResponseSchema
  {
    [JsonProperty("keys")]
    public virtual IList<Jwk.KeysData> Keys { get; set; }

    public virtual string ETag { get; set; }

    public class KeysData
    {
      [JsonProperty("alg")]
      public virtual string Alg { get; set; }

      [JsonProperty("e")]
      public virtual string E { get; set; }

      [JsonProperty("kid")]
      public virtual string Kid { get; set; }

      [JsonProperty("kty")]
      public virtual string Kty { get; set; }

      [JsonProperty("n")]
      public virtual string N { get; set; }

      [JsonProperty("use")]
      public virtual string Use { get; set; }
    }
  }
}
