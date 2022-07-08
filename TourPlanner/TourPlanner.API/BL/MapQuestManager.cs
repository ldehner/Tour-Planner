using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Dynamic;
using TourPlanner.API.DAL;
using TourPlanner.API.Mapping;

namespace TourPlanner.API.BL
{
    public class MapQuestManager : IMapQuestManager
    {
        private readonly IMapRepository _mapRepository;
        private readonly string _apiKey;

        public MapQuestManager(IMapRepository mapRepository, string apiKey)
        {
            _mapRepository = mapRepository;
            _apiKey = apiKey;
        }

        public async Task DeleteMapAsync(Guid TourId)
        {
            await _mapRepository.DeletePicture(TourId);
        }

        public async Task<byte[]> GetMapAsync(string sessionId)
        {
            HttpClient HttpClient = new();
            var result = await HttpClient.GetAsync("https://www.mapquestapi.com/staticmap/v5/map?key=" + _apiKey + " + &session=" + sessionId + "&routeArc=true&size=@2x");
            HttpClient.Dispose();
            return await result.Content.ReadAsByteArrayAsync();
        }

        public async Task<MapQuestRouteResult> GetRouteAsync(Adress from, Adress to, string type)
        {
            HttpClient HttpClient = new();
            var result = await HttpClient.GetAsync("https://www.mapquestapi.com/directions/v2/route" + "?key=" + _apiKey + "&from=" + from.GetAdressString() + "&to=" + to.GetAdressString() + "&routeType=" + type + "&unit=k");
            var content = await result.Content.ReadAsStringAsync();
            HttpClient.Dispose();
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            dynamic json = JsonConvert.DeserializeObject<ExpandoObject>(content, new ExpandoObjectConverter());
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
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
