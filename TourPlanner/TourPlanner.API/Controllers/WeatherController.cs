using Microsoft.AspNetCore.Mvc;
using TourPlanner.API.BL;
using TourPlanner.API.Mapping;

namespace TourPlanner.API.Controllers
{
    /// <summary>
    /// API Controller for the weather
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        private readonly IWeatherManager _weatherManager;
        private readonly ILogger<WeatherController> _logger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="weatherManager"></param>
        public WeatherController(ILogger<WeatherController> logger, IWeatherManager weatherManager)
        {
            _logger = logger;
            _weatherManager = weatherManager;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="start"></param>
        /// <param name="destination"></param>
        /// <returns></returns>
        [HttpGet("GetWeather/{start}/{destination}")]
        public async Task<ActionResult<WeatherResult>> GetWeather(string start, string destination)
        {
            _logger.LogInformation("API Request - Get weather");
            return Ok(await _weatherManager.GetWeatherAsync(start, destination));
        }
    }
}
