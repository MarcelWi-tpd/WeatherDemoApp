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

        public MainViewModel()
        {
            _Current = this;
        }
    }
}
