using TourPlanner.API.Mapping;

namespace TourPlanner.API.BL
{
    public interface IWeatherManager
    {
        public Task<WeatherResult> GetWeatherAsync(Adress from, Adress to);
    }
}
