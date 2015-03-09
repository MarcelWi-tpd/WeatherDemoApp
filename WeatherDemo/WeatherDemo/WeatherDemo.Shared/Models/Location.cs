using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using System.Text;
using Newtonsoft.Json;
using WeatherDemo.Common;

namespace WeatherDemo.Models
{
    public class Location : BindableBase
    {

        #region properties
        private string _Name;
        public string Name
        {
            get { return _Name; }
            set
            {
                SetProperty(ref _Name, value);
            }
        }

        private string _Country;
        public string Country
        {
            get { return _Country; }
            set
            {
                SetProperty(ref _Country, value);
            }
        }

        private Coordinates _Coord;

        public Coordinates Coord
        {
            get { return _Coord; }
            set
            {
                SetProperty(ref _Coord, value);
            }
        }

        public WeatherData TodaysWeatherData { get; set; }

        [JsonConstructor]
        public Location(string name, string country)
        {
            Name = name;
            Country = country;
        }
        #endregion
        public Location(string name, string country, Coordinates coord)
        {
            Name = name;
            Country = country;
            Coord = coord;
        }
        

    }
}
