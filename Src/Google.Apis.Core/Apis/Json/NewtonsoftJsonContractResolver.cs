// Decompiled with JetBrains decompiler
// Type: Google.Apis.Json.NewtonsoftJsonContractResolver
// Assembly: Google.Apis.Core, Version=1.30.0.0, Culture=neutral, PublicKeyToken=4b01fa6e34db77ab
// MVID: 4EC5AF78-9014-40C1-B780-6317D8DBC917
// Assembly location: C:\Users\Admin\Desktop\re\YoutubeApp\Google.Apis.Core.dll

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;

namespace Google.Apis.Json
{
  public class NewtonsoftJsonContractResolver : DefaultContractResolver
  {
    private static readonly JsonConverter DateTimeConverter = (JsonConverter) new RFC3339DateTimeConverter();
    private static readonly JsonConverter ExplicitNullConverter = (JsonConverter) new Google.Apis.Json.ExplicitNullConverter();

    protected override JsonContract CreateContract(Type objectType)
    {
      JsonContract contract = base.CreateContract(objectType);
      if (NewtonsoftJsonContractResolver.DateTimeConverter.CanConvert(objectType))
        contract.Converter = NewtonsoftJsonContractResolver.DateTimeConverter;
      else if (NewtonsoftJsonContractResolver.ExplicitNullConverter.CanConvert(objectType))
        contract.Converter = NewtonsoftJsonContractResolver.ExplicitNullConverter;
      return contract;
    }
  }
}
