using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml.Data;

namespace WeatherDemo.Converters
{
    public sealed class WeatherIdToIconSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is string)
            {
                int weatherId = 0;
                Int32.TryParse(value.ToString(), out weatherId);
                if (weatherId == 500 || weatherId == 501)
                    return "ms-appx:///Assets/TileIcon/17.png";
                if (weatherId < 501 && weatherId <600)
                    return "ms-appx:///Assets/TileIcon/18.png";
                if (weatherId == 600)
                    return "ms-appx:///Assets/TileIcon/21.png";
                if (weatherId == 601)
                    return "ms-appx:///Assets/TileIcon/23.png";
                if (weatherId > 601 && weatherId < 800)
                    return "ms-appx:///Assets/TileIcon/24.png";
                if (weatherId == 800)
                    return "ms-appx:///Assets/TileIcon/2.png";
                if (weatherId == 801)
                    return "ms-appx:///Assets/TileIcon/8.png";
                if (weatherId == 802)
                    return "ms-appx:///Assets/TileIcon/14.png";
                if (weatherId == 803 || weatherId == 804)
                    return "ms-appx:///Assets/TileIcon/25.png";
            }

            return "ms-appx:///Assets/TileIcon/25.png";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return new NotImplementedException();
        }
    }
}
