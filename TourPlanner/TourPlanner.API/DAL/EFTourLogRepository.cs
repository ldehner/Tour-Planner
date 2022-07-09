using Microsoft.EntityFrameworkCore;
using TourPlanner.API.Data;
using TourPlanner.API.Exceptions;
using TourPlanner.API.Mapping;
using TourPlanner.Data;

namespace TourPlanner.API.DAL
{
    public class EFTourLogRepository : ITourLogRepository
    {
        private ToursDataContext _context;
        public EFTourLogRepository(ToursDataContext toursDataContext) { _context = toursDataContext; }
        public async Task<PresentationLog> AddLogAsync(Guid TourId, SimpleLog log)
        {
            var tour = await this._context.Tours.Include(tour => tour.Logs).Include(tour => tour.Start).Include(tour => tour.Destination)
                .FirstOrDefaultAsync(i => i.TourId == TourId);
            if (tour is null) throw new TourNotFoundException();

            var logs = await LogConverter.SimpleLogToLogs(log, TourId);
            tour.Logs.Add(logs);

            this._context.SaveChanges();

            return await LogConverter.LogsToPresentationLog(logs); 
        }

        public async Task<List<PresentationLog>> DeleteLogAsync(Guid tourId, Guid logId)
        {
            var log = await this._context.Logs
                .FirstOrDefaultAsync(i => i.TourId == tourId);
            if (log is null) throw new LogNotFoundException();
            this._context.Logs.RemoveRange(log);
            await this._context.SaveChangesAsync();
            return await GetLogsAsync(tourId);
        }

        public async Task<PresentationLog> GetLogAsync(Guid tourId, Guid logId)
        {
            var log = await this._context.Logs
                .FirstOrDefaultAsync(i => i.TourId == tourId && i.LogId == logId);
            if (log is null) throw new LogNotFoundException();
            return await LogConverter.LogsToPresentationLog(log);
        }

        public async Task<List<PresentationLog>> GetLogsAsync(Guid tourId)
        {
            var logs = await this._context.Logs.Where(i => i.TourId == tourId).ToListAsync();
            if (logs is null) throw new TourNotFoundException();
            return await LogConverter.LogsListToPresentationLogList(logs);
        }

        public async Task<PresentationLog> UpdateLogAsync(Guid tourId, Guid logId, SimpleLog request)
        {
            var log = await this._context.Logs
                .FirstOrDefaultAsync(i => i.TourId == tourId && i.LogId == logId);
            if (log is null) throw new LogNotFoundException();
            var date = request.Date.Split("-");
            var time = request.Duration.Split(":");
            log.Comment = request.Comment;
            log.Rating = request.Rating;
            log.Difficulty = request.Difficulty;
            log.Date = new DateTime(Int16.Parse(date[0]), Int16.Parse(date[1]), Int16.Parse(date[2]));
            log.Duration = new TimeSpan(Int16.Parse(time[0]), Int16.Parse(time[1]), Int16.Parse(time[2]), 0);

            return await GetLogAsync(tourId, logId);
        }
    }
}
