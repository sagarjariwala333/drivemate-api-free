namespace DriveMate.Entities
{
    public class PaymentDetails : BaseClass
    {
        public string Mode { get; set; }
        public string Amount { get; set; }
        public Guid TripId { get; set; }
        public string data1 { get; set; }
        public string data2 { get; set; }
        public string data3 { get; set; }
        public Boolean status { get; set; }
    }
}
