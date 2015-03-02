using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using WeatherDemo.Common;

namespace WeatherDemo.Models
{
    public class Day : BindableBase
    {
        #region properties
        public DateTime Date { get; set; }

        private ObservableCollection<WeatherData> _WeatherDataList;

        public ObservableCollection<WeatherData> WeatherDataList
        {
            get { return _WeatherDataList ?? (_WeatherDataList = new ObservableCollection<WeatherData>()); }
            set
            {
                SetProperty(ref _WeatherDataList, value);
            }
        }
        

        #endregion
    }
}
