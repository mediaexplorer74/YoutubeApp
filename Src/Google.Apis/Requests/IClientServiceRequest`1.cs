// Decompiled with JetBrains decompiler
// Type: Google.Apis.Requests.IClientServiceRequest`1
// Assembly: Google.Apis, Version=1.30.0.0, Culture=neutral, PublicKeyToken=4b01fa6e34db77ab
// MVID: 6C44ABAE-71BD-4009-BDB7-D7E324A25671
// Assembly location: C:\Users\Admin\Desktop\re\YoutubeApp\Google.Apis.dll

using System.Threading;
using System.Threading.Tasks;

namespace Google.Apis.Requests
{
  public interface IClientServiceRequest<TResponse> : IClientServiceRequest
  {
    Task<TResponse> ExecuteAsync();

    Task<TResponse> ExecuteAsync(CancellationToken cancellationToken);

    TResponse Execute();
  }
}
