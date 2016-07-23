using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeatherApplication.Models
{
    public class ArchieveWeatherData
    {
        public int Id { get; set; }
        public string City { get; set; }
        public string Data { get; set; }
        public int Day { get; set; }
        public int Night { get; set; }
        public string Description { get; set; }

        public ArchieveWeatherData() { }
        public ArchieveWeatherData(string city,string data, int day,int night,string description)
        {
            City=city;
            Data = data;
            Day = day;
            Night = night;
            Description = description;
        }
    }

}