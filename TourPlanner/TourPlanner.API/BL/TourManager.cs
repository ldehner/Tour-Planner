using TourPlanner.API.DAL;

namespace TourPlanner.API.BL
{
    public class TourManager : ITourManager
    {
        private readonly ITourRepository _tourRepository;
        public TourManager(ITourRepository tourRepository)
        {
            _tourRepository = tourRepository;
        }
    }
}
