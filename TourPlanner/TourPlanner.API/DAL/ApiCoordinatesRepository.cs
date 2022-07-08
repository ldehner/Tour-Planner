using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System.Dynamic;
using TourPlanner.API.Exceptions;
using TourPlanner.API.Mapping;

namespace TourPlanner.API.DAL
{
    public class ApiCoordinatesRepository : ICoordinatesRepository
    {
        private readonly string _key;

        public ApiCoordinatesRepository(string key)
        {
            _key = key;
        }

        public async Task<Coordinates> GetCoordinatesAsync(string adress)
        {
            using var HttpClient = new HttpClient();
            var result = await HttpClient.GetAsync("https://api.openweathermap.org/geo/1.0/direct?q=" + adress + "&limit=1&appid=" + _key);
            var content = await result.Content.ReadAsStringAsync();
            HttpClient.Dispose();
            dynamic json = JArray.Parse(content);
            if (json.Count <= 0) throw new InvalidAdressException();
            return new Coordinates(json[0].lat.ToString(), json[0].lon.ToString());
        }
    }
}
