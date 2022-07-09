using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tour_planner.Business;

namespace Tour_planner.UI.Models
{
    public class TourLogModel
    {
        private string _logid;
        private string _tourid;
        private string _date;
        private string _comment;
        private int _difficulty;
        private string _time;
        private int _rating;



        public string Logid { get { return _logid; } set { _logid = value; }}
        public string TourId { get { return _tourid; } set { _tourid = value; }}
        public string Date { get { return _date; } set { _date = value; }}
        public string Comment { get { return _comment; } set { _comment = value; }}
        public int Difficulty { get { return _difficulty; } set { _difficulty = value; }}
        public string Time { get { return _time; } set { _time = value; }}
        public int Rating { get { return _rating; } set { _rating = value; }}
        public string DifficultyString { get { return "" + Difficulty; } }
        public string RatingString { get { return "" + Rating; } }

        public TourLogModel()
        {


        }
        public TourLogModel(TourLog log)
        {
            _logid = log.LogId;
            _tourid = log.TourId;
            _date = log.Date;
            _comment = log.Comment;
            _difficulty = log.Difficulty;
            _time = log.Time;
            _rating = log.Rating;
        }


        public TourLogModel(string logid, string tourid, string date, string comment, int difficulty, string time, int rating)
        {
            _logid = logid;
            _tourid = tourid;
            _date = date;
            _comment = comment;
            _difficulty = difficulty;
            _time = time;
            _rating = rating;
        }
    }
}
