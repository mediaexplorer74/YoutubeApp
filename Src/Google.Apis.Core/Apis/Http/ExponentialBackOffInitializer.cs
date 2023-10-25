// Decompiled with JetBrains decompiler
// Type: Google.Apis.Http.ExponentialBackOffInitializer
// Assembly: Google.Apis.Core, Version=1.30.0.0, Culture=neutral, PublicKeyToken=4b01fa6e34db77ab
// MVID: 4EC5AF78-9014-40C1-B780-6317D8DBC917
// Assembly location: C:\Users\Admin\Desktop\re\YoutubeApp\Google.Apis.Core.dll

using System;

namespace Google.Apis.Http
{
  public class ExponentialBackOffInitializer : IConfigurableHttpClientInitializer
  {
    private ExponentialBackOffPolicy Policy { get; set; }

    private Func<BackOffHandler> CreateBackOff { get; set; }

    public ExponentialBackOffInitializer(
      ExponentialBackOffPolicy policy,
      Func<BackOffHandler> createBackOff)
    {
      this.Policy = policy;
      this.CreateBackOff = createBackOff;
    }

    public void Initialize(ConfigurableHttpClient httpClient)
    {
      BackOffHandler handler = this.CreateBackOff();
      if ((this.Policy & ExponentialBackOffPolicy.Exception) == ExponentialBackOffPolicy.Exception)
        httpClient.MessageHandler.AddExceptionHandler((IHttpExceptionHandler) handler);
      if ((this.Policy & ExponentialBackOffPolicy.UnsuccessfulResponse503) != ExponentialBackOffPolicy.UnsuccessfulResponse503)
        return;
      httpClient.MessageHandler.AddUnsuccessfulResponseHandler((IHttpUnsuccessfulResponseHandler) handler);
    }
  }
}
