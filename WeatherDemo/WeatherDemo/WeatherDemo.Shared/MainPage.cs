using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using WeatherDemo.Views;

namespace WeatherDemo
{
    public sealed partial class MainPage : Page
    {
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void AddLocation_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof (AddLocation));
        }
    }
}
