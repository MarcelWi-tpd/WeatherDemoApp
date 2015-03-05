using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using WeatherDemo.Common;
using WeatherDemo.Models;
using WeatherDemo.Services;
using System.Xml.Linq;
using Windows.Data.Xml.Dom;
using System.Threading.Tasks;
using Windows.UI.Core;

namespace WeatherDemo.ViewModels
{
    public class MainViewModel : BindableBase
    {
        private static MainViewModel _Current;
        public static MainViewModel Current
        {
            get { return _Current ?? (_Current = new MainViewModel()); }
        }

        private Location _CurrentLocation;
        public Location CurrentLocation
        {
            get { return _CurrentLocation; }
            set { SetProperty(ref _CurrentLocation, value); }
        }

        private ObservableCollection<Location> _LocationCollection;
        public ObservableCollection<Location> LocationCollection
        {
            get { return _LocationCollection ?? (_LocationCollection = new ObservableCollection<Location>()); }
            set { SetProperty(ref _LocationCollection, value); }
        }

        private ObservableCollection<WeatherData> _ThreeHourIntervalForecast;
        public ObservableCollection<WeatherData> ThreeHourIntervalForecast
        {
            get { return _ThreeHourIntervalForecast ?? (_ThreeHourIntervalForecast = new ObservableCollection<WeatherData>()); }
            set { SetProperty(ref _ThreeHourIntervalForecast, value); }
        }

        private ObservableCollection<WeatherDataDailyForecast> _DailyIntervalForecast;
        public ObservableCollection<WeatherDataDailyForecast> DailyIntervalForecast
        {
            get { return _DailyIntervalForecast ?? (_DailyIntervalForecast = new ObservableCollection<WeatherDataDailyForecast>()); }
            set { SetProperty(ref _DailyIntervalForecast, value); }
        }

        public MainViewModel()
        {
            _Current = this;

            if (Windows.ApplicationModel.DesignMode.DesignModeEnabled)
            {
                var loc = new Location(0, "Cologne", "GER", new Coordinates(49, 122));
                var main = new Main(276.71, 96, 1022.55, 275.59, 277.95);
                var wind = new Wind(5.01, 259.504);
                var sys = new Sys(1425363096, 1425402990, "Germany");
                WeatherData data = new WeatherData();
                data.Main = main;
                data.Wind = wind;
                data.Sys = sys;
                loc.TodaysWeatherData = data;

                CurrentLocation = loc;
                CurrentLocation.TodaysWeatherData = data;
            }
        }
    }
}
