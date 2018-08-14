using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LocalWeather.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LocalWeather.Controllers
{
    public class LocationController : Controller
    {
        [HttpPost]
        public async Task<ActionResult> GetLocation(string textBoxSearch)
        {            
            var locationData = new LocalWeather.Data.LocationData();
            var locations = await locationData.ListLocation(textBoxSearch);                 
            return View(locations);
        }        
    }
}