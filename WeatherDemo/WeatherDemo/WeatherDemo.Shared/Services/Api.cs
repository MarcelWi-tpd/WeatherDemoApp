using System;
using System.Collections.Generic;
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

        public static async Task<WeatherDayData> DownloadWeatherData(string city)
        {
            var values = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("weather", city)
            };

            var weatherData = GetJsonFromWeb(ApiCallType.Weather, values);
            return new WeatherDayData();
        }

        public static async Task<Forecast> DownlaodForecastData()
        {

            return new Forecast();
        }

        private static async Task<object> GetJsonFromWeb(ApiCallType type, List<KeyValuePair<string, string>> parameterList)
        {
            if (App.IsInternetAvailable)
            {
                try
                {
                    var httpClient = new HttpClient();
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/html"));

                    HttpResponseMessage response = await httpClient.PostAsync(API_ROOT + type.ToString(), new FormUrlEncodedContent(parameterList));
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
