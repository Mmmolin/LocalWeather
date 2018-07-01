using System;
using System.Collections.Generic;
using System.Text;

namespace LocalWeather.Domain
{
    public class Weather
    {
        public DateTime ValidTime { get; set; }
        public string WeatherCategory { get; set; }
        public decimal Temperature { get; set; }
        public decimal Wind { get; set; }
        public string WindDirection { get; set; }
        public decimal PrecipitationMedian { get; set; }
        public string PrecipitationCategory { get; set; }
    }
}
