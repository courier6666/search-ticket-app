using SearchTicketApp.Data.Models;
using SearchTicketApp.Models.Query;
using SearchTicketApp.Shared;

namespace SearchTicketApp.Interfaces
{
    public interface IPurchasedTicketService : IEntityQueryService<PurchasedTicketQuery>
    {
        Task<ICollection<PurchasedTicketQuery>> GetAllForUserAsync(int userId);
        Task<PagingInfo<PurchasedTicketQuery>> GetAllForUserPagedAsync(int userId, int page, int pageSize);
    }
}
