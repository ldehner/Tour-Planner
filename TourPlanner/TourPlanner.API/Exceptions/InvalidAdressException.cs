namespace TourPlanner.API.Exceptions
{
    public class InvalidAdressException : Exception
    {
        public InvalidAdressException()
        {
        }

        public InvalidAdressException(string message) : base(message)
        {
        }

        public InvalidAdressException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
