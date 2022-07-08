using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tour_planner.Business
{
    public class Tour
    {
        public string Id { get; set; }
        public string Name { get; set; } = String.Empty;
        public string Description { get; set; } = String.Empty;
        public string Duration { get; set; }
        public double Distance { get; set; }
        public string Type { get; set; } = String.Empty;
        public Address Start { get; set; }
        public Address Destination { get; set; }

        public Tour()
        {

        }

        public Tour(string Id, string Name, string Description, string Duration, double Distance, string Type, Address Start, Address Destination)
        {
            this.Id = Id;
            this.Name = Name;
            this.Description = Description;
            this.Duration = Duration;
            this.Distance = Distance;
            this.Type = Type;
            this.Start = Start;
            this.Destination = Destination;
        }


    }
}
