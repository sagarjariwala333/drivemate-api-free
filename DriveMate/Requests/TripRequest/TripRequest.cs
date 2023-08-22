namespace DriveMate.Requests.TripRequest
{
    public class TripRequest
    {
        public Guid? Id { get; set; }
        public Guid DriverId { get; set; }
        public Guid CustomerId { get; set; }
        public Guid SourceAddressId { get; set; }
        public Guid DestinationAddressId { get; set; }
        public DateTime? TripStartTime { get; set; }
        public DateTime? TripEndTime { get; set; }
        public string? DriverFeedBack { get; set; }
        public string? TripFeedBack { get; set; }
        public char TripStatus { get; set; }
    }
}
