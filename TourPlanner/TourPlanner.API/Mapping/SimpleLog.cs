namespace TourPlanner.API.Data
{
    public class SimpleLog
    {
        public string Date { get; set; }
        public string Duration { get; set; }
        public string Comment { get; set; } = String.Empty;
        public Int16 Difficulty { get; set; }
        public Int16 Rating { get; set; }

        public SimpleLog(string date, string duration, string comment, short difficulty, short rating)
        {
            Date = date;
            Duration = duration;
            Comment = comment;
            Difficulty = difficulty;
            Rating = rating;    
        }
    }

}
