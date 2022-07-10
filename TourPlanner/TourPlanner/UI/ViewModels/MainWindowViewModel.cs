using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using TourPlanner.Business;
using TourPlanner.Data_Access;
using TourPlanner.UI.Commands;
using TourPlanner.UI.Models;

namespace TourPlanner.UI.ViewModels
{

    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private Visibility visibility = Visibility.Collapsed;
        private Visibility logVisibility = Visibility.Collapsed;
        private Visibility errorMsgVisibility = Visibility.Collapsed;
        private IQuery requests;
        private TourModel _tour;
        private TourLogModel _log;
        private bool _isSelected = false;
        private bool _isLogSelected = false;
        private string _searchText;
        private string _errmsg;

        public Visibility Visibility
        {
            get { return visibility; }
            set { visibility = value; OnPropertyChanged("Visibility"); }
        }

        public Visibility LogVisibility
        {
            get { return logVisibility; }
            set { logVisibility = value; OnPropertyChanged("LogVisibility"); }
        }

        public Visibility ErrorMsgVisibility
        {
            get { return errorMsgVisibility; }
            set { errorMsgVisibility = value; OnPropertyChanged("ErrorMsgVisibility"); }
        }
        public bool isSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value; OnPropertyChanged("isSelected");
                if (value == true)
                {
                    Visibility = Visibility.Visible;
                }
                else
                {
                    Visibility = Visibility.Collapsed;
                }
            }
        }

        public bool isLogSelected
        {
            get { return _isLogSelected; }
            set
            {
                _isLogSelected = value; OnPropertyChanged("isLogSelected");
                if (value == true)
                {
                    LogVisibility = Visibility.Visible;
                }
                else
                {
                    LogVisibility = Visibility.Collapsed;
                }
            }
        }

        public TourModel? TourModel
        {
            get { return _tour; }
            set
            {
                if (value != null)
                {
                    _tour = value;
                    isSelected = true;
                    if (TourLogModel != null)
                    {
                        TourLogModel = null;
                        LogVisibility = Visibility.Collapsed;
                    }
                    OnPropertyChanged("TourModel");
                }
            }
        }
        public TourLogModel? TourLogModel
        {
            get { return _log; }
            set
            {
                if (value != null)
                {
                    _log = value;
                    isLogSelected = true;
                    OnPropertyChanged("TourLogModel");
                }

            }
        }


        public TourListModel _tourList;

        public ObservableCollection<TourModel> TourList
        {
            get { return _tourList.Tourlist; }
            set { _tourList.Tourlist = value; }
        }

        public string SearchText
        {
            get { return _searchText; }
            set { _searchText = value; }
        }


        public string ErrorMsg
        {
            get { return _errmsg; }
            set { 
                _errmsg = value; 
                if(value != "")
                {
                    ErrorMsgVisibility = Visibility.Visible;
                }
                else
                {
                    ErrorMsgVisibility = Visibility.Collapsed;
                }
                OnPropertyChanged("ErrorMsg"); }
        }

        public MainWindowViewModel()
        {
            requests = new Requests();
            _tourList = new TourListModel();
            LoadTours();
        }

        public async void LoadTours()
        {
            isSelected = false;
            isLogSelected = false;
            Tourlist tourList = await requests.GetTours();
            _tourList.Tourlist.Clear();
            if (tourList != null)
            {

                DirectoryInfo di = new DirectoryInfo("../../../img");


                foreach (FileInfo file in di.GetFiles())
                {
                    file.Delete();
                }
                foreach (DirectoryInfo dir in di.GetDirectories())
                {
                    dir.Delete(true);
                }


                

                Dictionary<string, Tour>.ValueCollection data = tourList.TourList.Values;



                foreach (Tour t in data)
                {
                    string img = await requests.GetImageBytes(t.Id);

                    string result = img.Substring(1, img.Length - 2);
                    BitmapImage image = new BitmapImage();
                    byte[] binaryImg;

                    binaryImg = Convert.FromBase64String(result);

                    if (result != "")
                    {
                        image.BeginInit();
                        image.StreamSource = new MemoryStream(binaryImg);
                        image.EndInit();

                    }


                    string location = "../../../img/" + t.Id + ".jpg";

                    File.WriteAllBytes(location, binaryImg);

                    string jsonWeather = await requests.GetWeatherFromLocation(t.Start.City + ", " + t.Start.Country, t.Destination.City + ", " + t.Destination.Country);

                    JObject weather = JObject.Parse(jsonWeather);

                    t.Start.Weather = (string)weather["fromTemp"] + "°C - " + (string)weather["fromCondition"];
                    t.Destination.Weather = (string)weather["toTemp"] + "°C - " + (string)weather["toCondition"];

                    _tourList.AddTourToList(t, image);
                }
            }

        }

        private ICommand searchTours;

        public ICommand SearchTours
        {
            get 
            {
                if (searchTours != null) {
                    return searchTours;
                }
                searchTours = new Command(() => LoadSearchedTours(), true);
                return searchTours;
            }
        }

        public async void LoadSearchedTours()
        {
            try
            {
                ErrorMsg = "";
                _tourList.Tourlist.Clear();
                Tourlist searchedTourList = await requests.GetToursBySearchTerm(_searchText);

                if (searchedTourList != null)
                {
                    

                    Dictionary<string, Tour>.ValueCollection data = searchedTourList.TourList.Values;

                    BitmapImage image = new BitmapImage();

                    foreach (Tour t in data)
                    {
                        _tourList.AddTourToList(t, image);
                    }
                }
            }
            catch
            {
                ErrorMsg = "No Tours Found!";
            }
        }

        private ICommand _deleteTourCommand;

        public ICommand DeleteTourCommand
        {
            get
            {
                if (_deleteTourCommand != null)
                {
                    return _deleteTourCommand;
                }
                return new Command(() => DeleteTour(), true);
            }

        }
        public void DeleteTour()
        {
            if (TourModel != null)
            {
                requests.DeleteTour(TourModel.Id);
                LoadTours();
                TourModel = null;
                isSelected = false;
            }
        }
        private ICommand _deleteLogCommand;

        public ICommand DeleteLogCommand
        {
            get
            {
                if (_deleteLogCommand != null)
                {
                    return _deleteLogCommand;
                }
                return new Command(() => DeleteLog(), true);
            }
        }

        public void DeleteLog()
        {
            if (TourLogModel != null && TourModel != null)
            {
                requests.DeleteLog(TourModel.Id, TourLogModel.Logid);
                LoadTours();
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private ICommand _createTotalReportCommand;

        public ICommand CreateTotalReportCommand
        {
            get { if(_createTotalReportCommand != null)
                {
                    return _createTotalReportCommand;
                }
                _createTotalReportCommand = new Command(() => CreateTotalReport(), true);
                return _createTotalReportCommand;
            }
        }

        public async void CreateTotalReport()
        {
            string report = await requests.GetReport();

            string result = report.Substring(1, report.Length - 2);

            byte[] pdf = Convert.FromBase64String(result);


            File.WriteAllBytes("../../../reports/AllTourReport.pdf", pdf);

        }

        private ICommand _createReportCommand;

        public ICommand CreateReportCommand
        {
            get
            {
                if (_createReportCommand != null)
                {
                    return _createReportCommand;
                }
                _createReportCommand = new Command(() => CreateReport(TourModel.Id), true);
                return _createReportCommand;
            }
        }

        public async void CreateReport(string Id)
        {
            string report = await requests.GetReportById(Id);

            string result = report.Substring(1, report.Length - 2);

            byte[] pdf = Convert.FromBase64String(result);


            File.WriteAllBytes("../../../reports/" + Id + ".pdf", pdf);

        }

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
