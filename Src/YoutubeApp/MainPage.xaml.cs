using Google.Apis.Auth.OAuth2;
using Google.Apis.Oauth2.v2;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;
//using Google.Apis.Drive.v3;
using MetroLog;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using Windows.ApplicationModel.DataTransfer;
using Windows.Networking.BackgroundTransfer;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media.Imaging;
using YTApp.Classes;
using YTApp.Pages;
using Windows.UI.ViewManagement;
using YTApp.Classes.DataTypes;
using Windows.Storage;
using System.Threading.Tasks;
using System.Diagnostics;
// using YoutubeExplode;


namespace YTApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private ILogger Log = LogManagerFactory.DefaultLogManager.GetLogger<MainPage>();

        public ObservableCollection<SubscriptionDataType> subscriptionsList = 
            new ObservableCollection<SubscriptionDataType>();

        // MainPage
        public MainPage()
        {
            InitializeComponent();

            //Set titlebar colour
            var titleBar = ApplicationView.GetForCurrentView().TitleBar;
            titleBar.BackgroundColor = 
                ((Windows.UI.Xaml.Media.SolidColorBrush)Application.Current.Resources["AppBackgroundLighter"]).Color;
            titleBar.ButtonBackgroundColor = 
                ((Windows.UI.Xaml.Media.SolidColorBrush)Application.Current.Resources["AppBackgroundLighter"]).Color;
            titleBar.InactiveBackgroundColor = 
                ((Windows.UI.Xaml.Media.SolidColorBrush)Application.Current.Resources["AppBackgroundLightest"]).Color;
            titleBar.ButtonInactiveBackgroundColor =
                ((Windows.UI.Xaml.Media.SolidColorBrush)Application.Current.Resources["AppBackgroundLightest"]).Color;

            //Set a reference to this page for all other pages to use it's functions
            Constants.MainPageRef = this;

            //Enable going backwards
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = 
                AppViewBackButtonVisibility.Visible;

            SystemNavigationManager.GetForCurrentView().BackRequested += MainPage_BackRequested;

            Debug.WriteLine("[i] Main page startup has started");
            Log.Info("Main page startup has started");

            Startup();

        }//MainPage

        #region Main Events

        private void MainPage_BackRequested(object sender, BackRequestedEventArgs e)
        {
            if (contentFrame.CanGoBack && e.Handled == false)
            {
                e.Handled = true;
                contentFrame.GoBack();
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            SearchBox.Focus(FocusState.Keyboard);
        }

        #endregion Main Events


        #region Startup

        // Startup
        private void Startup()
        {
            Debug.WriteLine("[i] Loading subscriptions");
            Log.Info("Loading subscriptions");
            
            LoadSubscriptions();

            contentFrame.Navigate(typeof(HomePage));

            UpdateLoginDetails();

            //Plays Youtube link in clipboard (RnD)
            //PlayClipboardYLink();

            UpdateSyncData();
        }

        private async void PlayClipboardYLink()
        {
            try
            {
                var clipboardText = await Clipboard.GetContent().GetTextAsync();
                var videoID = YoutubeClient.ParseVideoId(clipboardText);
                StartVideo(videoID);
            }
            catch 
            {
                Debug.WriteLine("Exception thrown while loading video from clipboard");
                Log.Error("Exception thrown while loading video from clipboard"); 
            }
        }

        private async void UpdateSyncData()
        {
            //Update sync data
            try
            {
                StorageFolder roamingFolder = ApplicationData.Current.RoamingFolder;
                var wow = await roamingFolder.GetItemsAsync();

                var text = await FileIO.ReadTextAsync(
                    await roamingFolder.GetFileAsync("data.json"));

                if (text == "")
                    return;

                Constants.syncedData = 
                    JsonConvert.DeserializeObject<SyncedApplicationDataType>(text);
            }
            catch { }
        }

        #endregion Startup



        #region Menu

        // region Subscriptions ------------------------------------------------

        // LoadSubscriptions
        public async void LoadSubscriptions()
        {
            //Reset the subscriptions
            subscriptionsList.Clear();

            //Get the service
            YouTubeService service = await YoutubeMethodsStatic.GetServiceAsync();

            string nextPageToken;

            //Get the subscriptions
            SubscriptionListResponse tempSubscriptions = GetSubscriptions(null, service);
            if (tempSubscriptions == null)
            {
                Log.Error(
                    "Get Subscriptions returned a null object. " +
                    "The method \"LoadSubscriptions\" was cancelled");

                return;
            }

            foreach (Subscription sub in tempSubscriptions.Items)
            {
                SubscriptionDataType subscription = new SubscriptionDataType();
                try
                {
                    subscription = new SubscriptionDataType
                    {
                        Id = sub.Snippet.ResourceId.ChannelId,
                        Thumbnail = new BitmapImage(new Uri(sub.Snippet.Thumbnails.Medium.Url)),
                        Title = sub.Snippet.Title,
                        NewVideosCount = Convert.ToString(sub.ContentDetails.NewItemCount),
                        SubscriptionID = sub.Id
                    };

                    subscriptionsList.Add(subscription);
                }
                catch (Exception ex)
                {
                    Log.Error(string.Format("Subscription failed to load. Object:", 
                        JsonConvert.SerializeObject(subscription)));
                    Log.Error(ex.Message);

                    Debug.WriteLine("[ex] Subscription failed to load. Message: " + ex.Message);
                }
            }

            if (tempSubscriptions.NextPageToken != null)
            {
                nextPageToken = tempSubscriptions.NextPageToken;

                //while
                if(nextPageToken != null)
                {
                    SubscriptionListResponse tempSubs = GetSubscriptions(nextPageToken, service);

                    //RnD
                    if (tempSubs != null)
                    {
                        foreach (Subscription sub in tempSubs.Items)
                        {
                            SubscriptionDataType subscription = new SubscriptionDataType();
                            try
                            {
                                subscription = new SubscriptionDataType
                                {
                                    Id = sub.Snippet.ResourceId.ChannelId,
                                    Thumbnail = new BitmapImage(
                                        new Uri(sub.Snippet.Thumbnails.Medium.Url)),
                                    Title = sub.Snippet.Title,
                                    NewVideosCount = Convert.ToString(sub.ContentDetails.NewItemCount),
                                    SubscriptionID = sub.Id
                                };
                                subscriptionsList.Add(subscription);
                            }
                            catch (Exception ex)
                            {
                                Debug.WriteLine("[ex] " + string.Format("Subscription failed to load. Object:",
                                    JsonConvert.SerializeObject(subscription)));
                                Debug.WriteLine("[ex] " + ex.Message);
                                Log.Error(string.Format("Subscription failed to load. Object:",
                                    JsonConvert.SerializeObject(subscription)));
                                Log.Error(ex.Message);
                            }
                        }
                        nextPageToken = tempSubs.NextPageToken;
                    }//if...
                    else
                    {
                        //RnD
                        nextPageToken = null;
                    }

                }//if   //while...

            }//if...

        }//LoadSubscriptions


        // GetSubscriptions
        private SubscriptionListResponse GetSubscriptions(string NextPageToken, 
            YouTubeService service)
        {
            SubscriptionsResource.ListRequest subscriptions 
                = service.Subscriptions.List("snippet, contentDetails");
            try
            {
                subscriptions.PageToken = NextPageToken;
                subscriptions.Mine = true;
                subscriptions.MaxResults = 1;//50;
                subscriptions.Order = SubscriptionsResource.ListRequest.OrderEnum.Alphabetical;

                return subscriptions.Execute();
            }
            catch (Exception ex)
            {
                Log.Fatal(String.Format("GetSubscriptions failed to load"));
                Log.Fatal(ex.Message);
                return null;
            }
        }

        private void SubscriptionsList_ItemClick(object sender, ItemClickEventArgs e)
        {
            var temp = (SubscriptionDataType)e.ClickedItem;
            Constants.activeChannelID = temp.Id;
            contentFrame.Navigate(typeof(ChannelPage));

            // RnD
            SideBarSplitView.IsPaneOpen = false;
        }

        // endregion Subscriptions -------------------------------------------


        public async void UpdateLoginDetails()
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
                Oauth2Service.Scope.UserinfoProfile
                },
                "user",
                CancellationToken.None);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("[ex] UpdateLoginDetails - GoogleWebAuthorizationBroker.AuthorizeAsync ex.: " 
                    + ex.Message);

                txtLoginName.Text = "";
                imgProfileIcon.Fill =
                    new Windows.UI.Xaml.Media.SolidColorBrush(
                        Windows.UI.Color.FromArgb(0, 0, 0, 0));
            }


            // Create the service.
            var service = new Oauth2Service(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = Constants.ApplicationName,//"Youtube Viewer",
            });

            var GetLoginInfo = service.Userinfo.Get();

            try
            {
                var LoginInfo = await GetLoginInfo.ExecuteAsync();

                txtLoginName.Text = " ";//LoginInfo.Name;

                var profileImg = new Windows.UI.Xaml.Media.ImageBrush
                {
                    ImageSource = new BitmapImage(new Uri(LoginInfo.Picture))
                };
                imgProfileIcon.Fill = profileImg;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

                txtLoginName.Text = "";
                imgProfileIcon.Fill = 
                    new Windows.UI.Xaml.Media.SolidColorBrush(
                        Windows.UI.Color.FromArgb(0, 0, 0, 0));
            }
        }

        private void PageMenuControls_ItemClick(object sender, ItemClickEventArgs e)
        {
            var item = (SplitViewItemDataType)e.ClickedItem;
            if (item.Text == "Home" && Constants.Token != null)
                contentFrame.Navigate(typeof(HomePage));

            // RnD
            SideBarSplitView.IsPaneOpen = false;
        }

        private void PlaylistOptions_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Constants.Token != null)
            {
                var item = (SplitViewItemDataType)e.ClickedItem;
                if (item.Text == "Trending")
                    contentFrame.Navigate(typeof(TrendingPage));
                else if (item.Text == "History")
                    contentFrame.Navigate(typeof(HistoryPage));
            }

            // RnD
            SideBarSplitView.IsPaneOpen = false;
        }

        private void HamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            SideBarSplitView.IsPaneOpen = !SideBarSplitView.IsPaneOpen;
        }

        #endregion Menu


        #region Search

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            contentFrame.Navigate(typeof(SearchPage));

            // RnD
            SideBarSplitView.IsPaneOpen = false;
        }

        private void SearchBox_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter && Constants.Token != null)
            {
                if (Uri.IsWellFormedUriString(SearchBox.Text, UriKind.Absolute))
                {
                    PlayClipboardYLink();
                }
                contentFrame.Navigate(typeof(SearchPage));
            }
        }

        #endregion Search


        #region Play Video

        public void StartVideo(string Id)
        {
            videoFrame.Visibility = Visibility.Visible;
            Constants.activeVideoID = Id;
            videoFrame.Navigate(typeof(VideoPage));
        }

        #endregion Play Video


        #region User Info Region

        private async void BtnSignOut_Tapped(object sender, RoutedEventArgs e)
        {
            UserCredential credential = default;

            try
            {
                // phase 1
                credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                    new ClientSecrets
                    {
                        ClientId = Constants.ClientID,
                        ClientSecret = Constants.ClientSecret
                    }, new[]
                {
                YouTubeService.Scope.Youtube,
                Oauth2Service.Scope.UserinfoProfile
                },
                "user", CancellationToken.None);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("[ex] SignOut handling (phase 1) error: " + ex.Message);
            }

            try
            {
                // phase 2 
                await credential.RevokeTokenAsync(CancellationToken.None);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("[ex] SignOut handling (phase 2) error: " + ex.Message);
            }

            //Clear Login details
            txtLoginName.Text = "";
            imgProfileIcon.Fill = new Windows.UI.Xaml.Media.SolidColorBrush(
                Windows.UI.Color.FromArgb(0, 0, 0, 0));

            //Clear Subscriptions
            SubscriptionsList.ItemsSource = null;

            Frame.Navigate(typeof(WelcomePage));

            // RnD
            SideBarSplitView.IsPaneOpen = false;
        }

        private void BtnLoginFlyout_Tapped(object sender, TappedRoutedEventArgs e)
        {
            FlyoutBase.ShowAttachedFlyout((FrameworkElement)sender);
        }

        private async void BtnMyChannel_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var service = await YoutubeMethodsStatic.GetServiceAsync();

            var getMyChannel = service.Channels.List("snippet");
            getMyChannel.Mine = true;
            var result = await getMyChannel.ExecuteAsync();

            Constants.activeChannelID = result.Items[0].Id;

            contentFrame.Navigate(typeof(ChannelPage));

            // RnD
            SideBarSplitView.IsPaneOpen = false;
        }

        private void btnSettings_Click(object sender, RoutedEventArgs e)
        {
            contentFrame.Navigate(typeof(SettingsPage));

            // RnD
            SideBarSplitView.IsPaneOpen = false;
        }

        #endregion User Info Region



        #region Download

        public async void DownloadVideo()
        {
            YoutubeClient client = new YoutubeClient();

            if (Constants.videoInfo != null)
            {
                //RnD
                string videoUrl = Constants.videoInfo.Muxed[0].Url;

                Windows.Storage.Pickers.FileSavePicker savePicker = new Windows.Storage.Pickers.FileSavePicker
                {
                    SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.Downloads
                };
                savePicker.FileTypeChoices.Add("Video File", new List<string>() { ".mp4" });

                Windows.Storage.StorageFile file = await savePicker.PickSaveFileAsync();
                if (file != null)
                {
                    // Prevent updates to the remote version of the file until
                    // we finish making changes and call CompleteUpdatesAsync.
                    Windows.Storage.CachedFileManager.DeferUpdates(file);
                    // write to file
                    BackgroundDownloader downloader = new BackgroundDownloader();
                    DownloadOperation download = downloader.CreateDownload(new Uri(videoUrl), file);

                    DownloadProgress.Visibility = Visibility.Visible;

                    Progress<DownloadOperation> progress = new Progress<DownloadOperation>();
                    progress.ProgressChanged += Progress_ProgressChanged;
                    await download.StartAsync().AsTask(CancellationToken.None, progress);
                }
                else
                {
                    Debug.WriteLine("[i] Download operation was cancelled.");
                    Log.Info("Download operation was cancelled.");
                }
            }
            else
            {
                Debug.WriteLine("[i] videoInfo empty!");
                Log.Info("[i] videoInfo empty!");
            }
        }

        private void Progress_ProgressChanged(object sender, DownloadOperation e)
        {
            DownloadProgress.Value = (e.Progress.BytesReceived / 
                (double)e.Progress.TotalBytesToReceive) * 1000;

            if (e.Progress.BytesReceived == e.Progress.TotalBytesToReceive
                && e.Progress.TotalBytesToReceive != 0)
            {
                DownloadProgress.Visibility = Visibility.Collapsed;
                ShowNotifcation("Download complete.", 3000);
            }
        }

        #endregion Download


        #region Notifications

        public void ShowNotifcation(string Text, int Length = 3000)
        {
            InAppNotif.Content = Text;
            InAppNotif.Show(Length);
        }

        #endregion Notifications
    }
}