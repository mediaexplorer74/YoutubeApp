// Decompiled with JetBrains decompiler
// Type: Google.Apis.Auth.OAuth2.DefaultCredentialProvider
// Assembly: Google.Apis.Auth, Version=1.30.0.0, Culture=neutral, PublicKeyToken=4b01fa6e34db77ab
// MVID: A0E91A90-D8FA-470E-9253-CAE205F78A22
// Assembly location: C:\Users\Admin\Desktop\re\YoutubeApp\Google.Apis.Auth.dll

using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2.Responses;
using Google.Apis.Json;
using Google.Apis.Logging;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Google.Apis.Auth.OAuth2
{
  internal class DefaultCredentialProvider
  {
    private static readonly ILogger Logger = ApplicationContext.Logger.ForType<DefaultCredentialProvider>();
    public const string CredentialEnvironmentVariable = "GOOGLE_APPLICATION_CREDENTIALS";
    private const string WellKnownCredentialsFile = "application_default_credentials.json";
    private const string AppdataEnvironmentVariable = "APPDATA";
    private const string HomeEnvironmentVariable = "HOME";
    private const string CloudSDKConfigDirectoryWindows = "gcloud";
    private const string HelpPermalink = "https://developers.google.com/accounts/docs/application-default-credentials";
    private static readonly string CloudSDKConfigDirectoryUnix = Path.Combine(".config", "gcloud");
    private readonly Lazy<Task<GoogleCredential>> cachedCredentialTask;

    public DefaultCredentialProvider() => this.cachedCredentialTask = new Lazy<Task<GoogleCredential>>(new Func<Task<GoogleCredential>>(this.CreateDefaultCredentialAsync));

    public Task<GoogleCredential> GetDefaultCredentialAsync() => this.cachedCredentialTask.Value;

    private async Task<GoogleCredential> CreateDefaultCredentialAsync()
    {
      string environmentVariable = this.GetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS");
      if (!string.IsNullOrWhiteSpace(environmentVariable))
      {
        try
        {
          return this.CreateDefaultCredentialFromFile(environmentVariable);
        }
        catch (Exception ex)
        {
          throw new InvalidOperationException(string.Format("Error reading credential file from location {0}: {1}\nPlease check the value of the Environment Variable {2}", (object) environmentVariable, (object) ex.Message, (object) "GOOGLE_APPLICATION_CREDENTIALS"));
        }
      }
      else
      {
        string credentialFilePath = this.GetWellKnownCredentialFilePath();
        if (!string.IsNullOrWhiteSpace(credentialFilePath))
        {
          try
          {
            return this.CreateDefaultCredentialFromFile(credentialFilePath);
          }
          catch (FileNotFoundException ex)
          {
            DefaultCredentialProvider.Logger.Debug("Well-known credential file {0} not found.", (object) credentialFilePath);
          }
          catch (DirectoryNotFoundException ex)
          {
            DefaultCredentialProvider.Logger.Debug("Well-known credential file {0} not found.", (object) credentialFilePath);
          }
          catch (Exception ex)
          {
            throw new InvalidOperationException(string.Format("Error reading credential file from location {0}: {1}\nPlease rerun 'gcloud auth login' to regenerate credentials file.", (object) credentialFilePath, (object) ex.Message));
          }
        }
        DefaultCredentialProvider.Logger.Debug("Checking whether the application is running on ComputeEngine.");
        if (!await ComputeCredential.IsRunningOnComputeEngine().ConfigureAwait(false))
          throw new InvalidOperationException(string.Format("The Application Default Credentials are not available. They are available if running in Google Compute Engine. Otherwise, the environment variable {0} must be defined pointing to a file defining the credentials. See {1} for more information.", (object) "GOOGLE_APPLICATION_CREDENTIALS", (object) "https://developers.google.com/accounts/docs/application-default-credentials"));
        DefaultCredentialProvider.Logger.Debug("ComputeEngine check passed. Using ComputeEngine Credentials.");
        return new GoogleCredential((ICredential) new ComputeCredential());
      }
    }

    private GoogleCredential CreateDefaultCredentialFromFile(string credentialPath)
    {
      DefaultCredentialProvider.Logger.Debug("Loading Credential from file {0}", (object) credentialPath);
      using (Stream stream = this.GetStream(credentialPath))
        return this.CreateDefaultCredentialFromStream(stream);
    }

    internal GoogleCredential CreateDefaultCredentialFromStream(Stream stream)
    {
      JsonCredentialParameters credentialParameters;
      try
      {
        credentialParameters = NewtonsoftJsonSerializer.Instance.Deserialize<JsonCredentialParameters>(stream);
      }
      catch (Exception ex)
      {
        throw new InvalidOperationException("Error deserializing JSON credential data.", ex);
      }
      return DefaultCredentialProvider.CreateDefaultCredentialFromParameters(credentialParameters);
    }

    internal GoogleCredential CreateDefaultCredentialFromJson(string json)
    {
      JsonCredentialParameters credentialParameters;
      try
      {
        credentialParameters = NewtonsoftJsonSerializer.Instance.Deserialize<JsonCredentialParameters>(json);
      }
      catch (Exception ex)
      {
        throw new InvalidOperationException("Error deserializing JSON credential data.", ex);
      }
      return DefaultCredentialProvider.CreateDefaultCredentialFromParameters(credentialParameters);
    }

    private static GoogleCredential CreateDefaultCredentialFromParameters(
      JsonCredentialParameters credentialParameters)
    {
      switch (credentialParameters.Type)
      {
        case "authorized_user":
          return new GoogleCredential((ICredential) DefaultCredentialProvider.CreateUserCredentialFromParameters(credentialParameters));
        case "service_account":
          return GoogleCredential.FromCredential(DefaultCredentialProvider.CreateServiceAccountCredentialFromParameters(credentialParameters));
        default:
          throw new InvalidOperationException(string.Format("Error creating credential from JSON. Unrecognized credential type {0}.", (object) credentialParameters.Type));
      }
    }

    private static UserCredential CreateUserCredentialFromParameters(
      JsonCredentialParameters credentialParameters)
    {
      if (credentialParameters.Type != "authorized_user" || string.IsNullOrEmpty(credentialParameters.ClientId) || string.IsNullOrEmpty(credentialParameters.ClientSecret))
        throw new InvalidOperationException("JSON data does not represent a valid user credential.");
      TokenResponse token = new TokenResponse()
      {
        RefreshToken = credentialParameters.RefreshToken
      };
      GoogleAuthorizationCodeFlow.Initializer initializer = new GoogleAuthorizationCodeFlow.Initializer();
      initializer.ClientSecrets = new ClientSecrets()
      {
        ClientId = credentialParameters.ClientId,
        ClientSecret = credentialParameters.ClientSecret
      };
      return new UserCredential((IAuthorizationCodeFlow) new GoogleAuthorizationCodeFlow(initializer), "ApplicationDefaultCredentials", token);
    }

    private static ServiceAccountCredential CreateServiceAccountCredentialFromParameters(
      JsonCredentialParameters credentialParameters)
    {
      if (credentialParameters.Type != "service_account" || string.IsNullOrEmpty(credentialParameters.ClientEmail) || string.IsNullOrEmpty(credentialParameters.PrivateKey))
        throw new InvalidOperationException("JSON data does not represent a valid service account credential.");
      return new ServiceAccountCredential(new ServiceAccountCredential.Initializer(credentialParameters.ClientEmail).FromPrivateKey(credentialParameters.PrivateKey));
    }

    private string GetWellKnownCredentialFilePath()
    {
      string environmentVariable1 = this.GetEnvironmentVariable("APPDATA");
      if (environmentVariable1 != null)
        return Path.Combine(environmentVariable1, "gcloud", "application_default_credentials.json");
      string environmentVariable2 = this.GetEnvironmentVariable("HOME");
      return environmentVariable2 != null ? Path.Combine(environmentVariable2, DefaultCredentialProvider.CloudSDKConfigDirectoryUnix, "application_default_credentials.json") : Path.Combine("gcloud", "application_default_credentials.json");
    }

    protected virtual string GetEnvironmentVariable(string variableName) => Environment.GetEnvironmentVariable(variableName);

    protected virtual Stream GetStream(string filePath) => (Stream) new FileStream(filePath, FileMode.Open, FileAccess.Read);
  }
}
