// Decompiled with JetBrains decompiler
// Type: Google.Apis.Auth.OAuth2.GoogleClientSecrets
// Assembly: Google.Apis.Auth, Version=1.30.0.0, Culture=neutral, PublicKeyToken=4b01fa6e34db77ab
// MVID: A0E91A90-D8FA-470E-9253-CAE205F78A22
// Assembly location: C:\Users\Admin\Desktop\re\YoutubeApp\Google.Apis.Auth.dll

using Google.Apis.Json;
using Newtonsoft.Json;
using System;
using System.IO;

namespace Google.Apis.Auth.OAuth2
{
  public sealed class GoogleClientSecrets
  {
    [JsonProperty("installed")]
    private ClientSecrets Installed { get; set; }

    [JsonProperty("web")]
    private ClientSecrets Web { get; set; }

    public ClientSecrets Secrets
    {
      get
      {
        if (this.Installed == null && this.Web == null)
          throw new InvalidOperationException("At least one client secrets (Installed or Web) should be set");
        return this.Installed ?? this.Web;
      }
    }

    public static GoogleClientSecrets Load(Stream stream) => NewtonsoftJsonSerializer.Instance.Deserialize<GoogleClientSecrets>(stream);
  }
}
