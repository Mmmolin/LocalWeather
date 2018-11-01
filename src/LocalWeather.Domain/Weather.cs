using System;

namespace LocalWeather.Domain
{
    public class Weather
    {
        public DateTime ValidTime { get; set; }
        public int WeatherCategory { get; set; }
        public decimal Temperature { get; set; }
        public decimal PrecipitationMedian { get; set; }
        public decimal Wind { get; set; }
        public string WindDirection { get; set; }
        //public string PrecipitationCategory { get; set; }
    }
}
