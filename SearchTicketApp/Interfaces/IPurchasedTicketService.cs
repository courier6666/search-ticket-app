using SearchTicketApp.Data.Models;
using SearchTicketApp.Models.Result;
using SearchTicketApp.Shared;

namespace SearchTicketApp.Interfaces
{
    public interface IPurchasedTicketService : IEntityQueryService<PurchasedTicketResult>
    {
        Task<ICollection<PurchasedTicketResult>> GetAllForUserAsync(int userId);
        Task<PagingInfo<PurchasedTicketResult>> GetAllForUserPagedAsync(int userId, int page, int pageSize);
    }
}
