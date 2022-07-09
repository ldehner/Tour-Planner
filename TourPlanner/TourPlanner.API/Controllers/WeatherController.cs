using Microsoft.AspNetCore.Mvc;
using TourPlanner.API.BL;
using TourPlanner.API.Exceptions;
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
        /// Sets Loggger and Manager
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="weatherManager"></param>
        public WeatherController(ILogger<WeatherController> logger, IWeatherManager weatherManager)
        {
            _logger = logger;
            _weatherManager = weatherManager;
        }

        /// <summary>
        /// Gets the weather of start city and destination city
        /// </summary>
        /// <param name="start">City, Country</param>
        /// <param name="destination">City, Country</param>
        /// <response code="200">Success - Weather returned</response>
        /// <response code="404">Invalid Adress</response>
        /// <response code="500">Oops! Problem on our end</response>
        /// <returns>The weather</returns>
        [HttpGet("GetWeather/{start}/{destination}")]
        public async Task<ActionResult<WeatherResult>> GetWeather(string start, string destination)
        {
            _logger.LogInformation("API Request - Get weather");
            try
            {
                var result = await _weatherManager.GetWeatherAsync(start, destination);
                return Ok(result);
            }
            catch (InvalidAdressException)
            {
                _logger.LogError("Coordinates of start or/and destination adress could not be found");
                return NotFound("Coordinates of start or/and destination adress could not be found");
            }
            
        }
    }
}
