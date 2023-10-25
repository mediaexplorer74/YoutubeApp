// Decompiled with JetBrains decompiler
// Type: Google.Apis.Auth.OAuth2.GoogleWebAuthorizationBroker
// Assembly: Google.Apis.Auth, Version=1.30.0.0, Culture=neutral, PublicKeyToken=4b01fa6e34db77ab
// MVID: A0E91A90-D8FA-470E-9253-CAE205F78A22
// Assembly location: C:\Users\Admin\Desktop\re\YoutubeApp\Google.Apis.Auth.dll

using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Util.Store;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Windows.Storage;

namespace Google.Apis.Auth.OAuth2
{
  public class GoogleWebAuthorizationBroker
  {
    private static ICodeReceiver GetDefaultCodeReceiver()
    {
        return (ICodeReceiver)new UwpCodeReceiver();
    }

    private static IDataStore GetDefaultDataStore()
    {
        return (IDataStore)
            //new MemoryDataStore();
            //new WindowsStorageDataStore(KnownFolders.VideosLibrary);
            new PasswordVaultDataStore();
    }

    public static async Task<UserCredential> AuthorizeAsync(
      ClientSecrets clientSecrets,
      IEnumerable<string> scopes,
      string user,
      CancellationToken taskCancellationToken,
      IDataStore dataStore = null,
      ICodeReceiver codeReceiver = null)
    {
      GoogleAuthorizationCodeFlow.Initializer initializer
                = new GoogleAuthorizationCodeFlow.Initializer();

      initializer.ClientSecrets = clientSecrets;

      return await GoogleWebAuthorizationBroker.AuthorizeAsync(
          initializer, scopes, user, taskCancellationToken, dataStore, codeReceiver)
                .ConfigureAwait(false);
    }

    public static async Task<UserCredential> AuthorizeAsync(
      Stream clientSecretsStream,
      IEnumerable<string> scopes,
      string user,
      CancellationToken taskCancellationToken,
      IDataStore dataStore = null,
      ICodeReceiver codeReceiver = null)
    {
      GoogleAuthorizationCodeFlow.Initializer initializer = 
                new GoogleAuthorizationCodeFlow.Initializer();

      initializer.ClientSecretsStream = clientSecretsStream;

      return await GoogleWebAuthorizationBroker.AuthorizeAsync(
          initializer, scopes, user, taskCancellationToken, dataStore, codeReceiver)
                .ConfigureAwait(false);
    }

    public static async Task ReauthorizeAsync(
      UserCredential userCredential,
      CancellationToken taskCancellationToken,
      ICodeReceiver codeReceiver = null)
    {
      codeReceiver = codeReceiver ?? GoogleWebAuthorizationBroker.GetDefaultCodeReceiver();
      userCredential.Token = 
                (await new AuthorizationCodeInstalledApp(userCredential.Flow, codeReceiver)
                .AuthorizeAsync(userCredential.UserId, taskCancellationToken)
                .ConfigureAwait(false)).Token;
    }

    public static async Task<UserCredential> AuthorizeAsync(
      GoogleAuthorizationCodeFlow.Initializer initializer,
      IEnumerable<string> scopes,
      string user,
      CancellationToken taskCancellationToken,
      IDataStore dataStore = null,
      ICodeReceiver codeReceiver = null)
    {
      initializer.Scopes = scopes;

      initializer.DataStore = dataStore ?? GoogleWebAuthorizationBroker.GetDefaultDataStore();
      
      GoogleAuthorizationCodeFlow flow = new GoogleAuthorizationCodeFlow(initializer);
      codeReceiver = codeReceiver ?? GoogleWebAuthorizationBroker.GetDefaultCodeReceiver();
      ICodeReceiver codeReceiver1 = codeReceiver;

      return await new AuthorizationCodeInstalledApp((IAuthorizationCodeFlow) flow, codeReceiver1)
                .AuthorizeAsync(user, taskCancellationToken).ConfigureAwait(false);
    }
  }
}
