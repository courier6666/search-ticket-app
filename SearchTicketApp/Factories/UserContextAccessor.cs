using System.Text.Json;
using SearchTicketApp.Interfaces;
using SearchTicketApp.Models.User;

namespace SearchTicketApp.Factories
{
    public class UserContextAccessor : IUserContextAccessor
    {
        private const string UserContextCookieName = "userContext";

        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly JsonSerializerOptions jsonOptions;

        public UserContextAccessor(IHttpContextAccessor httpContextAccessor, JsonSerializerOptions jsonOptions)
        {
            ArgumentNullException.ThrowIfNull(httpContextAccessor);
            ArgumentNullException.ThrowIfNull(jsonOptions);

            this.httpContextAccessor = httpContextAccessor;
            this.jsonOptions = jsonOptions;
        }

        public UserContext? GetUserContext()
        {
            var cookieValue = GetUserContextCookie();
            if (cookieValue is null)
                return null;

            try
            {
                return JsonSerializer.Deserialize<UserContext>(cookieValue, jsonOptions);
            }
            catch (JsonException)
            {
                return null;
            }
        }

        private string? GetUserContextCookie()
        {
            return httpContextAccessor.HttpContext?
                .Request
                .Cookies[UserContextCookieName];
        }
    }

}
