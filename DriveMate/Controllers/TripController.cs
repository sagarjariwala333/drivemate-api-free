using DriveMate.BaseClass;
using DriveMate.Entities;
using DriveMate.Interfaces;
using DriveMate.Requests.TripRequest;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DriveMate.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TripController : ControllerBase
    {
        private ILogger<TripController> logger;
        private ITripService _tripService;
        
        public TripController(ILogger<TripController> logger, ITripService tripService)
        {
            this.logger = logger;
            this._tripService = tripService;
        }

        [HttpPost(Name = "AddTrip")]
        public async Task<JsonResponse> AddTrip(TripRequest tripRequest)
        {
            try
            {
                var result = await _tripService.SaveAsync(tripRequest);
                return new JsonResponse(200, true, "Success", result);
            }
            catch (Exception ex)
            {
                return new JsonResponse(200, true, "Fail", ex.Message);
            }
        }


    }
}
