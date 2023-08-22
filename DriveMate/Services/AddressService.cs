using AutoMapper;
using DriveMate.Context;
using DriveMate.Entities;
using DriveMate.Interfaces;
using DriveMate.Interfaces.BaseRepository;
using DriveMate.Requests.TripRequest;
using Microsoft.EntityFrameworkCore;

namespace DriveMate.Services
{
    public class AddressService : IAddress
    {
        private AppDBContext dBContext;
        private IMapper _mapper;

        public AddressService(AppDBContext appDBContext, IMapper mapper)
        {
            dBContext = appDBContext;
            _mapper = mapper;
        }

        public async Task<Address> SaveAsync(AddressModal address)
        {
            try
            {
                if (address.Id == null)
                {
                    Address add = _mapper.Map<Address>(address);
                    await dBContext.Addresses.AddAsync(add);
                    await dBContext.SaveChangesAsync();
                    return add;
                }
                else
                {
                    Address add = _mapper.Map<Address>(address);
                    
                    var data = await dBContext.Addresses.FindAsync(address.Id);

                    if(data != null)
                    {
                        data = add;
                        await dBContext.SaveChangesAsync();
                    }
                    return data;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Address>> GetAll()
        {
            try
            {
                return await dBContext.Addresses.ToListAsync();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
