// Decompiled with JetBrains decompiler
// Type: Google.Apis.Http.HttpExtenstions
// Assembly: Google.Apis.Core, Version=1.30.0.0, Culture=neutral, PublicKeyToken=4b01fa6e34db77ab
// MVID: 4EC5AF78-9014-40C1-B780-6317D8DBC917
// Assembly location: C:\Users\Admin\Desktop\re\YoutubeApp\Google.Apis.Core.dll

using System.Net;
using System.Net.Http;

namespace Google.Apis.Http
{
  public static class HttpExtenstions
  {
    internal static bool IsRedirectStatusCode(this HttpResponseMessage message)
    {
      switch (message.StatusCode)
      {
        case HttpStatusCode.MovedPermanently:
        case HttpStatusCode.Found:
        case HttpStatusCode.SeeOther:
        case HttpStatusCode.TemporaryRedirect:
          return true;
        default:
          return false;
      }
    }

    public static HttpContent SetEmptyContent(this HttpRequestMessage request)
    {
      request.Content = (HttpContent) new ByteArrayContent(new byte[0]);
      request.Content.Headers.ContentLength = new long?(0L);
      return request.Content;
    }
  }
}
