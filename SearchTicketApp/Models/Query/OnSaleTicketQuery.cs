using SearchTicketApp.Models.Abstracts;

namespace SearchTicketApp.Models.Query
{
    public class OnSaleTicketQuery : TicketQuery
    {
        public long ViewsCount { get; set; } = 0;

        public long PurchaseCount { get; set; } = 0;
    }
}
