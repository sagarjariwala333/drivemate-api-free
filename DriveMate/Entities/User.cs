namespace DriveMate.Entities
{
    public class User : BaseClass
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNo { get; set; }
        public string Password { get; set; }
        public Boolean Status { get; set; }
        public Char Role { get; set; }
        public Boolean IsEmailConfirmed { get; set; }
        public Boolean IsPhoneConfirmed { get; set; }
    }
}
