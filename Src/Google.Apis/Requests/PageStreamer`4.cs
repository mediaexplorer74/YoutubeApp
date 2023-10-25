// Decompiled with JetBrains decompiler
// Type: Google.Apis.Requests.PageStreamer`4
// Assembly: Google.Apis, Version=1.30.0.0, Culture=neutral, PublicKeyToken=4b01fa6e34db77ab
// MVID: 6C44ABAE-71BD-4009-BDB7-D7E324A25671
// Assembly location: C:\Users\Admin\Desktop\re\YoutubeApp\Google.Apis.dll

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Google.Apis.Requests
{
  public sealed class PageStreamer<TResource, TRequest, TResponse, TToken>
    where TRequest : IClientServiceRequest<TResponse>
    where TToken : class
  {
    private static readonly TResource[] emptyResources = new TResource[0];
    private readonly Action<TRequest, TToken> requestModifier;
    private readonly Func<TResponse, TToken> tokenExtractor;
    private readonly Func<TResponse, IEnumerable<TResource>> resourceExtractor;

    public PageStreamer(
      Action<TRequest, TToken> requestModifier,
      Func<TResponse, TToken> tokenExtractor,
      Func<TResponse, IEnumerable<TResource>> resourceExtractor)
    {
      if (requestModifier == null)
        throw new ArgumentNullException("requestProvider");
      if (tokenExtractor == null)
        throw new ArgumentNullException(nameof (tokenExtractor));
      if (resourceExtractor == null)
        throw new ArgumentNullException(nameof (resourceExtractor));
      this.requestModifier = requestModifier;
      this.tokenExtractor = tokenExtractor;
      this.resourceExtractor = resourceExtractor;
    }

    public IEnumerable<TResource> Fetch(TRequest request)
    {
      if ((object) request == null)
        throw new ArgumentNullException(nameof (request));
label_3:
      TResponse response = request.Execute();
      TToken token = this.tokenExtractor(response);
      this.requestModifier(request, token);
      foreach (TResource resource in (IEnumerable<TResource>) ((object) this.resourceExtractor(response) ?? (object) PageStreamer<TResource, TRequest, TResponse, TToken>.emptyResources))
        yield return resource;
      if ((object) token != null)
        goto label_3;
    }

    public async Task<IList<TResource>> FetchAllAsync(
      TRequest request,
      CancellationToken cancellationToken)
    {
      if ((object) request == null)
        throw new ArgumentNullException(nameof (request));
      List<TResource> results = new List<TResource>();
      TToken token;
      do
      {
        cancellationToken.ThrowIfCancellationRequested();
        TResponse response = await request.ExecuteAsync(cancellationToken).ConfigureAwait(false);
        token = this.tokenExtractor(response);
        this.requestModifier(request, token);
        results.AddRange((IEnumerable<TResource>) ((object) this.resourceExtractor(response) ?? (object) PageStreamer<TResource, TRequest, TResponse, TToken>.emptyResources));
      }
      while ((object) token != null);
      return (IList<TResource>) results;
    }
  }
}
