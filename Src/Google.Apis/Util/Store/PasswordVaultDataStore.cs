// Decompiled with JetBrains decompiler
// Type: Google.Apis.Util.Store.PasswordVaultDataStore
// Assembly: Google.Apis, Version=1.30.0.0, Culture=neutral, PublicKeyToken=4b01fa6e34db77ab
// MVID: 6C44ABAE-71BD-4009-BDB7-D7E324A25671
// Assembly location: C:\Users\Admin\Desktop\re\YoutubeApp\Google.Apis.dll

using Google.Apis.Json;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Windows.Security.Credentials;

namespace Google.Apis.Util.Store
{
  public class PasswordVaultDataStore : IDataStore
  {
    private const string ResourcePrefix = "google-datastore";
    private readonly PasswordVault vault = new PasswordVault();

    private string MakeResource<T>()
    {
        return string.Format("{0}-{1}", (object)"google-datastore", (object)typeof(T));
    }

    public Task ClearAsync()
    {
      try
      {
        foreach (PasswordCredential passwordCredential in this.vault.RetrieveAll()
                    .Where<PasswordCredential>((Func<PasswordCredential, bool>)
                    (x => x.Resource.StartsWith("google-datastore"))))
        {
            this.vault.Remove(passwordCredential);
        }
      }
      catch (Exception ex)
      {
        Debug.WriteLine("[ex] ClearAsync bug: " + ex.Message);
      }
      return Task.CompletedTask;
    }

    public Task DeleteAsync<T>(string key)
    {
      try
      {
        this.vault.Remove(this.vault.Retrieve(this.MakeResource<T>(), key));
      }
      catch (Exception ex)
      {
        Debug.WriteLine("[ex] DeleteAsync bug: " + ex.Message);
      }

      return Task.CompletedTask;
    }

    public Task<T> GetAsync<T>(string key)
    {
      try
      {
        PasswordCredential passwordCredential = this.vault.Retrieve(this.MakeResource<T>(), key);

        passwordCredential.RetrievePassword();

        return Task.FromResult<T>(NewtonsoftJsonSerializer.Instance.Deserialize<T>(passwordCredential.Password));
      }
      catch (Exception ex)
      {
        Debug.WriteLine("[ex] GetAsync bug: " + ex.Message);
        return Task.FromResult<T>(default (T));
      }
    }

    public Task StoreAsync<T>(string key, T value)
    {
      string str = NewtonsoftJsonSerializer.Instance.Serialize((object) value);

      try
      {
        this.vault.Add(new PasswordCredential(this.MakeResource<T>(), key, str));
      }
      catch (Exception ex)
      {
        Debug.WriteLine("[ex] GetAsync bug: " + ex.Message);
        //RnD
        //return Task.FromResult<T>(default(T));
      }

      return Task.CompletedTask;
      }//StoreAsync<T>
   }
}
