using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using WeatherDemo.Common;
using WeatherDemo.Models;
using WeatherDemo.Services;
using WeatherDemo.ViewModels;
using System.Threading.Tasks;

namespace WeatherDemo.Views
{
    public sealed partial class WeatherDetails : Page
    {
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedTo(e);

#if WINDOWS_PHONE_APP
            StatusBar statusBar = StatusBar.GetForCurrentView();
            await statusBar.HideAsync();
#endif

            await ShowLoading(true);
            MainViewModel.Current.ThreeHourIntervalForecast = await Api.DownlaodForecastData(MainViewModel.Current.CurrentLocation.Name);
            MainViewModel.Current.DailyIntervalForecast =
                await Api.DownlaodDailyForecastData(MainViewModel.Current.CurrentLocation.Name);
            await ShowLoading(false);

        }
        protected override async void OnNavigatedFrom(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedFrom(e);

#if WINDOWS_PHONE_APP
            StatusBar statusBar = StatusBar.GetForCurrentView();
            await statusBar.ShowAsync();
#endif
        }

        /// <summary>
        /// Gets the <see cref="NavigationHelper"/> associated with this <see cref="Page"/>.
        /// </summary>
        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }

        /// <summary>
        /// Populates the page with content passed during navigation.  Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="sender">
        /// The source of the event; typically <see cref="NavigationHelper"/>
        /// </param>
        /// <param name="e">Event data that provides both the navigation parameter passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested and
        /// a dictionary of state preserved by this page during an earlier
        /// session.  The state will be null the first time a page is visited.</param>
        private void NavigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="sender">The source of the event; typically <see cref="NavigationHelper"/></param>
        /// <param name="e">Event data that provides an empty dictionary to be populated with
        /// serializable state.</param>
        private void NavigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
        }

        private async Task ShowLoading(bool p)
        {
#if WINDOWS_PHONE_APP
            if (p)
                await App.ShowLoadingIndicatorAsync();
            else
            {
                await App.HideLoadingIndicatorAsync();
            }
#endif
        }
    }
}
