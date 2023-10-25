// Decompiled with JetBrains decompiler
// Type: Google.Apis.ISerializer
// Assembly: Google.Apis.Core, Version=1.30.0.0, Culture=neutral, PublicKeyToken=4b01fa6e34db77ab
// MVID: 4EC5AF78-9014-40C1-B780-6317D8DBC917
// Assembly location: C:\Users\Admin\Desktop\re\YoutubeApp\Google.Apis.Core.dll

using System;
using System.IO;

namespace Google.Apis
{
  public interface ISerializer
  {
    string Format { get; }

    void Serialize(object obj, Stream target);

    string Serialize(object obj);

    T Deserialize<T>(string input);

    object Deserialize(string input, Type type);

    T Deserialize<T>(Stream input);
  }
}
