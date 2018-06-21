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
            var smhi = new LocalWeather.Data.SmhiClient();
            var response = smhi.GetForecastAsync();
            Forecast test = response.Result;
            return View(test);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
