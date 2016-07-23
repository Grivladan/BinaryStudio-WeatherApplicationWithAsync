using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Script.Serialization;
using System.Net;
using WeatherApplication.Models;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Net.Http;

namespace WeatherApplication.Services
{
    public interface IWeatherService
    {
        WeatherData.RootObject GetWeatherForecast(string city);
        Task<WeatherData.RootObject> GetWeatherForecastAsync(string city);
    }

    public class WeatherService : IWeatherService
    {

        public WeatherData.RootObject GetWeatherForecast(string city)
        {
            WebClient wc = new WebClient();
            string url = "http://api.openweathermap.org/data/2.5/forecast/daily?q=" + city + "&mode=json&APPID=885b91841cbfc4e0644d236d80d78621";
            var json = wc.DownloadString(url);
            var obj = JsonConvert.DeserializeObject<WeatherData.RootObject>(json);
            return obj;
        }

        public async Task<WeatherData.RootObject> GetWeatherForecastAsync(string city)
        {
            string url = "http://api.openweathermap.org/data/2.5/forecast/daily?q=" + city + "&mode=json&APPID=885b91841cbfc4e0644d236d80d78621";
            using (HttpClient httpClient = new HttpClient())
            {
                return JsonConvert.DeserializeObject<WeatherData.RootObject>
                (
                    await httpClient.GetStringAsync(url)
                );
            }
        }
    }
}