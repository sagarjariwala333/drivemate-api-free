using DriveMate.BaseClass;
using DriveMate.Entities;
using DriveMate.HelperClasses;
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

                var result = await _tripService.InsertTripAsync(tripRequest, user_unique_id);
                return new JsonResponse(200, true, "Success", result);
            }
            catch(BookedTripsLimitException btlex)
            {
                return new JsonResponse(200, true, "Limit exceed", btlex.Message);
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

        [HttpPost(Name = "AcceptTrip")]
        public async Task<JsonResponse> AcceptTripAsync(AcceptTripRequest acceptTripRequest)
        {
            try
            {
                //tripRequest.DriverId = user_unique_id;
                var result = await _tripService.DriverAcceptTripAsync((Guid)acceptTripRequest.Id, user_unique_id);
                return new JsonResponse(200, true, "Success", result);
            }
            catch (Exception ex)
            {
                return new JsonResponse(200, true, "Fail", ex.Message);
            }
        }

        [HttpGet(Name = "ViewBookedTrip")]
        public async Task<JsonResponse> ViewBookedTrip()
        {
            try
            {
                //tripRequest.DriverId = user_unique_id;
                var result = await _tripService.ViewBookedTrips(user_unique_id);
                return new JsonResponse(200, true, "Success", result);
            }
            catch (Exception ex)
            {
                return new JsonResponse(200, true, "Fail", ex.Message);
            }
        }

        /*[HttpGet(Name = "CustomerViewBookedTrip")]
        public async Task<JsonResponse> CustomerViewBookedTrip()
        {
            try
            {
                //tripRequest.DriverId = user_unique_id;
                var result = await _tripService.CustomerViewBookedTrips(user_unique_id);
                return new JsonResponse(200, true, "Success", result);
            }
            catch (Exception ex)
            {
                return new JsonResponse(200, true, "Fail", ex.Message);
            }
        }*/

        [HttpPost(Name = "StartTrip")]
        public async Task<JsonResponse> StartTrip(StartTripRequest startTripRequest)
        {
            try
            {
                //tripRequest.DriverId = user_unique_id;
                var result = await _tripService.StartTrip(startTripRequest.Id);
                return new JsonResponse(200, true, "Success", result);
            }
            catch (Exception ex)
            {
                return new JsonResponse(200, true, "Fail", ex.Message);
            }
        }

        [HttpPost(Name = "EndTrip")]
        public async Task<JsonResponse> EndTrip(EndTripRequest endTripRequest)
        {
            try
            {
                //tripRequest.DriverId = user_unique_id;
                var result = await _tripService.EndTrip(endTripRequest.Id);
                return new JsonResponse(200, true, "Success", result);
            }
            catch (Exception ex)
            {
                return new JsonResponse(200, true, "Fail", ex.Message);
            }
        }


    }
}
