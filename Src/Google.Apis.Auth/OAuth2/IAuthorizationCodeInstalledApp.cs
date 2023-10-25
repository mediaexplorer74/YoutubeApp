// Decompiled with JetBrains decompiler
// Type: Google.Apis.Auth.OAuth2.IAuthorizationCodeInstalledApp
// Assembly: Google.Apis.Auth, Version=1.30.0.0, Culture=neutral, PublicKeyToken=4b01fa6e34db77ab
// MVID: A0E91A90-D8FA-470E-9253-CAE205F78A22
// Assembly location: C:\Users\Admin\Desktop\re\YoutubeApp\Google.Apis.Auth.dll

using Google.Apis.Auth.OAuth2.Flows;
using System.Threading;
using System.Threading.Tasks;

namespace Google.Apis.Auth.OAuth2
{
  public interface IAuthorizationCodeInstalledApp
  {
    IAuthorizationCodeFlow Flow { get; }

    ICodeReceiver CodeReceiver { get; }

    Task<UserCredential> AuthorizeAsync(string userId, CancellationToken taskCancellationToken);
  }
}
