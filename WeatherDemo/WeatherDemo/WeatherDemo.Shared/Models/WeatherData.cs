using System;
using System.Collections.Generic;
using System.Text;
using WeatherDemo.Common;

namespace WeatherDemo.Models
{
    public class WeatherData : BindableBase
    {
        #region properties
        private float _Temperature;
        public float Temperature
        {
            get { return _Temperature; }
            set
            {
                SetProperty(ref _Temperature, value);
            }
        }

        private float _MinTemperature;
	    public float MinTemperature
	    {
		    get { return _MinTemperature;}
            set
            {
                SetProperty(ref _MinTemperature, value);
            }
	    }

        private float _MaxTemperature;
	    public float MaxTemperature
	    {
            get { return _MaxTemperature; }
            set
            {
                SetProperty(ref _MaxTemperature, value);
            }
	    }
	
        #endregion

        
    }
}
