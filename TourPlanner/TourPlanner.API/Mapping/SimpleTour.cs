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

        public SimpleTour(string name, string description, string type, Adress start, Adress destination)
        {
            Name = name;
            Description = description;
            Type = type;
            Start = start;
            Destination = destination;
        }
    }
}
