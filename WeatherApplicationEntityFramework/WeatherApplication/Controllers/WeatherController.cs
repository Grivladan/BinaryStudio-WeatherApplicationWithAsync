using System;
using System.Web.Mvc;
using WeatherApplication.Services;
using WeatherApplication.Models;
using System.Data.Entity;
using System.Threading.Tasks;

namespace WeatherApplication.Controllers
{
    public class WeatherController : Controller
    {
        private IWeatherService service;
        WeatherDataContext bd = new WeatherDataContext();
        // GET: Weather
        public WeatherController(IWeatherService _service)
        {
            service = _service;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetWeather(string city,int term)
        {
            var result = service.GetWeatherForecast(city);
            for (int i = 0; i < term; i++)
            {
                ArchieveWeatherData wd = CreateArchieveWeatherData(i, result); 
                bd.weatherDataRecords.Add(wd);
            }
            bd.SaveChanges();
            ViewBag.days = term;
            return View(result);
        }

        private ArchieveWeatherData CreateArchieveWeatherData(int dayNumber, WeatherData.RootObject result)
        {
            var data = DateTime.Now;
            data = data.AddDays(dayNumber);
            int day = result.list[dayNumber].temp.DayInCelsius();
            int night = result.list[dayNumber].temp.NightInCelsius();
            string description = result.list[dayNumber].weather[0].description;
            return new ArchieveWeatherData(result.city.name, data.Date.ToString("dd/MM/yyyy"), day, night, description);
        }


        [HttpPost]
        public async Task<ActionResult> GetWeatherAsync(string city, int term)
        {

            var result = await service.GetWeatherForecastAsync(city);
            for (int i = 0; i < term; i++)
            {
                ArchieveWeatherData wd = CreateArchieveWeatherData(i, result);
                bd.weatherDataRecords.Add(wd);
            }
            await bd.SaveChangesAsync();
            ViewBag.days = term;
            return View("GetWeather", result);
        }

        public async Task<ActionResult> GetWeatherHistory()
        {
            ViewBag.records = await bd.weatherDataRecords.ToListAsync();
            return View();
        }

    }
}