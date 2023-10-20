using DriveMate.BaseClass;
using DriveMate.Entities;
using DriveMate.Interfaces;
using DriveMate.Interfaces.BaseRepository;
using DriveMate.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DriveMate.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AdminController : ApiBaseController
    {
        private readonly ILogger<AdminController> _logger;
        private readonly IAdminService _adminService;
        public AdminController(ILogger<AdminController> logger,
            IAdminService adminService)
        {
            _logger = logger;
            _adminService = adminService;

        }

        [HttpGet(Name = "GetAllTrips")]
        public async Task<JsonResponse> GetAllTrips()
        {
            try
            {
                Guid Id = user_unique_id;
                return await _adminService.GetAllTrips(Id);
            }
            catch (Exception ex)
            {
                return new JsonResponse(200, true, "Fail", ex.Message);
            }
        }

        [HttpPost(Name = "GetTripById")]
        public async Task<JsonResponse> GetTripById(TripRequest1 req)
        {
            try
            {
                return await _adminService.GetTripsById(req.Id);
            }
            catch (Exception ex)
            {
                return new JsonResponse(200, true, "Fail", ex.Message);
            }
        }

        [HttpPost(Name = "GetAllUsers")]
        public async Task<JsonResponse> GetAllUsers(AllUsersRequest req)
        {
            try
            {
                return await _adminService.GetAllUsers(req.FilterOn, req.FilterQuery, req.SortBy, req.IsAscending, req.Role);
            }
            catch (Exception ex)
            {
                return new JsonResponse(200, true, "Fail", ex.Message);
            }
        }


        [HttpPost(Name = "GetUserById")]
        public async Task<JsonResponse> GetUserById(UserByIdRequest req)
        {
            try
            {
                return await _adminService.GetUserById(req.Id);
            }
            catch (Exception ex)
            {
                return new JsonResponse(200, true, "Fail", ex.Message);
            }
        }

        [HttpPost(Name = "GetUserByIdProfile")]
        public async Task<JsonResponse> GetUserByIdProfile(UserByIdRequest req)
        {
            try
            {
                req.Id = user_unique_id;
                return await _adminService.GetUserById(req.Id);
            }
            catch (Exception ex)
            {
                return new JsonResponse(200, true, "Fail", ex.Message);
            }
        }
<<<<<<< HEAD
       
=======
>>>>>>> e5979e43305e195eb75554705d268f2d68a9024b
    }
}
