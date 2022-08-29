using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WeatherAPIApp.Models;

namespace WeatherAPIApp.ViewModels
{
    public class FiveDaysWeatherForecastVM
    {
        private FiveDaysWeatherForecastDTO _fw;

        public FiveDaysWeatherForecastVM(FiveDaysWeatherForecastDTO model)
        {
            _fw = model;
        }

        public InfoList[] GetInfoList { get { return _fw.list; } }

        public HtmlString Weather(InfoList fwItem)
        {
            if (fwItem.weather.Length > 1)
            {
                throw new Exception("Weather list has more than 1 item");
            }

            Weather weather = fwItem.weather.First();
            return new HtmlString("<img src=\"http://openweathermap.org/img/wn/"+weather.icon+"@2x.png\"></img> <br />" + weather.description);         
        }
        public string Temp(InfoList fwItem)
        {
            return "Tempature: Max = " + (fwItem.main.temp_max - 273.15).ToString("N1") +
                   " Min = " + (fwItem.main.temp_min - 273.15).ToString("N1"); //N1 is 1 decimals
        }
        public HtmlString Wind(InfoList fwItem)
        {
            return new HtmlString("Wind speed: " + (fwItem.wind.speed) + " m/s" + "<br />" + " Wind direction: " + (fwItem.wind.deg) + " deg");
        }
        public HtmlString Precipitation(InfoList fwItem)
        {
            if (fwItem.rain != null)
            {
                if (fwItem.rain._3h != 0)
                {
                    return new HtmlString($"Rain: {fwItem.rain._1h} mm last hour <br /> {fwItem.rain._3h} mm last 3 hours");
                }
                else
                {
                    return new HtmlString($"Rain: {fwItem.rain._1h} mm last hour");
                }
            }

            if (fwItem.snow != null)
            {
                if (fwItem.snow._3h != 0)
                {
                    return new HtmlString($"Snow: {fwItem.snow._1h} mm last hour <br /> {fwItem.snow._3h} mm last 3 hours");
                }
                else
                {
                    return new HtmlString($"Snow: {fwItem.snow._1h} mm last hour");
                }
            }

            //It occured that if rain or snow was of too small numbers, the rain or snow properties would be null, even if it technically would be light rain.
            if (fwItem.weather.First().description.Equals("light rain"))
            {
                return new HtmlString("Percipication: Less than 0.1 mm");
            }
            else
            {
                return new HtmlString("No precipitation!");
            }

            
        }
        public string TimeOfDataForecasted(InfoList fwItem)
        {
            return fwItem.dt_txt;
        }


    }
}