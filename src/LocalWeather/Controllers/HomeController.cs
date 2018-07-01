using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LocalWeather.Models;
using System.Net.Http;
using Newtonsoft.Json;
using LocalWeather.Domain;

namespace LocalWeather.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var weatherData = new LocalWeather.Data.WeatherData();
            var weatherAsync = weatherData.CreateWeatherData();
            var weather = weatherAsync.Result;
            return View(weather);
        }        

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
