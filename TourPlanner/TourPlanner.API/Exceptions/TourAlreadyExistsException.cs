namespace TourPlanner.API.Exceptions
{
    public class TourAlreadyExistsException : Exception
    {
        public TourAlreadyExistsException()
        {
        }

        public TourAlreadyExistsException(string message) : base(message)
        {
        }

        public TourAlreadyExistsException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
