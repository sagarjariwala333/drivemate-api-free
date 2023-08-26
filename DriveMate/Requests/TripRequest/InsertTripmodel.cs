namespace DriveMate.Requests.TripRequest
{
    public class InsertTripmodel
    {
        public Guid? Id { get; set; }
        public Guid CustomerId { get; set; }
        public double SourceLatitude { get; set; }  
        public double SourceLongitude { get; set; }
        public double DestinationLatitude { get; set; }
        public double DestinationLongitude { get; set; }
        public DateTime DateTime { get; set; }
                                                    
    }                                               
}                                                   
                                                    