// Decompiled with JetBrains decompiler
// Type: Google.Apis.Util.Store.NullDataStore
// Assembly: Google.Apis, Version=1.30.0.0, Culture=neutral, PublicKeyToken=4b01fa6e34db77ab
// MVID: 6C44ABAE-71BD-4009-BDB7-D7E324A25671
// Assembly location: C:\Users\Admin\Desktop\re\YoutubeApp\Google.Apis.dll

using System.Threading.Tasks;

namespace Google.Apis.Util.Store
{
  public class NullDataStore : IDataStore
  {
    private static readonly Task s_completedTask = (Task) NullDataStore.CompletedTask<int>();

    private static Task<T> CompletedTask<T>()
    {
      TaskCompletionSource<T> completionSource = new TaskCompletionSource<T>();
      completionSource.SetResult(default (T));
      return completionSource.Task;
    }

    public Task ClearAsync() => NullDataStore.s_completedTask;

    public Task DeleteAsync<T>(string key) => NullDataStore.s_completedTask;

    public Task<T> GetAsync<T>(string key) => NullDataStore.CompletedTask<T>();

    public Task StoreAsync<T>(string key, T value) => NullDataStore.s_completedTask;
  }
}
