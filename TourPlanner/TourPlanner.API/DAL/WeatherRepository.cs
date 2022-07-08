using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System.Dynamic;
using TourPlanner.API.Mapping;

namespace TourPlanner.API.DAL
{
    public class WeatherRepository : IWeatherRepository
    {
        private readonly string _key;

        public WeatherRepository(string key)
        {
            _key = key;
        }

        public async Task<WeatherResult> GetWeatherAsync(Coordinates coordinates)
        {
            using var HttpClient = new HttpClient();
            var result = await HttpClient.GetAsync("https://api.openweathermap.org/data/2.5/weather?lat="+coordinates.Lat+"&lon="+coordinates.Long+"&appid=" + _key);
            var content = await result.Content.ReadAsStringAsync();
            HttpClient.Dispose();
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            dynamic json = JsonConvert.DeserializeObject<ExpandoObject>(content, new ExpandoObjectConverter()); ;
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            return new WeatherResult { FromCondition = json.weather[0].description, FromTemp = (Math.Round(json.main.temp - 273.15, 1)).ToString() };
        }
    }
}
