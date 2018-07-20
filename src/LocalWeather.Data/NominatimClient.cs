using LocalWeather.Domain;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LocalWeather.Data
{
    public class NominatimClient
    {
        public async Task<Coordinates> GetCoordinatesAsync()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("User-Agent", "LocalWeatherApp");
            HttpResponseMessage response = await client.GetAsync("https://nominatim.openstreetmap.org/search?q=öxeryd&format=json");
            var content = response.Content.ReadAsStringAsync();
            return null;
        }
    }
}
