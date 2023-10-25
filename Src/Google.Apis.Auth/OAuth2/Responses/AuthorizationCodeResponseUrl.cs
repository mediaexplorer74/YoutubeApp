// Decompiled with JetBrains decompiler
// Type: Google.Apis.Auth.OAuth2.Responses.AuthorizationCodeResponseUrl
// Assembly: Google.Apis.Auth, Version=1.30.0.0, Culture=neutral, PublicKeyToken=4b01fa6e34db77ab
// MVID: A0E91A90-D8FA-470E-9253-CAE205F78A22
// Assembly location: C:\Users\Admin\Desktop\re\YoutubeApp\Google.Apis.Auth.dll

using System;
using System.Collections.Generic;

namespace Google.Apis.Auth.OAuth2.Responses
{
  public class AuthorizationCodeResponseUrl
  {
    public string Code { get; set; }

    public string State { get; set; }

    public string Error { get; set; }

    public string ErrorDescription { get; set; }

    public string ErrorUri { get; set; }

    public AuthorizationCodeResponseUrl(IDictionary<string, string> queryString) => this.InitFromDictionary(queryString);

    public AuthorizationCodeResponseUrl(string query)
    {
      string[] strArray1 = query.Split('&');
      Dictionary<string, string> queryString = new Dictionary<string, string>();
      foreach (string str in strArray1)
      {
        char[] chArray = new char[1]{ '=' };
        string[] strArray2 = str.Split(chArray);
        queryString[strArray2[0]] = strArray2[1];
      }
      this.InitFromDictionary((IDictionary<string, string>) queryString);
    }

    private void InitFromDictionary(IDictionary<string, string> queryString)
    {
      IDictionary<string, Action<string>> dictionary = (IDictionary<string, Action<string>>) new Dictionary<string, Action<string>>();
      dictionary["code"] = (Action<string>) (v => this.Code = v);
      dictionary["state"] = (Action<string>) (v => this.State = v);
      dictionary["error"] = (Action<string>) (v => this.Error = v);
      dictionary["error_description"] = (Action<string>) (v => this.ErrorDescription = v);
      dictionary["error_uri"] = (Action<string>) (v => this.ErrorUri = v);
      foreach (KeyValuePair<string, string> keyValuePair in (IEnumerable<KeyValuePair<string, string>>) queryString)
      {
        Action<string> action;
        if (dictionary.TryGetValue(keyValuePair.Key, out action))
          action(keyValuePair.Value);
      }
    }

    public AuthorizationCodeResponseUrl()
    {
    }
  }
}
