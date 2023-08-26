using DriveMate.Entities;
using DriveMate.Requests.UserRequest;
using DriveMate.Responses.UserResponse;

namespace DriveMate.Interfaces
{
    public interface IUserService
    {
        public Task<User?> Save(IFormCollection form);
        public Task<List<User>> GetAll();
        public Task<LoginResponse> SignIn(LoginRequest loginRequest);
        public Task<User> UserIdReq(UserIdReq userIdReq);
  //      public Task<User> InsertTrip();
    }
}
