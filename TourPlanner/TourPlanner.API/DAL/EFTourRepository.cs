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

namespace TourPlanner.API.DAL
{
    public class EFTourRepository : ITourRepository
    {
        private readonly ToursDataContext _context;
        public EFTourRepository(ToursDataContext toursDataContext) { _context = toursDataContext; }

        public async Task<PresentationTour> AddTourAsync(SimpleTour tour)
        {
            Tours tours = new Tours()
            {
                Description = tour.Description,
                Distance = tour.Distance,
                Duration = tour.Duration.TimeOfDay,
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
            if (tour == null) return null;
            this._context.Tours.Remove(tour); 
            await this._context.SaveChangesAsync();
            var tours = await this._context.Tours.ToListAsync();
            var presentationTours = new List<PresentationTour>();
            tours.ForEach(async tour => presentationTours.Add(await TourConverter.ToursToPresentationTour(tour)));

            return (presentationTours);
        }

        public async Task<PresentationTour> GetTourAsync(Guid tourId)
        {
            var tour = await this._context.Tours.Include(i => i.Logs)
                .FirstOrDefaultAsync(i => i.TourId == tourId);
            if (tour == null) return null;
            return await TourConverter.ToursToPresentationTour(tour);
        }

        public async Task<List<PresentationTour>> GetToursAsync()
        {
            List<Tours> tours = await this._context.Tours.Include(l => l.Logs).ToListAsync();
            var presentationTours = new List<PresentationTour>();
            tours.ForEach(async tour => presentationTours.Add(await TourConverter.ToursToPresentationTour(tour)));

            return (presentationTours);
        }

        public async Task<PresentationTour> UpdateTourAsync(Guid tourId, SimpleTour request)
        {
            var tour = await this._context.Tours.Include(i => i.Logs)
                .FirstOrDefaultAsync(i => i.TourId == tourId);
            if (tour == null) return null;
            tour.Name = request.Name;
            tour.Duration = request.Duration.TimeOfDay;
            tour.Start = request.Start;
            tour.Description = request.Description;
            tour.Destination = request.Destination;
            tour.Type = request.Type;

            await this._context.SaveChangesAsync();

            return await TourConverter.ToursToPresentationTour(await _context.Tours.Include(i => i.Logs)
                .FirstOrDefaultAsync(i => i.TourId == tourId)); 
        }
    }
}
