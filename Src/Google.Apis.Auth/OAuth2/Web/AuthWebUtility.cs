// Decompiled with JetBrains decompiler
// Type: Google.Apis.Auth.OAuth2.Web.AuthWebUtility
// Assembly: Google.Apis.Auth, Version=1.30.0.0, Culture=neutral, PublicKeyToken=4b01fa6e34db77ab
// MVID: A0E91A90-D8FA-470E-9253-CAE205F78A22
// Assembly location: C:\Users\Admin\Desktop\re\YoutubeApp\Google.Apis.Auth.dll

using Google.Apis.Auth.OAuth2.Responses;
using Google.Apis.Util.Store;
using System.Threading.Tasks;

namespace Google.Apis.Auth.OAuth2.Web
{
  public class AuthWebUtility
  {
    public static async Task<string> ExtracRedirectFromState(
      IDataStore dataStore,
      string userId,
      string state)
    {
      string oauthState = state;
      if (dataStore != null)
      {
        string userKey = "oauth_" + userId;
        if (!object.Equals((object) oauthState, (object) await dataStore.GetAsync<string>(userKey).ConfigureAwait(false)))
          throw new TokenResponseException(new TokenErrorResponse()
          {
            Error = "State is invalid"
          });
        await dataStore.DeleteAsync<string>(userKey).ConfigureAwait(false);
        oauthState = oauthState.Substring(0, oauthState.Length - 8);
        userKey = (string) null;
      }
      return oauthState;
    }
  }
}
