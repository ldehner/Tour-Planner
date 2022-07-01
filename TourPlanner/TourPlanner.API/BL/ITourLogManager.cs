using TourPlanner.API.Data;

namespace TourPlanner.API.BL
{
    public interface ITourLogManager
    {
        public Task<SimpleLog> AddLogAsync(SimpleLog log);
        public Task<SimpleLog> GetLogAsync(Guid logId);
        public Task<List<SimpleLog>> GetLogsAsync();
        public Task<SimpleLog> UpdateLogAsync(Guid logId, SimpleLog log);
        public Task DeleteLogAsync(Guid logId);
    }
}
