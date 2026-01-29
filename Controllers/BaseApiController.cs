using Microsoft.AspNetCore.Mvc;

namespace NewsApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseApiController : ControllerBase
    {
        protected Guid GetUserId()
        {
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out Guid userId))
                throw new UnauthorizedAccessException("User not authenticated");
            
            return userId;
        }

        protected bool IsAdmin()
        {
            return User.IsInRole("Admin");
        }
    }
}