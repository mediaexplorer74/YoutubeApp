// Decompiled with JetBrains decompiler
// Type: Google.Apis.Requests.Parameters.ParameterUtils
// Assembly: Google.Apis.Core, Version=1.30.0.0, Culture=neutral, PublicKeyToken=4b01fa6e34db77ab
// MVID: 4EC5AF78-9014-40C1-B780-6317D8DBC917
// Assembly location: C:\Users\Admin\Desktop\re\YoutubeApp\Google.Apis.Core.dll

using Google.Apis.Logging;
using Google.Apis.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;

namespace Google.Apis.Requests.Parameters
{
  public static class ParameterUtils
  {
    private static readonly ILogger Logger = ApplicationContext.Logger.ForType(typeof (ParameterUtils));

    public static FormUrlEncodedContent CreateFormUrlEncodedContent(object request)
    {
      IList<KeyValuePair<string, string>> list = (IList<KeyValuePair<string, string>>) new List<KeyValuePair<string, string>>();
      ParameterUtils.IterateParameters(request, (Action<RequestParameterType, string, object>) ((type, name, value) => list.Add(new KeyValuePair<string, string>(name, value.ToString()))));
      return new FormUrlEncodedContent((IEnumerable<KeyValuePair<string, string>>) list);
    }

    public static IDictionary<string, object> CreateParameterDictionary(object request)
    {
      Dictionary<string, object> dict = new Dictionary<string, object>();
      ParameterUtils.IterateParameters(request, (Action<RequestParameterType, string, object>) ((type, name, value) => dict.Add(name, value)));
      return (IDictionary<string, object>) dict;
    }

    public static void InitParameters(RequestBuilder builder, object request) => ParameterUtils.IterateParameters(request, (Action<RequestParameterType, string, object>) ((type, name, value) => builder.AddParameter(type, name, value.ToString())));

    private static void IterateParameters(
      object request,
      Action<RequestParameterType, string, object> action)
    {
      foreach (PropertyInfo property in TypeExtensions.GetProperties(request.GetType(), BindingFlags.Instance | BindingFlags.Public))
      {
        if (CustomAttributeExtensions.GetCustomAttributes(property, typeof (RequestParameterAttribute), false).FirstOrDefault<Attribute>() is RequestParameterAttribute parameterAttribute)
        {
          string str = parameterAttribute.Name ?? property.Name.ToLower();
          Type propertyType = property.PropertyType;
          object obj = property.GetValue(request, (object[]) null);
          if (propertyType.GetTypeInfo().IsValueType || obj != null)
          {
            if (parameterAttribute.Type == RequestParameterType.UserDefinedQueries)
            {
              if (TypeExtensions.IsAssignableFrom(typeof (IEnumerable<KeyValuePair<string, string>>), obj.GetType()))
              {
                foreach (KeyValuePair<string, string> keyValuePair in (IEnumerable<KeyValuePair<string, string>>) obj)
                  action(RequestParameterType.Query, keyValuePair.Key, (object) keyValuePair.Value);
              }
              else
                ParameterUtils.Logger.Warning("Parameter marked with RequestParameterType.UserDefinedQueries attribute was not of type IEnumerable<KeyValuePair<string, string>> and will be skipped.");
            }
            else
              action(parameterAttribute.Type, str, obj);
          }
        }
      }
    }
  }
}
