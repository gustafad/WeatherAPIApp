using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using WeatherAPIApp.Models;

namespace WeatherAPIApp.Services
{
    // The weather service job is to get the data from the OpenWeatherAPI and convert the data through a DTO so we can use it as an object
    public class WeatherService
    {
        private static Uri baseAdress = new Uri("https://api.openweathermap.org/data/2.5/");
        private static string API_Key = "c3516d45b53e07862e5b2d0d183fbb8c";

        // For ease of use, a Uri generator is used
        public static string GenerateUri(string weatherChoice, double lat, double lon) 
        {
                return $"{baseAdress}{weatherChoice}?lat={lat}&lon={lon}&appid={API_Key}";
        }
        
        // Does the actuall call to the Current weather API, returns the DTO (these two could be combined using parametric types)
        public static async Task<CurrentWeatherDTO> CurrentWeather(double lat, double lon) 
        {
            
            CurrentWeatherDTO cw = null;

            using (var httpClient = new HttpClient {BaseAddress = baseAdress})
            {
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                using (HttpResponseMessage response = await httpClient.GetAsync(GenerateUri("weather", lat, lon)))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string responseData = await response.Content.ReadAsStringAsync();
                        // An issue was that the API sent the amount of precipitation with a number as the first char in the parameter, so it is changed to _(x)h
                        responseData = responseData.Replace("3h", "_3h").Replace("1h", "_1h");
                        cw = JsonConvert.DeserializeObject<CurrentWeatherDTO>(responseData);
                    }
                    // In case the API may be down
                    else
                    {
                        return null;
                    }
                }
            }
            return cw;
        }

        // Does the actuall call to the forecast weather API, returns the DTO
        public async static Task<FiveDaysWeatherForecastDTO> FiveDaysWeatherForecast(double lat, double lon)
        {
            FiveDaysWeatherForecastDTO fw = null;

            using (var httpClient = new HttpClient { BaseAddress = baseAdress })
            {
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                using (HttpResponseMessage response = await httpClient.GetAsync(GenerateUri("forecast", lat, lon)))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string responseData = await response.Content.ReadAsStringAsync();
                        // An issue was that the API sent the amount of precipitation with a number as the first char in the parameter, so it is changed to _(x)h
                        responseData = responseData.Replace("3h", "_3h").Replace("1h", "_1h");
                        fw = JsonConvert.DeserializeObject<FiveDaysWeatherForecastDTO>(responseData);
                    }
                    // In case the API may be down
                    else
                    {
                        return null;
                    }
                }
            }
            return fw;
        }
    }
}