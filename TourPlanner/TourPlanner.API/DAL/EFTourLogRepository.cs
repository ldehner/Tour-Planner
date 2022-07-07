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
        public async Task<PresentationTour> AddLogAsync(Guid TourId, SimpleLog log)
        {
            var tour = await this._context.Tours.Include(tour => tour.Logs).Include(tour => tour.Start).Include(tour => tour.Destination)
                .FirstOrDefaultAsync(i => i.TourId == TourId);
            if (tour is null) throw new TourNotFoundException();

            var newlog = new Logs
            {
                TourId = TourId,
                Comment = log.Comment,
                Date = log.Date,
                Rating = log.Rating,
                Duration = log.Duration.TimeOfDay,
                Difficulty = log.Difficulty,
            };
            tour.Logs.Add(newlog);

            this._context.SaveChanges();

            return await TourConverter.ToursToPresentationTour(await this._context.Tours.Include(i => i.Logs)
                .FirstOrDefaultAsync(i => i.TourId == TourId)); 
        }

        public async Task<PresentationTour> DeleteLogAsync(Guid tourId, Guid logId)
        {
            var tour = await this._context.Tours.FindAsync(tourId);
            if (tour is null) throw new TourNotFoundException();
            Logs findLog = null;
            tour.Logs.ForEach(log =>
            {
                if (log.LogId.CompareTo(logId) == 0) findLog = log;
            });
            if (findLog is null) throw new LogNotFoundException();
            tour.Logs.Remove(findLog);
            await this._context.SaveChangesAsync();
            return await TourConverter.ToursToPresentationTour(await this._context.Tours.FindAsync(tourId));
        }

        public async Task<PresentationLog> GetLogAsync(Guid tourId, Guid logId)
        {
            var tour = await this._context.Tours.Include(i => i.Logs)
                .FirstOrDefaultAsync(i => i.TourId == tourId);
            if (tour is null) throw new TourNotFoundException();
            Logs findLog = null;
            tour.Logs.ForEach(log =>
            {
                if (log.LogId.CompareTo(logId) == 0) findLog = log;
            });
            if (findLog is null) throw new LogNotFoundException();
            return await LogConverter.LogsToPresentationLog(findLog);
        }

        public async Task<List<PresentationLog>> GetLogsAsync(Guid tourId)
        {
            var tour = await this._context.Tours.Include(i => i.Logs)
                .FirstOrDefaultAsync(i => i.TourId == tourId);
            if (tour is null) throw new TourNotFoundException();
            return await LogConverter.LogsListToPresentationLogList(tour.Logs);
        }

        public async Task<List<PresentationLog>> UpdateLogAsync(Guid tourId, Guid logId, SimpleLog log)
        {
            var tour = await this._context.Tours.FindAsync(tourId);
            if (tour is null) throw new TourNotFoundException();
            Logs findLog = null;
            tour.Logs.ForEach(log =>
            {
                if (log.LogId.CompareTo(logId) == 0) findLog = log;
            });
            if (findLog is null) throw new LogNotFoundException();
            tour.Logs.Remove(findLog);
            tour.Logs.Add(await LogConverter.SimpleLogToLogs(log, tourId));
            await this._context.SaveChangesAsync();
            return await LogConverter.LogsListToPresentationLogList((await this._context.Tours.FindAsync(tourId)).Logs);
        }
    }
}
