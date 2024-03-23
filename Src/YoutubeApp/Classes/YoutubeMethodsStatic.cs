using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Google.Apis.YouTube.v3;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml;
//using YoutubeExplode.Models.MediaStreams;

namespace YTApp.Classes
{
    static class YoutubeMethodsStatic
    {
        private static IDataStore DDataStore;

        // GetServiceAsync
        static async public Task<YouTubeService> GetServiceAsync()
        {
            UserCredential credential = default;

            try
            {
                credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                    new ClientSecrets
                    {
                        ClientId = Constants.ClientID,
                        ClientSecret = Constants.ClientSecret
                    },
                new[]
                {
                    Google.Apis.Oauth2.v2.Oauth2Service.Scope.UserinfoProfile,
                    YouTubeService.Scope.YoutubeForceSsl
                    //YouTubeService.Scope.Youtube
                },
                "user", 
                CancellationToken.None,
                DDataStore);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("[ex]  GetServiceAsync / AuthorizeAsync error: " + ex.Message);
            }
        
            // Create the service.
            return new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = Constants.ApiKey, //RnD
                HttpClientInitializer = credential,
                ApplicationName = Constants.ApplicationName,//"Unofficial Youtube Client",
            });
        }//GetServiceAsync


        // GetServiceNoAuth
        static public YouTubeService GetServiceNoAuth()
        {
            return new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = Constants.ApiKey,
                ApplicationName = Constants.ApplicationName,//"Unofficial Youtube Client"
            });
        }//GetServiceNoAuth

        // IsUserAuthenticated
        static public async Task<bool> IsUserAuthenticated()
        {
            GoogleAuthorizationCodeFlow.Initializer initializer 
                = new GoogleAuthorizationCodeFlow.Initializer();

            ClientSecrets secrets = new ClientSecrets
            {
                ClientId = Constants.ClientID,
                ClientSecret = Constants.ClientSecret
                
            };
            initializer.ClientSecrets = secrets;



            //RnD
            initializer.DataStore =
                //new MemoryDataStore();
                //new WindowsStorageDataStore(KnownFolders.VideosLibrary);
                new PasswordVaultDataStore();

            DDataStore = initializer.DataStore;

            var test = new AuthorizationCodeFlow(initializer);
            var token = await test.LoadTokenAsync("user", CancellationToken.None);
            if (token == null)
            {
                return false;
            }
            else
            {
                Constants.Token = token;
                return true;
            }
        }//IsUserAuthenticated


        // ViewCountShortner
        static public string ViewCountShortner(long viewCount, int decimals = 1)
        {
            if (viewCount > 1000000000)
                return Convert.ToString(Math.Round(Convert.ToDouble(viewCount) 
                    / 1000000000, decimals)) + "B";
            else if (viewCount > 1000000)
                return Convert.ToString(Math.Round(Convert.ToDouble(viewCount) 
                    / 1000000, decimals)) + "M";
            else if (viewCount > 1000)
                return Convert.ToString(Math.Round(Convert.ToDouble(viewCount) 
                    / 1000, decimals)) + "K";
            else
                return Convert.ToString(viewCount);
        }//ViewCountShortner

        // ViewCountShortner
        static public string ViewCountShortner(ulong? viewCount, int decimals = 1)
        {
            if (viewCount > 1000000000)
                return Convert.ToString(Math.Round(Convert.ToDouble(viewCount)
                    / 1000000000, decimals)) + "B";
            else if (viewCount > 1000000)
                return Convert.ToString(Math.Round(Convert.ToDouble(viewCount)
                    / 1000000, decimals)) + "M";
            else if (viewCount > 1000)
                return Convert.ToString(Math.Round(Convert.ToDouble(viewCount) 
                    / 1000, decimals)) + "K";
            else
                return Convert.ToString(viewCount);
        }//ViewCountShortner


        // GetVideoQuality
        static public string GetVideoQuality(VideoQuality quality, bool GetMuxed)
        {
            if (GetMuxed)
            {
                foreach (var video in Constants.videoInfo.Muxed)
                {
                    if (video.VideoQuality1 == quality)
                        return video.Url;
                }
            }
            else
            {
                foreach (var video in Constants.videoInfo.Video)
                {
                    if (video.VideoQuality1 == quality)
                        return video.Url;
                }
            }

            return null;
        }//GetVideoQuality


        // GetVideoQualityList
        static public List<VideoQuality> GetVideoQualityList()
        {
            var qualitiesString = Constants.videoInfo.GetAllVideoQualities().ToList();
            qualitiesString.Sort();
            return qualitiesString;
        }//GetVideoQualityList

    }//class end

}//namespace end
