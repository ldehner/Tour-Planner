namespace TourPlanner.API.BL
{
    public interface IMapQuestManager
    {
        public Task GetMapAsync();
        public Task SaveMapAsync();
    }
}
