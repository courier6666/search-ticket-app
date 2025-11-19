using SearchTicketApp.Models.Query;
using SearchTicketApp.Models.Result;
using SearchTicketApp.Shared;

namespace SearchTicketApp.Interfaces
{
    public interface IOnSaleTicketContextSearchService
    {
        public Task<PagingInfo<OnSaleTicketResult>> GetAllPagedBasedOnQueryAsync(OnSaleContextSearchQuery query,
            int page, int pageSize);
    }
}
