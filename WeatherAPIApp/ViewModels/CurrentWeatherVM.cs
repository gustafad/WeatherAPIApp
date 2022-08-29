using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WeatherAPIApp.Models;

namespace WeatherAPIApp.ViewModels
{
    public class CurrentWeatherVM
    {
        private CurrentWeatherDTO _cw;

        public CurrentWeatherVM(CurrentWeatherDTO model)
        {
            _cw = model;
        }
        public HtmlString Weather 
        {
            get
            {
                if (_cw.weather.Length > 1)
                {
                    throw new Exception("Weather list has more than 1 item");
                }
                Weather weather = _cw.weather.First();
                return new HtmlString("<img src=\"http://openweathermap.org/img/wn/" + weather.icon + "@2x.png\"></img> <br />" + weather.description);
            }
               
        }
        public string Temp
        {
            get
            {
                return "Tempature: " + (_cw.main.temp - 273.15).ToString("N1") +
                       " Feels like: " + (_cw.main.feels_like - 273.15).ToString("N1"); //N1 is 1 decimals
            }

        }
        public HtmlString Wind
        {
            get
            {
                return new HtmlString("Wind speed: " + (_cw.wind.speed) + " m/s" + "<br />" + " Wind direction: " + (_cw.wind.deg) + " deg");
            }
        }
        public HtmlString Precipitation 
        { get
            {
                if (_cw.rain != null)
                {
                    if (_cw.rain._3h != 0)
                    {
                        return new HtmlString($"Rain: {_cw.rain._1h} mm last hour <br /> {_cw.rain._3h} mm last 3 hours");
                    }
                    else
                    {
                        return new HtmlString($"Rain: {_cw.rain._1h} mm last hour");
                    }
                }

                if (_cw.snow != null)
                {
                    if (_cw.snow._3h != 0)
                    {
                        return new HtmlString($"Snow: {_cw.snow._1h} mm last hour <br /> {_cw.snow._3h} mm last 3 hours");
                    }
                    else
                    {
                        return new HtmlString($"Snow: {_cw.snow._1h} mm last hour");
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
        public string TimeOfDataForecasted
        {
            get
            {
                return "Last update: "+(DateTimeOffset.FromUnixTimeSeconds(_cw.dt).TimeOfDay.ToString());
            }
        }


    }
}