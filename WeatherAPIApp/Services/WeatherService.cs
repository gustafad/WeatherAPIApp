using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using WeatherAPIApp.Models;

namespace WeatherAPIApp.Services
{
    public class WeatherService
    {
        private static Uri baseAdress = new Uri("https://api.openweathermap.org/data/2.5/");
        //private double lat = GetLocation.lat; //57.689099
        //private double lon = GetLocation.lon; //11.974308
        private static string API_Key = "c3516d45b53e07862e5b2d0d183fbb8c";


        public static string GenerateUri(string weatherChoice) //name to be improved, add lat lon
        {
            GetLocation.GetLocationProp();
            double lat = GetLocation.lat;
            double lon = GetLocation.lon;
                return $"{baseAdress}{weatherChoice}?lat={lat}&lon={lon}&appid={API_Key}";
        }
        
        public static async Task<CurrentWeatherDTO> CurrentWeather() //allow set lat and lon
        {
            
            CurrentWeatherDTO cw = null;

            using (var httpClient = new HttpClient {BaseAddress = baseAdress})
            {
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                using (HttpResponseMessage response = await httpClient.GetAsync(GenerateUri("weather")))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string responseData = await response.Content.ReadAsStringAsync();
                        responseData = responseData.Replace("3h", "_3h").Replace("1h", "_1h");
                        cw = JsonConvert.DeserializeObject<CurrentWeatherDTO>(responseData);
                    }
                }
            }
            return cw;
        }
        public async static Task<FiveDaysWeatherForecastDTO> FiveDaysWeatherForecast()
        {
            FiveDaysWeatherForecastDTO fw = null;

            using (var httpClient = new HttpClient { BaseAddress = baseAdress })
            {
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                using (HttpResponseMessage response = await httpClient.GetAsync(GenerateUri("forecast")))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string responseData = await response.Content.ReadAsStringAsync();
                        responseData = responseData.Replace("3h", "_3h").Replace("1h", "_1h");
                        fw = JsonConvert.DeserializeObject<FiveDaysWeatherForecastDTO>(responseData);
                    }
                }
            }
            return fw;
        }
    }
}