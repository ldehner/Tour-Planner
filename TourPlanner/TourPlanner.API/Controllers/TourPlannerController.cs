using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        [HttpGet]
        public async Task<ActionResult<List<Tours>>> Get()
        {
            return Ok(await this.context.Tours.ToListAsync());
        }

        [HttpGet("{TourId}")]
        public async Task<ActionResult<Tours>> Get(Guid TourId)
        {
            var tour = await this.context.Tours.FindAsync(TourId);
            if (tour == null) return BadRequest("Tour not found.");
            return Ok(tour);
        }

        [HttpPost]
        public async Task<ActionResult<List<Tours>>> AddTour(Tours tour)
        {
            this.context.Tours.Add(tour);
            await this.context.SaveChangesAsync();

            return Ok(await this.context.Tours.ToListAsync());
        }
    }
}
