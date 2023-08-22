using DriveMate.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

namespace DriveMate.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    public class ApiBaseController : Controller
    {
        protected AppDBContext DbContext => (AppDBContext)HttpContext.RequestServices.GetService(typeof(AppDBContext));
        protected Guid user_unique_id;
        protected string user_email;
        protected string user_role;


        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if(User.HasClaim(c => c.Type == "Id"))
            {
                var userid = User.Claims.SingleOrDefault(c => c.Type == "Id");
                Guid.TryParse(userid.Value.ToString(), out user_unique_id);
            }

            if (User.HasClaim(c => c.Type == "Role"))
            {
                var role = User.Claims.SingleOrDefault(c => c.Type == "Role");
                user_role = role.Value.ToString();
            }

            if (User.HasClaim(c => c.Type == "Id"))
            {
                var email = User.Claims.SingleOrDefault(c => c.Type == "Email");
                user_email = email.Value.ToString();
            }
        }
    }
}
