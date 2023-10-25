// Decompiled with JetBrains decompiler
// Type: Google.Apis.YouTube.v3.LiveChatMessagesResource
// Assembly: Google.Apis.YouTube.v3, Version=1.29.2.1006, Culture=neutral, PublicKeyToken=4b01fa6e34db77ab
// MVID: 9E53E5C1-D9AB-4142-94A7-8A2435650008
// Assembly location: C:\Users\Admin\Desktop\re\YoutubeApp\Google.Apis.YouTube.v3.dll

using Google.Apis.Discovery;
using Google.Apis.Services;
using Google.Apis.Util;
using Google.Apis.YouTube.v3.Data;

namespace Google.Apis.YouTube.v3
{
  public class LiveChatMessagesResource
  {
    private const string Resource = "liveChatMessages";
    private readonly IClientService service;

    public LiveChatMessagesResource(IClientService service) => this.service = service;

    public virtual LiveChatMessagesResource.DeleteRequest Delete(string id) => new LiveChatMessagesResource.DeleteRequest(this.service, id);

    public virtual LiveChatMessagesResource.InsertRequest Insert(LiveChatMessage body, string part) => new LiveChatMessagesResource.InsertRequest(this.service, body, part);

    public virtual LiveChatMessagesResource.ListRequest List(string liveChatId, string part) => new LiveChatMessagesResource.ListRequest(this.service, liveChatId, part);

    public class DeleteRequest : YouTubeBaseServiceRequest<string>
    {
      public DeleteRequest(IClientService service, string id)
        : base(service)
      {
        this.Id = id;
        this.InitParameters();
      }

      [RequestParameter("id", RequestParameterType.Query)]
      public virtual string Id { get; private set; }

      public override string MethodName => "delete";

      public override string HttpMethod => "DELETE";

      public override string RestPath => "liveChat/messages";

      protected override void InitParameters()
      {
        base.InitParameters();
        this.RequestParameters.Add("id", (IParameter) new Parameter()
        {
          Name = "id",
          IsRequired = true,
          ParameterType = "query",
          DefaultValue = (string) null,
          Pattern = (string) null
        });
      }
    }

    public class InsertRequest : YouTubeBaseServiceRequest<LiveChatMessage>
    {
      public InsertRequest(IClientService service, LiveChatMessage body, string part)
        : base(service)
      {
        this.Part = part;
        this.Body = body;
        this.InitParameters();
      }

      [RequestParameter("part", RequestParameterType.Query)]
      public virtual string Part { get; private set; }

      private LiveChatMessage Body { get; set; }

      protected override object GetBody() => (object) this.Body;

      public override string MethodName => "insert";

      public override string HttpMethod => "POST";

      public override string RestPath => "liveChat/messages";

      protected override void InitParameters()
      {
        base.InitParameters();
        this.RequestParameters.Add("part", (IParameter) new Parameter()
        {
          Name = "part",
          IsRequired = true,
          ParameterType = "query",
          DefaultValue = (string) null,
          Pattern = (string) null
        });
      }
    }

    public class ListRequest : YouTubeBaseServiceRequest<LiveChatMessageListResponse>
    {
      public ListRequest(IClientService service, string liveChatId, string part)
        : base(service)
      {
        this.LiveChatId = liveChatId;
        this.Part = part;
        this.InitParameters();
      }

      [RequestParameter("liveChatId", RequestParameterType.Query)]
      public virtual string LiveChatId { get; private set; }

      [RequestParameter("part", RequestParameterType.Query)]
      public virtual string Part { get; private set; }

      [RequestParameter("hl", RequestParameterType.Query)]
      public virtual string Hl { get; set; }

      [RequestParameter("maxResults", RequestParameterType.Query)]
      public virtual long? MaxResults { get; set; }

      [RequestParameter("pageToken", RequestParameterType.Query)]
      public virtual string PageToken { get; set; }

      [RequestParameter("profileImageSize", RequestParameterType.Query)]
      public virtual long? ProfileImageSize { get; set; }

      public override string MethodName => "list";

      public override string HttpMethod => "GET";

      public override string RestPath => "liveChat/messages";

      protected override void InitParameters()
      {
        base.InitParameters();
        this.RequestParameters.Add("liveChatId", (IParameter) new Parameter()
        {
          Name = "liveChatId",
          IsRequired = true,
          ParameterType = "query",
          DefaultValue = (string) null,
          Pattern = (string) null
        });
        this.RequestParameters.Add("part", (IParameter) new Parameter()
        {
          Name = "part",
          IsRequired = true,
          ParameterType = "query",
          DefaultValue = (string) null,
          Pattern = (string) null
        });
        this.RequestParameters.Add("hl", (IParameter) new Parameter()
        {
          Name = "hl",
          IsRequired = false,
          ParameterType = "query",
          DefaultValue = (string) null,
          Pattern = (string) null
        });
        this.RequestParameters.Add("maxResults", (IParameter) new Parameter()
        {
          Name = "maxResults",
          IsRequired = false,
          ParameterType = "query",
          DefaultValue = "500",
          Pattern = (string) null
        });
        this.RequestParameters.Add("pageToken", (IParameter) new Parameter()
        {
          Name = "pageToken",
          IsRequired = false,
          ParameterType = "query",
          DefaultValue = (string) null,
          Pattern = (string) null
        });
        this.RequestParameters.Add("profileImageSize", (IParameter) new Parameter()
        {
          Name = "profileImageSize",
          IsRequired = false,
          ParameterType = "query",
          DefaultValue = (string) null,
          Pattern = (string) null
        });
      }
    }
  }
}
