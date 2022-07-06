namespace TourPlanner.API.Mapping
{
    public class PdfTourOverview
    {
        public string tourId { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string duration { get; set; }
        public string transport { get; set; }
        public string distance { get; set; }
        public string avgduration { get; set; }
        public string rating { get; set; }
        public string difficulty { get; set; }
        public string from { get; set; }
        public string to { get; set; }
        public string entrys { get; set; }
        public string baseUrl { get; set; }
    }
}
