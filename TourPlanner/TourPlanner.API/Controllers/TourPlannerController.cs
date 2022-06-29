using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;
using System.Security.Cryptography.Xml;
using TourPlanner.API.Data;
using TourPlanner.Data;

namespace TourPlannerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TourPlannerController : ControllerBase
    {
        private readonly ToursDataContext context;

        public TourPlannerController(ToursDataContext context)
        {
            this.context = context;
        }
        [HttpGet("GetTours")]
        public async Task<ActionResult<List<Tours>>> Get()
        {
            return Ok(await this.context.Tours.Include(l => l.Logs).ToListAsync());
        }

        [HttpGet("GetTours/{TourId}")]
        public async Task<ActionResult<Tours>> Get(Guid TourId)
        {
            var tour = await this.context.Tours.Include(i => i.Logs)
                .FirstOrDefaultAsync(i => i.TourId == TourId);
            if (tour == null) return BadRequest("Tour not found.");
            return Ok(tour);
        }

        [HttpPost("AddTour")]
        public async Task<ActionResult<List<Tours>>> AddTour(SimpleTour tour)
        {
            Console.WriteLine(tour);
            var tourId = Guid.NewGuid();

            Tours tours = new Tours()
            {
                TourId = tourId,
                Description = tour.Description,
                Distance = tour.Distance,
                Duration = tour.Duration.TimeOfDay,
                Type = tour.Type,
                Start = tour.Start,
                Destination = tour.Destination,
                Logs = new List<Logs> { new Logs
                {
                    TourId = tourId,
                LogId = Guid.NewGuid(),
                Comment = "comment",
                Date = new DateTime(),
                Rating = 1,
                Duration = new TimeSpan(),
                Difficulty = 2,
                } }
            };
            this.context.Tours.Add(tours);
            await this.context.SaveChangesAsync();

            return Ok(tours);
        }
        //DeleteTour
        [HttpDelete("DeleteTour/{TourId}")]
        public async Task<ActionResult<Tours>> DeleteTour(Guid TourId)
        {
            var tour = await this.context.Tours.FindAsync(TourId);
            if (tour == null) return BadRequest("Tour not found.");
            this.context.Tours.Remove(tour);
            await this.context.SaveChangesAsync();
            return Ok(await this.context.Tours.ToListAsync());
        }
        //EditTour
        [HttpPut("UpdateTour")]
        public async Task<ActionResult<Tours>> UpdateTour(Tours request)
        {

            var tour = await this.context.Tours.Include(i => i.Logs)
                .FirstOrDefaultAsync(i => i.TourId == request.TourId);
            if (tour == null) return BadRequest("Tour not found.");
            tour.Name = request.Name;
            tour.Duration = request.Duration;
            tour.Start = request.Start;
            tour.Description = request.Description;
            tour.Destination = request.Destination;
            tour.Type = request.Type;

            await this.context.SaveChangesAsync();

            return Ok(await this.context.Tours.ToListAsync());
        }
        //AddLog
        [HttpPost("AddLog/{TourId}")]
        public async Task<ActionResult<Tours>> AddLog(Guid TourId, SimpleLog log)
        {

            var tour = await this.context.Tours.Include(i => i.Logs)
                .FirstOrDefaultAsync(i => i.TourId == TourId);
            if (tour == null) return BadRequest("Tour not found.");
            
            /**
            tour.Logs = new List<Logs> {
            new Logs
            {
                TourId = TourId,
                LogId = Guid.NewGuid(),
                Comment = log.Comment,
                Date = log.Date,
                Rating = log.Rating,
                Duration = log.Duration.TimeOfDay,
                Difficulty = log.Difficulty,
            }
            };
            **/
            var newlog = new Logs
            {
                TourId = TourId,
                LogId = Guid.NewGuid(),
                Comment = log.Comment,
                Date = log.Date,
                Rating = log.Rating,
                Duration = log.Duration.TimeOfDay,
                Difficulty = log.Difficulty,
            };
            tour.Logs.Add(newlog);
            
            await this.context.SaveChangesAsync();

            return Ok(await this.context.Tours.ToListAsync());
        }
        
        //GetLogs
        [HttpGet("GetLogs/{TourId}")]
        public async Task<ActionResult<Tours>> GetLogs(Guid TourId)
        {

            var tour = await this.context.Tours.Include(i => i.Logs)
                .FirstOrDefaultAsync(i => i.TourId == TourId);
            if (tour == null) return BadRequest("Tour not found.");
            return Ok(tour.Logs);

        }
        //GetLogs/{LogId}
        [HttpGet("GetLogs/{TourId}/{LogId}")]
        public async Task<ActionResult<Tours>> GetLog(Guid TourId, Guid LogId)
        {

            var tour = await this.context.Tours.Include(i => i.Logs)
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
        public async Task<ActionResult<Tours>> DeleteLog(Guid TourId, Guid LogId)
        {

            var tour = await this.context.Tours.FindAsync(TourId);
            if (tour == null) return BadRequest("Tour not found.");
            Logs findLog = null;
            tour.Logs.ForEach(log =>
            {
                if (log.LogId.CompareTo(LogId) == 0) findLog = log;
            });
            if (findLog == null) return BadRequest("Log not found.");
            tour.Logs.Remove(findLog);
            await this.context.SaveChangesAsync();
            return Ok((await this.context.Tours.FindAsync(TourId)).Logs);
        }
        //EditLog
        [HttpPut("EditLog/{TourId}/{LogId}")]
        public async Task<ActionResult<Tours>> EditLog(Guid TourId, Guid LogId, Logs log)
        {

            var tour = await this.context.Tours.FindAsync(TourId);
            if (tour == null) return BadRequest("Tour not found.");
            Logs findLog = null;
            tour.Logs.ForEach(log =>
            {
                if (log.LogId.CompareTo(LogId) == 0) findLog = log;
            });
            if (findLog == null) return BadRequest("Log not found.");
            tour.Logs.Remove(findLog);
            tour.Logs.Add(log);
            await this.context.SaveChangesAsync();
            return Ok((await this.context.Tours.FindAsync(TourId)).Logs);
        }
    }
}
