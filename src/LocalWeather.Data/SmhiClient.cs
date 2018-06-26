using LocalWeather.Domain;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace LocalWeather.Data
{
    public class SmhiClient
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


        public async Task<Forecast> GetForecastAsync()
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync("https://opendata-download-metfcst.smhi.se/api/category/pmp3g/version/2/geotype/point/lon/12.275/lat/57.7386/data.json");
            string content = await response.Content.ReadAsStringAsync();
            Forecast forecast = JsonConvert.DeserializeObject<Forecast>(content);
            return (forecast);
        }

        public string ConvertWsymb2Async(int wsymb2)
        {
            var weatherCategory = WeatherCategory.GetValueOrDefault(wsymb2);
            return weatherCategory;
        }

        public string ConvertPcatAsync(int pcat)
        {
            var precipitationCategory = PrecipitationCategory.GetValueOrDefault(pcat);
            return precipitationCategory;
        }

        public string ConvertWindDegreeAsync(int degree)
        {
            string cardinal = string.Empty;
            if (degree >= 338 && degree <= 22) { cardinal = "N"; }
            else if (degree >= 23 && degree <= 67) { cardinal = "NE"; }
            else if (degree >= 68 && degree <= 112) { cardinal = "E"; }
            else if (degree >= 113 && degree <= 157) { cardinal = "SE"; }
            else if (degree >= 158 && degree <= 202) { cardinal = "S"; }
            else if (degree >= 203 && degree <= 247) { cardinal = "SW"; }
            else if (degree >= 248 && degree <= 292) { cardinal = "W"; }
            else if (degree >= 293 && degree <= 337) { cardinal = "NW"; }
            return cardinal;
        }

    }
}
