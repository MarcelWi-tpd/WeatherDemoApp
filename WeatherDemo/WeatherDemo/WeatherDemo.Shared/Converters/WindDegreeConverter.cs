using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml.Data;

namespace WeatherDemo.Converters
{
    public sealed class WindDegreeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is double)
            {
                double degree = (double)value;
                if (degree >= 348.75 && degree < 11.25)
                    return " N";
                if (degree >= 11.25 && degree < 33.75)
                    return " NNE";
                if (degree >= 33.75 && degree < 56.25)
                    return " NE";
                if (degree >= 56.25 && degree < 78.75)
                    return " ENE";
                if (degree >= 78.75 && degree < 101.25)
                    return " E";
                if (degree >= 101.25 && degree < 123.75)
                    return " ESE";
                if (degree >= 123.75 && degree < 146.25)
                    return " SE";
                if (degree >= 146.25 && degree < 168.75)
                    return " SSE";
                if (degree >= 168.75 && degree < 191.25)
                    return " S";
                if (degree >= 191.25 && degree < 213.75)
                    return " SSW";
                if (degree >= 213.75 && degree < 236.25)
                    return " SW";
                if (degree >= 236.25 && degree < 258.75)
                    return " WSW";
                if (degree >= 258.75 && degree < 281.25)
                    return " W";
                if (degree >= 281.25 && degree < 303.75)
                    return " WNW";
                if (degree >= 303.75 && degree < 326.25)
                    return " NW";
                if (degree >= 326.25 && degree < 348.75)
                    return " NNW";

                return value;
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return new NotImplementedException();
        }
    }
}
