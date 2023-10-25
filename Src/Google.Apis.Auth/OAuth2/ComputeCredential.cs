// Decompiled with JetBrains decompiler
// Type: Google.Apis.Auth.OAuth2.ComputeCredential
// Assembly: Google.Apis.Auth, Version=1.30.0.0, Culture=neutral, PublicKeyToken=4b01fa6e34db77ab
// MVID: A0E91A90-D8FA-470E-9253-CAE205F78A22
// Assembly location: C:\Users\Admin\Desktop\re\YoutubeApp\Google.Apis.Auth.dll

using Google.Apis.Auth.OAuth2.Responses;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Google.Apis.Auth.OAuth2
{
  public class ComputeCredential : ServiceCredential
  {
    public const string MetadataServerUrl = "http://metadata.google.internal";
    private static readonly Lazy<Task<bool>> isRunningOnComputeEngineCached = new Lazy<Task<bool>>((Func<Task<bool>>) (() => ComputeCredential.IsRunningOnComputeEngineNoCache()));
    private const int MetadataServerPingTimeoutInMilliseconds = 1000;
    private const string MetadataFlavor = "Metadata-Flavor";
    private const string GoogleMetadataHeader = "Google";
    private const string NotOnGceMessage = "Could not reach the Google Compute Engine metadata service. That is alright if this application is not running on GCE.";

    public ComputeCredential()
      : this(new ComputeCredential.Initializer())
    {
    }

    public ComputeCredential(ComputeCredential.Initializer initializer)
      : base((ServiceCredential.Initializer) initializer)
    {
    }

    public override async Task<bool> RequestAccessTokenAsync(CancellationToken taskCancellationToken)
    {
      ComputeCredential computeCredential = this;
      HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, computeCredential.TokenServerUrl);
      request.Headers.Add("Metadata-Flavor", "Google");
      TokenResponse tokenResponse = await TokenResponse.FromHttpResponseAsync(await computeCredential.HttpClient.SendAsync(request, taskCancellationToken).ConfigureAwait(false), computeCredential.Clock, ServiceCredential.Logger);
      computeCredential.Token = tokenResponse;
      return true;
    }

    public static Task<bool> IsRunningOnComputeEngine() => ComputeCredential.isRunningOnComputeEngineCached.Value;

    private static async Task<bool> IsRunningOnComputeEngineNoCache()
    {
      try
      {
        ServiceCredential.Logger.Info("Checking connectivity to ComputeEngine metadata server.");
        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "http://metadata.google.internal");
        CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        cancellationTokenSource.CancelAfter(1000);
        IEnumerable<string> values;
        if ((await new System.Net.Http.HttpClient().SendAsync(request, cancellationTokenSource.Token).ConfigureAwait(false)).Headers.TryGetValues("Metadata-Flavor", out values))
        {
          foreach (string str in values)
          {
            if (str == "Google")
              return true;
          }
        }
        ServiceCredential.Logger.Info("Response came from a source other than the Google Compute Engine metadata server.");
        return false;
      }
      catch (HttpRequestException ex)
      {
        ServiceCredential.Logger.Debug("Could not reach the Google Compute Engine metadata service. That is alright if this application is not running on GCE.");
        return false;
      }
      catch (WebException ex)
      {
        ServiceCredential.Logger.Debug("Could not reach the Google Compute Engine metadata service. That is alright if this application is not running on GCE.");
        return false;
      }
      catch (OperationCanceledException ex)
      {
        ServiceCredential.Logger.Warning("Could not reach the Google Compute Engine metadata service. Operation timed out.");
        return false;
      }
    }

    public new class Initializer : ServiceCredential.Initializer
    {
      public Initializer()
        : this("http://metadata/computeMetadata/v1/instance/service-accounts/default/token")
      {
      }

      public Initializer(string tokenUrl)
        : base(tokenUrl)
      {
      }
    }
  }
}
