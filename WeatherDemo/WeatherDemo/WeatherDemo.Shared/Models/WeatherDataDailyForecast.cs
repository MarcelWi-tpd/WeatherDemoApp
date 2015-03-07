using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml.Input;
using Newtonsoft.Json;
using WeatherDemo.Common;

namespace WeatherDemo.Models
{
    public class WeatherDataDailyForecast : BindableBase
    {
        public Temp Temp { get; set; }

        #region properties
        private List<Weather> _Weather;
        public List<Weather> Weather
        {
            get { return _Weather; }
            set
            {
                SetProperty(ref _Weather, value);
            }
        }

        private double _Pressure;

        [JsonProperty(PropertyName = "pressure")]
        public double Pressure
        {
            get { return _Pressure; }
            set { SetProperty(ref _Pressure, value); }
        }

        private int _Humidity;

        [JsonProperty(PropertyName = "humidity")]
        public int Humidity
        {
            get { return _Humidity; }
            set { SetProperty(ref _Humidity, value); }
        }

        private int _Time;
        [JsonProperty(PropertyName = "dt")]
        public int Time
        {
            get { return _Time; }
            set
            {
                SetProperty(ref _Time, value);
            }
        }

        private double _WindSpeed;

        [JsonProperty(PropertyName = "speed")]
        public double WindSpeed
        {
            get { return _WindSpeed; }
            set { SetProperty(ref _WindSpeed, value); }
        }

        private double _WindDegree;

        [JsonProperty(PropertyName = "deg")]
        public double WindDegree
        {
            get { return _WindDegree; }
            set { SetProperty(ref _WindDegree, value); }
        }

        private double _Clouds;

        [JsonProperty(PropertyName = "clouds")]
        public double Clouds
        {
            get { return _Clouds; }
            set { SetProperty(ref _Clouds, value); }
        }

        private double _Rain;

        [JsonProperty(PropertyName = "rain")]
        public double Rain
        {
            get { return _Rain; }
            set { SetProperty(ref _Rain, value); }
        }

        #endregion
    }

    public class Temp : BindableBase
    {
        #region properties
        private double _DayTemperature;
        [JsonProperty(PropertyName = "day")]
        public double DayTemperature
        {
            get { return _DayTemperature; }
            set
            {
                SetProperty(ref _DayTemperature, Math.Round(value - App.KELVINTOCELSIUS, 0));
            }
        }

        private double _MinTemperature;
        [JsonProperty(PropertyName = "min")]
        public double MinTemperature
        {
            get { return _MinTemperature; }
            set
            {
                SetProperty(ref _MinTemperature, Math.Round(value - App.KELVINTOCELSIUS, 0));
            }
        }

        private double _MaxTemperature;
        [JsonProperty(PropertyName = "max")]
        public double MaxTemperature
        {
            get { return _MaxTemperature; }
            set
            {
                SetProperty(ref _MaxTemperature, Math.Round(value - App.KELVINTOCELSIUS, 0));
            }
        }

        private double _NightTemperature;
        [JsonProperty(PropertyName = "night")]
        public double NightTemperature
        {
            get { return _NightTemperature; }
            set
            {
                SetProperty(ref _NightTemperature, Math.Round(value - App.KELVINTOCELSIUS, 0));
            }
        }

        private double _EveTemperature;
        [JsonProperty(PropertyName = "eve")]
        public double EveTemperature
        {
            get { return _EveTemperature; }
            set
            {
                SetProperty(ref _EveTemperature, Math.Round(value - App.KELVINTOCELSIUS, 0));
            }
        }

        private double _MorningTemperature;
        [JsonProperty(PropertyName = "morn")]
        public double MorningTemperature
        {
            get { return _MorningTemperature; }
            set
            {
                SetProperty(ref _MorningTemperature, Math.Round(value - App.KELVINTOCELSIUS, 0));
            }
        }
        #endregion

        public Temp(double dayTemp, double minTemp, double maxTemp, double nightTemp, double eveTemp, double mornTemp)
        {
            DayTemperature = dayTemp;
            MinTemperature = minTemp;
            MaxTemperature = maxTemp;
            NightTemperature = nightTemp;
            EveTemperature = eveTemp;
            MorningTemperature = mornTemp;
        }
    }
}
