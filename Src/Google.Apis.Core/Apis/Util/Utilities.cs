// Decompiled with JetBrains decompiler
// Type: Google.Apis.Util.Utilities
// Assembly: Google.Apis.Core, Version=1.30.0.0, Culture=neutral, PublicKeyToken=4b01fa6e34db77ab
// MVID: 4EC5AF78-9014-40C1-B780-6317D8DBC917
// Assembly location: C:\Users\Admin\Desktop\re\YoutubeApp\Google.Apis.Core.dll

using Google.Apis.Testing;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Google.Apis.Util
{
  public static class Utilities
  {
    [VisibleForTestOnly]
    public static string GetLibraryVersion() => Regex.Match(typeof (Utilities).GetTypeInfo().Assembly.FullName, "Version=([\\d\\.]+)").Groups[1].ToString();

    public static T ThrowIfNull<T>(this T obj, string paramName) => (object) obj != null ? obj : throw new ArgumentNullException(paramName);

    public static string ThrowIfNullOrEmpty(this string str, string paramName) => !string.IsNullOrEmpty(str) ? str : throw new ArgumentException("Parameter was empty", paramName);

    internal static bool IsNullOrEmpty<T>(this IEnumerable<T> coll) => coll == null || coll.Count<T>() == 0;

    public static T GetCustomAttribute<T>(this MemberInfo info) where T : Attribute
    {
      object[] array = (object[]) CustomAttributeExtensions.GetCustomAttributes(info, typeof (T), false).ToArray<Attribute>();
      return array.Length != 0 ? (T) array[0] : default (T);
    }

    internal static string GetStringValue(this Enum value)
    {
      FieldInfo field = TypeExtensions.GetField(value.GetType(), value.ToString());
      field.ThrowIfNull<FieldInfo>(nameof (value));
      return (field.GetCustomAttribute<StringValueAttribute>() ?? throw new ArgumentException(string.Format("Enum value '{0}' does not contain a StringValue attribute", (object) field), nameof (value))).Text;
    }

    public static string GetEnumStringValue(Enum value) => value.GetStringValue();

    [VisibleForTestOnly]
    public static string ConvertToString(object o)
    {
      if (o == null)
        return (string) null;
      if (o.GetType().GetTypeInfo().IsEnum)
      {
        StringValueAttribute customAttribute = TypeExtensions.GetField(o.GetType(), o.ToString()).GetCustomAttribute<StringValueAttribute>();
        return customAttribute == null ? o.ToString() : customAttribute.Text;
      }
      switch (o)
      {
        case DateTime date:
          return Utilities.ConvertToRFC3339(date);
        case bool _:
          return o.ToString().ToLowerInvariant();
        default:
          return o.ToString();
      }
    }

    internal static string ConvertToRFC3339(DateTime date)
    {
      if (date.Kind == DateTimeKind.Unspecified)
        date = date.ToUniversalTime();
      return date.ToString("yyyy-MM-dd'T'HH:mm:ss.fffK", (IFormatProvider) DateTimeFormatInfo.InvariantInfo);
    }

    public static DateTime? GetDateTimeFromString(string raw)
    {
      DateTime result;
      return !DateTime.TryParse(raw, out result) ? new DateTime?() : new DateTime?(result);
    }

    public static string GetStringFromDateTime(DateTime? date) => !date.HasValue ? (string) null : Utilities.ConvertToRFC3339(date.Value);
  }
}
