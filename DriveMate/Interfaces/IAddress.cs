using DriveMate.Entities;
using DriveMate.Requests.TripRequest;

namespace DriveMate.Interfaces
{
    public interface IAddress
    {
        public Task<Address> SaveAsync(AddressModal address);
        public Task<List<Address>> GetAll();
    }
}
