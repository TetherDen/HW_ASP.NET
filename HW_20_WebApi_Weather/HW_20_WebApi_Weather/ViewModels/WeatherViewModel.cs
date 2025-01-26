namespace HW_20_WebApi_Weather.ViewModels
{
    public class WeatherViewModel
    {
        public string City { get; set; }
        public double Temperature { get; set; }
        public string Description { get; set; }
        public double FeelsLike { get; set; }
        public double TempMin { get; set; }
        public double TempMax { get; set; }
        public int Pressure { get; set; }
        public int Humidity { get; set; }
        public double WindSpeed { get; set; }
        public int WindDegree { get; set; }
        public int CloudCoverage { get; set; }
        public double Visibility { get; set; }
        public string Country { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Sunrise { get; set; }
        public string Sunset { get; set; }
    }
}
