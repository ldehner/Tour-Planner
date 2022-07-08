
using TourPlanner.API.Mapping;

namespace TourPlanner.API.DAL
{
    public class ApiMapQuestRepository : IMapQuestRepository
    {

        private readonly string _apiKey;

        public ApiMapQuestRepository(string apiKey)
        {
            _apiKey = apiKey;
        }
        public async Task<byte[]> GetMapAsync(string sessionId)
        {
            using var HttpClient = new HttpClient();
            var result = await HttpClient.GetAsync("https://www.mapquestapi.com/staticmap/v5/map?key=" + _apiKey + " + &session=" + sessionId + "&routeArc=true&size=@2x");
            HttpClient.Dispose();
            return await result.Content.ReadAsByteArrayAsync();
        }

        public async Task<string> GetRouteAsync(Adress from, Adress to, string type)
        {
            using var HttpClient = new HttpClient();
            var result = await HttpClient.GetAsync("https://www.mapquestapi.com/directions/v2/route" + "?key=" + _apiKey + "&from=" + from.GetAdressString() + "&to=" + to.GetAdressString() + "&routeType=" + type + "&unit=k");
            HttpClient.Dispose();
            return await result.Content.ReadAsStringAsync();
        }
    }
}
