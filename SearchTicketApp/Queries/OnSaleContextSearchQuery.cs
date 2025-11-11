using SearchTicketApp.Models.User;

namespace SearchTicketApp.Queries
{
    public class OnSaleContextSearchQuery : OnSaleSearchQuery
    {
        public UserCookieContext UserCookieContext { get; set; } = default!;

    }
}
