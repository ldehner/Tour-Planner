﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TourPlanner.Business;
using TourPlanner.Data_Access;
using TourPlanner.UI.Commands;
using TourPlanner.UI.Models;

namespace TourPlanner.UI.ViewModels
{
    public class ChangeTourViewModel
    {

        private IQuery requets;
        private TourModel currenttourModel;
        private string Id;

        private MainWindowViewModel reloadModel;

        public ChangeTourViewModel(TourModel tour, ref MainWindowViewModel reload)
        {
            requets = new Requests(reload);
            CurrentTour = tour;
            Id = tour.Id;
            reloadModel = reload;
        }

        public TourModel CurrentTour
        {
            get { return currenttourModel; }
            set { currenttourModel = value; }
        }



        private ICommand updateTourCommand;

        public ICommand UpdateTourCommand
        {
            get 
            {
                if (updateTourCommand != null)
                {
                    return updateTourCommand;
                }
                else
                {
                    updateTourCommand = new Command(() => UpdateTour(), true);
                    return updateTourCommand;
                }
            
            }
        }


        public void UpdateTour()
        {
            try
            {
                if (CheckForImputs())
                {
                    throw new ArgumentException("Input cannot be Empty");
                }
                else
                {
                    if (string.IsNullOrEmpty(currenttourModel.Description))
                    {
                        currenttourModel.Description = " ";
                    }
                   


                    Tour tour = new Tour();
                    tour.Name = currenttourModel.Name;
                    tour.Start = currenttourModel.Start;
                    tour.Destination = currenttourModel.Destination;
                    tour.Type = currenttourModel.Type;
                    tour.Description = currenttourModel.Description;

                    requets.UpdateTour(tour, Id);
                }
            }
            catch
            {
                throw new Exception();
            }
        }
        
        private bool CheckForImputs()
        {
            if (string.IsNullOrEmpty(currenttourModel.Name) && string.IsNullOrEmpty(currenttourModel.StartCountry) && string.IsNullOrEmpty(currenttourModel.StartCity) &&
                string.IsNullOrEmpty(currenttourModel.DestinationCountry) && string.IsNullOrEmpty(currenttourModel.DestinationCity))
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
