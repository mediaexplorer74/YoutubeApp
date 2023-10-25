// Decompiled with JetBrains decompiler
// Type: Google.Apis.Json.RFC3339DateTimeConverter
// Assembly: Google.Apis.Core, Version=1.30.0.0, Culture=neutral, PublicKeyToken=4b01fa6e34db77ab
// MVID: 4EC5AF78-9014-40C1-B780-6317D8DBC917
// Assembly location: C:\Users\Admin\Desktop\re\YoutubeApp\Google.Apis.Core.dll

using Google.Apis.Util;
using Newtonsoft.Json;
using System;

namespace Google.Apis.Json
{
  public class RFC3339DateTimeConverter : JsonConverter
  {
    public override bool CanRead => false;

    public override object ReadJson(
      JsonReader reader,
      Type objectType,
      object existingValue,
      JsonSerializer serializer)
    {
      throw new NotImplementedException("Unnecessary because CanRead is false.");
    }

    public override bool CanConvert(Type objectType) => (object) objectType == (object) typeof (DateTime) || (object) objectType == (object) typeof (DateTime?);

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
      if (value == null)
        return;
      DateTime date = (DateTime) value;
      serializer.Serialize(writer, (object) Utilities.ConvertToRFC3339(date));
    }
  }
}
