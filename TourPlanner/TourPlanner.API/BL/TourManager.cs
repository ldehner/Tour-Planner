using TourPlanner.API.DAL;
using TourPlanner.API.Data;
using TourPlanner.API.Mapping;

namespace TourPlanner.API.BL
{
    public class TourManager : ITourManager
    {
        private readonly ITourRepository _tourRepository;
        public TourManager(ITourRepository tourRepository)
        {
            _tourRepository = tourRepository;
        }

        public async Task<PresentationTour> AddTourAsync(SimpleTour tour)
        {
            return await _tourRepository.AddTourAsync(tour);
        }

        public async Task<List<PresentationTour>> DeleteTourAsync(Guid tourId)
        {
            return await _tourRepository.DeleteTourAsync(tourId);
        }

        public async Task<PresentationTour> GetTourAsync(Guid tourId)
        {
            return await _tourRepository.GetTourAsync(tourId);
        }

        public async Task<List<PresentationTour>> GetToursAsync()
        {
            return await _tourRepository.GetToursAsync();
        }

        public async Task<PresentationTour> UpdateTourAsync(Guid tourId, SimpleTour tour)
        {
            return await _tourRepository.UpdateTourAsync(tourId, tour);
        }
    }
}
