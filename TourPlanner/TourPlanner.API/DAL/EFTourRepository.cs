using TourPlanner.API.Data;
using TourPlanner.API.Mapping;
using TourPlanner.Data;
using Microsoft.EntityFrameworkCore;
using TourPlanner.API.Exceptions;

namespace TourPlanner.API.DAL
{
    public class EFTourRepository : ITourRepository
    {
        private readonly ToursDataContext _context;
        public EFTourRepository(ToursDataContext toursDataContext) { _context = toursDataContext; }

        public async Task<PresentationTour> AddTourAsync(SimpleTour tour, double distance, TimeSpan duration)
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
            this._context.Tours.Add(tours);
            await this._context.SaveChangesAsync();

            return await TourConverter.ToursToPresentationTour(tours);
        }
        //TODO
        public async Task<List<PresentationTour>> DeleteTourAsync(Guid tourId)
        {
            var tour = await this._context.Tours.Include(tour => tour.Logs).Include(tour => tour.Start).Include(tour => tour.Destination)
                .FirstOrDefaultAsync(i => i.TourId == tourId);
            if (tour is null) throw new TourNotFoundException();
            this._context.Tours.RemoveRange(tour);
            var adresses = await this._context.Adress.Where(i => i.TourIdStart == tourId).ToListAsync();
            this._context.Adress.RemoveRange(adresses);
            await this._context.SaveChangesAsync();

            return await GetToursAsync();
        }

        public async Task<PresentationTour> GetTourAsync(Guid tourId)
        {
            var tour = await this._context.Tours.Include(tour => tour.Logs).Include(tour => tour.Start).Include(tour => tour.Destination)
                .FirstOrDefaultAsync(i => i.TourId == tourId);
            if (tour is null) throw new TourNotFoundException();
            return await TourConverter.ToursToPresentationTour(tour);
        }

        public async Task<List<PresentationTour>> GetToursAsync()
        {
            List<Tours> tours = await this._context.Tours.Include(tour => tour.Logs).Include(tour => tour.Start).Include(tour => tour.Destination).ToListAsync();
            var presentationTours = new List<PresentationTour>();
            tours.ForEach(async tour => presentationTours.Add(await TourConverter.ToursToPresentationTour(tour)));

            return (presentationTours);
        }

        public async Task<PresentationTour> UpdateTourAsync(Guid tourId, SimpleTour request, double distance, TimeSpan duration)
        {
            var tour = await this._context.Tours.Include(tour => tour.Logs).Include(tour => tour.Start).Include(tour => tour.Destination)
                .FirstOrDefaultAsync(i => i.TourId == tourId);
            if (tour is null) throw new TourNotFoundException();
            tour.Name = request.Name;
            tour.Start = await AdressConverter.AdressToEfAsync(request.Start, tourId);
            tour.Distance = distance;
            tour.Duration = duration;
            tour.Description = request.Description;
            tour.Destination = await AdressConverter.AdressToEfAsync(request.Destination, tourId);
            tour.Type = request.Type;

            await this._context.SaveChangesAsync();

            return await GetTourAsync(tourId); 
        }

        public async Task<List<PresentationTour>> SearchAsync(string searchTerm)
        {
            var tours = await this._context.Tours.Where(i => i.Name.ToUpper().Contains(searchTerm) || i.Description.ToUpper().Contains(searchTerm) || i.Start.Country.ToUpper().Contains(searchTerm) || i.Start.Street.ToUpper().Contains(searchTerm) || i.Start.City.ToUpper().Contains(searchTerm) || i.Destination.Country.ToUpper().Contains(searchTerm) || i.Destination.Street.ToUpper().Contains(searchTerm) || i.Destination.City.ToUpper().Contains(searchTerm)).ToListAsync();
            var presentationTours = new List<PresentationTour>();
            if(tours is not null) tours.ForEach(async tour => presentationTours.Add(await TourConverter.ToursToPresentationTour(tour)));

            return (presentationTours);
        }

        public async Task<PresentationTour> ImportTourAsync(PresentationTour tour)
        {
            var check = await this._context.Tours.FirstOrDefaultAsync(i => i.TourId == tour.TourId);
            if (check != null) throw new TourAlreadyExistsException();
            this._context.Tours.Add(await TourConverter.PresentationTourToTours(tour));
            await this._context.SaveChangesAsync();

            return tour;
        }
    }
}
