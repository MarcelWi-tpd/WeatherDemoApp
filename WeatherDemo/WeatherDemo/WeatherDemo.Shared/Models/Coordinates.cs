using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml.Data;
using WeatherDemo.Common;

namespace WeatherDemo.Models
{
    public class Coordinates : BindableBase
    {
        #region properties
        private float _Lon;

        public float Lon
        {
            get { return _Lon; }
            set
            {
                SetProperty(ref _Lon, value);
            }
        }

        private float _Lat;

        public float Lat
        {
            get { return _Lat; }
            set
            {
                SetProperty(ref _Lat, value);
            }
        }
        #endregion

        public Coordinates(float lon, float lat)
        {
            Lon = lon;
            Lat = lat;
        }

    }
}
