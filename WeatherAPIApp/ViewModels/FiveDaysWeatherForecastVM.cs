using System;
using System.Linq;
using System.Web;
using WeatherAPIApp.Models;

namespace WeatherAPIApp.ViewModels
{
    // The view model's task is to form a template for all data to be shown so we dont have to repeat it all in the cshtml
    public class FiveDaysWeatherForecastVM
    {
        private FiveDaysWeatherForecastDTO _fw;

        public FiveDaysWeatherForecastVM(FiveDaysWeatherForecastDTO model)
        {
            _fw = model;
        }

        // Done a little bit different than CurrentWeather as we are handling with an array of forecasts
        // Returns the Array of forecasts
        public InfoList[] GetInfoList { get { return _fw.list; } }
        // Returns the weather icon and the description
        public HtmlString Weather(InfoList fwItem)
        {
            // The weather object is technically an array of weathers, but it should never be more than one object in the array, but just in case, we throw an exception, cause then something weird is up
            if (fwItem.weather.Length > 1)
            {
                throw new Exception("Weather list has more than 1 item");
            }

            Weather weather = fwItem.weather.First();
            return new HtmlString("<img src=\"http://openweathermap.org/img/wn/"+weather.icon+"@2x.png\"></img> <br />" + weather.description);         
        }
        // Returns the current tempature
        public string Temp(InfoList fwItem)
        {
            //Only display min-max temp if the day is today as min-max is same as temp otherwise
            string day = fwItem.dt_txt.Substring(8, 2);
            if (day == DateTime.Today.Day.ToString())
            {
                return "Tempature: Max = " + (fwItem.main.temp_max - 273.15).ToString("N1") +
                   " Min = " + (fwItem.main.temp_min - 273.15).ToString("N1"); //N1 is 1 decimals
            }
            else
            {
                return "Tempature: " + (fwItem.main.temp - 273.15).ToString("N1");
            }
            
        }
        // Returns the wind speed and direction
        public HtmlString Wind(InfoList fwItem)
        {
            return new HtmlString("Wind speed: " + (fwItem.wind.speed) + " m/s" + "<br />" + " Wind direction: " + (fwItem.wind.deg) + " deg");
        }
        //Returns how much it rains
        public HtmlString Precipitation(InfoList fwItem)
        {
            if (fwItem.rain != null)
            {
                if (fwItem.rain._3h != 0)
                {
                    return new HtmlString($"Rain: {fwItem.rain._3h} mm / 3 hours");
                }
            }

            if (fwItem.snow != null)
            {
                if (fwItem.snow._3h != 0)
                {
                    return new HtmlString($"Snow: {fwItem.snow._3h} mm / 3 hours");
                }
            }

            //On rare occations, if rain or snow was of too small numbers, the rain or snow properties would be null, even if it technically would be light rain.
            if (fwItem.weather.First().description.Equals("light rain"))
            {
                return new HtmlString("Percipication: Less than 0.1 mm");
            }
            else
            {
                return new HtmlString("No precipitation!");
            }
        }
        // Returns the time the forecast references to
        public string TimeOfDataForecasted(InfoList fwItem)
        {
            return fwItem.dt_txt;
        }


    }
}