using LocalWeather.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LocalWeather.Controllers
{
    public class WeatherController : Controller
    {
        public JsonResult GetForecast(decimal lat, decimal lon)
        {
            var smhiClient = new LocalWeather.Data.SmhiClient();
            var weatherData = new LocalWeather.Data.WeatherData();
            var forecastAsync = weatherData.CreateWeatherForecast(smhiClient, lat, lon);
            var weatherForecast = new WeatherForecast();
            weatherForecast = forecastAsync.Result; // Can you do without this?
            return Json(weatherForecast);
        }
    }
}