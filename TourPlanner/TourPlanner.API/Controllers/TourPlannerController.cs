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
        [HttpGet]
        public async Task<ActionResult<List<Tours>>> Get()
        {
            return Ok(await this.context.Tours.ToListAsync());
        }
        private const string URL = "https://www.mapquestapi.com/directions/v2/route";
        private string urlParameters = "?key=M5qGCXac3rjCtRpRe2aRBlxK3GiVKnnE&unit=k&from=In%20Freybergen%2025%202120%20Wolkersdorf%20&to=Tulpenweg%204%20Bisingen";
        [HttpGet("mapquest")]
        public async Task<ActionResult<List<Tours>>> Get(int id)
        {
          
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(URL);

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            // List data response.
            HttpResponseMessage response = client.GetAsync(urlParameters).Result;  // Blocking call! Program will wait here until a response is received or a timeout occurs.
            if (response.IsSuccessStatusCode)
            {
                client.Dispose();
                return Ok(response.Content);
            }
            else
            {
                client.Dispose();
                return BadRequest(response);
            }

            //Make any other calls using HttpClient here.

            //Dispose once all HttpClient calls are complete. This is not necessary if the containing object will be disposed of; for example in this case the HttpClient instance will be disposed automatically when the application terminates so the following call is superfluous.
            
        
        }

        [HttpGet("{TourId}")]
        public async Task<ActionResult<Tours>> Get(Guid TourId)
        {
            var tour = await this.context.Tours.FindAsync(TourId);
            if (tour == null) return BadRequest("Tour not found.");
            return Ok(tour);
        }

        [HttpPost]
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
    }
}
