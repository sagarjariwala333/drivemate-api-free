namespace DriveMate.Requests.TripRequest
{
    public class TripRequest
    {
        public Guid? Id { get; set; }
        public Guid? DriverId { get; set; }
        public Guid? CustomerId { get; set; }
        public Guid? SourceAddressId { get; set; }
        public Guid? DestinationAddressId { get; set; }
        public DateTime? TripStartTime { get; set; }
        public DateTime? TripEndTime { get; set; }
        public string? DriverFeedBack { get; set; }
        public string? TripFeedBack { get; set; }
        public char? TripStatus { get; set; }
        public string? Amount { get; set; }
        public string? otp { get; set; }
        public bool? isDeleted { get; set; }
        public string? Source { get; set; }
        public string? Destination { get; set; }
        public string? Distance { get; set; }
        public string? Duration { get; set; }
        public string? datetime { get; set; }
    }
}
