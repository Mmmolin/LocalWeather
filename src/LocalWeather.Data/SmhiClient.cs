using LocalWeather.Domain;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace LocalWeather.Data
{
    public class SmhiClient
    {
        public async Task<Forecast> GetForecastAsync()
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync("https://opendata-download-metfcst.smhi.se/api/category/pmp3g/version/2/geotype/point/lon/12.275/lat/57.7386/data.json");
            string content = await response.Content.ReadAsStringAsync();
            Forecast forecast = JsonConvert.DeserializeObject<Forecast>(content);
            return (forecast);
        }
    }
}
