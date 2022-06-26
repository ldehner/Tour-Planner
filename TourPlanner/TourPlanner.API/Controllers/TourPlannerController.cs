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
            return Ok(await this.context.Tours.ToListAsync());
        }

        [HttpGet("GetTours/{TourId}")]
        public async Task<ActionResult<Tours>> Get(Guid TourId)
        {
            var tour = await this.context.Tours.FindAsync(TourId);
            if (tour == null) return BadRequest("Tour not found.");
            return Ok(tour);
        }

        [HttpPost("AddTour")]
        public async Task<ActionResult<List<Tours>>> AddTour(SimpleTour tour)
        {
            Console.WriteLine(tour);


            Tours tours = new Tours()
            {
                TourId = Guid.NewGuid(),
                Description = tour.Description,
                Distance = tour.Distance,
                Duration = tour.Duration.TimeOfDay,
                Type = tour.Type,
                Start = tour.Start,
                Destination = tour.Destination,
                Logs = new List<Logs>()
            };
            this.context.Tours.Add(tours);
            await this.context.SaveChangesAsync();

            return Ok(tours);
        }

        //DeleteTour
        //EditTour
        //AddLog
        //GetLogs
        //GetLogs/{LogId}
        //DeleteLog
        //EditLog
    }
}
