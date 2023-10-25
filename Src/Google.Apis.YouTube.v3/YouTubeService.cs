// Decompiled with JetBrains decompiler
// Type: Google.Apis.YouTube.v3.YouTubeService
// Assembly: Google.Apis.YouTube.v3, Version=1.29.2.1006, Culture=neutral, PublicKeyToken=4b01fa6e34db77ab
// MVID: 9E53E5C1-D9AB-4142-94A7-8A2435650008
// Assembly location: C:\Users\Admin\Desktop\re\YoutubeApp\Google.Apis.YouTube.v3.dll

using Google.Apis.Discovery;
using Google.Apis.Services;
using System.Collections.Generic;

namespace Google.Apis.YouTube.v3
{
  public class YouTubeService : BaseClientService
  {
    public const string Version = "v3";
    public static DiscoveryVersion DiscoveryVersionUsed;
    private readonly ActivitiesResource activities;
    private readonly CaptionsResource captions;
    private readonly ChannelBannersResource channelBanners;
    private readonly ChannelSectionsResource channelSections;
    private readonly ChannelsResource channels;
    private readonly CommentThreadsResource commentThreads;
    private readonly CommentsResource comments;
    private readonly FanFundingEventsResource fanFundingEvents;
    private readonly GuideCategoriesResource guideCategories;
    private readonly I18nLanguagesResource i18nLanguages;
    private readonly I18nRegionsResource i18nRegions;
    private readonly LiveBroadcastsResource liveBroadcasts;
    private readonly LiveChatBansResource liveChatBans;
    private readonly LiveChatMessagesResource liveChatMessages;
    private readonly LiveChatModeratorsResource liveChatModerators;
    private readonly LiveStreamsResource liveStreams;
    private readonly PlaylistItemsResource playlistItems;
    private readonly PlaylistsResource playlists;
    private readonly SearchResource search;
    private readonly SponsorsResource sponsors;
    private readonly SubscriptionsResource subscriptions;
    private readonly SuperChatEventsResource superChatEvents;
    private readonly ThumbnailsResource thumbnails;
    private readonly VideoAbuseReportReasonsResource videoAbuseReportReasons;
    private readonly VideoCategoriesResource videoCategories;
    private readonly VideosResource videos;
    private readonly WatermarksResource watermarks;

    public YouTubeService()
      : this(new BaseClientService.Initializer())
    {
    }

    public YouTubeService(BaseClientService.Initializer initializer)
      : base(initializer)
    {
      this.activities = new ActivitiesResource((IClientService) this);
      this.captions = new CaptionsResource((IClientService) this);
      this.channelBanners = new ChannelBannersResource((IClientService) this);
      this.channelSections = new ChannelSectionsResource((IClientService) this);
      this.channels = new ChannelsResource((IClientService) this);
      this.commentThreads = new CommentThreadsResource((IClientService) this);
      this.comments = new CommentsResource((IClientService) this);
      this.fanFundingEvents = new FanFundingEventsResource((IClientService) this);
      this.guideCategories = new GuideCategoriesResource((IClientService) this);
      this.i18nLanguages = new I18nLanguagesResource((IClientService) this);
      this.i18nRegions = new I18nRegionsResource((IClientService) this);
      this.liveBroadcasts = new LiveBroadcastsResource((IClientService) this);
      this.liveChatBans = new LiveChatBansResource((IClientService) this);
      this.liveChatMessages = new LiveChatMessagesResource((IClientService) this);
      this.liveChatModerators = new LiveChatModeratorsResource((IClientService) this);
      this.liveStreams = new LiveStreamsResource((IClientService) this);
      this.playlistItems = new PlaylistItemsResource((IClientService) this);
      this.playlists = new PlaylistsResource((IClientService) this);
      this.search = new SearchResource((IClientService) this);
      this.sponsors = new SponsorsResource((IClientService) this);
      this.subscriptions = new SubscriptionsResource((IClientService) this);
      this.superChatEvents = new SuperChatEventsResource((IClientService) this);
      this.thumbnails = new ThumbnailsResource((IClientService) this);
      this.videoAbuseReportReasons = new VideoAbuseReportReasonsResource((IClientService) this);
      this.videoCategories = new VideoCategoriesResource((IClientService) this);
      this.videos = new VideosResource((IClientService) this);
      this.watermarks = new WatermarksResource((IClientService) this);
    }

    public override IList<string> Features => (IList<string>) new string[0];

    public override string Name => "youtube";

    public override string BaseUri => "https://www.googleapis.com/youtube/v3/";

    public override string BasePath => "youtube/v3/";

    public override string BatchUri => "https://www.googleapis.com/batch/youtube/v3";

    public override string BatchPath => "batch/youtube/v3";

    public virtual ActivitiesResource Activities => this.activities;

    public virtual CaptionsResource Captions => this.captions;

    public virtual ChannelBannersResource ChannelBanners => this.channelBanners;

    public virtual ChannelSectionsResource ChannelSections => this.channelSections;

    public virtual ChannelsResource Channels => this.channels;

    public virtual CommentThreadsResource CommentThreads => this.commentThreads;

    public virtual CommentsResource Comments => this.comments;

    public virtual FanFundingEventsResource FanFundingEvents => this.fanFundingEvents;

    public virtual GuideCategoriesResource GuideCategories => this.guideCategories;

    public virtual I18nLanguagesResource I18nLanguages => this.i18nLanguages;

    public virtual I18nRegionsResource I18nRegions => this.i18nRegions;

    public virtual LiveBroadcastsResource LiveBroadcasts => this.liveBroadcasts;

    public virtual LiveChatBansResource LiveChatBans => this.liveChatBans;

    public virtual LiveChatMessagesResource LiveChatMessages => this.liveChatMessages;

    public virtual LiveChatModeratorsResource LiveChatModerators => this.liveChatModerators;

    public virtual LiveStreamsResource LiveStreams => this.liveStreams;

    public virtual PlaylistItemsResource PlaylistItems => this.playlistItems;

    public virtual PlaylistsResource Playlists => this.playlists;

    public virtual SearchResource Search => this.search;

    public virtual SponsorsResource Sponsors => this.sponsors;

    public virtual SubscriptionsResource Subscriptions => this.subscriptions;

    public virtual SuperChatEventsResource SuperChatEvents => this.superChatEvents;

    public virtual ThumbnailsResource Thumbnails => this.thumbnails;

    public virtual VideoAbuseReportReasonsResource VideoAbuseReportReasons => this.videoAbuseReportReasons;

    public virtual VideoCategoriesResource VideoCategories => this.videoCategories;

    public virtual VideosResource Videos => this.videos;

    public virtual WatermarksResource Watermarks => this.watermarks;

    public class Scope
    {
      public static string Youtube = "https://www.googleapis.com/auth/youtube";
      public static string YoutubeForceSsl = "https://www.googleapis.com/auth/youtube.force-ssl";
      public static string YoutubeReadonly = "https://www.googleapis.com/auth/youtube.readonly";
      public static string YoutubeUpload = "https://www.googleapis.com/auth/youtube.upload";
      public static string Youtubepartner = "https://www.googleapis.com/auth/youtubepartner";
      public static string YoutubepartnerChannelAudit = "https://www.googleapis.com/auth/youtubepartner-channel-audit";
    }
  }
}
