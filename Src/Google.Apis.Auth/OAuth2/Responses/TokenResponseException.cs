// Decompiled with JetBrains decompiler
// Type: Google.Apis.Auth.OAuth2.Responses.TokenResponseException
// Assembly: Google.Apis.Auth, Version=1.30.0.0, Culture=neutral, PublicKeyToken=4b01fa6e34db77ab
// MVID: A0E91A90-D8FA-470E-9253-CAE205F78A22
// Assembly location: C:\Users\Admin\Desktop\re\YoutubeApp\Google.Apis.Auth.dll

using System;
using System.Net;

namespace Google.Apis.Auth.OAuth2.Responses
{
  public class TokenResponseException : Exception
  {
    public TokenErrorResponse Error { get; }

    public HttpStatusCode? StatusCode { get; }

    public TokenResponseException(TokenErrorResponse error)
      : this(error, new HttpStatusCode?())
    {
    }

    public TokenResponseException(TokenErrorResponse error, HttpStatusCode? statusCode)
      : base(error.ToString())
    {
      this.Error = error;
      this.StatusCode = statusCode;
    }
  }
}
