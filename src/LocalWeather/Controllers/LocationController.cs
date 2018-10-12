using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LocalWeather.Business;
using LocalWeather.Data;
using LocalWeather.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LocalWeather.Controllers
{
    public class LocationController : Controller
    {
        [HttpPost]
        public async Task<JsonResult> GetLocation(string textBoxSearch)
        {
            var nominatimClient = new NominatimClient();
            var locationData = new LocationData();
            var response = await nominatimClient.GetCoordinatesAsync(textBoxSearch);

            locationData.SetSwedishLocations(response);
            var locations = locationData.GetSwedishLocations();
            return Json(locations);
        }
    }
}