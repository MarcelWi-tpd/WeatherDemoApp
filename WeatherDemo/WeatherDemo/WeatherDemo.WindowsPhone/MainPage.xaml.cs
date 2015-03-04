using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using WeatherDemo.Models;
using WeatherDemo.Services;
using System.Collections.ObjectModel;
using Windows.Data.Xml.Dom;
using Windows.UI.Popups;
using WeatherDemo.ViewModels;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace WeatherDemo
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            if (LocalStorage.GetSetting("LocationConsent") == null ||
                (bool) LocalStorage.GetSetting("LocationConsent") == false)
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
            LoadLocalLocation();
        }


        //TODO: zurück in MainViewModel, aber es kam eine Exception - aus Zeitgründen vorerst hierher verschoben
        private async void LoadLocalLocation()
        {
            var tempLocationList = new ObservableCollection<Location>();
            var localLocationAsXml = await LocalStorage.GetJsonFromLocalStorage("userLocation.xml");
            if (localLocationAsXml == null)
                return;

            XmlDocument xmlLocations = new XmlDocument();
            xmlLocations.LoadXml(localLocationAsXml);

            var locationList = xmlLocations.SelectNodes("Locations/Location/name");

            foreach (XmlElement xmlLocation in locationList)
            {
                var location = await Api.DownloadWeatherData(xmlLocation.InnerText.Trim());
                tempLocationList.Add(location);
            }

            MainViewModel.Current.LocationCollection = tempLocationList;

        } 
    }
}
