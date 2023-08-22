namespace DriveMate.Requests.UserRequest
{
    public class SignupRequest
    {
        public Guid? Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNo { get; set; }
        public string Password { get; set; }
        public Boolean Status { get; set; }
        public Char Role { get; set; }
        public string AadharNo { get ; set; }
        public string LicenceNo { get; set; }
        public Boolean IsEmailConfirmed { get; set; } = false;
        public Boolean IsPhoneConfirmed { get; set; } = false;
    }
}
