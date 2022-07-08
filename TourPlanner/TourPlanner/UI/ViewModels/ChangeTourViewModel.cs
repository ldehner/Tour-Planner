using System;
using System.Collections.Generic;
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
    public class ChangeTourViewModel
    {

        private IQuery requets;
        private TourModel currenttourModel;
        private string Id;

        public ChangeTourViewModel(TourModel tour)
        {
            requets = new Requests();
            CurrentTour = tour;
            Id = tour.Id;
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
                if (string.IsNullOrEmpty(currenttourModel.Name) || string.IsNullOrEmpty(currenttourModel.Start) || string.IsNullOrEmpty(currenttourModel.Destination))
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
    }
}
