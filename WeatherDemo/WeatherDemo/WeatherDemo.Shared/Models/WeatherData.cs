using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Microsoft.VisualBasic.CompilerServices;
using WeatherDemo.Common;
using Newtonsoft.Json;

namespace WeatherDemo.Models
{
    public class WeatherData : BindableBase
    {
        #region properties
        public Wind Wind { get; set; }
        public Rain Rain { get; set; }
        public Snow Snow { get; set; }
        public Clouds Clouds { get; set; }
        public Main Main { get; set; }
        public Sys Sys { get; set; }


        private DateTime _Time;
        [JsonProperty(PropertyName = "dt_txt")]
        public DateTime Time
        {
            get { return _Time; }
            set
            {
                SetProperty(ref _Time, value);
            }
        }

        private List<Weather> _Weather;
        public List<Weather> Weather
        {
            get { return _Weather; }
            set
            {
                SetProperty(ref _Weather, value);
            }
        }

        //TODO: Fehlermeldung kommt hier: public Weather Weather { get; set; }
        #endregion

        public WeatherData() { }

        [JsonConstructor]
        public WeatherData(DateTime time, Wind wind, Rain rain, Snow snow, Clouds clouds, Main main, Sys sys)
        {
            Time = time;
            Wind = wind;
            Rain = rain;
            Snow = snow;
            Clouds = clouds;
            Main = main;
            Sys = sys;
        }
    }
    public class Main : BindableBase
    {

        #region properties
        private double _Temperature;
        [JsonProperty(PropertyName = "temp")]
        public double Temperature
        {
            get { return _Temperature; }
            set
            {
                SetProperty(ref _Temperature, value);
            }
        }

        private double _MinTemperature;
        [JsonProperty(PropertyName = "temp_min")]
        public double MinTemperature
        {
            get { return _MinTemperature; }
            set
            {
                SetProperty(ref _MinTemperature, value);
            }
        }

        private double _MaxTemperature;
        [JsonProperty(PropertyName = "temp_max")]
        public double MaxTemperature
        {
            get { return _MaxTemperature; }
            set
            {
                SetProperty(ref _MaxTemperature, value);
            }
        }

        private int _Humidity;
        [JsonProperty(PropertyName = "humidity")]
        public int Humidity
        {
            get { return _Humidity; }
            set
            {
                SetProperty(ref _Humidity, value);
            }
        }

        private double _Pressure;
        [JsonProperty(PropertyName = "pressure")]
        public double Pressure
        {
            get { return _Pressure; }
            set
            {
                SetProperty(ref _Pressure, value);
            }
        }
        #endregion

        public Main(double temp, int humidity, double pressure, double temp_min, double temp_max)
        {
            Temperature = Math.Round(temp - App.KELVINTOCELSIUS, 0);
            Humidity = humidity;
            Pressure = pressure;
            MinTemperature = Math.Round(temp_min - App.KELVINTOCELSIUS, 0);
            MaxTemperature = Math.Round(temp_max - App.KELVINTOCELSIUS, 0);
        }
    }

    public class Sys : BindableBase
    {
        #region properties
        private string _Country;
        [JsonProperty(PropertyName = "country")]
        public string Country
        {
            get { return _Country; }
            set
            {
                SetProperty(ref _Country, value);
            }
        }

        private DateTime _Sunrise;
        [JsonProperty(PropertyName = "sunrise")]
        public DateTime Sunrise
        {
            get { return _Sunrise; }
            set
            {
                SetProperty(ref _Sunrise, value);
            }
        }

        private DateTime _Sunset;
        [JsonProperty(PropertyName = "sunset")]
        public DateTime Sunset
        {
            get { return _Sunset; }
            set
            {
                SetProperty(ref _Sunset, value);
            }
        }
        #endregion

        public Sys(int sunrise, int sunset, string country)
        {
            Country = country;
            Sunrise = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc).AddSeconds(sunrise).ToLocalTime();
            Sunset = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc).AddSeconds(sunset).ToLocalTime();
        }
    }


    public class Wind : BindableBase
    {
        #region properties
        private double _Speed;
        [JsonProperty(PropertyName = "speed")]
        public double Speed
        {
            get { return _Speed; }
            set
            {
                SetProperty(ref _Speed, value);
            }
        }

        private double _Degree;
        [JsonProperty(PropertyName = "deg")]
        public double Degree
        {
            get { return _Degree; }
            set
            {
                SetProperty(ref _Degree, value);
            }
        }
        #endregion

        public Wind(double speed, double deg)
        {
            Speed = speed;
            Degree = deg;
        }
    }

    public class Rain : BindableBase
    {
        #region properties
        private double _ThreeHours;
        [JsonProperty(PropertyName = "3h")]
        public double ThreeHours
        {
            get { return _ThreeHours; }
            set
            {
                SetProperty(ref _ThreeHours, value);
            }
        }
        #endregion

        public Rain(double threehours)
        {
            ThreeHours = threehours;
        }
    }

    public class Snow : BindableBase
    {
        #region properties
        private double _ThreeHours;
        [JsonProperty(PropertyName = "3h")]
        public double ThreeHours
        {
            get { return _ThreeHours; }
            set
            {
                SetProperty(ref _ThreeHours, value);
            }
        }
        #endregion

        public Snow(double threehours)
        {
            ThreeHours = threehours;
        }
    }

    public class Clouds : BindableBase
    {
        #region properties
        private double _DailyClouds;
        [JsonProperty(PropertyName = "all")]
        public double DailyClouds
        {
            get { return _DailyClouds; }
            set
            {
                SetProperty(ref _DailyClouds, value);
            }
        }
        #endregion

        public Clouds(double dailyClouds)
        {
            DailyClouds = dailyClouds;
        }
    }
}
