using TourPlanner.API.Mapping;

namespace TourPlanner.API.Data
{
    public class SimpleTour
    {
        public string Name { get; set; } = String.Empty;
        public string Description { get; set; } = String.Empty;
        public string Type { get; set; } = String.Empty;
        public Adress Start { get; set; }
        public Adress Destination { get; set; }
    }
}
