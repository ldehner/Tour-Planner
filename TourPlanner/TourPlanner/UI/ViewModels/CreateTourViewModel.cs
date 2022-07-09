using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Tour_planner.Business;
using Tour_planner.Data_Access;
using Tour_planner.UI.Commands;
using Tour_planner.UI.Models;


namespace Tour_planner.UI.ViewModels
{
    public class CreateTourViewModel : INotifyPropertyChanged
    {

        private IQuery requets;
        private TourModel _newtourModel;
        private MainWindowViewModel reloadModel;

        public CreateTourViewModel(ref MainWindowViewModel reloadModel)
        {
            requets = new Requests();
            _newtourModel = new TourModel();
            this.reloadModel = reloadModel;
        }


        public TourModel? newTourModel
        {
            get { return _newtourModel; }
            set { _newtourModel = value; }
        }

        private ICommand _saveCommand;
        public ICommand SaveCommand
        {
            get
            {
                if (_saveCommand != null)
                {
                    return _saveCommand;
                }
                _saveCommand = new Command(() => CreateTour(), true);
                return _saveCommand;
            }
        }

        public void CreateTour()
        {
            try
            {
                if (CheckForImputs())
                {
                    throw new ArgumentException("Input cannot be Empty");
                }
                else
                {
                    if (string.IsNullOrEmpty(_newtourModel.Description))
                    {
                        _newtourModel.Description = " ";
                    }

                    Tour tour = new Tour();
                    tour.Name = _newtourModel.Name;
                    tour.Start = _newtourModel.Start;
                    tour.Destination = _newtourModel.Destination;
                    tour.Type = _newtourModel.Type;
                    tour.Description = _newtourModel.Description;

                    requets.PostTour(tour);
                   
                    reloadModel.LoadTours();
                }
            }
            catch
            {
                throw new Exception();
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

        private bool CheckForImputs()
        {
            if (string.IsNullOrEmpty(newTourModel.Name) && string.IsNullOrEmpty(newTourModel.StartCountry) && string.IsNullOrEmpty(newTourModel.StartCity) &&
                string.IsNullOrEmpty(newTourModel.DestinationCountry) && string.IsNullOrEmpty(newTourModel.DestinationCity))
            {
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}
