﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using Tour_planner.Business;

namespace Tour_planner.UI.Models
{
    public class TourModel : INotifyPropertyChanged
    {
        private string _id;
        private string _name; 
        private string _description;     
        private string _duration { get; set; }
        private double _distance { get; set; }
        private string _type { get; set; }
        private Address _start { get; set; }
        private Address _destination { get; set; }
        private BitmapImage _image { get; set; }
        private string _route { get; set; }


        public TourModel()
        {
            Start = new Address();
            Destination = new Address();
        }
        public TourModel(Tour tour)
        {
            Id = tour.Id;
            Name = tour.Name;
            Description = tour.Description;
            Duration = tour.Duration;
            Distance = tour.Distance;
            Type = tour.Type;
            Start = tour.Start;
            Destination = tour.Destination;

        }

        public string Id { get { return _id; } set { _id = value; OnPropertyChanged(Id); } }   
        public string Name { get { return _name; } set { _name = value; OnPropertyChanged(Name); } }
        public string Description { get { return _description; } set { _description = value; OnPropertyChanged(Description); } }
        public string Type { get { return _type; } set { _type = value; OnPropertyChanged(Type); } }
        public Address Start { get { return _start; } set { _start = value; OnPropertyChanged(Start.Country); } }
        public string StartCountry { get { return Start.Country; } set { Start.Country = value; OnPropertyChanged(StartCountry); } }
        public string StartCity { get { return Start.City; } set { Start.City = value; OnPropertyChanged(StartCity); } }
        public string StartPostalCode { get { return Start.PostalCode; } set{ Start.PostalCode = value; OnPropertyChanged(StartPostalCode); } }
        public string StartStreet { get { return Start.Street; } set{ Start.Street = value; OnPropertyChanged(StartStreet); } }
        public string StartHouseNumber { get { return Start.HouseNumber; } set{ Start.HouseNumber = value; OnPropertyChanged(StartHouseNumber); } }
        public Address Destination { get { return _destination; } set { _destination = value; OnPropertyChanged(Destination.Country); } }
        public string DestinationCountry { get { return Destination.Country; } set { Destination.Country = value; } }
        public string DestinationCity { get { return Destination.City; } set { Destination.City = value; } }
        public string DestinationPostalCode { get { return Destination.PostalCode; } set { Destination.PostalCode = value; } }
        public string DestinationStreet { get { return Destination.Street; } set { Destination.Street = value; } }
        public string DestinationHouseNumber { get { return Destination.HouseNumber; } set { Destination.HouseNumber = value; } }
        public double Distance { get { return _distance; } set { _distance = value; OnPropertyChanged(Distance.ToString()); } }
        public string Duration { get { return _duration; } set { _duration = value; OnPropertyChanged(Duration.ToString()); } }
        public BitmapImage Image { get { return _image; } set { _image = value; } }
        public string TravelRoute { get { return Start.FullAddress + " - " + Destination.FullAddress; } }
        public string DistanceCalucalted { get { return "Estimated distance: " + Math.Round(Distance, 2) + " km"; } }
        public string DurationCalculated { get { return "Estimated duration: " + Duration.ToString(); } }
        


        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

    }
}
