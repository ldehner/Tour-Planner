using TourPlanner.API.Data;

namespace TourPlanner.API.BL
{
    public class TourLogManager : ITourLogManager
    {
        public async Task<SimpleLog> AddLogAsync(SimpleLog log)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteLogAsync(Guid logId)
        {
            throw new NotImplementedException();
        }

        public async Task<SimpleLog> GetLogAsync(Guid logId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<SimpleLog>> GetLogsAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<SimpleLog> UpdateLogAsync(Guid logId, SimpleLog log)
        {
            throw new NotImplementedException();
        }
    }
}
