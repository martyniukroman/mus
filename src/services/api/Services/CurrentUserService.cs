using application.common.interfaces;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace api.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        public string UserId { get; }
        public bool IsAuthenticated { get; }

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            this.UserId = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}
