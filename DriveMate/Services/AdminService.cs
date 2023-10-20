using AutoMapper;
using DriveMate.BaseClass;
using DriveMate.Entities;
using DriveMate.Interfaces;
using DriveMate.Interfaces.BaseRepository;
using Microsoft.AspNetCore.Mvc;
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

        public async Task<JsonResponse> GetAllUsers(string filterOn = null, string filterQuery = null, string sortBy = null, bool isAscending = true, string Role = null)
        {
            try
            {
                var query = _userRepository.Table.Where(x => x.IsDeleted == false);

                // Filter by role 
                if (!string.IsNullOrWhiteSpace(Role))
                {
                    query = query.Where(x => x.Role.ToString().ToLower() == Role.ToLower());
                }

                if (!string.IsNullOrWhiteSpace(filterOn) && !string.IsNullOrWhiteSpace(filterQuery))
                {
                    if (filterOn.Equals("FirstName", StringComparison.OrdinalIgnoreCase))
                    {
                        query = query.Where(x => x.FirstName.Contains(filterQuery));
                    }
                    else if (filterOn.Equals("LastName", StringComparison.OrdinalIgnoreCase))
                    {
                        query = query.Where(x => x.LastName.Contains(filterQuery));
                    }
                    else if (filterOn.Equals("Email", StringComparison.OrdinalIgnoreCase))
                    {
                        query = query.Where(x => x.Email.Contains(filterQuery));
                    }
                    else if (filterOn.Equals("PhoneNo", StringComparison.OrdinalIgnoreCase))
                    {
                        query = query.Where(x => x.PhoneNo.Contains(filterQuery));
                    }
                }

                if (!string.IsNullOrWhiteSpace(sortBy))
                {
                    if (sortBy.Equals("FirstName", StringComparison.OrdinalIgnoreCase))
                    {
                        query = isAscending ? query.OrderBy(x => x.FirstName) : query.OrderByDescending(x => x.FirstName);
                    }
                    else if (sortBy.Equals("LastName", StringComparison.OrdinalIgnoreCase))
                    {
                        query = isAscending ? query.OrderBy(x => x.LastName) : query.OrderByDescending(x => x.LastName);
                    }
                }

                var data = await query.ToListAsync();
                return new JsonResponse(200, true, "Success", data);
            }
            catch (Exception ex)
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

                return new JsonResponse(200, true, "Success", d);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        

    }


    }

