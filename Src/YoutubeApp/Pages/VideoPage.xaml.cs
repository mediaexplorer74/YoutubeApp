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
using libVLCX;//using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

using VideoLibrary;
using Windows.Storage;
using YTApp.Classes;
using Windows.Foundation.Metadata; 


namespace YTApp.Pages
{

    public sealed partial class VideoPage : Page
    {
        private VLC.MediaElement _libVLC;

       
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


        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
           
            Constants.MainPageRef.contentFrame.Navigated += ContentFrame_Navigated;
            SystemNavigationManager.GetForCurrentView().BackRequested += VideoPage_BackRequested;
            Constants.MainPageRef.Frame.Navigated += Frame_Navigated;
  

            try
            {
                await LoadPage();
            }
            catch (Exception ex) // AggregateException
            {
                MessageDialog m = new MessageDialog("Could play video: " + ex.Message,
                    "Youtube Video Error");
                await m.ShowAsync();
            }
        }

        private void Frame_Navigated(object sender, NavigationEventArgs e)
        {
            //If the user logs out, we need to stop the video
  
            mediaElement.Stop();

        }

        private void ContentFrame_Navigated(object sender, NavigationEventArgs e)
        {
            //
        }


        private void VideoPage_BackRequested(object sender, BackRequestedEventArgs e)
        {
            try
            {
               if (mediaElement != null)
                  mediaElement.MediaSource = null;
            }
            catch (Exception ex1_1)
            {
                Debug.WriteLine("[ex1_1] " + ex1_1.Message);
            }

     
            try
            {
                if (_libVLC != null)
                {
                   _libVLC = null;
                }
            }
            catch (Exception ex4) 
            { 
                Debug.WriteLine("[ex4] " + ex4.Message); 
            }

            Frame.Visibility = Visibility.Collapsed;
        }

        //AChangePlayerSize takes a bool allowing you to set it to fullscreen (true) or to a small view (false)
        public void ChangePlayerSize(bool MakeFullScreen)
        {
            if (!MakeFullScreen)
            {
             

                Frame.HorizontalAlignment = HorizontalAlignment.Right;
                Frame.VerticalAlignment = VerticalAlignment.Bottom;
                Frame.Width = 640;
                Frame.Height = 360;

                //Saves the current Media Player height
                Windows.Storage.ApplicationDataContainer localSettings
                    = Windows.Storage.ApplicationData.Current.LocalSettings;
                
            }
            else
            {

                Frame.HorizontalAlignment = HorizontalAlignment.Stretch;
                Frame.VerticalAlignment = VerticalAlignment.Stretch;
                Frame.Width = Double.NaN;
                Frame.Height = Double.NaN;

                //Set the media viewer to the previous height or to the default if a custom height is not found
                Windows.Storage.ApplicationDataContainer localSettings
                    = Windows.Storage.ApplicationData.Current.LocalSettings;

            }
        }


        private void Scrollviewer_ViewChanged(object sender, ScrollViewerViewChangedEventArgs e)
        {
            //
        }

        private async Task LoadPage()
        {

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
                             mediaElement.MediaSource = null;          
                            
                        }
                        catch (Exception ex1)
                        {
                            Debug.WriteLine("[ex1] " + ex1.Message);
                        }

                        

                        try
                        {
                            if (_libVLC != null)
                            {
                                _libVLC.Stop();
                            }
                        }
                        catch (Exception ex3)
                        {
                            Debug.WriteLine("[ex3] " + ex3.Message);
                        }
                    };

                    
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
                }
            }//using                                  
       
            
        }//LoadPage



        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
           // 
        }
    }
}

