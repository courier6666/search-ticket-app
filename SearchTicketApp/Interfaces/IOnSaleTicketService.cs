using SearchTicketApp.Data.Models;
using SearchTicketApp.Data.Models.Abstract;
using SearchTicketApp.Models.Command;
using SearchTicketApp.Models.Query;
using SearchTicketApp.Models.Result;
using SearchTicketApp.Shared;

namespace SearchTicketApp.Interfaces
{
    public interface IOnSaleTicketService :
        IEntityCommandService<OnSaleTicketCommand>,
        IEntityQueryService<OnSaleTicketResult>
    {
        Task PurchaseTicketAsync(int onSaleTicketId, int userId);

        Task ViewTicketAsync(int onSaleTicketId);

        Task<PagingInfo<OnSaleTicketResult>> GetAllPagedBasedOnQueryAsync(OnSaleSearchQuery query,
            int page, int pageSize);

        Task<TicketTravelTypeViewsByUserResult> GetTicketTravelTypeViewsByUserAsync(int userId);
    }
}
