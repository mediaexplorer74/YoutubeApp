// Decompiled with JetBrains decompiler
// Type: Google.Apis.Http.MaxUrlLengthInterceptor
// Assembly: Google.Apis.Core, Version=1.30.0.0, Culture=neutral, PublicKeyToken=4b01fa6e34db77ab
// MVID: 4EC5AF78-9014-40C1-B780-6317D8DBC917
// Assembly location: C:\Users\Admin\Desktop\re\YoutubeApp\Google.Apis.Core.dll

using Google.Apis.Testing;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace Google.Apis.Http
{
  [VisibleForTestOnly]
  public class MaxUrlLengthInterceptor : IHttpExecuteInterceptor
  {
    private readonly uint maxUrlLength;

    public MaxUrlLengthInterceptor(uint maxUrlLength) => this.maxUrlLength = maxUrlLength;

    public Task InterceptAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
      if (request.Method != HttpMethod.Get || (long) request.RequestUri.AbsoluteUri.Length <= (long) this.maxUrlLength)
        return (Task) Task.FromResult<int>(0);
      request.Method = HttpMethod.Post;
      string query = request.RequestUri.Query;
      if (!string.IsNullOrEmpty(query))
      {
        request.Content = (HttpContent) new StringContent(query.Substring(1));
        request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
        string str = request.RequestUri.ToString();
        request.RequestUri = new Uri(str.Remove(str.IndexOf("?")));
      }
      request.Headers.Add("X-HTTP-Method-Override", "GET");
      return (Task) Task.FromResult<int>(0);
    }
  }
}
