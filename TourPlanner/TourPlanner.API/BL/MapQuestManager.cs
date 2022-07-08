using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Dynamic;
using TourPlanner.API.DAL;
using TourPlanner.API.Exceptions;
using TourPlanner.API.Mapping;

namespace TourPlanner.API.BL
{
    public class MapQuestManager : IMapQuestManager
    {
        private readonly IMapRepository _mapRepository;
        private readonly IMapQuestRepository _mapQuestRepository;

        public MapQuestManager(IMapRepository mapRepository, IMapQuestRepository mapQuestRepository)
        {
            _mapRepository = mapRepository;
            _mapQuestRepository = mapQuestRepository;
        }

        public async Task DeleteMapAsync(Guid TourId)
        {
            await _mapRepository.DeletePicture(TourId);
        }

        public async Task<byte[]> GetMapAsync(string sessionId)
        {
            return await _mapQuestRepository.GetMapAsync(sessionId);
        }

        public async Task<MapQuestRouteResult> GetRouteAsync(Adress from, Adress to, string type)
        {
            var content = await _mapQuestRepository.GetRouteAsync(from, to, type);
            #pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            dynamic json = JsonConvert.DeserializeObject<ExpandoObject>(content, new ExpandoObjectConverter());
            #pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            if (json.info.statuscode != 0) throw new InvalidAdressException();
            string time = json.route.formattedTime;
            var timeArray = time.Split(":");
            return new MapQuestRouteResult(json.route.distance, new TimeSpan(int.Parse(timeArray[0]), int.Parse(timeArray[1]), int.Parse(timeArray[2])) ,json.route.sessionId);
        }

        public async Task<byte[]> LoadMapAsync(Guid TourId)
        {
            return await _mapRepository.GetPicture(TourId);
        }

        public async Task SaveMapAsync(Guid mapId, byte[] image)
        {
            await _mapRepository.SavePicture(mapId, image);
        }

        public async Task UpdateMapAsync(Guid mapId, byte[] image)
        {
            await DeleteMapAsync(mapId);
            await SaveMapAsync(mapId, image);
        }
    }
}
