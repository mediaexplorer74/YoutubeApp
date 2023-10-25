// Decompiled with JetBrains decompiler
// Type: Google.Apis.Oauth2.v2.Data.Tokeninfo
// Assembly: Google.Apis.Oauth2.v2, Version=1.29.2.994, Culture=neutral, PublicKeyToken=4b01fa6e34db77ab
// MVID: E0ACF747-2877-47FE-945D-4D75D59EA56A
// Assembly location: C:\Users\Admin\Desktop\re\YoutubeApp\Google.Apis.Oauth2.v2.dll

using Google.Apis.Requests;
using Newtonsoft.Json;

namespace Google.Apis.Oauth2.v2.Data
{
  public class Tokeninfo : IDirectResponseSchema
  {
    [JsonProperty("access_type")]
    public virtual string AccessType { get; set; }

    [JsonProperty("audience")]
    public virtual string Audience { get; set; }

    [JsonProperty("email")]
    public virtual string Email { get; set; }

    [JsonProperty("expires_in")]
    public virtual int? ExpiresIn { get; set; }

    [JsonProperty("issued_to")]
    public virtual string IssuedTo { get; set; }

    [JsonProperty("scope")]
    public virtual string Scope { get; set; }

    [JsonProperty("token_handle")]
    public virtual string TokenHandle { get; set; }

    [JsonProperty("user_id")]
    public virtual string UserId { get; set; }

    [JsonProperty("verified_email")]
    public virtual bool? VerifiedEmail { get; set; }

    public virtual string ETag { get; set; }
  }
}
