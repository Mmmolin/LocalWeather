using LocalWeather.Domain;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;

namespace LocalWeather.Data
{
    public class WeatherData
    {
        static Dictionary<int, string> WeatherCategory = new Dictionary<int, string>()
        {
            {1, "Clear sky" },{2, "Nearly clear sky" },{3, "Variable cloudiness" },{4, "Halfclear sky" },{5, "Cloudy sky" },
            {6, "Overcast" },{7, "Fog" },{8, "Light rain showers" },{9, "Moderate rain showers" },{10, "Heavy rain showers" },
            {11, "Thunderstorm" },{12, "Light sleet showers" },{13, "Moderate sleet showers" },{14, "Heavy sleet showers" },
            {15, "Light snow showers" },{16, "Moderate snow showers" },{17, "Heavy snow showers" },{18, "Light rain" },
            {19, "Moderate rain" },{20, "Heavy rain" },{21, "Thunder" },{22, "Light sleet" },{23, "Moderate sleet" },
            {24, "Heavy sleet" },{25, "Light snowfall" },{26, "Moderate snowfall" },{27, "Heavy snowfall" }
        };

        static Dictionary<int, string> PrecipitationCategory = new Dictionary<int, string>()
        {
            {0, "No precipitation"},{1, "Snow" },{2, "Snow and rain" },{3, "Rain" },{4, "Drizzle" },{5, "Freezing rain" },{6, "Freezing drizzle" }
        };

        public async Task<Weather> CreateWeatherData()
        {
            SmhiClient client = new SmhiClient();
            Weather weather = new Weather();
            var response = await client.GetForecastAsync();
            var forecast = response;

            var validTimeUtc = forecast.TimeSeries[0].ValidTime;
            var wsymb2 = Decimal.ToInt32(forecast.TimeSeries[0].Parameters[18].Values[0]);
            var windDegree = (int)forecast.TimeSeries[0].Parameters[3].Values[0];
            var precipitationCategory = Decimal.ToInt32(forecast.TimeSeries[0].Parameters[15].Values[0]);

            string cardinal = string.Empty;
            if (windDegree >= 338 && windDegree <= 22) { cardinal = "N"; }
            else if (windDegree >= 23 && windDegree <= 67) { cardinal = "NE"; }
            else if (windDegree >= 68 && windDegree <= 112) { cardinal = "E"; }
            else if (windDegree >= 113 && windDegree <= 157) { cardinal = "SE"; }
            else if (windDegree >= 158 && windDegree <= 202) { cardinal = "S"; }
            else if (windDegree >= 203 && windDegree <= 247) { cardinal = "SW"; }
            else if (windDegree >= 248 && windDegree <= 292) { cardinal = "W"; }
            else if (windDegree >= 293 && windDegree <= 337) { cardinal = "NW"; }

            CultureInfo en = new CultureInfo("en-US");
            var month = DateTime.Now.ToString("MMMM", en);            

            weather.ValidTime = validTimeUtc.AddHours(1);
            weather.WeatherCategory = WeatherCategory.GetValueOrDefault(wsymb2);
            weather.Temperature = forecast.TimeSeries[0].Parameters[1].Values[0];
            weather.Wind = forecast.TimeSeries[0].Parameters[4].Values[0];
            weather.WindDirection = cardinal;
            weather.PrecipitationMedian = forecast.TimeSeries[0].Parameters[17].Values[0];
            weather.PrecipitationCategory = PrecipitationCategory.GetValueOrDefault(precipitationCategory);

            return weather;
        }
        
    }
}
