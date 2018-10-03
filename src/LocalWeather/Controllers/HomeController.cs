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
            return View();
        }
    }
}
