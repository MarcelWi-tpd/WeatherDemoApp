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
using WeatherDemo.Models;

namespace WeatherDemo.Services
{
    public class Api
    {
        private const string API_ROOT = "http://api.openweathermap.org/data/2.5/";

        public static async Task<Location> DownloadWeatherData(string city)
        {
            var weatherDataAsJson = await GetWeatherInfoJsonFromWeb(ApiCallType.Weather, city);
            // TODO: if succeed: 200 else 404 in "cod"

            var location = JsonConvert.DeserializeObject<Location>(weatherDataAsJson as String);
            return location;
        }

        public static async Task<ObservableCollection<Day>> DownlaodForecastData()
        {

            return new ObservableCollection<Day>();
        }

        private static async Task<object> GetWeatherInfoJsonFromWeb(ApiCallType type, string city)
        {
            if (App.IsInternetAvailable)
            {
                try
                {
                    var httpClient = new HttpClient();
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/html"));
                    Debug.WriteLine(type.ToString());
                    HttpResponseMessage response = await httpClient.GetAsync(API_ROOT + type.ToString() + "?q=" + city);
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
