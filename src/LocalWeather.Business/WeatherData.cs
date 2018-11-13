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

                Weather weather = new Weather()
                {
                    ValidTime = validTime,
                    WeatherCategory = weatherCategory,
                    Temperature = temperature,
                    Wind = wind,
                    WindDirection = GetCardinalDirection(windDegree),
                    PrecipitationMedian = precipitationMedian
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

        private int GetWeatherCategory(TimeSerie index)
        {
            var wsymb2 = Decimal.ToInt32(index.Parameters
                    .Where(p => p.Name == "Wsymb2").Select(ws => ws.Values[0]).FirstOrDefault());
            return wsymb2;
        }
    }
}
