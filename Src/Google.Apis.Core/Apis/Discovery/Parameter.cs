﻿// Decompiled with JetBrains decompiler
// Type: Google.Apis.Discovery.Parameter
// Assembly: Google.Apis.Core, Version=1.30.0.0, Culture=neutral, PublicKeyToken=4b01fa6e34db77ab
// MVID: 4EC5AF78-9014-40C1-B780-6317D8DBC917
// Assembly location: C:\Users\Admin\Desktop\re\YoutubeApp\Google.Apis.Core.dll

namespace Google.Apis.Discovery
{
  public class Parameter : IParameter
  {
    public string Name { get; set; }

    public string Pattern { get; set; }

    public bool IsRequired { get; set; }

    public string ParameterType { get; set; }

    public string DefaultValue { get; set; }
  }
}
