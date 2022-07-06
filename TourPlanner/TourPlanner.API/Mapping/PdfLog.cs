namespace TourPlanner.API.Mapping
{
    public class PdfLog
    {
        public string Date { get; set; }
        public string Duration { get; set; }
        public string Comment { get; set; } = String.Empty;
        public Int16 Difficulty { get; set; }
        public Int16 Rating { get; set; }
    }
}
