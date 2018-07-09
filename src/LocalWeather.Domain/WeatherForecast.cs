using System;
using System.Collections.Generic;
using System.Text;

namespace LocalWeather.Domain
{
    public class WeatherForecast
    {
        public List<Weather> Forecast { get; set; }
        public WeatherForecast()
        {
            Forecast = new List<Weather>();
        }
    }
}
