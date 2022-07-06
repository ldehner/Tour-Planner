namespace TourPlanner.API.DAL
{
    public class PdfTemplateRepository : IPdfTemplateRepository
    {
        private readonly string _path;
        public PdfTemplateRepository(string path)
        {
            _path = path;   
        }
        public async Task<string> GetTourDetailTemplateAsync()
        {
            return await File.ReadAllTextAsync(_path + "/TourReport.html");
        }

        public async Task<string> GetTourOverviewTemplateAsync()
        {
            return await File.ReadAllTextAsync(_path + "/AllToursReport.html");
        }
    }
}
