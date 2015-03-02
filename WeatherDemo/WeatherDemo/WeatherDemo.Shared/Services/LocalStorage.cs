using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace WeatherDemo.Services
{
    public class LocalStorage
    {
        public static async Task SaveJsonToLocalStorage(string fileName, string json)
        {
            var localStorage = ApplicationData.Current.LocalFolder;
            var file = await localStorage.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);
            await FileIO.WriteTextAsync(file, json);
        }

        public static async Task<string> GetJsonFromLocalStorage(string fileName)
        {
            try
            {
                var localStorage = ApplicationData.Current.LocalFolder;
                var file = await localStorage.GetFileAsync(fileName);
                return await FileIO.ReadTextAsync(file);
            }
            catch (FileNotFoundException)
            {
                return null;
            }
        }

        public static void SaveSetting(string key, object value)
        {
            var localStorage = ApplicationData.Current.LocalSettings;
            if (!localStorage.Values.ContainsKey(key))
            {
                localStorage.Values.Add(key, value);
            }
            else
            {
                localStorage.Values[key] = value;
            }
        }

        public static object GetSetting(string key)
        {
            var localStorage = ApplicationData.Current.LocalSettings;

            if (localStorage.Values.ContainsKey(key))
                return localStorage.Values[key];

            return null;
        }
    }
}
