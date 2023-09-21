using DriveMate.BaseClass;
using DriveMate.Entities;
using DriveMate.Interfaces;
using DriveMate.Interfaces.BaseRepository;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace DriveMate.Services
{
    public class AdminService : IAdminService
    {
        private readonly IRepository<DriveMate.Entities.Trip> _tripRepository;
        private readonly IRepository<User> _userRepository;

        public AdminService(IRepository<Trip> TripRepository,
            IRepository<User> UserRepository)
        {
            _tripRepository = TripRepository;
            _userRepository = UserRepository;
        }

        public async Task<JsonResponse> GetAllTrips()
        {
            try
            {
                var data = await (from trip in _tripRepository.Table.Where(x => x.IsDeleted == false)
                                  join user in _userRepository.Table.Where(x => x.IsDeleted == false)
                                  on trip.CustomerId equals user.Id
                                  join driver in _userRepository.Table.Where(x => x.IsDeleted == false)
                                  on trip.DriverId equals driver.Id
                                  select new
                                  {
                                      DriverName = driver.FirstName + " " + driver.LastName,
                                      CustomerName = user.FirstName + " " + user.LastName,
                                      Date = trip.CreatedDate,
                                      Source = trip.Source,
                                      Destination = trip.Destination,
                                      Amount = trip.Amount,
                                      Distance = trip.Distance
                                  }).ToListAsync();

                return new JsonResponse(200, true, "Success", data);
            }
            catch (Exception ex)
            {
                throw;
            } 
        }

        public async Task<JsonResponse> GetTripsById(Guid Id)
        {
            try
            {
                var data = await (from trip in _tripRepository.Table.Where(x => x.IsDeleted == false)
                                  join user in _userRepository.Table.Where(x => x.IsDeleted == false)
                                  on trip.CustomerId equals user.Id
                                  where trip.Id == Id
                                  select trip).FirstOrDefaultAsync();   

                return new JsonResponse(200, true, "Success", data);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<JsonResponse> GetAllUsers(string Role)
        {
            try
            {
                var data = await _userRepository.Table.
                    Where(x => x.IsDeleted == false &&
                    x.Role.ToString().ToLower() == Role.ToLower())
                    .ToListAsync();

                return new JsonResponse(200, true, "Success",data);
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public async Task<JsonResponse> GetUserById(Guid Id)
        {
            try
            {
                var data = await _userRepository.Table.
                    Where(x => x.IsDeleted == false &&
                    x.Id == Id)
                    .FirstOrDefaultAsync();

                return new JsonResponse(200, true, "Success", data);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
