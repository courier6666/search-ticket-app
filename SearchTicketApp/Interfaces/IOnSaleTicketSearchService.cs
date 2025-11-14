using SearchTicketApp.Data.Models;
using SearchTicketApp.Models.Query;

namespace SearchTicketApp.Interfaces
{
    public interface IOnSaleTicketSearchService
    {
        Task GetOnSaleTicketsAsync(OnSaleSearchQuery query);
        Task GetOnSaleTicketsAsync(OnSaleContextSearchQuery query);
    }
}
