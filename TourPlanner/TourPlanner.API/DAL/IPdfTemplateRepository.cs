namespace TourPlanner.API.DAL
{
    public interface IPdfTemplateRepository
    {
        public Task<string> GetTourDetailTemplateAsync();
        public Task<string> GetTourOverviewTemplateAsync();
    }
}
