using DriveMate.BaseClass;
using DriveMate.Entities;
using DriveMate.Interfaces;
using DriveMate.Requests.UserRequest;
using DriveMate.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DriveMate.Controllers
{

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> logger;
        private IUserService userService;

        public UserController(ILogger<UserController> logger, IUserService userService)
        {
            this.logger = logger;
            this.userService = userService;
        }


        [Authorize]
        [HttpGet(Name = "GetAll")]
        public async Task<JsonResponse> GetAll()
        {
            try
            {
                var result = await userService.GetAll();
                return new JsonResponse(200, true, "Success", result);
            }
            catch (Exception ex)
            {
                return new JsonResponse(200, true, "Fail", ex.Message);
            }
        }

        [HttpPost(Name = "SignIn")]
        public async Task<JsonResponse> SignIn(LoginRequest loginRequest)
        {
            try
            {
                var result = await userService.SignIn(loginRequest);
                return new JsonResponse(200, true, "Success", result);
            }
            catch (Exception ex)
            {
                return new JsonResponse(200, true, "Fail", ex.Message);
            }
        }

        [HttpPost(Name = "SignUp")]
        public async Task<JsonResponse> SignUp(IFormCollection form)
        {
            
            try
            {
                var result = await userService.Save(form);
                return new JsonResponse(200, true, "Success", result);
            }
            catch(Exception ex)
            {
                return new JsonResponse(200, true, "Fail", ex.Message);
            }
        }
    }
}
