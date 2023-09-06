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
    public class TripController : ApiBaseController
    {
        private ILogger<TripController> logger;
        private ITripService _tripService;
        
        public TripController(ILogger<TripController> logger, ITripService tripService)
        {
            this.logger = logger;
            this._tripService = tripService;
        }

        [HttpPost(Name = "InserTrip")]
        public async Task<JsonResponse> InserTrip(TripRequest tripRequest)
        {
            try
            {
                if (user_role == "C")
                {
                    tripRequest.CustomerId = user_unique_id;
                }
                else
                {
                    tripRequest.DriverId = user_unique_id;
                }

                var result = await _tripService.InsertTripAsync(tripRequest);
                return new JsonResponse(200, true, "Success", result);
            }
            catch (Exception ex)
            {
                return new JsonResponse(200, true, "Fail", ex.Message);
            }
        }

        [HttpGet(Name = "GetRemainTrips")]
        public async Task<JsonResponse> GetRemainTrips()
        {
            try
            {
                var result = await _tripService.GetRemainTripAsync();
                return new JsonResponse(200, true, "Success", result);
            }
            catch (Exception ex)
            {
                return new JsonResponse(200, true, "Fail", ex.Message);
            }
        }


    }
}
