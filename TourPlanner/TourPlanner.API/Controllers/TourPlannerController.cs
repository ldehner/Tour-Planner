using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;
using System.Security.Cryptography.Xml;
using TourPlanner.API.BL;
using TourPlanner.API.DAL;
using TourPlanner.API.Data;
using TourPlanner.API.Mapping;
using TourPlanner.Data;

namespace TourPlannerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TourPlannerController : ControllerBase
    {
        private readonly ToursDataContext _context;
        private readonly ITourManager _tourManager;

        public TourPlannerController(ToursDataContext context, ITourManager tourManager)
        {
            this._context = context;
            this._tourManager = tourManager;
        }
        [HttpGet("GetTours")]
        public async Task<ActionResult<List<PresentationTour>>> Get()
        {
            return Ok(await _tourManager.GetToursAsync());
        }

        [HttpGet("GetTours/{TourId}")]
        public async Task<ActionResult<PresentationTour>> Get(Guid tourId)
        {
            return Ok(await _tourManager.GetTourAsync(tourId));
        }

        [HttpPost("AddTour")]
        public async Task<ActionResult<List<PresentationTour>>> AddTour(SimpleTour tour)
        {
            return Ok(await _tourManager.AddTourAsync(tour));
        }
        //DeleteTour
        [HttpDelete("DeleteTour/{TourId}")]
        public async Task<ActionResult<List<PresentationTour>>> DeleteTour(Guid TourId)
        {
            return Ok(await _tourManager.DeleteTourAsync(TourId));
        }
        //EditTour
        [HttpPut("UpdateTour/{TourId}")]
        public async Task<ActionResult<PresentationTour>> UpdateTour(Guid tourId, SimpleTour request)
        {
            return Ok(await _tourManager.UpdateTourAsync(tourId, request));
        }
        //AddLog
        [HttpPost("AddLog/{TourId}")]
        public async Task<ActionResult<PresentationTour>> AddLog(Guid TourId, SimpleLog log)
        {

            var tour = await this._context.Tours.Include(i => i.Logs)
                .FirstOrDefaultAsync(i => i.TourId == TourId);
            if (tour == null) return BadRequest("Tour not found.");
         
            var newlog = new Logs
            {
                TourId = TourId,
                Comment = log.Comment,
                Date = log.Date,
                Rating = log.Rating,
                Duration = log.Duration.TimeOfDay,
                Difficulty = log.Difficulty,
            };
            tour.Logs.Add(newlog);
            
            this._context.SaveChanges();

            return Ok(await this._context.Tours.ToListAsync());
        }
        
        //GetLogs
        [HttpGet("GetLogs/{TourId}")]
        public async Task<ActionResult<List<PresentationLog>>> GetLogs(Guid TourId)
        {

            var tour = await this._context.Tours.Include(i => i.Logs)
                .FirstOrDefaultAsync(i => i.TourId == TourId);
            if (tour == null) return BadRequest("Tour not found.");
            return Ok(tour.Logs);

        }
        //GetLogs/{LogId}
        [HttpGet("GetLogs/{TourId}/{LogId}")]
        public async Task<ActionResult<PresentationLog>> GetLog(Guid TourId, Guid LogId)
        {

            var tour = await this._context.Tours.Include(i => i.Logs)
                .FirstOrDefaultAsync(i => i.TourId == TourId);
            if (tour == null) return BadRequest("Tour not found.");
            Logs findLog = null;
            tour.Logs.ForEach(log =>
            {
                if (log.LogId.CompareTo(LogId) == 0) findLog = log;
            });
            if (findLog == null) return BadRequest("Log not found.");
            return Ok(findLog);

        }
        //DeleteLog
        [HttpDelete("DeleteLog/{TourId}/{LogId}")]
        public async Task<ActionResult<List<PresentationLog>>> DeleteLog(Guid TourId, Guid LogId)
        {

            var tour = await this._context.Tours.FindAsync(TourId);
            if (tour == null) return BadRequest("Tour not found.");
            Logs findLog = null;
            tour.Logs.ForEach(log =>
            {
                if (log.LogId.CompareTo(LogId) == 0) findLog = log;
            });
            if (findLog == null) return BadRequest("Log not found.");
            tour.Logs.Remove(findLog);
            await this._context.SaveChangesAsync();
            return Ok((await this._context.Tours.FindAsync(TourId)).Logs);
        }
        //EditLog
        [HttpPut("EditLog/{TourId}/{LogId}")]
        public async Task<ActionResult<PresentationLog>> EditLog(Guid TourId, Guid LogId, SimpleLog log)
        {

            var tour = await this._context.Tours.FindAsync(TourId);
            if (tour == null) return BadRequest("Tour not found.");
            Logs findLog = null;
            tour.Logs.ForEach(log =>
            {
                if (log.LogId.CompareTo(LogId) == 0) findLog = log;
            });
            if (findLog == null) return BadRequest("Log not found.");
            tour.Logs.Remove(findLog);
            tour.Logs.Add(log);
            await this._context.SaveChangesAsync();
            return Ok((await this._context.Tours.FindAsync(TourId)).Logs);
        }
    }
}
