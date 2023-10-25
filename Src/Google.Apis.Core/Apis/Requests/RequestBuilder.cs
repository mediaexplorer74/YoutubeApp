// Decompiled with JetBrains decompiler
// Type: Google.Apis.Requests.RequestBuilder
// Assembly: Google.Apis.Core, Version=1.30.0.0, Culture=neutral, PublicKeyToken=4b01fa6e34db77ab
// MVID: 4EC5AF78-9014-40C1-B780-6317D8DBC917
// Assembly location: C:\Users\Admin\Desktop\re\YoutubeApp\Google.Apis.Core.dll

using Google.Apis.Logging;
using Google.Apis.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;

namespace Google.Apis.Requests
{
  public class RequestBuilder
  {
    private static readonly ILogger Logger = ApplicationContext.Logger.ForType<RequestBuilder>();
    private static Regex PathParametersPattern = new Regex("{[^{}]*}*");
    private static IEnumerable<string> SupportedMethods = (IEnumerable<string>) new List<string>()
    {
      "GET",
      "POST",
      "PUT",
      "DELETE",
      "PATCH"
    };
    private string method;
    private const string OPERATORS = "+#./;?&|!@=";

    static RequestBuilder() => UriPatcher.PatchUriQuirks();

    private IDictionary<string, IList<string>> PathParameters { get; set; }

    private List<KeyValuePair<string, string>> QueryParameters { get; set; }

    public Uri BaseUri { get; set; }

    public string Path { get; set; }

    public string Method
    {
      get => this.method;
      set => this.method = RequestBuilder.SupportedMethods.Contains<string>(value) ? value : throw new ArgumentOutOfRangeException(nameof (Method));
    }

    public RequestBuilder()
    {
      this.PathParameters = (IDictionary<string, IList<string>>) new Dictionary<string, IList<string>>();
      this.QueryParameters = new List<KeyValuePair<string, string>>();
      this.Method = "GET";
    }

    public Uri BuildUri()
    {
      StringBuilder stringBuilder = this.BuildRestPath();
      if (this.QueryParameters.Count > 0)
      {
        stringBuilder.Append(stringBuilder.ToString().Contains("?") ? "&" : "?");
        stringBuilder.Append(string.Join("&", this.QueryParameters.Select<KeyValuePair<string, string>, string>((Func<KeyValuePair<string, string>, string>) (x => !string.IsNullOrEmpty(x.Value) ? string.Format("{0}={1}", (object) Uri.EscapeDataString(x.Key), (object) Uri.EscapeDataString(x.Value)) : Uri.EscapeDataString(x.Key))).ToArray<string>()));
      }
      return new Uri(this.BaseUri, stringBuilder.ToString());
    }

    private StringBuilder BuildRestPath()
    {
      if (string.IsNullOrEmpty(this.Path))
        return new StringBuilder(string.Empty);
      StringBuilder stringBuilder1 = new StringBuilder(this.Path);
      foreach (object match in RequestBuilder.PathParametersPattern.Matches(stringBuilder1.ToString()))
      {
        string oldValue = match.ToString();
        string str1 = oldValue.Substring(1, oldValue.Length - 2);
        string empty = string.Empty;
        if ("+#./;?&|!@=".Contains(str1[0].ToString()))
        {
          empty = str1[0].ToString();
          str1 = str1.Substring(1);
        }
        StringBuilder stringBuilder2 = new StringBuilder();
        string[] strArray = str1.Split(',');
        for (int index = 0; index < strArray.Length; ++index)
        {
          string key = strArray[index];
          bool flag = false;
          int result = 0;
          if (key[key.Length - 1] == '*')
          {
            flag = true;
            key = key.Substring(0, key.Length - 1);
          }
          if (key.Contains(":"))
            key = int.TryParse(key.Substring(key.IndexOf(":") + 1), out result) ? key.Substring(0, key.IndexOf(":")) : throw new ArgumentException(string.Format("Can't parse number after ':' in Path \"{0}\". Parameter is \"{1}\"", (object) this.Path, (object) key), this.Path);
          string separator = empty;
          string str2 = empty;
          switch (empty)
          {
            case "#":
              str2 = index == 0 ? "#" : ",";
              separator = ",";
              break;
            case "&":
            case ";":
              str2 = empty + key + "=";
              separator = ",";
              if (flag)
              {
                separator = empty + key + "=";
                break;
              }
              break;
            case "+":
              str2 = index == 0 ? "" : ",";
              separator = ",";
              break;
            case ".":
              if (!flag)
              {
                separator = ",";
                break;
              }
              break;
            case "/":
              if (!flag)
              {
                separator = ",";
                break;
              }
              break;
            case "?":
              str2 = (index == 0 ? "?" : "&") + key + "=";
              separator = ",";
              if (flag)
              {
                separator = "&" + key + "=";
                break;
              }
              break;
            default:
              if (index > 0)
                str2 = ",";
              separator = ",";
              break;
          }
          if (!this.PathParameters.ContainsKey(key))
            throw new ArgumentException(string.Format("Path \"{0}\" misses a \"{1}\" parameter", (object) this.Path, (object) key), this.Path);
          string stringToEscape = string.Join(separator, (IEnumerable<string>) this.PathParameters[key]);
          if (result != 0 && result < stringToEscape.Length)
            stringToEscape = stringToEscape.Substring(0, result);
          if (empty != "+" && empty != "#" && this.PathParameters[key].Count == 1)
            stringToEscape = Uri.EscapeDataString(stringToEscape);
          string str3 = str2 + stringToEscape;
          stringBuilder2.Append(str3);
        }
        if (empty == ";")
        {
          if (stringBuilder2[stringBuilder2.Length - 1] == '=')
            stringBuilder2 = stringBuilder2.Remove(stringBuilder2.Length - 1, 1);
          stringBuilder2 = stringBuilder2.Replace("=;", ";");
        }
        stringBuilder1 = stringBuilder1.Replace(oldValue, stringBuilder2.ToString());
      }
      return stringBuilder1;
    }

    public void AddParameter(RequestParameterType type, string name, string value)
    {
      name.ThrowIfNull<string>(nameof (name));
      if (value == null)
        RequestBuilder.Logger.Warning("Add parameter should not get null values. type={0}, name={1}", (object) type, (object) name);
      else if (type != RequestParameterType.Path)
      {
        if (type != RequestParameterType.Query)
          throw new ArgumentOutOfRangeException(nameof (type));
        this.QueryParameters.Add(new KeyValuePair<string, string>(name, value));
      }
      else if (!this.PathParameters.ContainsKey(name))
        this.PathParameters[name] = (IList<string>) new List<string>()
        {
          value
        };
      else
        this.PathParameters[name].Add(value);
    }

    public HttpRequestMessage CreateRequest() => new HttpRequestMessage(new HttpMethod(this.Method), this.BuildUri());
  }
}
