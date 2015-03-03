using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using WeatherDemo.Models;

namespace WeatherDemo.Services
{
    public class Api
    {
        private const string API_ROOT = "http://api.openweathermap.org/data/2.5/";

        public static async Task<Location> DownloadWeatherData(string city)
        {
            var weatherDataAsJson = await GetWeatherInfoJsonFromWeb("weather?q=", city);
            // TODO: if succeed: 200 else 404 in "cod"

            // convert data relevant for current location
            var location = JsonConvert.DeserializeObject<Location>(weatherDataAsJson as String);
            // convert data relevant for current weather
            location.TodaysWeatherData = JsonConvert.DeserializeObject<WeatherData>(weatherDataAsJson as String);

            return location;
        }

        public static async Task<ObservableCollection<Day>> DownlaodForecastData(string city, ApiCallType type)
        {
            var weatherDataForecastAsJson = new object();
            switch(type)
            {
                case ApiCallType.Forecast:
                    weatherDataForecastAsJson = await GetWeatherInfoJsonFromWeb("forecast?q=", city); break;
                case ApiCallType.ForeCastDaily:
                    weatherDataForecastAsJson = await GetWeatherInfoJsonFromWeb("forecast/daily?q=", city); break;
            }
            try
            {
                JObject jsonObjectForWeatherList = JObject.Parse(weatherDataForecastAsJson as String);
                ObservableCollection<WeatherData> forecastList = JsonConvert.DeserializeObject<ObservableCollection<WeatherData>>(jsonObjectForWeatherList["list"].ToString());
            }
            catch (JsonSerializationException e)
            {
                Debug.WriteLine(e.ToString());
            }
                
            return new ObservableCollection<Day>();
        }

        private static async Task<object> GetWeatherInfoJsonFromWeb(string parameter, string city)
        {
            if (App.IsInternetAvailable)
            {
                try
                {
                    var httpClient = new HttpClient();
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/html"));
                    Debug.WriteLine(parameter);
                    HttpResponseMessage response = await httpClient.GetAsync(API_ROOT + parameter + city);
                    response.EnsureSuccessStatusCode();

                    var responseString = await response.Content.ReadAsStringAsync();
                    return responseString;
                }
                catch (WebException)
                {
                    return null;
                }
            }

            return null;
        }
    }
}
