namespace TourPlanner.API.Data
{
    public class SimpleLog
    {
        public DateTime Date { get; set; }
        public DateTime Duration { get; set; }
        public string Comment { get; set; } = String.Empty;
        public Int16 Difficulty { get; set; }
        public Int16 Rating { get; set; }
    }
}
