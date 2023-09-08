namespace DriveMate.Requests.TripRequest
{
    public class BookedTrips
    {
        public Guid Id { get; set; }
        public string Source { get; set; }
        public string Destination { get; set; }
        public string Otp { get; set; }
    }
}
