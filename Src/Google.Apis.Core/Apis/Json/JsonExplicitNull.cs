// Decompiled with JetBrains decompiler
// Type: Google.Apis.Json.JsonExplicitNull
// Assembly: Google.Apis.Core, Version=1.30.0.0, Culture=neutral, PublicKeyToken=4b01fa6e34db77ab
// MVID: 4EC5AF78-9014-40C1-B780-6317D8DBC917
// Assembly location: C:\Users\Admin\Desktop\re\YoutubeApp\Google.Apis.Core.dll

using System;
using System.Collections;
using System.Collections.Generic;

namespace Google.Apis.Json
{
  public static class JsonExplicitNull
  {
    public static IList<T> ForIList<T>() => (IList<T>) JsonExplicitNull.ExplicitNullList<T>.Instance;

    [JsonExplicitNull]
    private sealed class ExplicitNullList<T> : IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable
    {
      public static JsonExplicitNull.ExplicitNullList<T> Instance = new JsonExplicitNull.ExplicitNullList<T>();

      public T this[int index]
      {
        get => throw new NotSupportedException();
        set => throw new NotSupportedException();
      }

      public int Count => throw new NotSupportedException();

      public bool IsReadOnly => throw new NotSupportedException();

      public void Add(T item) => throw new NotSupportedException();

      public void Clear() => throw new NotSupportedException();

      public bool Contains(T item) => throw new NotSupportedException();

      public void CopyTo(T[] array, int arrayIndex) => throw new NotSupportedException();

      public IEnumerator<T> GetEnumerator() => throw new NotSupportedException();

      public int IndexOf(T item) => throw new NotSupportedException();

      public void Insert(int index, T item) => throw new NotSupportedException();

      public bool Remove(T item) => throw new NotSupportedException();

      public void RemoveAt(int index) => throw new NotSupportedException();

      IEnumerator IEnumerable.GetEnumerator() => throw new NotSupportedException();
    }
  }
}
