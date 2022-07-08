using TourPlanner.API.Mapping;

namespace TourPlanner.API.DAL
{
    public interface IWeatherRepository
    {
        public Task<WeatherResult> GetWeatherAsync(Coordinates coordinates);
    }
}
