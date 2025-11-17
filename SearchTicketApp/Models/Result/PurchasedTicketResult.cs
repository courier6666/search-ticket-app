using SearchTicketApp.Models.Abstracts;

namespace SearchTicketApp.Models.Result
{
    public class PurchasedTicketResult : TicketResult
    {
        public int PurchaserId { get; set; }

        public UserResult Purchaser { get; set; } = default!;
    }
}
