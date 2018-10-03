using LocalWeather.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LocalWeather.Controllers
{
    public class WeatherController : Controller
    {
        public ActionResult GetWeather(decimal lat, decimal lon)
        {
            var smhiClient = new LocalWeather.Data.SmhiClient();
            var weatherData = new LocalWeather.Data.WeatherData();
            var weatherAsync = weatherData.CreateWeatherForecast(smhiClient, lat, lon);
            var weatherForecast = new WeatherForecast();
            weatherForecast = weatherAsync.Result; // Can you do without this?
            return View(weatherForecast);
        }
    }
}