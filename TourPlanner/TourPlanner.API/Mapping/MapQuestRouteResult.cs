namespace TourPlanner.API.Mapping
{
    public class MapQuestRouteResult
    {
        public MapQuestRouteResult(double distance, TimeSpan time, string sessionId)
        {
            Distance = distance;
            Time = time;
            SessionId = sessionId;
        }

        public double Distance { get; set; }
        public TimeSpan Time { get; set; }
        public string SessionId { get; set; }
    }
}
