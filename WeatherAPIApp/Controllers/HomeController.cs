using System.Threading.Tasks;
using System.Web.Mvc;
using WeatherAPIApp.Services;
using WeatherAPIApp.ViewModels;

namespace WeatherAPIApp.Controllers
{
    // The MVC controller, simply launches the views and initializes the viewbags containing the view models
    public class HomeController : Controller
    {
        // Index page (home page) has the actual data
        public async Task<ActionResult> Index(double lat = 57.688988, double lon = 11.974486)
        {
            if (lat == 57.688988 && lon == 11.974486)
            {
                ViewBag.LocationText = "Weather for default location is Chalmers. Location can be changed by appending: /?lat={Latitude}&lon={Longitude} to the URL";
            }
            else
            {
                ViewBag.LocationText = "Weather for location: Lat = "+ lat.ToString() + " Lon = " + lon.ToString();
            }
            ViewBag.CurrentWeather = new CurrentWeatherVM(await WeatherService.CurrentWeather(lat, lon));
            ViewBag.FiveDaysWeatherForecast = new FiveDaysWeatherForecastVM(await WeatherService.FiveDaysWeatherForecast(lat, lon));

            // if no weather data was recived from the API
            if (ViewBag.CurrentWeather == null || ViewBag.FiveDaysWeatherForecast == null)
            {
                return View("NoData");
            }
            return View();
        }
        // About page just has information about the developer (me :D) and a description
        public ActionResult About()
        {
            return View();
        }


    }
}