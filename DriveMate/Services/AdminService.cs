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
        private readonly IRepository<UserDocument> _documentRepository;

        public AdminService(IRepository<Trip> TripRepository,
            IRepository<User> UserRepository,
            IRepository<UserDocument> DocumentRepository)
        {
            _tripRepository = TripRepository;
            _userRepository = UserRepository;
            _documentRepository = DocumentRepository;
        }

        public async Task<JsonResponse> GetAllTrips(Guid UserId)
        {
            try
            {
                var data = await (from trip in _tripRepository.Table.Where(x => x.IsDeleted == false)
                                  join user in _userRepository.Table.Where(x => x.IsDeleted == false)
                                  on trip.CustomerId equals user.Id
                                  join driver in _userRepository.Table.Where(x => x.IsDeleted == false)
                                  on trip.DriverId equals driver.Id
                                  where 
                                  ((UserId.ToString().ToLower() != "6CE75E35-FE61-442C-C2D9-08DBBB3133E1".ToLower()) ? 
                                  (trip.CustomerId == UserId || trip.DriverId == UserId) : true) && 
                                  trip.TripStatus.ToString().ToLower() == "c"                              
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
            var Trips = await _tripRepository.Table.Where(x => x.IsDeleted == false && 
            (x.DriverId == Id || x.CustomerId == Id) && 
            x.TripStatus.ToString().ToLower() == "c").ToListAsync();

            var totalTrips = Trips.Count();
            var totalAmount = Trips.Select(x => int.Parse(x.Amount)).Sum();
            var totalCustomers = Trips.Select(x => x.CustomerId).Distinct().Count();

            var totalDistance = 0.0;

            foreach (var item in Trips)
            {
                totalDistance+= float.Parse(item.Distance.Split()[0]);
            }


            try
            {
                var d = 
                        (from user in _userRepository.Table.Where(x => x.IsDeleted == false).ToList()
                         join photo in _documentRepository.Table.Where(x => x.IsDeleted == false).ToList()
                         on user.Id equals photo.UserId
                         where user.Id == Id && photo.Type.ToLower() != ".pdf"
                         select new
                         {
                             FirstName = user.FirstName,
                             LastName = user.LastName,
                             Email = user.Email,
                             PhoneNo = user.PhoneNo,
                             Data = photo.FileDate,
                             Name = photo.Name,
                             Type = photo.Type,
                             TotalTrips = totalTrips,
                             TotalCustomers = totalCustomers,
                             TotalDistance = totalDistance,
                             TotalAmount = totalAmount,
                             Role = user.Role
                         }).FirstOrDefault();

                var data = await _userRepository.Table.
                    Where(x => x.IsDeleted == false &&
                    x.Id == Id)
                    .FirstOrDefaultAsync();

                return new JsonResponse(200, true, "Success", d);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
