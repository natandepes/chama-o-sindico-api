using System.Security.Claims;

namespace ChamaOSindico.WebAPI.Extensions
{
    public static class HttpContextExtensions
    {
        public static string? GetUserId(this ClaimsPrincipal user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
                
            var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier);
            return userIdClaim?.Value;
        }
    }
}
