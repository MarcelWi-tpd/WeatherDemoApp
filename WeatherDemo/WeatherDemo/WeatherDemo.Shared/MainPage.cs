using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Windows.Data.Xml.Dom;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;
using WeatherDemo.Models;
using WeatherDemo.Views;
using WeatherDemo.Services;
using WeatherDemo.ViewModels;

namespace WeatherDemo
{
    public sealed partial class MainPage : Page
    {
        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (LocalStorage.GetSetting("LocationConsent") == null ||
                (bool)LocalStorage.GetSetting("LocationConsent") == false)
            {
                var messageDialog =
                    new MessageDialog(
                        "Um die Wetterinformationen für den aktuellen Standort abzurufen muss die Ortung erlaubt werden. Ortung erlauben?",
                        "Ortung erlauben");
                messageDialog.Commands.Add(new UICommand("Ja", command =>
                {
                    LocalStorage.SaveSetting("LocationConsent", true);
                }));
                messageDialog.Commands.Add(new UICommand("Nein", command =>
                {
                    LocalStorage.SaveSetting("LocationConsent", false);
                }));
                await messageDialog.ShowAsync();
            }
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.NavigationMode != NavigationMode.Back)
                LoadLocalLocation();
        }


        //TODO: zurück in MainViewModel, aber es kam eine Exception - aus Zeitgründen vorerst hierher verschoben
        private async void LoadLocalLocation()
        {
            var localLocationAsXml = await LocalStorage.GetJsonFromLocalStorage("userLocation.xml");
            if (localLocationAsXml == null || String.IsNullOrEmpty(localLocationAsXml))
                return;

            XmlDocument xmlLocations = new XmlDocument();
            xmlLocations.LoadXml(localLocationAsXml);

            var locationList = xmlLocations.SelectNodes("Locations/Location");

            foreach (XmlElement xmlLocation in locationList)
            {
                string name = xmlLocation.SelectSingleNode("Name").InnerText.Trim();
                string country = xmlLocation.SelectSingleNode("Country").InnerText.Trim();
                var location = await Api.DownloadWeatherData("weather?q=" + name + "," + country);

                if (location == null)
                {
                    location = new Location(name, country);
                }
                else
                    location.Country = country;

                MainViewModel.Current.LocationCollection.Add(location);
            }
        }

        private void AddLocation_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof (AddLocation));
        }

        private void Border_Holding(object sender, HoldingRoutedEventArgs e)
        {
            Flyout.ShowAttachedFlyout((FrameworkElement)sender);
        }

        private async void DeleteLocationFlayFlyoutItem_Click(object sender, RoutedEventArgs e)
        {
            var dataContext = ((MenuFlyoutItem) e.OriginalSource).DataContext;
            if (dataContext == null || !(dataContext is Location))
                return;

            MainViewModel.Current.LocationCollection.Remove(dataContext as Location);

            await App.SaveLocationsInLocalXml(MainViewModel.Current.LocationCollection);
        } 
    }
}
