// Decompiled with JetBrains decompiler
// Type: Google.Apis.Http.CreateHttpClientArgs
// Assembly: Google.Apis.Core, Version=1.30.0.0, Culture=neutral, PublicKeyToken=4b01fa6e34db77ab
// MVID: 4EC5AF78-9014-40C1-B780-6317D8DBC917
// Assembly location: C:\Users\Admin\Desktop\re\YoutubeApp\Google.Apis.Core.dll

using System.Collections.Generic;

namespace Google.Apis.Http
{
  public class CreateHttpClientArgs
  {
    public bool GZipEnabled { get; set; }

    public string ApplicationName { get; set; }

    public IList<IConfigurableHttpClientInitializer> Initializers { get; private set; }

    public CreateHttpClientArgs() => this.Initializers = (IList<IConfigurableHttpClientInitializer>) new List<IConfigurableHttpClientInitializer>();
  }
}
