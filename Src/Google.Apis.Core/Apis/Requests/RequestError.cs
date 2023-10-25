// Decompiled with JetBrains decompiler
// Type: Google.Apis.Requests.RequestError
// Assembly: Google.Apis.Core, Version=1.30.0.0, Culture=neutral, PublicKeyToken=4b01fa6e34db77ab
// MVID: 4EC5AF78-9014-40C1-B780-6317D8DBC917
// Assembly location: C:\Users\Admin\Desktop\re\YoutubeApp\Google.Apis.Core.dll

using Google.Apis.Util;
using System.Collections.Generic;
using System.Text;

namespace Google.Apis.Requests
{
  public class RequestError
  {
    public IList<SingleError> Errors { get; set; }

    public int Code { get; set; }

    public string Message { get; set; }

    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.AppendLine(this.GetType().FullName).Append(this.Message).AppendFormat(" [{0}]", (object) this.Code).AppendLine();
      if (this.Errors.IsNullOrEmpty<SingleError>())
      {
        stringBuilder.AppendLine("No individual errors");
      }
      else
      {
        stringBuilder.AppendLine("Errors [");
        foreach (SingleError error in (IEnumerable<SingleError>) this.Errors)
          stringBuilder.Append('\t').AppendLine(error.ToString());
        stringBuilder.AppendLine("]");
      }
      return stringBuilder.ToString();
    }

    public enum ErrorCodes
    {
      ETagConditionFailed = 412, // 0x0000019C
    }
  }
}
