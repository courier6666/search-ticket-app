using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SearchTicketApp.Data.Models
{
    public class Ticket
    {
        public int Id { get; set; }

        [MaxLength(200)]
        public string Title { get; set; } = default!;

        public DateTime DepartureTime { get; set; }

        public DateTime ArrivalTime { get; set; }

        public TravelTransportationType TravelTransportationType { get; set; } = default!;

        public TimeTable TimeTable { get; set; } = default!;

        [ForeignKey(nameof(Destination))]
        public int DestinationId { get; set; }

        public Location Destination { get; set; } = default!;

        [ForeignKey(nameof(Origin))]
        public int OriginId { get; set; }

        public Location Origin { get; set; } = default!;

        public long ViewsCount { get; set; } = 0;

        public long PurchaseCount { get; set; } = 0;

    }
}
