using AutoMapper;
using DriveMate.Context;
using DriveMate.Entities;
using DriveMate.HelperClasses;
using DriveMate.Interfaces;
using DriveMate.Interfaces.BaseRepository;
using DriveMate.Requests.TripRequest;
using DriveMate.Requests.UserRequest;
using DriveMate.Responses;
using DriveMate.Responses.Trip;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;

namespace DriveMate.Services
{
    public class TripService : ITripService
    {
        private AppDBContext AppDBContext;
        private IAddress _addressService;
        private IRepository<Trip> _tripRepository;
        private IUserAddress _userAddressService;
        private IMapper _mapper;
        

        public TripService(AppDBContext appDBContext,
            IAddress addressService,
            IRepository<Trip> _tripRepository,
            IUserAddress userAddressService,
            IMapper mapper)
        {
            this.AppDBContext = appDBContext;
            this._tripRepository = _tripRepository;
            this._addressService = addressService;
            this._userAddressService = userAddressService;
            this._mapper = mapper;
        }


        public async Task<Trip> InsertTripAsync(TripRequest tripRequest, Guid Id)
        {
            
            try
            {
                var cont = await AppDBContext.Trips.Where(x => x.CustomerId == Id && x.TripStatus == 'R').CountAsync();

                if (cont > 1)
                {
                    throw new BookedTripsLimitException("One trip already booked");
                }

                if (tripRequest.Id == null)
                {
                    var trip = _mapper.Map<Trip>(tripRequest);
                    trip.TripStatus = 'R';
                    await AppDBContext.Trips.AddAsync(trip);
                    await AppDBContext.SaveChangesAsync();
                    return trip;
                }
                else
                {
                    var trip = _mapper.Map<Trip>(tripRequest);
                    var data = AppDBContext.Trips.FirstOrDefault(x => x.Id == tripRequest.Id);
                    if(data == null)
                    {
                        data = trip;
                    }
                    await AppDBContext.SaveChangesAsync();
                    return trip;
                }
            }
            catch(BookedTripsLimitException btlx)
            {
                throw;
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public async Task<Trip> SaveAsync(Trip trip)
        {
            try
            {
                if (trip.Id == null)
                {
                    await _tripRepository.InsertAsync(trip);
                    return trip;
                }
                else
                {
                    await _tripRepository.UpdateAsync(trip);
                    return trip;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<RemainTripResponse>> GetRemainTripAsync()
        {
            try
            {
                var AllTrips = await AppDBContext.Trips
                    .Where(x => x.DriverId == null && x.TripStatus == 'R')
                    .ToListAsync();

                var AllCustomers = await AppDBContext.Users
                    .Where(x => x.Role == 'C')
                    .ToListAsync();

                var data = (from trip in AllTrips
                           join cust in AllCustomers
                           on trip.CustomerId equals cust.Id
                           select new RemainTripResponse
                           {
                               Id = (Guid)trip.Id,
                               CustomerId = cust.Id,
                               CustomerName = cust.FirstName + cust.LastName,
                               MobileNo = cust.PhoneNo,
                               TripStartTime = trip.TripStartTime,
                               Amount = trip.Amount,
                               TripStatus = trip.TripStatus,
                               Source = trip.Source,
                               Destination = trip.Destination,
                               Distance = trip.Distance,
                               ExpTime = trip.ExpTime,
                           }).ToList();
                
                return data;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<AddTripAddressResponse> SaveTripAddressAsync(AddTripAddressRequest addTripAddressRequest)
        {
            using (var transaction = AppDBContext.Database.BeginTransaction())
            {
                try
                {
                    Address address = _mapper.Map<Address>(addTripAddressRequest);
                   // await _addressService.SaveAsync(address);

                    UserAddress userAddress = new UserAddress()
                    {
                        UserId = addTripAddressRequest.UserId,
                        AddressId = (Guid)address.Id
                    };

                    await transaction.CommitAsync();

                    return new AddTripAddressResponse()
                    {
                        AddressId = (Guid)address.Id
                    };
                    
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public Task<Trip> SaveAsync(TripRequest tripRequest)
        {
            throw new NotImplementedException();
        }

        public async Task<Trip> DriverAcceptTripAsync(Guid TripId, Guid DriverId)
        {
            var data = await AppDBContext.Trips.FirstOrDefaultAsync(x => x.Id == TripId);
            if (data != null)
            {
                data.DriverId = DriverId;
                data.TripStatus = 'B';
                data.Otp = "1234";
            }
            await AppDBContext.SaveChangesAsync();
            return data;
        }

        public async Task<List<BookedTrips>> ViewBookedTrips(Guid Id)
        {
            try
            {
                
                var data = await (from trip in AppDBContext.Trips
                                  join user in AppDBContext.Users
                                  on trip.CustomerId equals user.Id
                                  where trip.TripStatus == 'R' && trip.CustomerId == Id
                                  select new BookedTrips
                                  {
                                      Id = (Guid)trip.Id,
                                      Source = trip.Source,
                                      Destination = trip.Destination,
                                      Otp = trip.Otp
                                  })
                            .ToListAsync();
                return data;
            }
            catch(Exception ex)
            {
                throw new Exception("");
            }

        }

        /*public async Task<List<BookedTrips>> CustomerViewBookedTrips(Guid Id)
        {
            var data = await (from trip in AppDBContext.Trips
                              join user in AppDBContext.Users
                              on trip.CustomerId equals user.Id
                              where trip.TripStatus == 'R' && trip.CustomerId == Id
                              select new BookedTrips
                              {
                                  Id = (Guid)trip.Id,
                                  Source = trip.Source,
                                  Destination = trip.Destination,
                                  MobileNo = user.PhoneNo,
                                  DriverId = (Guid)trip.DriverId,
                                  DriverName = user.FirstName + user.LastName,
                                  Otp = trip.Otp
                              })
                        .ToListAsync();

            return data;
        }*/

        public async Task<Trip> StartTrip(Guid Id)
        {
            var data = await AppDBContext.Trips.FirstOrDefaultAsync(x => x.Id == Id);

            if (data != null)
            {
                data.TripStatus = 'A';
            }

            await AppDBContext.SaveChangesAsync();
            return data;
        }

        public async Task<Trip> EndTrip(Guid Id)
        {
            var data = await AppDBContext.Trips.FirstOrDefaultAsync(x => x.Id == Id);

            if (data != null)
            {
                data.TripStatus = 'C';
            }

            await AppDBContext.SaveChangesAsync();
            return data;
        }

    }
}
