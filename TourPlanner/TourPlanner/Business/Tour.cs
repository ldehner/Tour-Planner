using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tour_planner.Business
{
    public class Tour
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string TransportType { get; set; }
        public double Distance { get; set; }
        public string Time { get; set; }
        public string Info { get; set; }
        public string? ImageLocation { get; set; }
        public int Popularity { get; set; }
        public int Childfriendliness { get; set; }


        public Tour(string Name, string Description, string From, string To, string TransportType, double Distance, string Time, string Info, string ImageLocation)
        {
            this.Name = Name;
            this.Description = Description;
            this.From = From;
            this.To = To;
            this.TransportType = TransportType;
            this.Distance = Distance;
            this.Time = Time;
            this.Info = Info;
            this.ImageLocation = ImageLocation;
        }


    }
}
