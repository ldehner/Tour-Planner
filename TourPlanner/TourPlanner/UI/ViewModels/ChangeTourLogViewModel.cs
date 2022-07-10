using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using Tour_planner.Business;
using Tour_planner.Data_Access;
using Tour_planner.UI.Commands;
using Tour_planner.UI.Models;

namespace Tour_planner.UI.ViewModels
{
    public class ChangeTourLogViewModel
    {
        private IQuery requets;
        private TourLogModel currenttourLogModel;
        private string Id;

        public ChangeTourLogViewModel(TourLogModel currenttourModel, string id, ref MainWindowViewModel refresh)
        {
            this.requets = new Requests(refresh);
            this.currenttourLogModel = currenttourModel;
            Id = id;
        }

        public TourLogModel CurrentLogModel
        {
            get { return currenttourLogModel; }
            set { currenttourLogModel = value; }
        }


        private ICommand updateLogCommand;
        public ICommand UpdateLogCommand
        {
            get 
            {
                if (updateLogCommand != null)
                {
                    return updateLogCommand;
                }
                else
                {
                    updateLogCommand = new Command(() => UpdateLog(), true);
                    return updateLogCommand;
                }
            }
        }


        public void UpdateLog()
        {
            try
            {
                if (checkForInputs())
                {
                    throw new InvalidOperationException("Input not set");
                }
                else
                {
                    if (string.IsNullOrEmpty(CurrentLogModel.RatingInput))
                    {
                        currenttourLogModel.Rating = 0;
                    }
                    else
                    {
                        currenttourLogModel.SetRatingByString();
                    }
                    if (string.IsNullOrEmpty(CurrentLogModel.DifficultyInput))
                    {
                        currenttourLogModel.Difficulty = 0;
                    }
                    else
                    {
                        currenttourLogModel.SetDifficultyByString();
                    }

                    TourLog log = new TourLog();
                    log.LogId = CurrentLogModel.Logid;
                    log.TourId = Id;
                    log.Difficulty = CurrentLogModel.Difficulty;
                    log.Rating = CurrentLogModel.Rating;
                    log.Comment = CurrentLogModel.Comment;
                    log.Time = CurrentLogModel.TimeDay + ":" + CurrentLogModel.Hours + ":" + CurrentLogModel.Minutes; //CurrentLogModel.TimeTime.ToString("dd:HH:mm");
                    log.Date = CurrentLogModel.Year + "-" + CurrentLogModel.Month + "-" + CurrentLogModel.Day; //CurrentLogModel.DateTime.ToString("yyyy-MM-dd");
                    requets.UpdateLog(log, Id);
                }
            }
            catch
            {
                
            }
        }

        
        private bool checkForInputs()
        {

            if (string.IsNullOrEmpty(currenttourLogModel.DateTime.ToString()) || string.IsNullOrEmpty(currenttourLogModel.TimeTime.ToString()))
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
