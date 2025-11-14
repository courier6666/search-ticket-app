using SearchTicketApp.Data.Models;
using SearchTicketApp.Models.Query;
using SearchTicketApp.Shared;

namespace SearchTicketApp.Interfaces
{
    public interface IOnSaleTicketSearchService
    {
        Task<PagingInfo<OnSaleTicketQuery>> GetAllTicketsPagedAsync(OnSaleSearchQuery query);
        Task<PagingInfo<OnSaleTicketQuery>> GetAllTicketsWithUserContextPagedAsync(OnSaleSearchQuery query);
    }
}
