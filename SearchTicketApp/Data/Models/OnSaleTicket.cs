using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SearchTicketApp.Data.Models.Abstract;

namespace SearchTicketApp.Data.Models
{
    public class OnSaleTicket : Ticket
    {
        public long ViewsCount { get; set; } = 0;

        public long PurchaseCount { get; set; } = 0;
    }
}
