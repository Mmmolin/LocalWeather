using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LocalWeather.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LocalWeather.Controllers
{
    public class WeatherController : Controller
    {
        public ActionResult GetWeather(decimal lat, decimal lon)
        {
            var weatherData = new LocalWeather.Data.WeatherData();
            var weatherAsync = weatherData.CreateWeatherForecast(lat, lon);
            var weatherForecast = new WeatherForecast();
            weatherForecast = weatherAsync.Result; // Can you do without this?
            return View(weatherForecast);
        }

        // GET: Weather
        public ActionResult Index()
        {
            return View();
        }

        // GET: Weather/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Weather/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Weather/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Weather/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Weather/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Weather/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Weather/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}