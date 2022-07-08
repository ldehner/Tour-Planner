using TourPlanner.API.Mapping;

namespace TourPlanner.API.DAL
{
    public interface IMapQuestRepository
    {
        public Task<string> GetRouteAsync(Adress from, Adress to, string type);
        public Task<byte[]> GetMapAsync(string sessionId);
    }
}
