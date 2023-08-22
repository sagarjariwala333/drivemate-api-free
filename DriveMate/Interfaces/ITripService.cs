using DriveMate.Entities;
using DriveMate.Requests.TripRequest;
using DriveMate.Responses.Trip;

namespace DriveMate.Interfaces
{
    public interface ITripService
    {
        public Task<Trip> SaveAsync(TripRequest tripRequest);
    }
}
