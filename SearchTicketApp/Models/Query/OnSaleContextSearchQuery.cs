using SearchTicketApp.Models.User;

namespace SearchTicketApp.Models.Query
{
    public class OnSaleContextSearchQuery : OnSaleSearchQuery
    {
        public UserContext UserCookieContext { get; set; } = default!;

    }
}
