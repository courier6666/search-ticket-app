using SearchTicketApp.Models.Query;
using SearchTicketApp.Models.Result;
using SearchTicketApp.Shared;

namespace SearchTicketApp.Models.ViewModels
{
    public class TicketContextSearchViewModel
    {
        public PagingInfo<OnSaleTicketResult> Tickets { get; set; }
        public OnSaleContextSearchQuery Query { get; set; }
    }
}
