namespace DriveMate.Responses.UserResponse
{
    public class LoginResponse
    {
        public char? Role { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNo { get; set; }
        public string? Token { get; set; }
        public string? Message { get; set; }
        public int status { get; set; }
    }
}
