// Decompiled with JetBrains decompiler
// Type: Google.GoogleApiException
// Assembly: Google.Apis.Core, Version=1.30.0.0, Culture=neutral, PublicKeyToken=4b01fa6e34db77ab
// MVID: 4EC5AF78-9014-40C1-B780-6317D8DBC917
// Assembly location: C:\Users\Admin\Desktop\re\YoutubeApp\Google.Apis.Core.dll

using Google.Apis.Requests;
using Google.Apis.Util;
using System;
using System.Net;

namespace Google
{
  public class GoogleApiException : Exception
  {
    private readonly string serviceName;

    public string ServiceName => this.serviceName;

    public GoogleApiException(string serviceName, string message, Exception inner)
      : base(message, inner)
    {
      serviceName.ThrowIfNull<string>(nameof (serviceName));
      this.serviceName = serviceName;
    }

    public GoogleApiException(string serviceName, string message)
      : this(serviceName, message, (Exception) null)
    {
    }

    public RequestError Error { get; set; }

    public HttpStatusCode HttpStatusCode { get; set; }

    public override string ToString() => string.Format("The service {1} has thrown an exception: {0}", (object) base.ToString(), (object) this.serviceName);
  }
}
