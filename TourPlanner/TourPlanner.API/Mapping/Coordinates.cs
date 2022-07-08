namespace TourPlanner.API.Mapping
{
    public class Coordinates
    {
        public string Long { get; set; }
        public string Lat { get; set; }

        public Coordinates(string lat, string lon)
        {
            Long = lon;
            Lat = lat;
        }
    }
}
