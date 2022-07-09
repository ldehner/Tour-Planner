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
using Tour_planner.Business;
using Tour_planner.Data_Access;
using Tour_planner.UI.Commands;
using Tour_planner.UI.Models;

namespace Tour_planner.UI.ViewModels
{

    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private Visibility visibility = Visibility.Collapsed;
        private Visibility logVisibility = Visibility.Collapsed; 

        private IQuery requests;
        private TourModel _tour;
        private TourLogModel _log;
        private bool _isSelected = false;
        private bool _isLogSelected = false;



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
        public bool isSelected
        {
            get { return _isSelected; }
            set { _isSelected = value; OnPropertyChanged("isSelected"); 
                if (value == true) { 
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
            set { _isLogSelected = value; OnPropertyChanged("isLogSelected");
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
            set {
                if (value != null)
                {
                    _tour = value;
                    isSelected = true;
                    OnPropertyChanged("TourModel");
                }
            }
        }
        public TourLogModel? TourLogModel 
        { 
            get { return _log; } 
            set { 
                if(value != null)
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
            set { _tourList.Tourlist = value;}
        }


        public MainWindowViewModel()
        {
            requests = new Requests();
            _tourList = new TourListModel();
            LoadTours();
        }

        public async void LoadTours()
        {
            
            Tourlist tourList = await requests.GetTours();

            if(tourList != null)
            {
                _tourList.Tourlist.Clear();

                Dictionary<string, Tour>.ValueCollection data = tourList.TourList.Values;

                BitmapImage image = new BitmapImage();

                foreach(Tour t in data)
                {
                    string img = await requests.GetImageBytes(t.Id);

                    string result = img.Substring(1, img.Length - 2);

                    byte[] binaryImg = Convert.FromBase64String(result);

                    
                    File.WriteAllBytes(t.Id + ".jpg", binaryImg);

                    _tourList.AddTourToList(t, image);
                }
            }
            CreateReport();
        }

        public async void LoadSearchedTours(string searchTerm)
        {
            Tourlist searchedTourList = await requests.GetToursBySearchTerm(searchTerm);

            if(searchedTourList != null)
            {
                _tourList.Tourlist.Clear();

                Dictionary<string, Tour>.ValueCollection data = searchedTourList.TourList.Values;

                BitmapImage image = new BitmapImage();

                foreach (Tour t in data)
                {
                    _tourList.AddTourToList(t, image);
                }
            }
        }

        private ICommand _deleteTourCommand;

        public ICommand DeleteTourCommand
        {
            get 
            { 
                if(_deleteTourCommand != null)
                {
                    return _deleteTourCommand;
                }
                return new Command(() => DeleteTour(), true); 
            }
           
        }
        public void DeleteTour()
        {
            if(TourModel != null)
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
                if(_deleteLogCommand != null)
                {
                    return _deleteLogCommand;
                }
                return new Command(() => DeleteLog(), true);
            }
        }

        public void DeleteLog()
        {
            if(TourLogModel != null && TourModel != null)
            {
                requests.DeleteLog(TourModel.Id, TourLogModel.Logid); ;
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        

        public async void CreateReport()
        {
            string report = await requests.GetReport();

            string result = report.Substring(1, report.Length - 2);

            byte[] pdf = Convert.FromBase64String(result);


            File.WriteAllBytes("AllTourReport.pdf",pdf);

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
