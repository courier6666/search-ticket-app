using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SearchTicketApp.Data.Models.Abstract;

namespace SearchTicketApp.Data.Models
{
    public class PurchasedTicket : Ticket
    {
        [Required]
        public DateTime DepartureTime { get; set; }

        [Required]
        public DateTime ArrivalTime { get; set; }

        [ForeignKey(nameof(Purchaser)), Required]
        public int PurchaserId { get; set; }

        public User Purchaser { get; set; } = default!;

        [Required]
        public string DepartureTimeZone { get; set; } = default!;
    }
}
