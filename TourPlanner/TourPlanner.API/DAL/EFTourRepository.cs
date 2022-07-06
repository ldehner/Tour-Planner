using TourPlanner.API.Data;
using TourPlanner.API.Mapping;
using TourPlanner.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;
using System.Security.Cryptography.Xml;
using TourPlanner.API.BL;
using TourPlanner.API.DAL;
using TourPlanner.API.Data;
using TourPlanner.Data;
using TourPlanner.API.Exceptions;

namespace TourPlanner.API.DAL
{
    public class EFTourRepository : ITourRepository
    {
        private readonly ToursDataContext _context;
        public EFTourRepository(ToursDataContext toursDataContext) { _context = toursDataContext; }

        public async Task<PresentationTour> AddTourAsync(SimpleTour tour, double distance, TimeSpan duration)
        {
            Tours tours = new Tours()
            {
                Name = tour.Name,
                Description = tour.Description,
                Distance = distance,
                Duration = duration,
                Type = tour.Type,
                Start = tour.Start,
                Destination = tour.Destination,
                Logs = new List<Logs>(),
            }; 
            this._context.Tours.Add(tours);
            await this._context.SaveChangesAsync();

            return await TourConverter.ToursToPresentationTour(tours);
        }
        //TODO
        public async Task<List<PresentationTour>> DeleteTourAsync(Guid tourId)
        {
            var tour = await this._context.Tours.FindAsync(tourId);
            if (tour is null) throw new TourNotFoundException();
            this._context.Tours.Remove(tour); 
            await this._context.SaveChangesAsync();

            return await GetToursAsync();
        }

        public async Task<PresentationTour> GetTourAsync(Guid tourId)
        {
            var tour = await this._context.Tours.Include(i => i.Logs)
                .FirstOrDefaultAsync(i => i.TourId == tourId);
            if (tour is null) throw new TourNotFoundException();
            return await TourConverter.ToursToPresentationTour(tour);
        }

        public async Task<List<PresentationTour>> GetToursAsync()
        {
            List<Tours> tours = await this._context.Tours.Include(l => l.Logs).ToListAsync();
            var presentationTours = new List<PresentationTour>();
            tours.ForEach(async tour => presentationTours.Add(await TourConverter.ToursToPresentationTour(tour)));

            return (presentationTours);
        }

        public async Task<PresentationTour> UpdateTourAsync(Guid tourId, SimpleTour request, double distance, TimeSpan duration)
        {
            var tour = await this._context.Tours.Include(i => i.Logs)
                .FirstOrDefaultAsync(i => i.TourId == tourId);
            if (tour is null) throw new TourNotFoundException();
            tour.Name = request.Name;
            tour.Start = request.Start;
            tour.Distance = distance;
            tour.Duration = duration;
            tour.Description = request.Description;
            tour.Destination = request.Destination;
            tour.Type = request.Type;

            await this._context.SaveChangesAsync();

            return await GetTourAsync(tourId); 
        }

        public async Task<List<PresentationTour>> SearchAsync(string searchTerm)
        {
            var tours = await this._context.Tours.Where(i => i.Name.ToUpper().Contains(searchTerm) || i.Description.ToUpper().Contains(searchTerm) || i.Start.ToUpper().Contains(searchTerm) || i.Destination.ToUpper().Contains(searchTerm)).ToListAsync();
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
