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
        private readonly ToursDataContext _context;
        private readonly ITourManager _tourManager;
        private readonly ITourLogManager _tourLogManager;
        private readonly IMapQuestManager _mapQuestManager;

        public TourPlannerController(ToursDataContext context, ITourManager tourManager, ITourLogManager tourLogManager, IMapQuestManager mapQuestManager)
        {
            _context = context;
            _tourManager = tourManager;
            _tourLogManager = tourLogManager;
            _mapQuestManager = mapQuestManager;
        }
        [HttpGet("GetTours")]
        public async Task<ActionResult<List<PresentationTour>>> Get()
        {
            return Ok(await _tourManager.GetToursAsync());
        }

        [HttpGet("GetTours/{TourId}")]
        public async Task<ActionResult<PresentationTour>> Get(Guid TourId)
        {
            try
            {
                var result = await _tourManager.GetTourAsync(TourId);
                return Ok(result);
            }
            catch (TourNotFoundException)
            {
                return BadRequest("Tour not found");
            }
        }

        [HttpPost("AddTour")]
        public async Task<ActionResult<PresentationTour>> AddTour(SimpleTour tour)
        {
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
            try
            {
                var result = await _tourManager.DeleteTourAsync(TourId);
                await _mapQuestManager.DeleteMapAsync(TourId);
                return Ok(result);
            }catch(TourNotFoundException)
            {
                return BadRequest("Tour not found");
            }
        }
        //EditTour
        [HttpPut("UpdateTour/{TourId}")]
        public async Task<ActionResult<PresentationTour>> UpdateTour(Guid TourId, SimpleTour request)
        {
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
                return BadRequest("Tour not found");
            }
        }
        //AddLog
        [HttpPost("AddLog/{TourId}")]
        public async Task<ActionResult<PresentationTour>> AddLog(Guid TourId, SimpleLog log)
        {
            try
            {
                var result = await _tourLogManager.AddLogAsync(TourId, log);
                return Ok(result);
            }
            catch (TourNotFoundException)
            {

                return BadRequest("Tour not found");
            }
        }
        
        //GetLogs
        [HttpGet("GetLogs/{TourId}")]
        public async Task<ActionResult<List<PresentationLog>>> GetLogs(Guid TourId)
        {
            try
            {
                var result = await _tourLogManager.GetLogsAsync(TourId);
                return Ok(result);
            }
            catch (TourNotFoundException)
            {

                return BadRequest("Tour not found");
            }

        }
        //GetLogs/{LogId}
        [HttpGet("GetLogs/{TourId}/{LogId}")]
        public async Task<ActionResult<PresentationLog>> GetLog(Guid TourId, Guid LogId)
        {
            try
            {
                var result = await _tourLogManager.GetLogAsync(TourId, LogId);
                return Ok(result);
            }
            catch (TourNotFoundException)
            {
                return BadRequest("Tour not found");
            }
            catch (LogNotFoundException)
            {
                return BadRequest("Log not found");
            }
        }
        //DeleteLog
        [HttpDelete("DeleteLog/{TourId}/{LogId}")]
        public async Task<ActionResult<List<PresentationLog>>> DeleteLog(Guid TourId, Guid LogId)
        {
            try
            {
                var result = await _tourLogManager.DeleteLogAsync(TourId, LogId);
                return Ok(result);
            }
            catch (TourNotFoundException)
            {
                return BadRequest("Tour not found");
            }
            catch (LogNotFoundException)
            {
                return BadRequest("Log not found");
            }
        }

        [HttpGet("GetMap/{TourId}")]
        public async Task<ActionResult<byte[]>> GetMap(Guid TourId)
        {
            return Ok(await _mapQuestManager.LoadMapAsync(TourId));
        }

        [HttpGet("TourReport/{TourId}")]
        public async Task<ActionResult<byte[]>> RouteReport(Guid TourId)
        {
            return Ok(await _tourManager.GenerateTourReportAsync(TourId));
        }

        [HttpGet("TourOverviewReport")]
        public async Task<ActionResult<byte[]>> TourOverviewReport()
        {
            return Ok(await _tourManager.GenerateTourOverviewAsync());
        }

        [HttpGet("search/{searchTerm}")]
        public async Task<ActionResult<List<PresentationTour>>> SearchTours(string searchTerm)
        {
            return Ok(await _tourManager.SearchAsync(searchTerm.ToUpper()));
        }

        [HttpGet("export/{TourId}")]
        public async Task<ActionResult<string>> ExportTour(Guid TourId)
        {
            try
            {
                var result = await _tourManager.ExportTourAsync(TourId);
                return Ok(result);
            }
            catch (TourNotFoundException)
            {
                return BadRequest("Tour not found");
            }
            
        }
        [HttpPost("import")]
        public async Task<ActionResult<PresentationTour>> ImportTour(PresentationTour tour)
        {
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
                return BadRequest("Tour already exists");
            }
        }



    }
}
