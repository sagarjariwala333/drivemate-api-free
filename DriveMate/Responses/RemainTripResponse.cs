namespace DriveMate.Responses
{
    public class RemainTripResponse
    {
        public Guid? CustomerId { get; set; }
        public string? CustomerName { get; set; }
        public string? MobileNo { get; set; }
        public DateTime? TripStartTime { get; set; }
        public string? Amount { get; set; }
        public char? TripStatus { get; set; }
        public string? Source { get; set; }
        public string? Destination { get; set; }
        public string? Distance { get; set; }
        public string? ExpTime { get; set; }
    }
}
