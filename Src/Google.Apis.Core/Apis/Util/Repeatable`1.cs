// Decompiled with JetBrains decompiler
// Type: Google.Apis.Util.Repeatable`1
// Assembly: Google.Apis.Core, Version=1.30.0.0, Culture=neutral, PublicKeyToken=4b01fa6e34db77ab
// MVID: 4EC5AF78-9014-40C1-B780-6317D8DBC917
// Assembly location: C:\Users\Admin\Desktop\re\YoutubeApp\Google.Apis.Core.dll

using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Google.Apis.Util
{
  public class Repeatable<T> : IEnumerable<T>, IEnumerable
  {
    private readonly IList<T> values;

    public Repeatable(IEnumerable<T> enumeration) => this.values = (IList<T>) new ReadOnlyCollection<T>((IList<T>) new List<T>(enumeration));

    public IEnumerator<T> GetEnumerator() => this.values.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this.GetEnumerator();

    public static implicit operator Repeatable<T>(T elem)
    {
      if ((object) elem == null)
        return (Repeatable<T>) null;
      return new Repeatable<T>((IEnumerable<T>) new T[1]
      {
        elem
      });
    }

    public static implicit operator Repeatable<T>(T[] elem) => elem.Length == 0 ? (Repeatable<T>) null : new Repeatable<T>((IEnumerable<T>) elem);

    public static implicit operator Repeatable<T>(List<T> elem) => new Repeatable<T>((IEnumerable<T>) elem);
  }
}
