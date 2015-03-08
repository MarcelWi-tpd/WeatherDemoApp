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
using Windows.UI.Popups;
using Newtonsoft.Json.Linq;
using WeatherDemo.Models;
using WeatherDemo.ViewModels;

namespace WeatherDemo.Services
{
    public class Api
    {
        private const string API_ROOT = "http://api.openweathermap.org/data/2.5/";

        public static async Task<Location> DownloadWeatherData(string values)
        {
            var weatherDataAsJson = await GetWeatherInfoJsonFromWeb(values);

            if (weatherDataAsJson == null)
                return null;

            // TODO: if succeed: 200 else 404 in "cod"

            try
            {
                // convert data relevant for current location
                var location = JsonConvert.DeserializeObject<Location>(weatherDataAsJson as String);

                // convert data relevant for current weather
                location.TodaysWeatherData = JsonConvert.DeserializeObject<WeatherData>(weatherDataAsJson as String);

                return location;
            }
            catch (JsonReaderException exception)
            {
                Debug.WriteLine(exception.ToString());
                ErrorMessageWhileReadingJson();
                return null;
            }
            catch (JsonSerializationException e)
            {
                Debug.WriteLine(e.ToString());
                ErrorMessageWhileReadingJson();
                return null;
            }
        }

        public static async Task<ObservableCollection<WeatherData>> DownlaodForecastData(string city)
        {
            var weatherDataForecastAsJson = new object();

            weatherDataForecastAsJson = await GetWeatherInfoJsonFromWeb("forecast?q=" + city);

            try
            {
                JObject jsonObjectForWeatherList = JObject.Parse(weatherDataForecastAsJson as String);
                ObservableCollection<WeatherData> forecastList = JsonConvert.DeserializeObject<ObservableCollection<WeatherData>>(jsonObjectForWeatherList["list"].ToString());
                return forecastList;
            }
            catch (JsonReaderException exception)
            {
                Debug.WriteLine(exception.ToString());
                ErrorMessageWhileReadingJson();
                return null;
            }
            catch (JsonSerializationException e)
            {
                Debug.WriteLine(e.ToString());
                ErrorMessageWhileReadingJson();
                return null;
            }
        }

        public static async Task<ObservableCollection<WeatherDataDailyForecast>> DownlaodDailyForecastData(string city)
        {
            var weatherDataForecastAsJson = new object();

            weatherDataForecastAsJson = await GetWeatherInfoJsonFromWeb("forecast/daily?q=" + city + "&cnt=5");

            try
            {
                JObject jsonObjectForDailyWeatherList = JObject.Parse(weatherDataForecastAsJson as String);
                ObservableCollection<WeatherDataDailyForecast> dailyForecastList =
                    JsonConvert.DeserializeObject<ObservableCollection<WeatherDataDailyForecast>>(
                        jsonObjectForDailyWeatherList["list"].ToString());
                return dailyForecastList;
            }
            catch (JsonReaderException exception)
            {
                Debug.WriteLine(exception.ToString());
                ErrorMessageWhileReadingJson();
                return null;
            }
            catch (JsonSerializationException e)
            {
                Debug.WriteLine(e.ToString());
                ErrorMessageWhileReadingJson();
                return null;
            }
        }


        private static async Task<object> GetWeatherInfoJsonFromWeb(string parameter)
        {
            if (App.IsInternetAvailable)
            {
                try
                {
                    var httpClient = new HttpClient();
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/html"));
                    Debug.WriteLine(parameter);
                    HttpResponseMessage response = await httpClient.GetAsync(API_ROOT + parameter);
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

        private static async void ErrorMessageWhileReadingJson()
        {
                var messageDialog =
                    new MessageDialog(
                        "Bei der Auswertung der aktuellen Wetterdaten ist ein Fehler aufegetreten. Ein erneutes Laden könnte dieses Problem beheben. Ansonsten melden Sie sich bitte bei den Entwicklern.",
                        "Verarbeitungsfehler");
                messageDialog.Commands.Add(new UICommand("Ok"));
                await messageDialog.ShowAsync();
        }
    }
}
