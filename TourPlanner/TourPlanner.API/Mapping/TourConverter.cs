using TourPlanner.API.Data;
using TourPlanner.Data;

namespace TourPlanner.API.Mapping
{
    public static class TourConverter
    {
        public async static Task<SimpleTour> ToursToSimpleTour(Tours tour)
        {
            return new SimpleTour
            {
                Name = tour.Name,
                Description = tour.Description,
                Type = tour.Type,
                Start = tour.Start,
                Destination = tour.Destination
            };
        }

        public async static Task<PresentationTour> ToursToPresentationTour(Tours tour){
            return new PresentationTour
            {
                TourId = tour.TourId,
                Name = tour.Name,
                Description = tour.Description,
                Duration = tour.Duration,
                Distance = tour.Distance,
                Type = tour.Type,
                Start = tour.Start,
                Destination = tour.Destination,
                Logs = await LogConverter.LogsListToPresentationLogList(tour.Logs),
            };
        }

        public async static Task<SimpleTour>PresentationTourToSimpleTour(PresentationTour tour)
        {
            DateTime dt = new DateTime(2022, 01, 01);
            dt = dt + tour.Duration;
            return new SimpleTour
            {
                Name = tour.Name,
                Description = tour.Description,
                Type = tour.Type,
                Start = tour.Start,
                Destination = tour.Destination,
            };
        }

        public async static Task<Tours> PresentationTourToTours(PresentationTour tour)
        {
            return new Tours
            {
                TourId = tour.TourId,
                Name = tour.Name,
                Description = tour.Description,
                Duration = tour.Duration,
                Distance = tour.Distance,
                Type = tour.Type,
                Start = tour.Start,
                Destination = tour.Destination,
                Logs = await LogConverter.PresentationLogListToLogsList(tour.Logs),
            };
        }
    }
}
