// Decompiled with JetBrains decompiler
// Type: Google.Apis.Oauth2.v2.Data.Userinfoplus
// Assembly: Google.Apis.Oauth2.v2, Version=1.29.2.994, Culture=neutral, PublicKeyToken=4b01fa6e34db77ab
// MVID: E0ACF747-2877-47FE-945D-4D75D59EA56A
// Assembly location: C:\Users\Admin\Desktop\re\YoutubeApp\Google.Apis.Oauth2.v2.dll

using Google.Apis.Requests;
using Newtonsoft.Json;

namespace Google.Apis.Oauth2.v2.Data
{
  public class Userinfoplus : IDirectResponseSchema
  {
    [JsonProperty("email")]
    public virtual string Email { get; set; }

    [JsonProperty("family_name")]
    public virtual string FamilyName { get; set; }

    [JsonProperty("gender")]
    public virtual string Gender { get; set; }

    [JsonProperty("given_name")]
    public virtual string GivenName { get; set; }

    [JsonProperty("hd")]
    public virtual string Hd { get; set; }

    [JsonProperty("id")]
    public virtual string Id { get; set; }

    [JsonProperty("link")]
    public virtual string Link { get; set; }

    [JsonProperty("locale")]
    public virtual string Locale { get; set; }

    [JsonProperty("name")]
    public virtual string Name { get; set; }

    [JsonProperty("picture")]
    public virtual string Picture { get; set; }

    [JsonProperty("verified_email")]
    public virtual bool? VerifiedEmail { get; set; }

    public virtual string ETag { get; set; }
  }
}
