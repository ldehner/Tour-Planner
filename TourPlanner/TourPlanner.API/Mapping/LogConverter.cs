using TourPlanner.API.Data;
using TourPlanner.Data;

namespace TourPlanner.API.Mapping
{
    public static class LogConverter
    {
        public static async Task<PresentationLog> LogsToPresentationLog(Logs log)
        {
            return new PresentationLog
            {
                LogId = log.LogId,
                TourId = log.TourId,
                Date = log.Date,
                Duration = log.Duration,
                Comment = log.Comment,
                Difficulty = log.Difficulty,
                Rating = log.Rating,
            };
        }

        public static async Task<Logs> SimpleLogToLogs(SimpleLog log, Guid tourId)
        {
            return new Logs
            {
                TourId = tourId,
                Date = log.Date,
                Duration = log.Duration.TimeOfDay,
                Comment = log.Comment,
                Difficulty = log.Difficulty,
                Rating = log.Rating,
            };
        }

        public static async Task<List<PresentationLog>> LogsListToPresentationLogList(List<Logs> logs)
        {
            var list = new List<PresentationLog>();
            if(logs.Count > 0) logs.ForEach(async log => list.Add(await LogsToPresentationLog(log)));
            return list;
        }

        public static async Task<List<Logs>> PresentationLogListToLogsList(List<PresentationLog> logs)
        {
            var list = new List<Logs>();
            logs.ForEach(async log => list.Add(await PresentationLogToLogs(log)));
            return list;
        }

        public static async Task<Logs> PresentationLogToLogs(PresentationLog log)
        {
            return new Logs
            {
                LogId = log.LogId,
                TourId = log.TourId,
                Date = log.Date,
                Duration = log.Duration,
                Comment = log.Comment,
                Difficulty = log.Difficulty,
                Rating = log.Rating,
            };
        }
    }
}
