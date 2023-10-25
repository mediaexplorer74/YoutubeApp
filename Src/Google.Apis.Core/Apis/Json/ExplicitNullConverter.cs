// Decompiled with JetBrains decompiler
// Type: Google.Apis.Json.ExplicitNullConverter
// Assembly: Google.Apis.Core, Version=1.30.0.0, Culture=neutral, PublicKeyToken=4b01fa6e34db77ab
// MVID: 4EC5AF78-9014-40C1-B780-6317D8DBC917
// Assembly location: C:\Users\Admin\Desktop\re\YoutubeApp\Google.Apis.Core.dll

using Newtonsoft.Json;
using System;
using System.Linq;
using System.Reflection;

namespace Google.Apis.Json
{
  public class ExplicitNullConverter : JsonConverter
  {
    public override bool CanRead => false;

    public override bool CanConvert(Type objectType) => CustomAttributeExtensions.GetCustomAttributes(objectType.GetTypeInfo(), typeof (JsonExplicitNullAttribute), false).Any<Attribute>();

    public override object ReadJson(
      JsonReader reader,
      Type objectType,
      object existingValue,
      JsonSerializer serializer)
    {
      throw new NotImplementedException("Unnecessary because CanRead is false.");
    }

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) => writer.WriteNull();
  }
}
