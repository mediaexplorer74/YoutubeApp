// Decompiled with JetBrains decompiler
// Type: Google.Apis.Requests.HttpRequestMessageExtenstions
// Assembly: Google.Apis, Version=1.30.0.0, Culture=neutral, PublicKeyToken=4b01fa6e34db77ab
// MVID: 6C44ABAE-71BD-4009-BDB7-D7E324A25671
// Assembly location: C:\Users\Admin\Desktop\re\YoutubeApp\Google.Apis.dll

using Google.Apis.Services;
using System.IO;
using System.IO.Compression;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace Google.Apis.Requests
{
  internal static class HttpRequestMessageExtenstions
  {
    internal static void SetRequestSerailizedContent(
      this HttpRequestMessage request,
      IClientService service,
      object body,
      bool gzipEnabled)
    {
      if (body == null)
        return;
      string mediaType = "application/" + service.Serializer.Format;
      string content = service.SerializeObject(body);
      HttpContent httpContent;
      if (gzipEnabled)
      {
        httpContent = HttpRequestMessageExtenstions.CreateZipContent(content);
        httpContent.Headers.ContentType = new MediaTypeHeaderValue(mediaType)
        {
          CharSet = Encoding.UTF8.WebName
        };
      }
      else
        httpContent = (HttpContent) new StringContent(content, Encoding.UTF8, mediaType);
      request.Content = httpContent;
    }

    internal static HttpContent CreateZipContent(string content)
    {
      StreamContent zipContent = new StreamContent(HttpRequestMessageExtenstions.CreateGZipStream(content));
      zipContent.Headers.ContentEncoding.Add("gzip");
      return (HttpContent) zipContent;
    }

    private static Stream CreateGZipStream(string serializedObject)
    {
      byte[] bytes = Encoding.UTF8.GetBytes(serializedObject);
      using (MemoryStream memoryStream = new MemoryStream())
      {
        using (GZipStream gzipStream = new GZipStream((Stream) memoryStream, CompressionMode.Compress, true))
          gzipStream.Write(bytes, 0, bytes.Length);
        memoryStream.Position = 0L;
        byte[] buffer = new byte[memoryStream.Length];
        memoryStream.Read(buffer, 0, buffer.Length);
        return (Stream) new MemoryStream(buffer);
      }
    }
  }
}
