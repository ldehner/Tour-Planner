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

        public CreateTourLogViewModel(string tourId)
        {
            requets = new Requests();
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
                if (CheckForImputs())
                {
                    throw new ArgumentException("Imput cannot be empty");
                }
                else
                {
                    TourLog tourLog = new TourLog();
                    tourLog.TourId = TourId;
                    tourLog.Difficulty = NewTourLogModel.Difficulty;
                    tourLog.Rating = NewTourLogModel.Rating;
                    tourLog.Comment = NewTourLogModel.Comment;
                    tourLog.Time = NewTourLogModel.Time;
                    tourLog.Date = NewTourLogModel.Date;
                    requets.PostLog(tourLog);

                }
            }
            catch
            {

            }
        }
        private bool CheckForImputs()
        {
            return true;
        }
    }
}
