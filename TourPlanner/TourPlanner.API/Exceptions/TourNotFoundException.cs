namespace TourPlanner.API.Exceptions
{
    public class TourNotFoundException : Exception
    {
        public TourNotFoundException()
        {
        }

        public TourNotFoundException(string message) : base(message)
        {
        }

        public TourNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
