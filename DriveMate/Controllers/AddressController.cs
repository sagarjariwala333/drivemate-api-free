using DriveMate.BaseClass;
using DriveMate.Interfaces;
using DriveMate.Requests.TripRequest;
using DriveMate.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DriveMate.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly ILogger<UserController> logger;
        private IAddress address;

        public AddressController(ILogger<UserController> logger, IAddress address)
        {
            this.logger = logger;
            this.address = address;
        }

        [HttpGet(Name = "GetAllAddresses")]
        public async Task<JsonResponse> GetAllAddresses()
        {
            try
            {
                var result = await address.GetAll();
                return new JsonResponse(200, true, "Success", result);
            }
            catch (Exception ex)
            {
                return new JsonResponse(200, true, "Fail", ex.Message);
            }
        }

        [HttpPost(Name = "Add")]
        public async Task<JsonResponse> Add(AddressModal addressModal)
        {
            try
            {
                var result = await address.SaveAsync(addressModal);
                return new JsonResponse(200, true, "Success", result);
            }
            catch (Exception ex)
            {
                return new JsonResponse(200, true, "Fail", ex.Message);
            }
        }
    }
}
