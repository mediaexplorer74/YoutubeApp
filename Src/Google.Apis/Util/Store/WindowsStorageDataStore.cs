// Decompiled with JetBrains decompiler
// Type: Google.Apis.Util.Store.WindowsStorageDataStore
// Assembly: Google.Apis, Version=1.30.0.0, Culture=neutral, PublicKeyToken=4b01fa6e34db77ab
// MVID: 6C44ABAE-71BD-4009-BDB7-D7E324A25671
// Assembly location: C:\Users\Admin\Desktop\re\YoutubeApp\Google.Apis.dll

using Google.Apis.Json;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace Google.Apis.Util.Store
{
  public class WindowsStorageDataStore : IDataStore
  {
    public WindowsStorageDataStore(StorageFolder folder) => this.Folder = folder;

    public StorageFolder Folder { get; }

    private async Task EnsureFolderExists()
    {
      StorageFolder parent = await this.Folder.GetParentAsync();
      if (parent == null)
        throw new InvalidOperationException("Storage directory does not exist.");
      if (await parent.TryGetItemAsync(this.Folder.Name) != null)
        return;
      StorageFolder folderAsync = await parent.CreateFolderAsync(this.Folder.Name, (CreationCollisionOption) 2);
    }

    private static string GenerateStoredKey(string key, Type t) => string.Format("{0}-{1}", (object) t.FullName, (object) key);

    public async Task ClearAsync()
    {
      try
      {
        await this.Folder.DeleteAsync();
      }
      catch
      {
      }
    }

    public async Task DeleteAsync<T>(string key)
    {
      IStorageItem itemAsync = await this.Folder.TryGetItemAsync(WindowsStorageDataStore.GenerateStoredKey(key, typeof (T)));
      if (itemAsync == null)
        return;
      await itemAsync.DeleteAsync();
    }

    public async Task<T> GetAsync<T>(string key)
    {
      key.ThrowIfNullOrEmpty(nameof (key));
      IStorageItem itemAsync = await this.Folder.TryGetItemAsync(WindowsStorageDataStore.GenerateStoredKey(key, typeof (T)));
      if (itemAsync == null)
        return default (T);
      using (Stream stream = await WindowsRuntimeStorageExtensions.OpenStreamForReadAsync((IStorageFile) itemAsync))
      {
        MemoryStream ms = new MemoryStream();
        await stream.CopyToAsync((Stream) ms);
        return NewtonsoftJsonSerializer.Instance.Deserialize<T>(Encoding.UTF8.GetString(ms.ToArray()));
      }
    }

    public async Task StoreAsync<T>(string key, T value)
    {
      key.ThrowIfNullOrEmpty(nameof (key));
      await this.EnsureFolderExists();
      byte[] serialized = Encoding.UTF8.GetBytes(NewtonsoftJsonSerializer.Instance.Serialize((object) (T) value));
      using (Stream stream = await WindowsRuntimeStorageExtensions.OpenStreamForWriteAsync((IStorageFolder) this.Folder, WindowsStorageDataStore.GenerateStoredKey(key, typeof (T)), (CreationCollisionOption) 1))
        await stream.WriteAsync(serialized, 0, serialized.Length);
    }
  }
}
