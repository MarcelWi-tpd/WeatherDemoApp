using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using WeatherDemo.Common;

namespace WeatherDemo.Models
{

    public class Weather : BindableBase
    {
        #region properties
        private int _WeatherID;
        [JsonProperty(PropertyName = "id")]
        public int WeatherID
        {
            get { return _WeatherID; }
            set
            {
                SetProperty(ref _WeatherID, value);
            }
        }

        private string _Name;
        [JsonProperty(PropertyName = "main")]
        public string Name
        {
            get { return _Name; }
            set
            {
                SetProperty(ref _Name, value);
            }
        }
        private string _Description;
        [JsonProperty(PropertyName = "description")]
        public string Description
        {
            get { return _Description; }
            set
            {
                SetProperty(ref _Description, value);
            }
        }
        #endregion

        public Weather(int weatherId, string name, string description)
        {
            WeatherID = weatherId;
            Name = name;
            Description = description;
        }
    }
}
