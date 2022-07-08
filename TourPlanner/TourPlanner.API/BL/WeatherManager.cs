using TourPlanner.API.DAL;
using TourPlanner.API.Mapping;

namespace TourPlanner.API.BL
{
    public class WeatherManager : IWeatherManager
    {
        private readonly IWeatherRepository _weatherRepository;
        private readonly ICoordinatesRepository _coordinatesRepository;

        public WeatherManager(IWeatherRepository weatherRepository, ICoordinatesRepository coordinatesRepository)
        {
            _weatherRepository = weatherRepository;
            _coordinatesRepository = coordinatesRepository;
        }

        public async Task<WeatherResult> GetWeatherAsync(Adress from, Adress to)
        {
            var weather = await _weatherRepository.GetWeatherAsync(await _coordinatesRepository.GetCoordinatesAsync(from.City, from.Country));
            var tmp = await _weatherRepository.GetWeatherAsync(await _coordinatesRepository.GetCoordinatesAsync(to.City, to.Country));
            weather.ToCondition = tmp.FromCondition;
            weather.ToTemp = tmp.FromTemp;

            return weather;
        }
    }
}
