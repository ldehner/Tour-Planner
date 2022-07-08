using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System.Dynamic;
using TourPlanner.API.Mapping;

namespace TourPlanner.API.DAL
{
    public class CoordinatesRepository : ICoordinatesRepository
    {
        private readonly string _key;

        public CoordinatesRepository(string key)
        {
            _key = key;
        }

        public async Task<Coordinates> GetCoordinatesAsync(string city, string country)
        {
            using var HttpClient = new HttpClient();
            var result = await HttpClient.GetAsync("https://api.openweathermap.org/geo/1.0/direct?q=" + city + "," + country + "&limit=1&appid=" + _key);
            var content = await result.Content.ReadAsStringAsync();
            HttpClient.Dispose();
            dynamic json = JArray.Parse(content);
            return new Coordinates { Lat = json[0].lat, Long = json[0].lon };
        }
    }
}
