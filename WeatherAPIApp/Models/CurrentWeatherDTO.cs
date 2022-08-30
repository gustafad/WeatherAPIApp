namespace WeatherAPIApp.Models
{
    // The current weather data transfer object (DTO) job is to transfer the data we get from the API upon an object of this class so we can access the data as one
    // Some of these are unused but now the object contain all data if we ever need them
    public class CurrentWeatherDTO
    {
        public Coord coord { get; set; }
        public Weather[] weather { get; set; }
        public string _base { get; set; }
        public Main main { get; set; }
        public int visibility { get; set; }
        public Wind wind { get; set; }
        public Clouds clouds { get; set; }
        public Rain rain { get; set; }
        public Snow snow { get; set; }
        public int dt { get; set; }
        public Sys sys { get; set; }
        public int timezone { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public int cod { get; set; }
    }
}