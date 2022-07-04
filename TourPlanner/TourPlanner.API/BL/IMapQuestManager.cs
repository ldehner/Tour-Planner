using TourPlanner.API.Mapping;

namespace TourPlanner.API.BL
{
    public interface IMapQuestManager
    {
        public Task<MapQuestRouteResult> GetRouteAsync(string from, string to, string type);
        public Task<byte[]> GetMapAsync(string sessionId);
        public Task SaveMapAsync(Guid mapId, byte[] image);
        public Task UpdateMapAsync(Guid mapId, byte[] image);
        public Task DeleteMapAsync(Guid TourId);
        public Task<byte[]> LoadMapAsync(Guid TourId);
    }
}
