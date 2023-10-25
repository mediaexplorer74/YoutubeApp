// Decompiled with JetBrains decompiler
// Type: Google.Apis.Util.Store.MemoryDataStore
// Assembly: Google.Apis, Version=1.30.0.0, Culture=neutral, PublicKeyToken=4b01fa6e34db77ab
// MVID: 6C44ABAE-71BD-4009-BDB7-D7E324A25671
// Assembly location: C:\Users\Admin\Desktop\re\YoutubeApp\Google.Apis.dll

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Google.Apis.Util.Store
{
  public class MemoryDataStore : IDataStore
  {
    private static readonly Task s_completedTask = (Task) Task.FromResult<int>(0);

    internal static string GenerateStoredKey(string key, Type t) => string.Format("{0}-{1}", (object) t.FullName, (object) key);

    public Dictionary<string, object> Items { get; } = new Dictionary<string, object>();

    public Task ClearAsync()
    {
      this.Items.Clear();
      return MemoryDataStore.s_completedTask;
    }

    public Task DeleteAsync<T>(string key)
    {
      this.Items.Remove(MemoryDataStore.GenerateStoredKey(key, typeof (T)));
      return MemoryDataStore.s_completedTask;
    }

    public Task<T> GetAsync<T>(string key)
    {
      object result;
      return this.Items.TryGetValue(MemoryDataStore.GenerateStoredKey(key, typeof (T)), out result) ? Task.FromResult<T>((T) result) : Task.FromResult<T>(default (T));
    }

    public Task StoreAsync<T>(string key, T value)
    {
      this.Items[MemoryDataStore.GenerateStoredKey(key, typeof (T))] = (object) value;
      return MemoryDataStore.s_completedTask;
    }
  }
}
