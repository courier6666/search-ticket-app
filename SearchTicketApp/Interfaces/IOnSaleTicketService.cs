using SearchTicketApp.Data.Models;
using SearchTicketApp.Data.Models.Abstract;
using SearchTicketApp.Models.Query;
using SearchTicketApp.Shared;

namespace SearchTicketApp.Interfaces
{
    public interface IOnSaleTicketService :
        IEntityCommandService<OnSaleTicketCommand>,
        IEntityQueryService<OnSaleTicketQuery>
    {
        Task PurchaseTicketAsync(int onSaleTicketId, int userId);
    }
}
