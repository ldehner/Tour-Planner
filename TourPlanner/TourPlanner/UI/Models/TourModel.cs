using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using TourPlanner.Business;

namespace TourPlanner.UI.Models
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
        private string _imageLocation { get; set; }


        public BitmapImage Image{
            get
            {
                return _image;
            }
            set
            {
                _image = value;
                OnPropertyChanged("Image");
            }
        }

        public string ImageLocation
        {
            get
            {
                return _imageLocation;
            }
            set
            {
                _imageLocation = value;
                OnPropertyChanged("ImageLocation");
            }
        }
        public Dictionary<string, TourLog> LogList { get; set; }


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
            LogList = tour.LogList;

            TourLogList = new ObservableCollection<TourLogModel>();

            foreach (KeyValuePair<string, TourLog> entry in LogList)
            {
                TourLogModel newtourLog = new TourLogModel(entry.Value);

                TourLogList.Add(newtourLog);
            }

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
        public string TravelRoute { get { return Start.FullAddress + " - " + Destination.FullAddress; } }
        public string DistanceCalucalted { get { return "Estimated distance: " + Math.Round(Distance, 2) + " km"; } }
        public string DurationCalculated { get { return "Estimated duration: " + Duration.ToString(); } }
        public string StartWeather { get { return Start.City+", " + Start.Country +": " + Start.Weather; } }
        public string DestinationWeather { get { return Destination.City + ", " + Destination.Country + ": " + Destination.Weather; } }
        public string ChildFriendliness { get { return "Childfriendliness: "+ CalculateChildFriendliness(); } }
        public string Popularity { get { return "Popularity: "+ CalculatePopularity(); } }

        private ObservableCollection<TourLogModel> _tourLogList;

        public ObservableCollection<TourLogModel> TourLogList
        {
            get
            {
                return _tourLogList;
            }

            set { _tourLogList = value; }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        public string CalculateChildFriendliness()
        {
            if(TourLogList.Count == 0)
            {
                return "Not yet known";
            }
            int difficulty = 0;

            foreach (var item in TourLogList)
            {
                difficulty += item.Difficulty;
            }

            if(difficulty / TourLogList.Count > 2)
            {
                return "Not ChildFriendly";
            }
            else
            {
                return "Is ChildFriendly";
            }
        }

        public string CalculatePopularity()
        {
            int length = LogList.Count;
            if (length <= 0) {
                return "Not yet done";
            } else if (length > 0 && length < 5) {
                return "Done a few times";
            }else if(length > 10 && length < 15)
            {
                return "Absolut Banger";
            }
            else
            {
                return "Hotspot route";
            }
        }
    }
}
