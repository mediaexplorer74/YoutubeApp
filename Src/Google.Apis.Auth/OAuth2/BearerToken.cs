// Decompiled with JetBrains decompiler
// Type: Google.Apis.Auth.OAuth2.BearerToken
// Assembly: Google.Apis.Auth, Version=1.30.0.0, Culture=neutral, PublicKeyToken=4b01fa6e34db77ab
// MVID: A0E91A90-D8FA-470E-9253-CAE205F78A22
// Assembly location: C:\Users\Admin\Desktop\re\YoutubeApp\Google.Apis.Auth.dll

using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Google.Apis.Auth.OAuth2
{
  public class BearerToken
  {
    public class AuthorizationHeaderAccessMethod : IAccessMethod
    {
      private const string Schema = "Bearer";

      public void Intercept(HttpRequestMessage request, string accessToken) => request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

      public string GetAccessToken(HttpRequestMessage request) => request.Headers.Authorization != null && request.Headers.Authorization.Scheme == "Bearer" ? request.Headers.Authorization.Parameter : (string) null;
    }

    public class QueryParameterAccessMethod : IAccessMethod
    {
      private const string AccessTokenKey = "access_token";

      public void Intercept(HttpRequestMessage request, string accessToken)
      {
        Uri requestUri = request.RequestUri;
        request.RequestUri = new Uri(string.Format("{0}{1}{2}={3}", (object) requestUri.ToString(), string.IsNullOrEmpty(requestUri.Query) ? (object) "?" : (object) "&", (object) "access_token", (object) Uri.EscapeDataString(accessToken)));
      }

      public string GetAccessToken(HttpRequestMessage request)
      {
        string query = request.RequestUri.Query;
        if (string.IsNullOrEmpty(query))
          return (string) null;
        string str1 = query.Substring(1);
        char[] chArray1 = new char[1]{ '&' };
        foreach (string str2 in str1.Split(chArray1))
        {
          char[] chArray2 = new char[1]{ '=' };
          string[] strArray = str2.Split(chArray2);
          if (strArray[0].Equals("access_token"))
            return strArray[1];
        }
        return (string) null;
      }
    }
  }
}
