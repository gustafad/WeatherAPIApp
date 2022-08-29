using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeatherAPIApp.Models
{

    public class FiveDaysWeatherForecastDTO
    {
        public string cod { get; set; }
        public int message { get; set; }
        public int cnt { get; set; }
        public InfoList[] list { get; set; }
        public City city { get; set; }
    }
}