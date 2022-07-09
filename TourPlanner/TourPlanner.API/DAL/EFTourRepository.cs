using TourPlanner.API.Data;
using TourPlanner.API.Mapping;
using TourPlanner.Data;
using Microsoft.EntityFrameworkCore;
using TourPlanner.API.Exceptions;

namespace TourPlanner.API.DAL
{
    public class EFTourRepository : ITourRepository
    {
        private readonly DbContextOptions<ToursDataContext> _options;
        public EFTourRepository(DbContextOptions<ToursDataContext> options)
        {
            _options = options;
        }

        public async Task<PresentationTour> AddTourAsync(SimpleTour tour, double distance, TimeSpan duration)
        {
            using (var context = new ToursDataContext(_options))
            {
                var guid = Guid.NewGuid();
                Tours tours = new Tours()
                {
                    TourId = guid,
                    Name = tour.Name,
                    Description = tour.Description,
                    Distance = distance,
                    Duration = duration,
                    Type = tour.Type,
                    Start = await AdressConverter.AdressToEfAsync(tour.Start, guid),
                    Destination = await AdressConverter.AdressToEfAsync(tour.Destination, guid),
                    Logs = new List<Logs>(),
                };
                context.Tours.Add(tours);
                await context.SaveChangesAsync();

                return await TourConverter.ToursToPresentationTour(tours);
            }
                
        }
        //TODO
        public async Task<List<PresentationTour>> DeleteTourAsync(Guid tourId)
        {
            using (var context = new ToursDataContext(_options))
            {
                var tour = await context.Tours.Include(tour => tour.Logs).Include(tour => tour.Start).Include(tour => tour.Destination)
                .FirstOrDefaultAsync(i => i.TourId == tourId);
                if (tour is null) throw new TourNotFoundException();
                context.Tours.RemoveRange(tour);
                var adresses = await context.Adress.Where(i => i.TourIdStart == tourId).ToListAsync();
                context.Adress.RemoveRange(adresses);
                await context.SaveChangesAsync();

                return await GetToursAsync();
            }
            
        }

        public async Task<PresentationTour> GetTourAsync(Guid tourId)
        {
            using (var context = new ToursDataContext(_options))
            {
                var tour = await context.Tours.Include(tour => tour.Logs).Include(tour => tour.Start).Include(tour => tour.Destination)
                .FirstOrDefaultAsync(i => i.TourId == tourId);
                if (tour is null) throw new TourNotFoundException();
                return await TourConverter.ToursToPresentationTour(tour);
            }
            
        }

        public async Task<List<PresentationTour>> GetToursAsync()
        {
            var presentationTours = new List<PresentationTour>();
            using (var context = new ToursDataContext(_options))
            {
                List<Tours> tours = await context.Tours.Include(tour => tour.Logs).Include(tour => tour.Start).Include(tour => tour.Destination).ToListAsync();
                
                tours.ForEach(async tour => presentationTours.Add(await TourConverter.ToursToPresentationTour(tour)));

                
            }
            return presentationTours;
        }

        public async Task<PresentationTour> UpdateTourAsync(Guid tourId, SimpleTour request, double distance, TimeSpan duration)
        {
            using (var context = new ToursDataContext(_options))
            {
                var tour = await context.Tours.Include(tour => tour.Logs).Include(tour => tour.Start).Include(tour => tour.Destination)
                .FirstOrDefaultAsync(i => i.TourId == tourId);
                if (tour is null) throw new TourNotFoundException();
                tour.Name = request.Name;
                tour.Start = await AdressConverter.AdressToEfAsync(request.Start, tourId);
                tour.Distance = distance;
                tour.Duration = duration;
                tour.Description = request.Description;
                tour.Destination = await AdressConverter.AdressToEfAsync(request.Destination, tourId);
                tour.Type = request.Type;

                await context.SaveChangesAsync();

                return await GetTourAsync(tourId);
            }
            
        }

        public async Task<List<PresentationTour>> SearchAsync(string searchTerm)
        {
            var presentationTours = new List<PresentationTour>();
            using (var context = new ToursDataContext(_options))
            {
                var tours = await context.Tours.Include(tour => tour.Start).Include(tour => tour.Destination).Where(i => i.Name.ToUpper().Contains(searchTerm) || i.Description.ToUpper().Contains(searchTerm) || i.Start.Country.ToUpper().Contains(searchTerm) || i.Start.Street.ToUpper().Contains(searchTerm) || i.Start.City.ToUpper().Contains(searchTerm) || i.Destination.Country.ToUpper().Contains(searchTerm) || i.Destination.Street.ToUpper().Contains(searchTerm) || i.Destination.City.ToUpper().Contains(searchTerm)).ToListAsync();
                
                if (tours is not null) tours.ForEach(async tour => presentationTours.Add(await TourConverter.ToursToPresentationTour(tour)));
                if (presentationTours.Count <= 0) throw new TourNotFoundException();
            }
            
            return presentationTours;
        }

        public async Task<PresentationTour> ImportTourAsync(PresentationTour tour)
        {
            using (var context = new ToursDataContext(_options))
            {
                var check = await context.Tours.FirstOrDefaultAsync(i => i.TourId == tour.TourId);
                if (check != null) throw new TourAlreadyExistsException();
                context.Tours.Add(await TourConverter.PresentationTourToTours(tour));
                await context.SaveChangesAsync();
            }
            
            return tour;
        }
    }
}
