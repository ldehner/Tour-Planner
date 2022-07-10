using System;
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
    public class CreateTourLogViewModel
    {
        private IQuery requets;
        private TourLogModel _newTourLogModel;
        private string TourId;

        public TourLogModel NewTourLogModel
        {
            get { return _newTourLogModel; }
            set { _newTourLogModel = value; }
        }

        public CreateTourLogViewModel(string tourId, ref MainWindowViewModel refresh)
        {
            requets = new Requests(refresh);
            _newTourLogModel = new TourLogModel();
            TourId = tourId;
        }

        private ICommand _saveLogCommand;
        public ICommand SaveLogCommand
        {
            get { if (_saveLogCommand != null)
                {
                    return _saveLogCommand;

                }
                _saveLogCommand = new Command(() => CreateLog(), true);
                return _saveLogCommand;
            }
        }
        public void CreateLog()
        {
            try
            {
                if (CheckForInputs())
                {
                    throw new ArgumentException("Imput cannot be empty");
                }
                else
                {
                    if (string.IsNullOrEmpty(NewTourLogModel.RatingInput))
                    {
                        NewTourLogModel.Rating = 0;
                    }
                    else
                    {
                        NewTourLogModel.SetRatingByString();
                    }
                    if (string.IsNullOrEmpty(NewTourLogModel.DifficultyInput))
                    {
                        NewTourLogModel.Difficulty = 0;
                    }
                    else
                    {
                        NewTourLogModel.SetDifficultyByString();
                    }

                    
                    
                    TourLog tourLog = new TourLog();
                    tourLog.TourId = TourId;
                    tourLog.Difficulty = NewTourLogModel.Difficulty;
                    tourLog.Rating = NewTourLogModel.Rating;
                    tourLog.Comment = NewTourLogModel.Comment;
                    tourLog.Time = NewTourLogModel.TimeDay + ":" + NewTourLogModel.Hours + ":" + NewTourLogModel.Minutes; //CurrentLogModel.TimeTime.ToString("dd:HH:mm");
                    tourLog.Date = NewTourLogModel.Year + "-" + NewTourLogModel.Month + "-" + NewTourLogModel.Day; //CurrentLogModel.DateTime.ToString("yyyy-MM-dd");

                    requets.PostLog(tourLog, TourId);

                }
            }
            catch
            {
                
            }
        }
        private bool CheckForInputs()
        {
            if (string.IsNullOrEmpty(NewTourLogModel.DateTime.ToString()) || string.IsNullOrEmpty(NewTourLogModel.TimeTime.ToString()))
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
