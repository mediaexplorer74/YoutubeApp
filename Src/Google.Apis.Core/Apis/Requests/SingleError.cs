// Decompiled with JetBrains decompiler
// Type: Google.Apis.Requests.SingleError
// Assembly: Google.Apis.Core, Version=1.30.0.0, Culture=neutral, PublicKeyToken=4b01fa6e34db77ab
// MVID: 4EC5AF78-9014-40C1-B780-6317D8DBC917
// Assembly location: C:\Users\Admin\Desktop\re\YoutubeApp\Google.Apis.Core.dll

namespace Google.Apis.Requests
{
  public class SingleError
  {
    public string Domain { get; set; }

    public string Reason { get; set; }

    public string Message { get; set; }

    public string LocationType { get; set; }

    public string Location { get; set; }

    public override string ToString() => string.Format("Message[{0}] Location[{1} - {2}] Reason[{3}] Domain[{4}]", (object) this.Message, (object) this.Location, (object) this.LocationType, (object) this.Reason, (object) this.Domain);
  }
}
