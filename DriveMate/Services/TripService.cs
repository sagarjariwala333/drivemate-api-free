using AutoMapper;
using DriveMate.Context;
using DriveMate.Entities;
using DriveMate.Interfaces;
using DriveMate.Interfaces.BaseRepository;
using DriveMate.Requests.TripRequest;
using DriveMate.Requests.UserRequest;
using DriveMate.Responses.Trip;

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

        public Task<Trip> SaveAsync(TripRequest tripRequest)
        {
            throw new NotImplementedException();
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



        /*public async Task<AddTripResponse> InsertTripAsync(AddTripRequest addTripRequest)
        {
            using (var transaction = AppDBContext.Database.BeginTransaction())
            {
                try
                {
                    Address address = _mapper.Map<Address>(addTripRequest);
                    await _addressRepository.InsertAsync(address);

                    UserAddressService userAddress = new UserAddressService()
                    {
                        UserId = addTripRequest.UserId,
                        AddressId = (Guid)address.Id,
                    };
                    await _userAddressRepository.InsertAsync(userAddress);

                    Trip trip = _mapper.Map<Trip>(addTripRequest);
                    await _tripRepository.InsertAsync(trip);
                    
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new Exception(ex.Message);
                }
            }
        }*/


    }
}
