using LocalWeather.Data;
using LocalWeather.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<WeatherForecast> CreateWeatherForecast(SmhiClient client, decimal lat, decimal lon)
        {
            //--- This has to be handled better, maybe a method
            var latitude = Math.Round(lat, 3).ToString().Replace(",", ".");
            var longitude = Math.Round(lon, 4).ToString().Replace(",", ".");
            //---
            var response = await client.GetForecastAsync(latitude, longitude);
            var forecast = response;

            WeatherForecast weatherForecast = new WeatherForecast();
            foreach (var index in forecast.TimeSeries)
            {
                var validTime = index.ValidTime.ToLocalTime();
                var weatherCategory = GetWeatherCategory(index);
                var temperature = GetTemperature(index);
                var wind = GetWind(index);
                var windDegree = GetWindDegree(index);
                var precipitationMedian = GetPrecipitationMedian(index);
                var precipitationCategory = GetPrecipitationCategory(index);

                Weather weather = new Weather()
                {
                    ValidTime = validTime,
                    WeatherCategory = weatherCategory,
                    Temperature = temperature,
                    Wind = wind,
                    WindDirection = GetCardinalDirection(windDegree),
                    PrecipitationMedian = precipitationMedian,
                    PrecipitationCategory = precipitationCategory
                };

                weatherForecast.Forecast.Add(weather);
            }
            return weatherForecast;
        }
        private decimal GetTemperature(TimeSerie index)
        {
            var temperature = index.Parameters.Where(p => p.Name == "t")
                .Select(t => t.Values[0]).FirstOrDefault();
            return temperature;
        }

        private decimal GetWind(TimeSerie index)
        {
            var wind = index.Parameters.Where(p => p.Name == "ws")
                .Select(ws => ws.Values[0]).FirstOrDefault();
            return wind;
        }

        private int GetWindDegree(TimeSerie index)
        {
            var windDegree = (int)index.Parameters.Where(p => p.Name == "wd")
                .Select(wd => wd.Values[0]).FirstOrDefault();
            return windDegree;
        }

        private decimal GetPrecipitationMedian(TimeSerie index)
        {
            var precipitationMedian = index.Parameters.Where(p => p.Name == "pmedian")
                .Select(pm => pm.Values[0]).FirstOrDefault();
            return precipitationMedian;
        }

    private string GetCardinalDirection(int degree)
        {
            string cardinal = string.Empty;
            if (degree >= 338 && degree <= 360) { cardinal = "N"; }
            else if (degree >= 0 && degree <= 22) { cardinal = "N"; }
            else if (degree >= 23 && degree <= 67) { cardinal = "NE"; }
            else if (degree >= 68 && degree <= 112) { cardinal = "E"; }
            else if (degree >= 113 && degree <= 157) { cardinal = "SE"; }
            else if (degree >= 158 && degree <= 202) { cardinal = "S"; }
            else if (degree >= 203 && degree <= 247) { cardinal = "SW"; }
            else if (degree >= 248 && degree <= 292) { cardinal = "W"; }
            else if (degree >= 293 && degree <= 337) { cardinal = "NW"; }
            return cardinal;
        }

        private string GetWeatherCategory(TimeSerie index)
        {
            var wsymb2 = Decimal.ToInt32(index.Parameters
                    .Where(p => p.Name == "Wsymb2").Select(ws => ws.Values[0]).FirstOrDefault());
            var weatherCategory = WeatherCategory.GetValueOrDefault(wsymb2);
            return weatherCategory;
        }

        private string GetPrecipitationCategory(TimeSerie index)
        {
            var pcat = (int)index.Parameters
                    .Where(p => p.Name == "pcat").Select(pc => pc.Values[0]).FirstOrDefault();
            var precipitationCategory = PrecipitationCategory.GetValueOrDefault(pcat);
            return precipitationCategory;
        }
    }
}
