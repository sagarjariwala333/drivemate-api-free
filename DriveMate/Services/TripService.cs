using AutoMapper;
using DriveMate.Context;
using DriveMate.Entities;
using DriveMate.Interfaces;
using DriveMate.Interfaces.BaseRepository;
using DriveMate.Requests.TripRequest;
using DriveMate.Requests.UserRequest;
using DriveMate.Responses.Trip;
using Microsoft.EntityFrameworkCore;

namespace DriveMate.Services
{
    public class TripService : ITripService
    {
        private AppDBContext _appDBContext;
        private IMapper _mapper;
        

        public TripService(AppDBContext appDBContext,
            IMapper mapper)
        {
            this._appDBContext = appDBContext;
            this._mapper = mapper;
        }

        public async Task<Trip> SaveAsync(TripRequest tripRequest)
        {
            try
            {
                if (tripRequest.Id == null)
                {
                    Trip trip = _mapper.Map<Trip>(tripRequest);

                    await _appDBContext.Trips.AddAsync(trip);

                    await _appDBContext.SaveChangesAsync();

                    return trip;
                }
                else
                {
                    var data = await _appDBContext.Trips.FirstOrDefaultAsync(x => x.Id == tripRequest.Id);
                    Trip trip = _mapper.Map<Trip>(tripRequest);
                    
                    if(data != null) 
                    {
                        data = trip;
                    }

                    await _appDBContext.SaveChangesAsync();
                    return trip;
                }
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
