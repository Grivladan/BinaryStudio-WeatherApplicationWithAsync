using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace WeatherApplication.Models
{
    public class WeatherDataContext:DbContext
    {
        public WeatherDataContext()
            :base("DbConnection") { }
          
        public DbSet<ArchieveWeatherData> weatherDataRecords { get; set; }
    }
}