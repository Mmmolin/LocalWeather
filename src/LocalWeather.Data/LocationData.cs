using LocalWeather.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LocalWeather.Data
{
    public class LocationData
    {
        public async Task<List<Location>> ListLocation(string location)
        {
            NominatimClient client = new NominatimClient();
            var response = await client.GetCoordinatesAsync(location);
            return response;
        }
        
    }
}
