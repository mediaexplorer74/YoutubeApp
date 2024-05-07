using SharpDX;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Foundation.Metadata;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace YTApp.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SettingsPage : Page
    {
        // SettingsPage
        public SettingsPage()
        {
            InitializeComponent();


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

                    //SettingsPage_BackRequested(default, default);

                    if (Frame.CanGoBack)
                    {
                        Frame.GoBack();
                        a.Handled = true;
                    }
                    a.Handled = true;
                };
            }

            // Theme Switch -----------------------------------
            if ((string)Windows.Storage.ApplicationData.Current.LocalSettings.Values["Theme"] == "Dark" 
                || Windows.Storage.ApplicationData.Current.LocalSettings.Values["Theme"] == null)
                ThemeToggleSwitch.IsOn = true;

            //We set it here so that it doesn't fire when we set the initial value
            ThemeToggleSwitch.Toggled += ToggleSwitch_Toggled;

            // API Credentials ---------------------------------
            ApiKeyTextBox.Text = "";            
            if (Windows.Storage.ApplicationData.Current.LocalSettings.Values["ApiKey"] != null)
            {
                ApiKeyTextBox.Text = 
                    (string)Windows.Storage.ApplicationData.Current.LocalSettings.Values["ApiKey"];
            }

            ClientIDTextBox.Text = "";
            if (Windows.Storage.ApplicationData.Current.LocalSettings.Values["ClientID"] != null)
            {
                ClientIDTextBox.Text =
                    (string)Windows.Storage.ApplicationData.Current.LocalSettings.Values["ClientID"];
            }

            ClientSecretTextBox.Text = "";
            if (Windows.Storage.ApplicationData.Current.LocalSettings.Values["ClientSecret"] != null)
            {
                ClientSecretTextBox.Text = 
                    (string)Windows.Storage.ApplicationData.Current.LocalSettings.Values["ClientSecret"];
            }


            // Results Limits -----------------------------------
            PlaylistVideosMaxResultsTextBox.Text = "1";
            if (Windows.Storage.ApplicationData.Current.LocalSettings.Values["PlaylistVideosMaxResults"] != null)
            {
                PlaylistVideosMaxResultsTextBox.Text =
                    (string)Windows.Storage.ApplicationData.Current.LocalSettings.Values["PlaylistVideosMaxResults"];
            }

            SubscriptionsMaxResultsTextBox.Text = "1";
            if (Windows.Storage.ApplicationData.Current.LocalSettings.Values["SubscriptionsMaxResults"] != null)
            {
                SubscriptionsMaxResultsTextBox.Text =
                    (string)Windows.Storage.ApplicationData.Current.LocalSettings.Values["SubscriptionsMaxResults"];
            }

            RelatedVideosMaxResultsTextBox.Text = "1";
            if (Windows.Storage.ApplicationData.Current.LocalSettings.Values["RelatedVideosMaxResults"] != null)
            {
                RelatedVideosMaxResultsTextBox.Text =
                    (string)Windows.Storage.ApplicationData.Current.LocalSettings.Values["RelatedVideosMaxResults"];
            }

            SearchListRequestMaxResultsTextBox.Text = "1";
            if (Windows.Storage.ApplicationData.Current.LocalSettings.Values["SearchListRequestMaxResults"] != null)
            {
                SearchListRequestMaxResultsTextBox.Text =
                    (string)Windows.Storage.ApplicationData.Current.LocalSettings.Values["SearchListRequestMaxResults"];
            }

            TempServiceMaxResultsTextBox.Text = "1";
            if (Windows.Storage.ApplicationData.Current.LocalSettings.Values["TempServiceMaxResults"] != null)
            {
                TempServiceMaxResultsTextBox.Text =
                    (string)Windows.Storage.ApplicationData.Current.LocalSettings.Values["TempServiceMaxResults"];
            }

            ChannelVideosPopularMaxResultsTextBox.Text = "1";
            if (Windows.Storage.ApplicationData.Current.LocalSettings.Values["ChannelVideosPopularMaxResults"] != null)
            {
                ChannelVideosPopularMaxResultsTextBox.Text =
                    (string)Windows.Storage.ApplicationData.Current.LocalSettings.Values["ChannelVideosPopularMaxResults"];
            }

        }//SettingsPage


        /*
        private void SettingsPage_BackRequested(object sender, BackRequestedEventArgs e)
        {
            //Frame.Visibility = Visibility.Collapsed;
        }
        */


        private void ToggleSwitch_Toggled(object sender, RoutedEventArgs e)
        {
            ThemeToggleSwitchRestartMessage.Visibility = Visibility.Visible;

            Windows.Storage.ApplicationDataContainer localSettings 
                = Windows.Storage.ApplicationData.Current.LocalSettings;

            if (((ToggleSwitch)sender).IsOn)
                localSettings.Values["Theme"] = "Dark";
            else
                localSettings.Values["Theme"] = "Light";
        }

        
        // API Credentials ------------------------------------------------

        private void ApiKeyTextBox_TextChanged(object sender, RoutedEventArgs e)
        {
            Windows.Storage.ApplicationDataContainer localSettings
                = Windows.Storage.ApplicationData.Current.LocalSettings;

            localSettings.Values["ApiKey"] = ApiKeyTextBox.Text;
            App.ApiKey = ApiKeyTextBox.Text;

        }

        private void ClientIDTextBox_TextChanged(object sender, RoutedEventArgs e)
        {
            Windows.Storage.ApplicationDataContainer localSettings
                = Windows.Storage.ApplicationData.Current.LocalSettings;

            localSettings.Values["ClientID"] = ClientIDTextBox.Text;
            App.ClientID = ClientIDTextBox.Text;
        }

        private void ClientSecretTextBox_TextChanged(object sender, RoutedEventArgs e)
        {
            Windows.Storage.ApplicationDataContainer localSettings
                = Windows.Storage.ApplicationData.Current.LocalSettings;

            localSettings.Values["ClientSecret"] = ClientSecretTextBox.Text;
            App.ClientSecret = ClientSecretTextBox.Text;
        }


        // Results Limits ------------------------------------------------
        private void PlaylistVideosMaxResultsTextBox_TextChanged(object sender, RoutedEventArgs e)
        {
            Windows.Storage.ApplicationDataContainer localSettings
                = Windows.Storage.ApplicationData.Current.LocalSettings;

            localSettings.Values["PlaylistVideosMaxResults"] = PlaylistVideosMaxResultsTextBox.Text;

            int result = 1;
            try
            {
                result = Int32.Parse(PlaylistVideosMaxResultsTextBox.Text);
                if (result < 0)
                    return;
            }
            catch (FormatException)
            {
                return; 
            }            

            App.PlaylistVideosMaxResults = result;
        }

        private void SubscriptionsMaxResultsTextBox_TextChanged(object sender, RoutedEventArgs e)
        {
            Windows.Storage.ApplicationDataContainer localSettings
                = Windows.Storage.ApplicationData.Current.LocalSettings;

            localSettings.Values["SubscriptionsMaxResults"] = SubscriptionsMaxResultsTextBox.Text;

            int result = 1;
            try
            {
                result = Int32.Parse(SubscriptionsMaxResultsTextBox.Text);
                if (result < 0)
                    return;
            }
            catch (FormatException)
            {
                return;
            }

            App.SubscriptionsMaxResults = result;
        }

        private void RelatedVideosMaxResultsTextBox_TextChanged(object sender, RoutedEventArgs e)
        {
            Windows.Storage.ApplicationDataContainer localSettings
                = Windows.Storage.ApplicationData.Current.LocalSettings;

            localSettings.Values["RelatedVideosMaxResults"] = RelatedVideosMaxResultsTextBox.Text;

            int result = 1;
            try
            {
                result = Int32.Parse(RelatedVideosMaxResultsTextBox.Text);
                if (result < 0)
                    return;
            }
            catch (FormatException)
            {
                return;
            }

            App.RelatedVideosMaxResults = result;
        }

        private void SearchListRequestMaxResultsTextBox_TextChanged(object sender, RoutedEventArgs e)
        {
            Windows.Storage.ApplicationDataContainer localSettings
                = Windows.Storage.ApplicationData.Current.LocalSettings;

            localSettings.Values["SearchListRequestMaxResults"] = SearchListRequestMaxResultsTextBox.Text;

            int result = 1;
            try
            {
                result = Int32.Parse(SearchListRequestMaxResultsTextBox.Text);
                if (result < 0)
                    return;
            }
            catch (FormatException)
            {
                return;
            }

            App.SearchListRequestMaxResults = result;
        }

        private void ChannelVideosPopularMaxResultsTextBox_TextChanged(object sender, RoutedEventArgs e)
        {
            Windows.Storage.ApplicationDataContainer localSettings
                = Windows.Storage.ApplicationData.Current.LocalSettings;

            localSettings.Values["ChannelVideosPopularMaxResults"] = ChannelVideosPopularMaxResultsTextBox.Text;

            int result = 1;
            try
            {
                result = Int32.Parse(ChannelVideosPopularMaxResultsTextBox.Text);
                if (result < 0)
                    return;
            }
            catch (FormatException)
            {
                return;
            }

            App.ChannelVideosPopularMaxResults = result;
        }

        private void TempServiceMaxResultsTextBox_TextChanged(object sender, RoutedEventArgs e)
        {
            Windows.Storage.ApplicationDataContainer localSettings
                = Windows.Storage.ApplicationData.Current.LocalSettings;

            localSettings.Values["TempServiceMaxResults"] = TempServiceMaxResultsTextBox.Text;

            int result = 1;
            try
            {
                result = Int32.Parse(TempServiceMaxResultsTextBox.Text);
                if (result < 0)
                    return;
            }
            catch (FormatException)
            {
                return;
            }

            App.TempServiceMaxResults = result;
        }


    }
}
