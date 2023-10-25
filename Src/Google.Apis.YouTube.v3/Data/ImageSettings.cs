// Decompiled with JetBrains decompiler
// Type: Google.Apis.YouTube.v3.Data.ImageSettings
// Assembly: Google.Apis.YouTube.v3, Version=1.29.2.1006, Culture=neutral, PublicKeyToken=4b01fa6e34db77ab
// MVID: 9E53E5C1-D9AB-4142-94A7-8A2435650008
// Assembly location: C:\Users\Admin\Desktop\re\YoutubeApp\Google.Apis.YouTube.v3.dll

using Google.Apis.Requests;
using Newtonsoft.Json;

namespace Google.Apis.YouTube.v3.Data
{
  public class ImageSettings : IDirectResponseSchema
  {
    [JsonProperty("backgroundImageUrl")]
    public virtual LocalizedProperty BackgroundImageUrl { get; set; }

    [JsonProperty("bannerExternalUrl")]
    public virtual string BannerExternalUrl { get; set; }

    [JsonProperty("bannerImageUrl")]
    public virtual string BannerImageUrl { get; set; }

    [JsonProperty("bannerMobileExtraHdImageUrl")]
    public virtual string BannerMobileExtraHdImageUrl { get; set; }

    [JsonProperty("bannerMobileHdImageUrl")]
    public virtual string BannerMobileHdImageUrl { get; set; }

    [JsonProperty("bannerMobileImageUrl")]
    public virtual string BannerMobileImageUrl { get; set; }

    [JsonProperty("bannerMobileLowImageUrl")]
    public virtual string BannerMobileLowImageUrl { get; set; }

    [JsonProperty("bannerMobileMediumHdImageUrl")]
    public virtual string BannerMobileMediumHdImageUrl { get; set; }

    [JsonProperty("bannerTabletExtraHdImageUrl")]
    public virtual string BannerTabletExtraHdImageUrl { get; set; }

    [JsonProperty("bannerTabletHdImageUrl")]
    public virtual string BannerTabletHdImageUrl { get; set; }

    [JsonProperty("bannerTabletImageUrl")]
    public virtual string BannerTabletImageUrl { get; set; }

    [JsonProperty("bannerTabletLowImageUrl")]
    public virtual string BannerTabletLowImageUrl { get; set; }

    [JsonProperty("bannerTvHighImageUrl")]
    public virtual string BannerTvHighImageUrl { get; set; }

    [JsonProperty("bannerTvImageUrl")]
    public virtual string BannerTvImageUrl { get; set; }

    [JsonProperty("bannerTvLowImageUrl")]
    public virtual string BannerTvLowImageUrl { get; set; }

    [JsonProperty("bannerTvMediumImageUrl")]
    public virtual string BannerTvMediumImageUrl { get; set; }

    [JsonProperty("largeBrandedBannerImageImapScript")]
    public virtual LocalizedProperty LargeBrandedBannerImageImapScript { get; set; }

    [JsonProperty("largeBrandedBannerImageUrl")]
    public virtual LocalizedProperty LargeBrandedBannerImageUrl { get; set; }

    [JsonProperty("smallBrandedBannerImageImapScript")]
    public virtual LocalizedProperty SmallBrandedBannerImageImapScript { get; set; }

    [JsonProperty("smallBrandedBannerImageUrl")]
    public virtual LocalizedProperty SmallBrandedBannerImageUrl { get; set; }

    [JsonProperty("trackingImageUrl")]
    public virtual string TrackingImageUrl { get; set; }

    [JsonProperty("watchIconImageUrl")]
    public virtual string WatchIconImageUrl { get; set; }

    public virtual string ETag { get; set; }
  }
}
