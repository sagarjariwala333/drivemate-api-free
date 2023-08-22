using DriveMate.Entities;
using DriveMate.Requests;

namespace DriveMate.Interfaces
{
    public interface IUserAddress
    {
        public Task<UserAddress> SaveAsync(UserAddressRequestDto userAddressRequestDto);
    }
}
