using SearchTicketApp.Data.Models;
using SearchTicketApp.Data.Models.Abstract;
using SearchTicketApp.Shared;

namespace SearchTicketApp.Interfaces
{
    public interface IOnSaleTicketService : IEntityService<OnSaleTicket>
    {
        Task PurchaseTicketAsync(int onSaleTicketId, int userId);
    }
}
