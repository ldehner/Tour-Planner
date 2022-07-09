using Microsoft.EntityFrameworkCore;
using TourPlanner.API.Data;
using TourPlanner.API.Exceptions;
using TourPlanner.API.Mapping;
using TourPlanner.Data;

namespace TourPlanner.API.DAL
{
    public class EFTourLogRepository : ITourLogRepository
    {
        private readonly DbContextOptions<ToursDataContext> _options;
        public EFTourLogRepository( DbContextOptions<ToursDataContext> options) { 
            _options = options;
        }
        public async Task<PresentationLog> AddLogAsync(Guid TourId, SimpleLog log)
        {
            using(var context = new ToursDataContext(_options)) {
                var tour = await context.Tours.Include(tour => tour.Logs).Include(tour => tour.Start).Include(tour => tour.Destination)
                    .FirstOrDefaultAsync(i => i.TourId == TourId);
                if (tour is null) throw new TourNotFoundException();

                var logs = await LogConverter.SimpleLogToLogs(log, TourId);
                tour.Logs.Add(logs);

                context.SaveChanges();

                return await LogConverter.LogsToPresentationLog(logs);
            }
            
        }

        public async Task<List<PresentationLog>> DeleteLogAsync(Guid tourId, Guid logId)
        {
            using (var context = new ToursDataContext(_options)) {
                var log = await context.Logs
                    .FirstOrDefaultAsync(i => i.LogId == logId);
                if (log is null) throw new LogNotFoundException();
                context.Logs.RemoveRange(log);
                await context.SaveChangesAsync();
                return await GetLogsAsync(tourId);
            }
            
        }

        public async Task<PresentationLog> GetLogAsync(Guid tourId, Guid logId)
        {
            using (var context = new ToursDataContext(_options)) {
                var log = await context.Logs
                    .FirstOrDefaultAsync(i => i.TourId == tourId && i.LogId == logId);
                if (log is null) throw new LogNotFoundException();
                return await LogConverter.LogsToPresentationLog(log);
            }
            
        }

        public async Task<List<PresentationLog>> GetLogsAsync(Guid tourId)
        {
            using (var context = new ToursDataContext(_options)) {
                var logs = await context.Logs.Where(i => i.TourId == tourId).ToListAsync();
                if (logs is null) throw new TourNotFoundException();
                return await LogConverter.LogsListToPresentationLogList(logs);
            }
            
        }

        public async Task<PresentationLog> UpdateLogAsync(Guid tourId, Guid logId, SimpleLog request)
        {
            using(var context = new ToursDataContext(_options))
            {
                var log = await context.Logs
                .FirstOrDefaultAsync(i => i.TourId == tourId && i.LogId == logId);
                if (log is null) throw new LogNotFoundException();
                var date = request.Date.Split("-");
                var time = request.Duration.Split(":");
                log.Comment = request.Comment;
                log.Rating = request.Rating;
                log.Difficulty = request.Difficulty;
                log.Date = new DateTime(Int16.Parse(date[0]), Int16.Parse(date[1]), Int16.Parse(date[2]));
                log.Duration = new TimeSpan(Int16.Parse(time[0]), Int16.Parse(time[1]), Int16.Parse(time[2]), 0);
                await context.SaveChangesAsync();
                return await GetLogAsync(tourId, logId);
            }
            
        }
    }
}
