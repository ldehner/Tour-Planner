﻿using System;
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
        

        public CreateTourViewModel()
        {
            requets = new Requests();
            _newtourModel = new TourModel();
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
                if (string.IsNullOrEmpty(_newtourModel.Name) || string.IsNullOrEmpty(_newtourModel.Start) || string.IsNullOrEmpty(_newtourModel.Destination))
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
                    
                }
            }
            catch
            {
                throw new Exception();
            }
        }

        public Action CloseAction { get; set; }
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
