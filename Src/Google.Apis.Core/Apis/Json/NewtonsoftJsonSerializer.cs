// Decompiled with JetBrains decompiler
// Type: Google.Apis.Json.NewtonsoftJsonSerializer
// Assembly: Google.Apis.Core, Version=1.30.0.0, Culture=neutral, PublicKeyToken=4b01fa6e34db77ab
// MVID: 4EC5AF78-9014-40C1-B780-6317D8DBC917
// Assembly location: C:\Users\Admin\Desktop\re\YoutubeApp\Google.Apis.Core.dll

using Google.Apis.Util;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.IO;

namespace Google.Apis.Json
{
  public class NewtonsoftJsonSerializer : IJsonSerializer, ISerializer
  {
    private readonly JsonSerializerSettings settings;
    private readonly JsonSerializer serializer;

    public static NewtonsoftJsonSerializer Instance { get; } = new NewtonsoftJsonSerializer();

    public NewtonsoftJsonSerializer()
      : this(NewtonsoftJsonSerializer.CreateDefaultSettings())
    {
    }

    public NewtonsoftJsonSerializer(JsonSerializerSettings settings)
    {
      settings.ThrowIfNull<JsonSerializerSettings>(nameof (settings));
      this.settings = settings;
      this.serializer = JsonSerializer.Create(settings);
    }

    public static JsonSerializerSettings CreateDefaultSettings() => new JsonSerializerSettings()
    {
      NullValueHandling = NullValueHandling.Ignore,
      MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
      ContractResolver = (IContractResolver) new NewtonsoftJsonContractResolver()
    };

    public string Format => "json";

    public void Serialize(object obj, Stream target)
    {
      using (StreamWriter streamWriter = new StreamWriter(target))
      {
        if (obj == null)
          obj = (object) string.Empty;
        this.serializer.Serialize((TextWriter) streamWriter, obj);
      }
    }

    public string Serialize(object obj)
    {
      using (TextWriter textWriter = (TextWriter) new StringWriter())
      {
        if (obj == null)
          obj = (object) string.Empty;
        this.serializer.Serialize(textWriter, obj);
        return textWriter.ToString();
      }
    }

    public T Deserialize<T>(string input) => string.IsNullOrEmpty(input) ? default (T) : JsonConvert.DeserializeObject<T>(input, this.settings);

    public object Deserialize(string input, Type type) => string.IsNullOrEmpty(input) ? (object) null : JsonConvert.DeserializeObject(input, type, this.settings);

    public T Deserialize<T>(Stream input)
    {
      using (StreamReader reader = new StreamReader(input))
        return (T) this.serializer.Deserialize((TextReader) reader, typeof (T));
    }
  }
}
