using DriveMate.Entities;
using DriveMate.Requests.TripRequest;
using DriveMate.Responses;
using DriveMate.Responses.Trip;

namespace DriveMate.Interfaces
{
    public interface ITripService
    {
        public Task<Trip> SaveAsync(TripRequest tripRequest);
        public Task<Trip> InsertTripAsync(TripRequest tripRequest, Guid Id);
        public Task<List<RemainTripResponse>> GetRemainTripAsync();
        public Task<Trip> DriverAcceptTripAsync(Guid TripId, Guid DriverId);
        public Task<List<BookedTrips>> ViewBookedTrips(Guid id);
        //public Task<List<BookedTrips>> CustomerViewBookedTrips(Guid Id);
        public Task<Trip> StartTrip(Guid Id);
        public Task<Trip> EndTrip(Guid Id);
    }
}
