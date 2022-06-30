using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tour_planner.Business
{
    public class TourLog
    {

        public string Tourname { get; set; }
        public string? Date { get; set; }
        public string? Comment { get; set; }
        public int Difficulty { get; set; }
        public string? Time { get; set; }
        public int Rating { get; set; }
        public int Id { get; set; }

        public TourLog(string tourname)
        {
            Tourname = tourname;
        }

        public TourLog(string tourname, string date, string comment, int difficulty, string time, int rating)
        {
            Tourname = tourname;
            Date = date;
            Comment = comment;
            Difficulty = difficulty;
            Time = time;
            Rating = rating;
        }

        public TourLog(string tourname, string date, string comment, int difficulty, string time, int rating, int id)
        {
            Tourname = tourname;
            Date = date;
            Comment = comment;
            Difficulty = difficulty;
            Time = time;
            Rating = rating;
            Id = id;
        }
    }
}
