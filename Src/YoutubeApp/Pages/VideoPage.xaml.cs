// VideoPage

/*
using Windows.UI.Xaml.Controls;

namespace YTApp
{
    /// <summary>
    /// Main page.
    /// </summary>
    public sealed partial class VideoPage : Page
    {
        /// <summary>
        /// Initializes a new instance of MainPage2 class.
        /// </summary>
        public VideoPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Gets the main viewmodel.
        /// </summary>
        public MainViewModel Vm => DataContext as MainViewModel;
    }
}
*/




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
using libVLCX;//using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

using VideoLibrary;
using Windows.Storage;
using YTApp.Classes;
using Windows.Foundation.Metadata; //using MyToolkit.Multimedia;


namespace YTApp.Pages
{

    public sealed partial class VideoPage : Page
    {
        private VLC.MediaElement _libVLC;

        //private MediaPlayer _mediaPlayer;

        //MainPage.Video ytvideo;

        //YouTubeQuality selectedQuality = YouTubeQuality.QualityHigh;//
        //YouTubeUri mainVideo;
        DataTransferManager dataTransferManager;

        /// <summary>
        /// Youtube "entities"
        /// </summary>
     

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

            if (ApiInformation.IsApiContractPresent("Windows.Phone.PhoneContract", 1, 0))
            {
                Windows.Phone.UI.Input.HardwareButtons.BackPressed += (s, a) =>
                {
                    Debug.WriteLine("[i] Hardware Back button Requested");

                    VideoPage_BackRequested(default, default);

                    if (Frame.CanGoBack)
                    {
                        Frame.GoBack();
                        a.Handled = true;
                    }
                    a.Handled = true;
                };
            }

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
            //VideoView.Visibility = Visibility.Visible;

            // Experimental -----------------
            Constants.MainPageRef.contentFrame.Navigated += ContentFrame_Navigated;
            SystemNavigationManager.GetForCurrentView().BackRequested += VideoPage_BackRequested;
            Constants.MainPageRef.Frame.Navigated += Frame_Navigated;
            // ------------------------------

            //videosTitle.Text = ".";// ytvideo.Title";

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

        private void Frame_Navigated(object sender, NavigationEventArgs e)
        {
            //If the user logs out, we need to stop the video
            //viewer.StopVideo();
            //RnD
            //_mediaPlayer.Stop();
            mediaElement.Stop();

        }

        private void ContentFrame_Navigated(object sender, NavigationEventArgs e)
        {
            //ChangePlayerSize(false);
        }


        private void VideoPage_BackRequested(object sender, BackRequestedEventArgs e)
        {
            try
            {
                //VideoView.MediaPlayer = null;
                //mediaElement.Source = null;
                if (mediaElement != null)
                  mediaElement.MediaSource = null;
            }
            catch (Exception ex1_1)
            {
                Debug.WriteLine("[ex1_1] " + ex1_1.Message);
            }

            try
            {
                //if (_mediaPlayer != null)
                //{
                    //RnD
                    //_mediaPlayer.Dispose();
                //}
            }
            catch { }

            try
            {
                if (_libVLC != null)
                {
                    //RnD
                    //_libVLC.Dispose();
                    _libVLC = null;
                }
            }
            catch (Exception ex4) 
            { 
                Debug.WriteLine("[ex4] " + ex4.Message); 
            }

            //ChangePlayerSize(false);
            Frame.Visibility = Visibility.Collapsed;
        }

        //AChangePlayerSize takes a bool allowing you to set it to fullscreen (true) or to a small view (false)
        public void ChangePlayerSize(bool MakeFullScreen)
        {
            if (!MakeFullScreen)
            {
                //viewer.transportControls.Visibility = Visibility.Collapsed;

                //Scrollviewer.ChangeView(0, 0, 1, true);
                //Scrollviewer.VerticalScrollMode = ScrollMode.Disabled;
                //Scrollviewer.VerticalScrollBarVisibility = ScrollBarVisibility.Disabled;

                Frame.HorizontalAlignment = HorizontalAlignment.Right;
                Frame.VerticalAlignment = VerticalAlignment.Bottom;
                Frame.Width = 640;
                Frame.Height = 360;

                //Saves the current Media Player height
                Windows.Storage.ApplicationDataContainer localSettings
                    = Windows.Storage.ApplicationData.Current.LocalSettings;
                //localSettings.Values["MediaViewerHeight"] = MediaRow.Height.Value;

                //MediaRow.Height = new GridLength(360);

                //Disable the taps on the viewer
                //TODO
                //viewer.IsHitTestVisible = false;
            }
            else
            {
                //TODO
                //viewer.transportControls.Visibility = Visibility.Visible;

                //Scrollviewer.VerticalScrollMode = ScrollMode.Auto;
                //Scrollviewer.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;

                Frame.HorizontalAlignment = HorizontalAlignment.Stretch;
                Frame.VerticalAlignment = VerticalAlignment.Stretch;
                Frame.Width = Double.NaN;
                Frame.Height = Double.NaN;

                //Set the media viewer to the previous height or to the default if a custom height is not found
                Windows.Storage.ApplicationDataContainer localSettings
                    = Windows.Storage.ApplicationData.Current.LocalSettings;

                /*
                if (localSettings.Values["MediaViewerHeight"] != null
                    && (double)localSettings.Values["MediaViewerHeight"] > 360)
                {
                    MediaRow.Height = new GridLength(Convert.ToDouble(localSettings.Values["MediaViewerHeight"]));
                }
                else
                {
                    MediaRow.Height = new GridLength(600);
                }
                */

                //Enable the taps on the viewer
                //TODO
                //viewer.IsHitTestVisible = true;
            }
        }


        private void Scrollviewer_ViewChanged(object sender, ScrollViewerViewChangedEventArgs e)
        {
            //if (Scrollviewer.VerticalOffset > Scrollviewer.ScrollableHeight - 700)
            //{
                //TODO
                //if (CommentsOptionComboBox.SelectedIndex == 0)
                //{
                //    AddComments(CommentThreadsResource.ListRequest.OrderEnum.Relevance);
                //}
                //else
                //{
                //    AddComments(CommentThreadsResource.ListRequest.OrderEnum.Time);
                //}
            //}
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
                       //Experimental
                       mediaElement.MediaSource = VLC.MediaSource.CreateFromUri(video.Uri);

                    };


                    Unloaded += (s, e) =>
                    {
                        try
                        {
                            //mediaElement.Source = null;
                            mediaElement.MediaSource = null;
                            //mediaElement = null;
                            //VLC.MediaElement = null;
                            //VideoView.MediaPlayer = null;
                            
                        }
                        catch (Exception ex1)
                        {
                            Debug.WriteLine("[ex1] " + ex1.Message);
                        }

                        try
                        {
                            //if (_mediaPlayer != null)
                            //{
                               // _mediaPlayer.Dispose();
                            //}
                        }
                        catch (Exception ex2)
                        {
                            Debug.WriteLine("[ex2] " + ex2.Message);
                        }

                        try
                        {
                            if (_libVLC != null)
                            {
                                _libVLC.Stop();//.Dispose();
                                //_libVLC = null;
                            }
                        }
                        catch (Exception ex3)
                        {
                            Debug.WriteLine("[ex3] " + ex3.Message);
                        }
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

