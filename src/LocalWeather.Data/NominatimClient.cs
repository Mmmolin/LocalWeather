using LocalWeather.Domain;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LocalWeather.Data
{
    public class NominatimClient
    {
        public async Task<List<Location>> GetCoordinatesAsync(string place)
        {
            var searchAddress = "https://nominatim.openstreetmap.org/search?q=" + place + "&format=json";
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("User-Agent", "LocalWeatherApp");
            HttpResponseMessage response = await client.GetAsync(searchAddress);
            var content = await response.Content.ReadAsStringAsync();
            List<Location> locations = JsonConvert.DeserializeObject<List<Location>>(content);
            return locations;
        }
    }
}
