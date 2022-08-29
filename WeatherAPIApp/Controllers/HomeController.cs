using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WeatherAPIApp.Models;
using WeatherAPIApp.Services;
using WeatherAPIApp.ViewModels;

namespace WeatherAPIApp.Controllers
{
    public class HomeController : Controller
    {
        public async Task<ActionResult> Index()
        {
            ViewBag.CurrentWeather = new CurrentWeatherVM(await WeatherService.CurrentWeather());
            ViewBag.FiveDaysWeatherForecast = new FiveDaysWeatherForecastVM(await WeatherService.FiveDaysWeatherForecast());
            return View();
        }

        public ActionResult About()
        {
           

            return View();
        }


    }
}