// VideoPage

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using LibVLCSharp.Shared;//using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

using VideoLibrary;
using Windows.Storage;
using YTApp.Classes; //using MyToolkit.Multimedia;


namespace YTApp.Pages
{

    public sealed partial class VideoPage : Page
    {
        private LibVLC _libVLC;

        private MediaPlayer _mediaPlayer;

        //MainPage.Video ytvideo;

        //YouTubeQuality selectedQuality = YouTubeQuality.QualityHigh;//
        //YouTubeUri mainVideo;
        DataTransferManager dataTransferManager;

        /// <summary>
        /// Youtube "entities"
        /// </summary>
        /*
        public string link { get; set; }
        public string Title { get; set; }
        public string FileName { get; set; }
        public string Extension { get; set; }
        public string Length { get; set; }
        public string AudioBitrate { get; set; }
        public string AudioFormats { get; set; }
        public string VideoFormat { get; set; }
        public string VideoRes { get; set; }
        public string FPS { get; set; }
        public string VideoID { get; set; }
        public bool IsHDQuality { get; set; }
        public YouTubeVideo video { get; set; }
        public YouTubeVideo maxVideo { get; set; }
        public YouTubeVideo maxBitrate { get; set; }
        public string ThrownEncodingError { get; set; }
        public IEnumerable<YouTubeVideo> videoInfos { get; set; }
        */

        public string ChosenQuality { get; set; }
        public int ChosenQualityInt { get; set; }


        public VideoPage()
        {
            this.InitializeComponent();

            Windows.UI.Core.SystemNavigationManager
                .GetForCurrentView().AppViewBackButtonVisibility
                = AppViewBackButtonVisibility.Visible;

            Windows.UI.Core.SystemNavigationManager.GetForCurrentView().BackRequested += (s, a) =>
            {
                if (Frame.CanGoBack)
                {
                    Frame.GoBack();
                    a.Handled = true;
                }
            };

            dataTransferManager = DataTransferManager.GetForCurrentView();

            ChosenQuality = "480p";
            ChosenQualityInt = 480;           
           
        }

        private void OnDataRequested(DataTransferManager sender, DataRequestedEventArgs args)
        {
            var dataPackage = args.Request.Data;
            dataPackage.Properties.Title = "WinBeta Videos";
            dataPackage.Properties.Description = "Sharing Video Link";

            //RnD
            //dataPackage.SetWebLink(new Uri("https://youtube.com/watch?v=" + YTApp.Pages.VideoPage));
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            //ytvideo = (MainPage.Video)e.Parameter;

            videosTitle.Text = "ytvideo.Title";

            try
            {
                await LoadPage();
            }
            catch (Exception ex) // AggregateException
            {
                MessageDialog m = new MessageDialog("Could play video: " + ex.Message,
                    "WinBeta Videos Error");
                await m.ShowAsync();
            }
        }


        private async Task LoadPage()
        {
            //progressRing.IsActive = true;

            // use YouTubeVideo client
            using (Client<YouTubeVideo> service = Client.For(YouTube.Default))
            {
                // set video id
                string id = Constants.activeVideoID;//ytvideo.Id;

                // exploring yt video

                YouTubeVideo video = service.GetVideo("https://youtube.com/watch?v=" + id);               

                try
                {
                    

                    Loaded += (s, e) =>
                    {
                        _libVLC = new LibVLC(VideoView.SwapChainOptions);
                        _mediaPlayer = new MediaPlayer(_libVLC);
                        VideoView.MediaPlayer = _mediaPlayer;

                        _mediaPlayer.Play(new Media(
                            _libVLC,
                           video.Uri,//"https://download.blender.org/peach/bigbuckbunny_movies/BigBuckBunny_320x180.mp4",
                            FromType.FromLocation));
                    };


                    Unloaded += (s, e) =>
                    {
                        VideoView.MediaPlayer = null;
                        _mediaPlayer.Dispose();
                        _libVLC.Dispose();
                    };

                    //progressRing.IsActive = false;
                    
                }
                catch (Exception ex)
                {
                    if (ex.HResult == -2146233088)
                    {
                        Debug.WriteLine("[ex] Quality Not Supported, try something else");
                    }

                    MessageDialog m = new MessageDialog(
                    "Could play video: " + ex.Message, "WinBeta Videos Error");

                    await m.ShowAsync();

                    // progressRing.IsActive = false;
                }
            }//using 
                                   
            //progressRing.IsActive = true;          
            
        }//LoadPage


        // MenuFlyoutItem_Click
        private async void MenuFlyoutItem_Click(object sender, RoutedEventArgs e)
        {
            var s = sender as MenuFlyoutItem;

            switch (s.Text)
            {
                case "144p":
                    //selectedQuality = YouTubeQuality.Quality144P;
                    ChosenQuality = "144p";
                    ChosenQualityInt = 144;
                    break;
                case "240p":
                    //selectedQuality = YouTubeQuality.Quality240P;
                    ChosenQuality = "240p";
                    ChosenQualityInt = 240;
                    break;
                case "270p":
                    //selectedQuality = YouTubeQuality.Quality270P;
                    ChosenQuality = "270p";
                    ChosenQualityInt = 270;
                    break;
                case "360p":
                    //selectedQuality = YouTubeQuality.Quality360P;
                    ChosenQuality = "360p";
                    ChosenQualityInt = 360;
                    break;
                case "480p":
                    //selectedQuality = YouTubeQuality.Quality480P;
                    ChosenQuality = "480p";
                    ChosenQualityInt = 480;
                    break;
                case "520p":
                    //selectedQuality = YouTubeQuality.Quality520P;
                    ChosenQuality = "520p";
                    ChosenQualityInt = 520;
                    break;
                case "720p":
                    //selectedQuality = YouTubeQuality.Quality720P;
                    ChosenQuality = "720p";
                    ChosenQualityInt = 720;
                    break;
                case "1080p":
                    //selectedQuality = YouTubeQuality.Quality1080P;
                    ChosenQuality = "1080p";
                    ChosenQualityInt = 1080;
                    break;
                case "4k":
                    //selectedQuality = YouTubeQuality.Quality2160P;
                    ChosenQuality = "2160p";
                    ChosenQualityInt = 2160;
                    break;
                default:
                    MessageDialog m = new MessageDialog("Ubnknown Quality", "WinBeta Videos Error");
                    await m.ShowAsync();
                    break;
            }

            try
            {
                await LoadPage();
            }
            catch (AggregateException ex)
            {
                MessageDialog m = new MessageDialog("Could play video: " + ex.Message, "WinBeta Videos Error");
                await m.ShowAsync();
            }

        }//MenuFlyoutItem_Click


        // shareButton_Tapped
        private void shareButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            dataTransferManager.DataRequested -= OnDataRequested;
            dataTransferManager.DataRequested += OnDataRequested;

            DataTransferManager.ShowShareUI();
        }//shareButton_Tapped

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
           // 
        }
    }
}
