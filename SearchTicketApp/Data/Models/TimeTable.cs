using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SearchTicketApp.Data.Models.Abstract;

namespace SearchTicketApp.Data.Models
{
    public class TimeTable : Entity
    {

        [Required]
        public DayOfWeek DayOfWeek { get; set; }

        [Required]
        public TimeOnly Time { get; set; }

        [Required]
        public TimeSpan TravelDuration { get; set; }

        [Required]
        public string DepartureTimeZone { get; set; } = default!;

        [ForeignKey(nameof(OnSaleTicket)), Required]
        public int TicketId { get; set; }

        public OnSaleTicket OnSaleTicket { get; set; } = default!;
    }
}
