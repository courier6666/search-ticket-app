using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SearchTicketApp.Data.Models.Abstract
{
    public abstract class Ticket : Entity
    {

        [MaxLength(200), Required]
        public string Title { get; set; } = default!;

        [Required]
        public TravelTransportationType TravelTransportationType { get; set; } = default!;

        [ForeignKey(nameof(Destination)), Required]
        public int DestinationId { get; set; }

        public Location Destination { get; set; } = default!;

        [ForeignKey(nameof(Origin)), Required]
        public int OriginId { get; set; }

        public Location Origin { get; set; } = default!;

        [Range(0.0f, float.PositiveInfinity), Required]
        public float Price { get; set; }

        [Required]
        public DateTime DepartureTimeUtc { get; set; }

        [Required]
        public DateTime ArrivalTimeUtc { get; set; }

        [Required, MaxLength(200)]
        public string DepartureLocalTimeZone { get; set; } = default!;
    }
}
