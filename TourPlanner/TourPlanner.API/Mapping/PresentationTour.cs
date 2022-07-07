namespace TourPlanner.API.Mapping
{
    public class PresentationTour
    {
        public Guid TourId { get; set; }
        public string Name { get; set; } = String.Empty;
        public string Description { get; set; } = String.Empty;
        public TimeSpan Duration { get; set; }
        public double Distance { get; set; }
        public string Type { get; set; } = String.Empty;
        public Adress Start { get; set; }
        public Adress Destination { get; set; }
        public List<PresentationLog> Logs { get; set; }
    }
}
