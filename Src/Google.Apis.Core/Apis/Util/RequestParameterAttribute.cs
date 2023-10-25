// Decompiled with JetBrains decompiler
// Type: Google.Apis.Util.RequestParameterAttribute
// Assembly: Google.Apis.Core, Version=1.30.0.0, Culture=neutral, PublicKeyToken=4b01fa6e34db77ab
// MVID: 4EC5AF78-9014-40C1-B780-6317D8DBC917
// Assembly location: C:\Users\Admin\Desktop\re\YoutubeApp\Google.Apis.Core.dll

using System;

namespace Google.Apis.Util
{
  [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
  public class RequestParameterAttribute : Attribute
  {
    private readonly string name;
    private readonly RequestParameterType type;

    public string Name => this.name;

    public RequestParameterType Type => this.type;

    public RequestParameterAttribute(string name)
      : this(name, RequestParameterType.Query)
    {
    }

    public RequestParameterAttribute(string name, RequestParameterType type)
    {
      this.name = name;
      this.type = type;
    }
  }
}
