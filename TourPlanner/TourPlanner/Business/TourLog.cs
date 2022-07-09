using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tour_planner.Business
{
    public class TourLog
    {

        public string LogId { get; set; }
        public string TourId { get; set; }
        public string? Date { get; set; }
        public string? Comment { get; set; }
        public int Difficulty { get; set; }
        public string? Time { get; set; }
        public int Rating { get; set; }
        

        public TourLog()
        {

        }
        public TourLog(string logid,string tourid, string date, string comment, int difficulty, string time, int rating)
        {
            LogId = logid;
            TourId = tourid;
            Date = date;
            Comment = comment;
            Difficulty = difficulty;
            Time = time;
            Rating = rating;
        }
    }
}
