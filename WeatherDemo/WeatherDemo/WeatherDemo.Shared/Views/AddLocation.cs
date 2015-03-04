using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Newtonsoft.Json;
using WeatherDemo.Common;
using WeatherDemo.Services;
using WeatherDemo.ViewModels;
using WeatherDemo.Models;
using Windows.Data.Xml.Dom;
using System.Xml.Linq;
using Windows.Devices.Geolocation;

namespace WeatherDemo.Views
{
    public sealed partial class AddLocation : Page
    {
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedTo(e);
        }
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedFrom(e);
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

        #region appBarButtonEvents
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            if (Frame.CanGoBack)
                Frame.GoBack();
        }

        private async void Save_Click(object sender, RoutedEventArgs e)
        {
            if (tbxPlz.Text.Length > 0 && tbxPlace.Text.Length > 0)
            {
                var messageDialog =
                    new MessageDialog(
                        "In der Demo ist nur eine Eingabe erlaubt. Bitte suche entweder nach der Postleitzahl oder einem Ort.",
                        "Fehlerhafte Eingabe");
                messageDialog.Commands.Add(new UICommand("OK"));
                await messageDialog.ShowAsync();
            }
            else
            {
                var location = await Api.DownloadWeatherData("weather?q=" + tbxPlace.Text.Trim());

                if (location == null)
                {
                    var messageDialog =
                        new MessageDialog(
                            "Der gesuchte Ort konnte nicht in der Wetterdatenbank gefunden werden.",
                            "Fehlerhafte Eingabe");
                    messageDialog.Commands.Add(new UICommand("OK"));
                    await messageDialog.ShowAsync();
                }
                else
                {
                    SaveLocationLocalAndAddToCollection(location);
                    if (Frame.CanGoBack)
                        Frame.GoBack();
                }
            }

        }

        private async void CurrentLocation_Click(object sender, RoutedEventArgs e)
        {
            if (LocalStorage.GetSetting("LocationConsent") != null && (bool) LocalStorage.GetSetting("LocationConsent"))
            {
                Geolocator geolocator = new Geolocator();
                geolocator.DesiredAccuracy = PositionAccuracy.High;

                Geoposition devicePosition = await geolocator.GetGeopositionAsync(
                    maximumAge: TimeSpan.FromMinutes(5),
                    timeout: TimeSpan.FromSeconds(10));

                var latitude = devicePosition.Coordinate.Latitude.ToString("0.00");
                var longitude = devicePosition.Coordinate.Longitude.ToString("0.00");

                if (latitude == null || longitude == null)
                {
                    ErrorMessageForGpsLocation();
                    return;
                }

                var location = await Api.DownloadWeatherData("weather?lat=" + latitude + "&lon=" + longitude);

                if (location == null)
                {
                    ErrorMessageForGpsLocation();
                    return;
                }

                SaveLocationLocalAndAddToCollection(location);
                if (Frame.CanGoBack)
                    Frame.GoBack();
            }
            else
            {
                var messageDialog =
                    new MessageDialog(
                        "Der Standort kann nicht ermittelt werden. Die Ortung für diese App muss erlaubt werden.",
                        "Verweigerte Ortung");
                messageDialog.Commands.Add(new UICommand("OK"));
                await messageDialog.ShowAsync();
            }
        }
        #endregion


        private async void ErrorMessageForGpsLocation()
        {
            var messageDialog =
                new MessageDialog(
                    "Der Standort konnte nicht ermittelt werden. Bitte erneut versuchen oder die manuelle Suche benutzen.",
                    "Fehlerhafte Ortung");
            messageDialog.Commands.Add(new UICommand("OK"));
            await messageDialog.ShowAsync();
        }

        private async void SaveLocationLocalAndAddToCollection(Location location)
        {
            //Aufgrund des Aufbaus des JSON muss Country hier einmal manuell beschrieben werden
            location.Country = location.TodaysWeatherData.Sys.Country;
            MainViewModel.Current.LocationCollection.Add(location);

            XDocument xmlLocations = new XDocument();
            XElement xmlRoot = new XElement("Locations");
            xmlLocations.Add(xmlRoot);

            foreach (Location locationInCollection in MainViewModel.Current.LocationCollection)
            {
                XElement xmlElement = new XElement("Location");
                xmlElement.Add(new XElement("Name", locationInCollection.Name));
                xmlElement.Add(new XElement("Country", locationInCollection.TodaysWeatherData.Sys.Country));
                xmlRoot.Add(xmlElement);
            }

            await LocalStorage.SaveJsonToLocalStorage("userLocation.xml", xmlLocations.ToString());
        }
    }
}
