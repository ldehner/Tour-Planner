using TourPlanner.API.DAL;
using TourPlanner.API.Data;
using TourPlanner.API.Mapping;

namespace TourPlanner.API.BL
{
    public class TourLogManager : ITourLogManager
    {
        private readonly ITourLogRepository _tourLogRepository;
        public TourLogManager(ITourLogRepository tourLogRepository)
        {
            _tourLogRepository = tourLogRepository;
        }
        public async Task<PresentationTour> AddLogAsync(Guid TourId, SimpleLog log)
        {
            return await _tourLogRepository.AddLogAsync(TourId, log);
        }

        public async Task<PresentationTour> DeleteLogAsync(Guid tourId, Guid logId)
        {
            return await _tourLogRepository.DeleteLogAsync(tourId, logId);
        }

        public async Task<PresentationLog> GetLogAsync(Guid tourId, Guid logId)
        {
            return await _tourLogRepository.GetLogAsync(tourId, logId);
        }

        public async Task<List<PresentationLog>> GetLogsAsync(Guid tourId)
        {
            return await _tourLogRepository.GetLogsAsync(tourId);
        }

        public async Task<List<PresentationLog>> UpdateLogAsync(Guid tourId, Guid logId, SimpleLog log)
        {
            return await _tourLogRepository.UpdateLogAsync(tourId, logId, log);
        }
    }
}
