using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        private DateTime _dateTimePicker;
        private DateTime _timePicker;
        private string year;
        private string month;
        private string day;
        private string daytime;
        private string hour;
        private string minute;



        public string Logid { get { return _logid; } set { _logid = value; }}
        public string TourId { get { return _tourid; } set { _tourid = value; }}
        public string Date { get { return _date; } set { _date = value; OnPropertyChanged("Date"); } }
        public string Comment { get { return _comment; } set { _comment = value; OnPropertyChanged("Comment"); } }
        public int Difficulty { get { return _difficulty; } set { _difficulty = value; OnPropertyChanged("Difficulty"); } }
        public string Time { get { return _time; } set { _time = value; OnPropertyChanged("Time"); } }
        public int Rating { get { return _rating; } set { _rating = value; OnPropertyChanged("Rating"); } }
        public string DifficultyString { get { SetDifficultyStringByInt(); return DifficultyInput; } }
        public string RatingString { get { SetRatingStringByInt(); return RatingInput;  } }
        public string DifficultyInput { get; set; }
        public string RatingInput { get; set; }
        public DateTime DateTime { get { return _dateTimePicker; } set { _dateTimePicker = value; OnPropertyChanged("DateTime"); } }
        public DateTime TimeTime { get { return _timePicker; } set { _timePicker = value; OnPropertyChanged("TimeTime"); } }
        public string Year { get { return year; } set { year = value; OnPropertyChanged("Year"); } }
        public string Month { get { return month; } set { month = value; OnPropertyChanged("Month"); } }
        public string Day { get { return day; } set { day = value; OnPropertyChanged("Day"); } }
        public string TimeDay { get { return daytime; } set { daytime = value; OnPropertyChanged("TimeDay"); } }
        public string Hours { get { return hour; } set { hour = value; OnPropertyChanged("Hours"); } }
        public string Minutes { get { return minute; } set { minute = value; OnPropertyChanged("Minutes"); } }

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

        public Dictionary<string, int> ratingDictionary = new Dictionary<string, int>()
        {
            { "Not Rated", 0 },
            { "Very Bad", 1 },
            { "Bad", 2},
            { "Moderate", 3},
            { "Good", 4},
            { "Very Good", 5}

        };
        public Dictionary<string, int> difficultyDictionary = new Dictionary<string, int>()
        {
            { "Not Rated", 0 },
            { "Very Easy", 1 },
            { "Easy", 2},
            { "Moderate", 3},
            { "Hard", 4},
            { "Very Hard", 5}
        };


        public Dictionary<int, string> ratingDictionaryinverted = new Dictionary<int, string>()
        {
            { 0,"Not Rated" },
            { 1, "Very Bad" },
            { 2,"Bad"},
            { 3,"Moderate"},
            { 4,"Good"},
            { 5,"Very Good"}

        };
        public Dictionary<int, string> difficultyDictionaryinverted = new Dictionary<int, string>()
        {
            { 0,"Not Rated"},
            { 1,"Very Easy"},
            { 2,"Easy"},
            { 3,"Moderate"},
            { 4,"Hard"},
            { 5,"Very Hard"}
        };

        public void SetDifficultyByString()
        {
            _difficulty = difficultyDictionary[DifficultyInput];
        }

        public void SetRatingByString()
        {
            _rating = ratingDictionary[RatingInput];
        }
        public void SetDifficultyStringByInt()
        {
            DifficultyInput = difficultyDictionaryinverted[Difficulty];
        }
        public void SetRatingStringByInt()
        {
            RatingInput = ratingDictionaryinverted[_rating];
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
