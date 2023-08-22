namespace DriveMate.Requests.TripRequest
{
    public class AddTripAddressRequest
    {
        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set;}
        public string State { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public Guid CreatedBy { get; set; }
        public Guid ModifiedBy { get; set;}
        public Boolean IsDeleted { get; set;}
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set;}
    }
}
