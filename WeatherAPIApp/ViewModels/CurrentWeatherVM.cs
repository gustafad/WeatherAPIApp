using System;
using System.Linq;
using System.Web;
using WeatherAPIApp.Models;

namespace WeatherAPIApp.ViewModels
{
    // The view model's task is to form a template for all data to be shown so we dont have to repeat it all in the cshtml
    public class CurrentWeatherVM
    {
        private CurrentWeatherDTO _cw;

        public CurrentWeatherVM(CurrentWeatherDTO model)
        {
            _cw = model;
        }
        // Returns the weather icon and the description
        public HtmlString Weather 
        {
            get
            {
                // The weather object is technically an array of weathers, but it should never be more than one object in the array, but just in case, we throw an exception, cause then something weird is up
                if (_cw.weather.Length > 1)
                {
                    throw new Exception("Weather list has more than 1 item");
                }
                Weather weather = _cw.weather.First();
                return new HtmlString("<img src=\"http://openweathermap.org/img/wn/" + weather.icon + "@2x.png\"></img> <br />" + weather.description);
            }
               
        }
        // Returns the current tempature
        public string Temp
        {
            get
            {
                return "Tempature: " + (_cw.main.temp - 273.15).ToString("N1") +
                       " Feels like: " + (_cw.main.feels_like - 273.15).ToString("N1"); //N1 is 1 decimals
            }

        }
        // Returns the wind speed and direction
        public HtmlString Wind
        {
            get
            {
                return new HtmlString("Wind speed: " + (_cw.wind.speed) + " m/s" + "<br />" + " Wind direction: " + (_cw.wind.deg) + " deg");
            }
        }
        //Returns how much it rains
        public HtmlString Precipitation 
        { get
            {
                if (_cw.rain != null)
                {
                    if (_cw.rain._3h != 0)
                    {
                        return new HtmlString($"Rain: {_cw.rain._1h} mm last hour <br /> {_cw.rain._3h} mm last 3 hours");
                    }
                }

                if (_cw.snow != null)
                {
                    if (_cw.snow._3h != 0)
                    {
                        return new HtmlString($"Snow: {_cw.snow._1h} mm last hour <br /> {_cw.snow._3h} mm last 3 hours");
                    }
                }

                //It occured that if rain or snow was of too small numbers, the rain or snow properties would be null, even if it technically would be light rain.
                if (_cw.weather.First().description.Equals("light rain"))
                {
                    return new HtmlString("Percipication: Less than 0.1 mm");
                }
                else
                {
                    return new HtmlString("No precipitation!");
                }
            }
        }
        // Returns the time the data was messured
        public string TimeOfDataMeassured
        {
            get
            {
                return "Last update: "+(DateTimeOffset.FromUnixTimeSeconds(_cw.dt).TimeOfDay.ToString());
            }
        }


    }
}