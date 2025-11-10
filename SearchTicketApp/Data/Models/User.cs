using Microsoft.AspNetCore.Identity;

namespace SearchTicketApp.Data.Models
{
    public class User : IdentityUser<int>
    {
        public ICollection<Ticket> PurchasedTickets { get; set; } = default!;
    }
}
