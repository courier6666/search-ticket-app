using SearchTicketApp.Models.Abstracts;

namespace SearchTicketApp.Models.Query
{
    public class PurchasedTicketQuery : TicketQuery
    {
        public int PurchaserId { get; set; }

        public UserQuery Purchaser { get; set; } = default!;
    }
}
