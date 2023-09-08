using System;

namespace DriveMate.HelperClasses
{
    public class BookedTripsLimitException : Exception
    {
        public BookedTripsLimitException()
        {
        }

        public BookedTripsLimitException(string message)
            : base(message)
        {
        }

        public BookedTripsLimitException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }

}
