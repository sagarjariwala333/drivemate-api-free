namespace DriveMate.Entities
{
    public class Trip : BaseClass
    {
        public Guid? DriverId { get; set; }
        public Guid? CustomerId { get; set; }
        public Guid? SourceAddressId { get; set; }
        public Guid? DestinationAddressId { get; set;}
        public DateTime? TripStartTime { get; set; }
        public DateTime? TripEndTime { get; set;}
        public string? Amount { get; set; }
        public string? DriverFeedBack { get; set; }
        public string? TripFeedBack { get; set; }
        public string? Otp { get; set; }
        public char? TripStatus { get; set; }
        public string? Source { get; set; }
        public string? Destination { get; set; }
        public string? Distance { get; set; }
        public string? ExpTime { get; set; }
    }
}
