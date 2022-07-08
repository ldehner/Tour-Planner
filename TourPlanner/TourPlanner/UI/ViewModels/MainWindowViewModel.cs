using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        private IQuery requests;
        private TourModel _tour;
        private bool _isSelected = false;


        public bool isSelected
        {
            get { return _isSelected; }
            set { _isSelected = value; OnPropertyChanged("isSelected"); }
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

        private TourListModel _tourList;

        public ObservableCollection<TourModel> TourList
        {
            get { return _tourList.Tourlist; }
            set { _tourList.Tourlist = value; }
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
                    _tourList.AddTourToList(t, image);
                }
            }
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
            }
        }




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
