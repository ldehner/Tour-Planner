using TourPlanner.API.Data;
using TourPlanner.API.Mapping;

namespace TourPlanner.API.DAL
{
    public interface ITourRepository
    {
        public Task<PresentationTour> AddTourAsync(SimpleTour tour, double distance, TimeSpan duration);
        public Task<List<PresentationTour>> GetToursAsync();

        public Task<PresentationTour> GetTourAsync(Guid tourId);

        public Task<PresentationTour> UpdateTourAsync(Guid tourId, SimpleTour tour, double distance, TimeSpan duration);

        public Task<List<PresentationTour>> DeleteTourAsync(Guid tourId);
        public Task<List<PresentationTour>> SearchAsync(string searchTerm);
    }
}
