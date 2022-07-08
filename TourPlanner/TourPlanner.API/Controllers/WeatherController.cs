using Microsoft.AspNetCore.Mvc;
using TourPlanner.API.BL;
using TourPlanner.API.Mapping;

namespace TourPlanner.API.Controllers
{
    /// <summary>
    /// API Controller for the tour planner
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        private readonly IWeatherManager _weatherManager;
        private readonly ILogger<WeatherController> _logger;

        /// <summary>
        /// Tour Planner Controller Constructor
        /// </summary>
        /// <param name="logger">Log4Net logger</param>
        /// <param name="tourManager">the tour manager</param>
        /// <param name="tourLogManager">the log manager</param>
        /// <param name="mapQuestManager">the mapquest manager</param>
        public WeatherController(ILogger<WeatherController> logger, IWeatherManager weatherManager)
        {
            _logger = logger;
            _weatherManager = weatherManager;
        }

        [HttpGet("GetWeather/{start}/{destination}")]
        public async Task<ActionResult<WeatherResult>> GetWeather(string start, string destination)
        {
            _logger.LogInformation("API Request - Get weather");
            return Ok(await _weatherManager.GetWeatherAsync(start, destination));
        }
    }
}
