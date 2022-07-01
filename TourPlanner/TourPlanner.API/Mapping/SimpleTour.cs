namespace TourPlanner.API.Data
{
    public class SimpleTour
    {
        public string Name { get; set; } = String.Empty;
        public string Description { get; set; } = String.Empty;
        public DateTime Duration { get; set; }
        public double Distance { get; set; }
        public string Type { get; set; } = String.Empty;
        public string Start { get; set; } = String.Empty;
        public string Destination { get; set; } = String.Empty;
    }
}
