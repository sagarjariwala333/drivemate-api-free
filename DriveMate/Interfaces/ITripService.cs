using DriveMate.Entities;
using DriveMate.Requests.TripRequest;
using DriveMate.Responses;
using DriveMate.Responses.Trip;

namespace DriveMate.Interfaces
{
    public interface ITripService
    {
        public Task<Trip> SaveAsync(TripRequest tripRequest);
        public Task<Trip> InsertTripAsync(TripRequest tripRequest);
        public Task<List<RemainTripResponse>> GetRemainTripAsync();
    }
}
