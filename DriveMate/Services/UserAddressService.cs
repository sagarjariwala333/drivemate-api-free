using AutoMapper;
using DriveMate.Context;
using DriveMate.Entities;
using DriveMate.Interfaces;
using DriveMate.Interfaces.BaseRepository;
using DriveMate.Requests;
using Microsoft.EntityFrameworkCore;

namespace DriveMate.Services
{
    public class UserAddressService : IUserAddress
    {
        private AppDBContext _dBContext;
        private IMapper _mapper;

        public UserAddressService(IRepository<UserAddress> userAddressRepository,
            AppDBContext appDBContext,
            IMapper mapper)
        {
            _dBContext = appDBContext;
            _mapper = mapper;
        }

        public async Task<UserAddress> SaveAsync(UserAddressRequestDto userAddressRequestDto)
        {
            try
            {
                if(userAddressRequestDto.Id == null)
                {
                    UserAddress userAddress=_mapper.Map<UserAddress>(userAddressRequestDto);
                    await _dBContext.UserAddresses.AddAsync(userAddress);
                    await _dBContext.SaveChangesAsync();
                    return userAddress;
                }
                else
                {
                    UserAddress userAddress = _mapper.Map<UserAddress>(userAddressRequestDto);
                    _dBContext.UserAddresses.Update(userAddress);
                    await _dBContext.SaveChangesAsync();
                    return userAddress;
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
