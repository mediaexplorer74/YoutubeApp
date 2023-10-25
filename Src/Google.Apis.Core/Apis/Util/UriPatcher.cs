// Decompiled with JetBrains decompiler
// Type: Google.Apis.Util.UriPatcher
// Assembly: Google.Apis.Core, Version=1.30.0.0, Culture=neutral, PublicKeyToken=4b01fa6e34db77ab
// MVID: 4EC5AF78-9014-40C1-B780-6317D8DBC917
// Assembly location: C:\Users\Admin\Desktop\re\YoutubeApp\Google.Apis.Core.dll

using System;
using System.Reflection;

namespace Google.Apis.Util
{
  public static class UriPatcher
  {
    public static void PatchUriQuirks()
    {
      Type uriParser = typeof (Uri).GetTypeInfo().Assembly.GetType("System.UriParser");
      if ((object) uriParser == null)
        return;
      if (new Uri("http://example.com/%2f").AbsolutePath == "//" || new Uri("https://example.com/%2f").AbsolutePath == "//")
      {
        MethodInfo setUpdatableFlagsMethod = TypeExtensions.GetMethod(uriParser, "SetUpdatableFlags", BindingFlags.Instance | BindingFlags.NonPublic);
        if ((object) setUpdatableFlagsMethod != null)
        {
          Action<string> action = (Action<string>) (fieldName =>
          {
            FieldInfo field = TypeExtensions.GetField(uriParser, fieldName, BindingFlags.Static | BindingFlags.NonPublic);
            if ((object) field == null)
              return;
            object obj = field.GetValue((object) null);
            if (obj == null)
              return;
            setUpdatableFlagsMethod.Invoke(obj, new object[1]
            {
              (object) 0
            });
          });
          action("HttpUri");
          action("HttpsUri");
        }
      }
      if (!(Uri.EscapeDataString("*") == "*"))
        return;
      FieldInfo field1 = TypeExtensions.GetField(uriParser, "s_QuirksVersion", BindingFlags.Static | BindingFlags.NonPublic);
      if ((object) field1 == null || (int) field1.GetValue((object) null) > 2)
        return;
      field1.SetValue((object) null, (object) 3);
    }
  }
}
