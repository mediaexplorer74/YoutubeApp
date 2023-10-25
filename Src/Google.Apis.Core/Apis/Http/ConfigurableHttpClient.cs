// Decompiled with JetBrains decompiler
// Type: Google.Apis.Http.ConfigurableHttpClient
// Assembly: Google.Apis.Core, Version=1.30.0.0, Culture=neutral, PublicKeyToken=4b01fa6e34db77ab
// MVID: 4EC5AF78-9014-40C1-B780-6317D8DBC917
// Assembly location: C:\Users\Admin\Desktop\re\YoutubeApp\Google.Apis.Core.dll

using System.Net.Http;

namespace Google.Apis.Http
{
  public class ConfigurableHttpClient : HttpClient
  {
    public ConfigurableMessageHandler MessageHandler { get; private set; }

    public ConfigurableHttpClient(ConfigurableMessageHandler handler)
      : base((HttpMessageHandler) handler)
    {
      this.MessageHandler = handler;
      this.DefaultRequestHeaders.ExpectContinue = new bool?(false);
    }
  }
}
