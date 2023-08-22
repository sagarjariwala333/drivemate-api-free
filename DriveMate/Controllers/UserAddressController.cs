using DriveMate.BaseClass;
using DriveMate.Entities;
using DriveMate.Interfaces;
using DriveMate.Requests;
using DriveMate.Requests.TripRequest;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DriveMate.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserAddressController : ApiBaseController
    {
        private readonly ILogger<UserController> logger;
        private IUserAddress _userAddress;

        public UserAddressController(ILogger<UserController> logger, IUserAddress address)
        {
            this.logger = logger;
            this._userAddress = address;
        }

        [HttpPost(Name = "SaveAsync")]
        public async Task<JsonResponse> SaveAsync(UserAddressRequestDto userAddressRequestDto)
        {
            try
            {
                userAddressRequestDto.UserId = user_unique_id;
                var result = await _userAddress.SaveAsync(userAddressRequestDto);
                return new JsonResponse(200, true, "Success", result);
            }
            catch (Exception ex)
            {
                return new JsonResponse(200, true, "Fail", ex.Message);
            }
        }
    }
}
