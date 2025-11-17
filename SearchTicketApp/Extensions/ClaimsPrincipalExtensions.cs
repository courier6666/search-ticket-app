using System.Security.Claims;

namespace SearchTicketApp.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static int? GetUserId(this ClaimsPrincipal claims)
        {
            string? userId = claims.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return null;
            }

            return int.Parse(userId);
        }

        public static string? GetUserName(this ClaimsPrincipal claims)
        {
            return claims.FindFirstValue(ClaimTypes.Email);
        }

        public static bool IsAuthenticated(this ClaimsPrincipal claims)
        {
            return claims.Identity?.IsAuthenticated ?? false;
        }
    }
}
