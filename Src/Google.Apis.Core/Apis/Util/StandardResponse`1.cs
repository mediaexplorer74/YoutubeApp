// Decompiled with JetBrains decompiler
// Type: Google.Apis.Util.StandardResponse`1
// Assembly: Google.Apis.Core, Version=1.30.0.0, Culture=neutral, PublicKeyToken=4b01fa6e34db77ab
// MVID: 4EC5AF78-9014-40C1-B780-6317D8DBC917
// Assembly location: C:\Users\Admin\Desktop\re\YoutubeApp\Google.Apis.Core.dll

using Google.Apis.Requests;
using Newtonsoft.Json;

namespace Google.Apis.Util
{
  public sealed class StandardResponse<InnerType>
  {
    [JsonProperty("data")]
    public InnerType Data { get; set; }

    [JsonProperty("error")]
    public RequestError Error { get; set; }
  }
}
