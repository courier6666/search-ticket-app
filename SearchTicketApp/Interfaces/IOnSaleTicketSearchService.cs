using SearchTicketApp.Data.Models;
using SearchTicketApp.Models.Query;
using SearchTicketApp.Models.Result;
using SearchTicketApp.Shared;

namespace SearchTicketApp.Interfaces
{
    public interface IOnSaleTicketSearchService
    {
        Task<PagingInfo<OnSaleTicketResult>> GetAllTicketsPagedAsync(OnSaleSearchQuery query);
        Task<PagingInfo<OnSaleTicketResult>> GetAllTicketsWithUserContextPagedAsync(OnSaleSearchQuery query);
    }
}
