// Decompiled with JetBrains decompiler
// Type: Google.Apis.Auth.OAuth2.UwpCodeReceiver
// Assembly: Google.Apis.Auth, Version=1.30.0.0, Culture=neutral, PublicKeyToken=4b01fa6e34db77ab
// MVID: A0E91A90-D8FA-470E-9253-CAE205F78A22
// Assembly location: C:\Users\Admin\Desktop\re\YoutubeApp\Google.Apis.Auth.dll

using Google.Apis.Auth.OAuth2.Requests;
using Google.Apis.Auth.OAuth2.Responses;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Windows.Security.Authentication.Web;

namespace Google.Apis.Auth.OAuth2
{
  public class UwpCodeReceiver : ICodeReceiver
  {
    public string RedirectUri => "urn:ietf:wg:oauth:2.0:oob";

    public async Task<AuthorizationCodeResponseUrl> ReceiveCodeAsync(
      AuthorizationCodeRequestUrl url,
      CancellationToken taskCancellationToken)
    {
      if (taskCancellationToken.CanBeCanceled)
      {
        //throw new NotSupportedException
        Debug.WriteLine("[!] Cancellation is not supported in UwpCodeReceiver.ReceiveCodeAsync()");
        
        return default;
      }

      Uri uri = new Uri("https://accounts.google.com/o/oauth2/approval");
      
      WebAuthenticationResult authenticationResult 
                = await WebAuthenticationBroker.AuthenticateAsync((WebAuthenticationOptions) 2, 
                url.Build(), uri);

      WebAuthenticationStatus responseStatus = authenticationResult.ResponseStatus;

        if (responseStatus == null)
        {
        return new AuthorizationCodeResponseUrl(
            authenticationResult.ResponseData.ToLowerInvariant().StartsWith("success ") 
            ? authenticationResult.ResponseData.Substring(8) : authenticationResult.ResponseData);
        }

        if (responseStatus == (WebAuthenticationStatus)2)
        {
            throw new UwpCodeReceiver.AuthenticateException(
                string.Format("WebAuthenticationBroker.AuthenticateAsync() returned HTTP error: {0}",
                (object)authenticationResult.ResponseErrorDetail));
        }

       //throw new UwpCodeReceiver.AuthenticateException
        Debug.WriteLine(
          string.Format("WebAuthenticationBroker.AuthenticateAsync() error: {0}", 
          (object) authenticationResult.ResponseStatus));

       //RnD    
       return new AuthorizationCodeResponseUrl(
            authenticationResult.ResponseData.ToLowerInvariant().StartsWith("success ")
            ? authenticationResult.ResponseData.Substring(8) : authenticationResult.ResponseData);
    }

    public sealed class AuthenticateException : Exception
    {
      public AuthenticateException(string message)
        : base(message)
      {
      }
    }
  }
}
