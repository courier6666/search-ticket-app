using SearchTicketApp.Models.Abstracts;

namespace SearchTicketApp.Models.Result
{
    public class OnSaleTicketResult : TicketResult
    {
        public long ViewsCount { get; set; } = 0;

        public long PurchaseCount { get; set; } = 0;

        public bool IsBought { get; set; } = false;
    }
}
