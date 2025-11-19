using SearchTicketApp.Models.Query;
using SearchTicketApp.Models.Result;
using SearchTicketApp.Shared;

namespace SearchTicketApp.Models.ViewModels
{
    public class TicketSearchViewModel
    {
        public PagingInfo<OnSaleTicketResult> Tickets { get; set; }

        public OnSaleSearchQuery Query { get; set; }
    }
}
