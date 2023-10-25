// Decompiled with JetBrains decompiler
// Type: Google.Apis.Requests.Parameters.ParameterValidator
// Assembly: Google.Apis.Core, Version=1.30.0.0, Culture=neutral, PublicKeyToken=4b01fa6e34db77ab
// MVID: 4EC5AF78-9014-40C1-B780-6317D8DBC917
// Assembly location: C:\Users\Admin\Desktop\re\YoutubeApp\Google.Apis.Core.dll

using Google.Apis.Discovery;
using Google.Apis.Testing;
using System.Text.RegularExpressions;

namespace Google.Apis.Requests.Parameters
{
  public static class ParameterValidator
  {
    [VisibleForTestOnly]
    public static bool ValidateRegex(IParameter param, string paramValue) => string.IsNullOrEmpty(param.Pattern) || new Regex(param.Pattern).IsMatch(paramValue);

    public static bool ValidateParameter(IParameter parameter, string value) => string.IsNullOrEmpty(value) ? !parameter.IsRequired : ParameterValidator.ValidateRegex(parameter, value);
  }
}
