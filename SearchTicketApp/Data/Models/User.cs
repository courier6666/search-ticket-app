using Microsoft.AspNetCore.Identity;
using SearchTicketApp.Data.Models.Abstract;

namespace SearchTicketApp.Data.Models
{
    public class User : IdentityUser<int>
    {
        public ICollection<PurchasedTicket> PurchasedTickets { get; set; } = default!;

        public ICollection<OnSaleTicket> ViewedTickets { get; set; } = default!;
    }
}
