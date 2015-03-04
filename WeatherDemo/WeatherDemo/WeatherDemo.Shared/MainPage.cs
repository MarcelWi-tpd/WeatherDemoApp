using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using WeatherDemo.Views;
using WeatherDemo.Services;

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

        private void AddLocation_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof (AddLocation));
        }
    }
}
