using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Net;
using System;
using Newtonsoft.Json;
using HW_20_WebApi_Weather.ViewModels;

namespace HW_20_WebApi_Weather.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        //- Please, use the endpoint api.openweathermap.org for your API calls
        //- Example of API call:
        //api.openweathermap.org/data/2.5/weather?q=London,uk&APPID=44247035819aa4ce1f41441cc42c15b9

        private readonly string _apiKey = "44247035819aa4ce1f41441cc42c15b9";
        private readonly string _baseUrl = "https://api.openweathermap.org/data/2.5/weather";

        [HttpGet]
        public async Task<IActionResult> GetWeather(string city)
        {
            if (string.IsNullOrEmpty(city))
            {
                return BadRequest("City name cannot be empty.");
            }

            using (var client = new HttpClient())
            {
                var url = $"{_baseUrl}?q={city}&appid={_apiKey}&units=metric";

                try
                {
                    var response = await client.GetAsync(url);

                    if (!response.IsSuccessStatusCode)
                    {
                        return BadRequest("Failed to retrieve weather data.");
                    }

                    var content = await response.Content.ReadAsStringAsync();
                    var weatherData = JsonConvert.DeserializeObject<dynamic>(content);

                    // переводим в локальное время 
                    var sunrise = DateTimeOffset.FromUnixTimeSeconds((long)weatherData.sys.sunrise).ToLocalTime().ToString("HH:mm");
                    var sunset = DateTimeOffset.FromUnixTimeSeconds((long)weatherData.sys.sunset).ToLocalTime().ToString("HH:mm");

                    WeatherViewModel weatherInfo = new()
                    {
                        City = weatherData.name,
                        Temperature = weatherData.main.temp,
                        Description = weatherData.weather[0].description,
                        FeelsLike = weatherData.main.feels_like,
                        TempMin = weatherData.main.temp_min,
                        TempMax = weatherData.main.temp_max,
                        Pressure = weatherData.main.pressure,
                        Humidity = weatherData.main.humidity,
                        WindSpeed = weatherData.wind.speed,
                        WindDegree = weatherData.wind.deg,
                        CloudCoverage = weatherData.clouds.all,
                        Visibility = weatherData.visibility,
                        Country = weatherData.sys.country,
                        Latitude = weatherData.coord.lat,
                        Longitude = weatherData.coord.lon,
                        Sunrise = sunrise,
                        Sunset = sunset
                    };

                    return Ok(weatherInfo);

                }
                catch
                {
                    return StatusCode(500, "An error occurred while processing the request.");
                }
            }
        }


    }
}
