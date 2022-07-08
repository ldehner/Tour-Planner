using TourPlanner.API.Mapping;

namespace TourPlanner.API.DAL
{
    public interface ICoordinatesRepository
    {
        public Task<Coordinates> GetCoordinatesAsync(string adress);
    }
}
