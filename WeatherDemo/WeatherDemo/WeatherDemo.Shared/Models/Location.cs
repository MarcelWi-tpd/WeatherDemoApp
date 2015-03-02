using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using System.Text;
using WeatherDemo.Common;

namespace WeatherDemo.Models
{
    public class Location : BindableBase
    {

        #region properties
        private int _LocationId;
        public int LocationId
        {
            get { return _LocationId; }
            set
            {
                SetProperty(ref _LocationId, value);
            }
        }

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

        private ObservableCollection<Day> _DayList;

        public ObservableCollection<Day> DayList
        {
            get { return _DayList ?? (_DayList = new ObservableCollection<Day>()); }
            set
            {
                SetProperty(ref _DayList, value);
            }
        }
        
        #endregion
        public Location(int locationId, string name, string country, Coordinates coord)
        {
            LocationId = locationId;
            Name = name;
            Country = country;
            Coord = coord;
        }
        

    }
}
