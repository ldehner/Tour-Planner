using TourPlanner.API.Data;
using TourPlanner.API.Mapping;

namespace TourPlanner.API.DAL
{
    public interface ITourLogRepository
    {
        public Task<PresentationLog> AddLogAsync(Guid TourId, SimpleLog log);
        public Task<PresentationLog> GetLogAsync(Guid tourId, Guid logId);
        public Task<List<PresentationLog>> GetLogsAsync(Guid tourId);
        public Task<PresentationLog> UpdateLogAsync(Guid tourId, Guid logId, SimpleLog log);
        public Task<List<PresentationLog>> DeleteLogAsync(Guid tourId, Guid logId);
    }
}
