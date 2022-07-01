namespace TourPlanner.API.Mapping
{
    public class PresentationLog
    {
        public Guid LogId { get; set; }
        public Guid TourId { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Duration { get; set; }
        public string Comment { get; set; } = String.Empty;
        public Int16 Difficulty { get; set; }
        public Int16 Rating { get; set; }
    }
}
