namespace WeatherAPIApp.Models
{
    // The current weather data transfer object (DTO) job is to transfer the data we get from the API upon an object of this class so we can access the data as one
    // Some of these are unused but now the object contain all data if we ever need them
    public class FiveDaysWeatherForecastDTO
    {
        public string cod { get; set; }
        public int message { get; set; }
        public int cnt { get; set; }
        public InfoList[] list { get; set; }
        public City city { get; set; }
    }
}