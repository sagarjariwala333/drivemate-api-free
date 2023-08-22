namespace DriveMate.Requests
{
    public class UserAddressRequestDto
    {
        public Guid? Id { get; set; }
        public Guid? UserId { get; set; }
        public Guid AddressId { get; set; }
        public Boolean IsDeleted { get; set; }
    }
}
