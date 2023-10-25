// Decompiled with JetBrains decompiler
// Type: Google.Apis.Requests.Parameters.ParameterCollection
// Assembly: Google.Apis.Core, Version=1.30.0.0, Culture=neutral, PublicKeyToken=4b01fa6e34db77ab
// MVID: 4EC5AF78-9014-40C1-B780-6317D8DBC917
// Assembly location: C:\Users\Admin\Desktop\re\YoutubeApp\Google.Apis.Core.dll

using Google.Apis.Util;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Google.Apis.Requests.Parameters
{
  public class ParameterCollection : List<KeyValuePair<string, string>>
  {
    public ParameterCollection()
    {
    }

    public ParameterCollection(
      IEnumerable<KeyValuePair<string, string>> collection)
      : base(collection)
    {
    }

    public void Add(string key, string value) => this.Add(new KeyValuePair<string, string>(key, value));

    public bool ContainsKey(string key)
    {
      key.ThrowIfNullOrEmpty(nameof (key));
      return this.TryGetValue(key, out string _);
    }

    public bool TryGetValue(string key, out string value)
    {
      key.ThrowIfNullOrEmpty(nameof (key));
      foreach (KeyValuePair<string, string> keyValuePair in (List<KeyValuePair<string, string>>) this)
      {
        if (keyValuePair.Key.Equals(key))
        {
          value = keyValuePair.Value;
          return true;
        }
      }
      value = (string) null;
      return false;
    }

    public string GetFirstMatch(string key)
    {
      string firstMatch;
      if (!this.TryGetValue(key, out firstMatch))
        throw new KeyNotFoundException("Parameter with the name '" + key + "' was not found.");
      return firstMatch;
    }

    public IEnumerable<string> GetAllMatches(string key)
    {
      ParameterCollection parameterCollection = this;
      key.ThrowIfNullOrEmpty(nameof (key));
      foreach (KeyValuePair<string, string> keyValuePair in (List<KeyValuePair<string, string>>) parameterCollection)
      {
        if (keyValuePair.Key.Equals(key))
          yield return keyValuePair.Value;
      }
    }

    public IEnumerable<string> this[string key] => this.GetAllMatches(key);

    public static ParameterCollection FromQueryString(string qs)
    {
      ParameterCollection parameterCollection = new ParameterCollection();
      string str1 = qs;
      char[] chArray = new char[1]{ '&' };
      foreach (string str2 in str1.Split(chArray))
      {
        string[] strArray = str2.Split('=');
        if (strArray.Length != 2)
          throw new ArgumentException(string.Format("Invalid query string [{0}]. Invalid part [{1}]", (object) qs, (object) str2));
        parameterCollection.Add(Uri.UnescapeDataString(strArray[0]), Uri.UnescapeDataString(strArray[1]));
      }
      return parameterCollection;
    }

    public static ParameterCollection FromDictionary(IDictionary<string, object> dictionary)
    {
      ParameterCollection parameterCollection = new ParameterCollection();
      foreach (KeyValuePair<string, object> keyValuePair in (IEnumerable<KeyValuePair<string, object>>) dictionary)
      {
        IEnumerable enumerable = keyValuePair.Value as IEnumerable;
        if (!(keyValuePair.Value is string) && enumerable != null)
        {
          foreach (object o in enumerable)
            parameterCollection.Add(keyValuePair.Key, Utilities.ConvertToString(o));
        }
        else
          parameterCollection.Add(keyValuePair.Key, keyValuePair.Value == null ? (string) null : Utilities.ConvertToString(keyValuePair.Value));
      }
      return parameterCollection;
    }
  }
}
