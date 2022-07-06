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
    [Route("api/[controller]")]
    [ApiController]
    public class TourPlannerController : ControllerBase
    {
        private readonly ITourManager _tourManager;
        private readonly ITourLogManager _tourLogManager;
        private readonly IMapQuestManager _mapQuestManager;
        private readonly ILogger<TourPlannerController> _logger;

        public TourPlannerController(ILogger<TourPlannerController> logger, ITourManager tourManager, ITourLogManager tourLogManager, IMapQuestManager mapQuestManager)
        {
            _logger = logger;
            _tourManager = tourManager;
            _tourLogManager = tourLogManager;
            _mapQuestManager = mapQuestManager;
        }
        [HttpGet("GetTours")]
        public async Task<ActionResult<List<PresentationTour>>> Get()
        {
            _logger.LogInformation("API Request - Get all tours");
            return Ok(await _tourManager.GetToursAsync());
        }

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
                _logger.LogInformation("Requested tour " + TourId.ToString() + " not found");
                return BadRequest("Tour not found");
            }
        }

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
        //DeleteTour
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
                _logger.LogInformation("Requested tour " + TourId.ToString() + " not found");
                return BadRequest("Tour not found");
            }
        }
        //EditTour
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
                _logger.LogInformation("Requested tour " + TourId.ToString() + " not found");
                return BadRequest("Tour not found");
            }
        }
        //AddLog
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
                _logger.LogInformation("Requested tour " + TourId.ToString() + " not found");
                return BadRequest("Tour not found");
            }
        }
        
        //GetLogs
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
                _logger.LogInformation("Requested tour " + TourId.ToString() + " not found");
                return BadRequest("Tour not found");
            }

        }
        //GetLogs/{LogId}
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
                _logger.LogInformation("Requested tour " + TourId.ToString() + " not found");
                return BadRequest("Tour not found");
            }
            catch (LogNotFoundException)
            {
                _logger.LogInformation("Requested log " + LogId.ToString() + " not found in tour " + TourId.ToString());
                return BadRequest("Log not found");
            }
        }
        //DeleteLog
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
                return BadRequest("Tour not found");
            }
            catch (LogNotFoundException)
            {
                _logger.LogInformation("Requested log " + LogId.ToString() + " not found in tour " + TourId.ToString());
                return BadRequest("Log not found");
            }
        }

        [HttpGet("GetMap/{TourId}")]
        public async Task<ActionResult<byte[]>> GetMap(Guid TourId)
        {
            _logger.LogInformation("API Request - Get map of tour " + TourId.ToString());
            return Ok(await _mapQuestManager.LoadMapAsync(TourId));
        }

        [HttpGet("TourReport/{TourId}")]
        public async Task<ActionResult<byte[]>> RouteReport(Guid TourId)
        {
            _logger.LogInformation("API Request - Generate report of tour " + TourId.ToString());
            return Ok(await _tourManager.GenerateTourReportAsync(TourId));
        }

        [HttpGet("TourOverviewReport")]
        public async Task<ActionResult<byte[]>> TourOverviewReport()
        {
            _logger.LogInformation("API Request - Generate tour overview report");
            return Ok(await _tourManager.GenerateTourOverviewAsync());
        }

        [HttpGet("search/{searchTerm}")]
        public async Task<ActionResult<List<PresentationTour>>> SearchTours(string searchTerm)
        {
            _logger.LogInformation("API Request - Search for term " + searchTerm);
            return Ok(await _tourManager.SearchAsync(searchTerm.ToUpper()));
        }

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
                _logger.LogInformation("Requested tour " + TourId.ToString() + " not found");
                return BadRequest("Tour not found");
            }
            
        }
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
                _logger.LogInformation("Tour " + tour.TourId.ToString() + " already exists");
                return BadRequest("Tour already exists");
            }
        }



    }
}
