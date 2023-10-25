// Decompiled with JetBrains decompiler
// Type: Google.Apis.Media.MediaApiErrorHandling
// Assembly: Google.Apis, Version=1.30.0.0, Culture=neutral, PublicKeyToken=4b01fa6e34db77ab
// MVID: 6C44ABAE-71BD-4009-BDB7-D7E324A25671
// Assembly location: C:\Users\Admin\Desktop\re\YoutubeApp\Google.Apis.dll

using Google.Apis.Json;
using Google.Apis.Requests;
using Google.Apis.Services;
using Google.Apis.Util;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace Google.Apis.Media
{
  internal static class MediaApiErrorHandling
  {
    internal static Task<GoogleApiException> ExceptionForResponseAsync(
      IClientService service,
      HttpResponseMessage response)
    {
      return MediaApiErrorHandling.ExceptionForResponseAsync(service.Serializer, service.Name, response);
    }

    internal static async Task<GoogleApiException> ExceptionForResponseAsync(
      ISerializer serializer,
      string name,
      HttpResponseMessage response)
    {
      string input = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
      string message = input;
      RequestError error = default;
      try
      {
        StandardResponse<object> standardResponse = (serializer 
                    ?? (ISerializer) NewtonsoftJsonSerializer.Instance).Deserialize<StandardResponse<object>>(input);
        if (standardResponse != null)
        {
          if (standardResponse.Error != null)
          {
            error = standardResponse.Error;
            message = error.ToString();
          }
        }
      }
      catch (JsonException ex)
      {
      }
      return new GoogleApiException(name ?? "", message)
      {
        Error = error,
        HttpStatusCode = response.StatusCode
      };
    }
  }
}
