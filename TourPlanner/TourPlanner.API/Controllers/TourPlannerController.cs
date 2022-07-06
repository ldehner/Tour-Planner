using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;
using System.Security.Cryptography.Xml;
using TourPlanner.API.BL;
using TourPlanner.API.DAL;
using TourPlanner.API.Data;
using TourPlanner.API.Exceptions;
using TourPlanner.API.Mapping;
using TourPlanner.Data;
using Newtonsoft.Json;
using System.Dynamic;
using Newtonsoft.Json.Converters;

namespace TourPlannerAPI.Controllers
{
    /// <summary>
    /// API Controller for the tour planner
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class TourPlannerController : ControllerBase
    {
        private readonly ITourManager _tourManager;
        private readonly ITourLogManager _tourLogManager;
        private readonly IMapQuestManager _mapQuestManager;
        private readonly ILogger<TourPlannerController> _logger;

        /// <summary>
        /// Tour Planner Controller Constructor
        /// </summary>
        /// <param name="logger">Log4Net logger</param>
        /// <param name="tourManager">the tour manager</param>
        /// <param name="tourLogManager">the log manager</param>
        /// <param name="mapQuestManager">the mapquest manager</param>
        public TourPlannerController(ILogger<TourPlannerController> logger, ITourManager tourManager, ITourLogManager tourLogManager, IMapQuestManager mapQuestManager)
        {
            _logger = logger;
            _tourManager = tourManager;
            _tourLogManager = tourLogManager;
            _mapQuestManager = mapQuestManager;
        }

        /// <summary>
        /// Gets all tours
        /// </summary>
        /// <response code="200">Success - All existing tours returned</response>
        /// <response code="500">Oops! Problem on our end</response>
        /// <returns>All existing tours</returns>
        [HttpGet("GetTours")]
        public async Task<ActionResult<List<PresentationTour>>> Get()
        {
            _logger.LogInformation("API Request - Get all tours");
            return Ok(await _tourManager.GetToursAsync());
        }

        /// <summary>
        /// Gets tour with specific tourId
        /// </summary>
        /// <param name="TourId"></param>
        /// <response code="200">Success - Tour returned</response>
        /// <response code="404">Tour could not be found</response>
        /// <response code="500">Oops! Problem on our end</response>
        /// <returns>The searched tour</returns>
        [HttpGet("GetTours/{TourId}")]
        public async Task<ActionResult<PresentationTour>> Get(Guid TourId)
        {
            _logger.LogInformation("API Request - Get tour "+ TourId.ToString());
            try
            {
                var result = await _tourManager.GetTourAsync(TourId);
                return Ok(result);
            }
            catch (TourNotFoundException)
            {
                _logger.LogError("Requested tour " + TourId.ToString() + " not found");
                return NotFound("Tour not found");
            }
        }

        /// <summary>
        /// Adds a new tour
        /// </summary>
        /// <param name="tour">the tour</param>
        /// <response code="200">Success - Tour created</response>
        /// <response code="500">Oops! Problem on our end</response>
        /// <returns>the created tour</returns>
        [HttpPost("AddTour")]
        public async Task<ActionResult<PresentationTour>> AddTour(SimpleTour tour)
        {
            _logger.LogInformation("API Request - Add new tour");
            var result = await _mapQuestManager.GetRouteAsync(tour.Start, tour.Destination, tour.Type);
            var map = await _mapQuestManager.GetMapAsync(result.SessionId);
            var presentation = await _tourManager.AddTourAsync(tour, result.Distance, result.Time);
            await _mapQuestManager.SaveMapAsync(presentation.TourId, map);
            return Ok(presentation);
        }

        /// <summary>
        /// Deletes a tour with a given id
        /// </summary>
        /// <param name="TourId">The id of the tour</param>
        /// <response code="200">Success - Tour deleted</response>
        /// <response code="404">Tour could not be found</response>
        /// <response code="500">Oops! Problem on our end</response>
        /// <returns>all tours</returns>
        [HttpDelete("DeleteTour/{TourId}")]
        public async Task<ActionResult<List<PresentationTour>>> DeleteTour(Guid TourId)
        {
            _logger.LogInformation("API Request - Delete tour " + TourId.ToString());
            try
            {
                var result = await _tourManager.DeleteTourAsync(TourId);
                await _mapQuestManager.DeleteMapAsync(TourId);
                return Ok(result);
            }catch(TourNotFoundException)
            {
                _logger.LogError("Requested tour " + TourId.ToString() + " not found");
                return NotFound("Tour not found");
            }
        }

        /// <summary>
        /// Updates a tour
        /// </summary>
        /// <param name="TourId">id of the tour</param>
        /// <param name="request">the new tour</param>
        /// <response code="200">Success - Tour updated</response>
        /// <response code="404">Tour could not be found</response>
        /// <response code="500">Oops! Problem on our end</response>
        /// <returns>the updated tour</returns>
        [HttpPut("UpdateTour/{TourId}")]
        public async Task<ActionResult<PresentationTour>> UpdateTour(Guid TourId, SimpleTour request)
        {
            _logger.LogInformation("API Request - Update tour " + TourId.ToString());
            var result = await _mapQuestManager.GetRouteAsync(request.Start, request.Destination, "fastest");
            var map = await _mapQuestManager.GetMapAsync(result.SessionId);
            
            try
            {
                var presentation = await _tourManager.UpdateTourAsync(TourId, request, result.Distance, result.Time);
                await _mapQuestManager.UpdateMapAsync(presentation.TourId, map);
                return Ok(presentation);
            }
            catch (TourNotFoundException)
            {
                _logger.LogError("Requested tour " + TourId.ToString() + " not found");
                return NotFound("Tour not found");
            }
        }

        /// <summary>
        /// Adds a log to a tour
        /// </summary>
        /// <param name="TourId">the id of the tour</param>
        /// <param name="log">the log</param>
        /// <response code="200">Success - Tour updated</response>
        /// <response code="404">Tour could not be found</response>
        /// <response code="500">Oops! Problem on our end</response>
        /// <returns>the tour including the log</returns>
        [HttpPost("AddLog/{TourId}")]
        public async Task<ActionResult<PresentationTour>> AddLog(Guid TourId, SimpleLog log)
        {
            _logger.LogInformation("API Request - Add log to tour " + TourId.ToString());
            try
            {
                var result = await _tourLogManager.AddLogAsync(TourId, log);
                return Ok(result);
            }
            catch (TourNotFoundException)
            {
                _logger.LogError("Requested tour " + TourId.ToString() + " not found");
                return NotFound("Tour not found");
            }
        }

        /// <summary>
        /// Gets all logs of tour
        /// </summary>
        /// <param name="TourId">the id of the tour</param>
        /// <response code="200">Success - Tour found</response>
        /// <response code="404">Tour could not be found</response>
        /// <response code="500">Oops! Problem on our end</response>
        /// <returns>All the logs of the tour</returns>
        [HttpGet("GetLogs/{TourId}")]
        public async Task<ActionResult<List<PresentationLog>>> GetLogs(Guid TourId)
        {
            _logger.LogInformation("API Request - Get all logs of tour " + TourId.ToString());
            try
            {
                var result = await _tourLogManager.GetLogsAsync(TourId);
                return Ok(result);
            }
            catch (TourNotFoundException)
            {
                _logger.LogError("Requested tour " + TourId.ToString() + " not found");
                return NotFound("Tour not found");
            }

        }

        /// <summary>
        /// Gets a specific log of a tour
        /// </summary>
        /// <param name="TourId">the id of the tour</param>
        /// <param name="LogId">the id of the log</param>
        /// <response code="200">Success - Log found</response>
        /// <response code="404">Tour or Log could not be found</response>
        /// <response code="500">Oops! Problem on our end</response>
        /// <returns>the log</returns>
        [HttpGet("GetLogs/{TourId}/{LogId}")]
        public async Task<ActionResult<PresentationLog>> GetLog(Guid TourId, Guid LogId)
        {
            _logger.LogInformation("API Request - Get log " +LogId.ToString() +" of tour "+ TourId.ToString());
            try
            {
                var result = await _tourLogManager.GetLogAsync(TourId, LogId);
                return Ok(result);
            }
            catch (TourNotFoundException)
            {
                _logger.LogError("Requested tour " + TourId.ToString() + " not found");
                return NotFound("Tour not found");
            }
            catch (LogNotFoundException)
            {
                _logger.LogError("Requested log " + LogId.ToString() + " not found in tour " + TourId.ToString());
                return NotFound("Log not found");
            }
        }

        /// <summary>
        /// Deletes log of tour
        /// </summary>
        /// <param name="TourId">the id of the tour</param>
        /// <param name="LogId">the id of the log</param>
        /// <response code="200">Success - Log deleted</response>
        /// <response code="404">Tour or Log could not be found</response>
        /// <response code="500">Oops! Problem on our end</response>
        /// <returns>all the existing logs</returns>
        [HttpDelete("DeleteLog/{TourId}/{LogId}")]
        public async Task<ActionResult<List<PresentationLog>>> DeleteLog(Guid TourId, Guid LogId)
        {
            _logger.LogInformation("API Request - Delete log " + LogId.ToString() + " of tour " + TourId.ToString());
            try
            {
                var result = await _tourLogManager.DeleteLogAsync(TourId, LogId);
                return Ok(result);
            }
            catch (TourNotFoundException)
            {
                _logger.LogInformation("Requested tour " + TourId.ToString() + " not found");
                return NotFound("Tour not found");
            }
            catch (LogNotFoundException)
            {
                _logger.LogError("Requested log " + LogId.ToString() + " not found in tour " + TourId.ToString());
                return NotFound("Log not found");
            }
        }

        /// <summary>
        ///  Gets the map of a round as byte array
        /// </summary>
        /// <param name="TourId">the tour id</param>
        /// <response code="200">Success - Map found</response>
        /// <response code="404">Tour could not be found</response>
        /// <response code="500">Oops! Problem on our end</response>
        /// <returns>the map</returns>
        [HttpGet("GetMap/{TourId}")]
        public async Task<ActionResult<byte[]>> GetMap(Guid TourId)
        {
            _logger.LogInformation("API Request - Get map of tour " + TourId.ToString());
            try
            {
                var result = await _mapQuestManager.LoadMapAsync(TourId);
                return Ok(result);
            }
            catch (TourNotFoundException)
            {
                _logger.LogInformation("Requested tour " + TourId.ToString() + " not found");
                return NotFound("Tour not found");
            }
        }

        /// <summary>
        /// Generates report of tour
        /// </summary>
        /// <param name="TourId">the tour id</param>
        /// <response code="200">Success - Report generated</response>
        /// <response code="404">Tour could not be found</response>
        /// <response code="500">Oops! Problem on our end</response>
        /// <returns>the report</returns>
        [HttpGet("TourReport/{TourId}")]
        public async Task<ActionResult<byte[]>> RouteReport(Guid TourId)
        {
            _logger.LogInformation("API Request - Generate report of tour " + TourId.ToString());
            try
            {
                var result = await _tourManager.GenerateTourReportAsync(TourId);
                return Ok(result);
            }
            catch (TourNotFoundException)
            {
                _logger.LogInformation("Requested tour " + TourId.ToString() + " not found");
                return NotFound("Tour not found");
            }
        }

        /// <summary>
        /// Generates overview report of all tours
        /// </summary>
        /// <response code="200">Success - Overview report generated</response>
        /// <response code="500">Oops! Problem on our end</response>
        /// <returns>the report</returns>
        [HttpGet("TourOverviewReport")]
        public async Task<ActionResult<byte[]>> TourOverviewReport()
        {
            _logger.LogInformation("API Request - Generate tour overview report");
            return Ok(await _tourManager.GenerateTourOverviewAsync());
        }

        /// <summary>
        /// Searches for a term
        /// </summary>
        /// <param name="searchTerm">the search term</param>
        /// <response code="200">Success - Found matches</response>
        /// /// <response code="404">No match found</response>
        /// <response code="500">Oops! Problem on our end</response>
        /// <returns>all the matching tours</returns>
        [HttpGet("search/{searchTerm}")]
        public async Task<ActionResult<List<PresentationTour>>> SearchTours(string searchTerm)
        {
            _logger.LogInformation("API Request - Search for term " + searchTerm);
            return Ok(await _tourManager.SearchAsync(searchTerm.ToUpper()));
        }

        /// <summary>
        /// Exports a tour
        /// </summary>
        /// <param name="TourId">the tour id</param>
        /// <response code="200">Success - Tour exported</response>
        /// <response code="404">Tour could not be found</response>
        /// <response code="500">Oops! Problem on our end</response>
        /// <returns>the tour</returns>
        [HttpGet("export/{TourId}")]
        public async Task<ActionResult<string>> ExportTour(Guid TourId)
        {
            _logger.LogInformation("API Request - Export tour " + TourId.ToString());
            try
            {
                var result = await _tourManager.ExportTourAsync(TourId);
                return Ok(result);
            }
            catch (TourNotFoundException)
            {
                _logger.LogError("Requested tour " + TourId.ToString() + " not found");
                return NotFound("Tour not found");
            }
            
        }

        /// <summary>
        /// imports a tour
        /// </summary>
        /// <param name="tour">the tour id</param>
        /// <response code="200">Success - Tour imported</response>
        /// <response code="409">Tour already exists</response>
        /// <response code="500">Oops! Problem on our end</response>
        /// <returns>the imported tour</returns>
        [HttpPost("import")]
        public async Task<ActionResult<PresentationTour>> ImportTour(PresentationTour tour)
        {
            _logger.LogInformation("API Request - Import tour " + tour.TourId.ToString());
            try
            {
                var route = await _mapQuestManager.GetRouteAsync(tour.Start, tour.Destination, tour.Type);
                tour.Distance = route.Distance;
                tour.Duration = route.Time;
                var result = await _tourManager.ImportTourAsync(tour);
                var map = await _mapQuestManager.GetMapAsync(route.SessionId);
                await _mapQuestManager.SaveMapAsync(tour.TourId, map);
                return Ok(result);
            }
            catch (TourAlreadyExistsException)
            {
                _logger.LogError("Tour " + tour.TourId.ToString() + " already exists");
                return Conflict("Tour already exists");
            }
        }
    }
}
