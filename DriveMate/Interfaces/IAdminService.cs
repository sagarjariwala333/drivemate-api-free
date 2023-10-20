using DriveMate.BaseClass;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;

namespace DriveMate.Interfaces
{
    public interface IAdminService
    {
        public Task<JsonResponse> GetAllTrips(Guid UserId);
        public Task<JsonResponse> GetTripsById(Guid Id);
        public Task<JsonResponse> GetAllUsers(string filterOn = null, string filterQuery = null, string sortBy = null, bool isAscending = true, string Role = null);
        public Task<JsonResponse> GetUserById(Guid Id);
    }
}