﻿using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Dynamic;
using TourPlanner.API.Mapping;

namespace TourPlanner.API.BL
{
    public class MapQuestManager : IMapQuestManager
    {
        public async Task<byte[]> GetMapAsync(string sessionId)
        {
            HttpClient HttpClient = new();
            var result = await HttpClient.GetAsync("https://www.mapquestapi.com/staticmap/v5/map?key=M5qGCXac3rjCtRpRe2aRBlxK3GiVKnnE&session=" + sessionId + "&routeArc=true&size=@2x");
            return await result.Content.ReadAsByteArrayAsync();
        }

        public async Task<MapQuestRouteResult> GetRouteAsync(string from, string to, string type)
        {
            HttpClient HttpClient = new();
            var result = await HttpClient.GetAsync("https://www.mapquestapi.com/directions/v2/route" + "?key=" + "M5qGCXac3rjCtRpRe2aRBlxK3GiVKnnE" + "&from=" + from + "&to=" + to + "&routeType=" + type + "&unit=k");
            var content = await result.Content.ReadAsStringAsync();
            dynamic json = JsonConvert.DeserializeObject<ExpandoObject>(content, new ExpandoObjectConverter());
            string time = json.route.formattedTime;
            var timeArray = time.Split(":");
            return new MapQuestRouteResult(json.route.distance, new TimeSpan(int.Parse(timeArray[0]), int.Parse(timeArray[1]), int.Parse(timeArray[2])) ,json.route.sessionId);
        }

        public Task SaveMapAsync()
        {
            throw new NotImplementedException();
        }
    }
}
