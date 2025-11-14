using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SearchTicketApp.Models.Dto;
using SearchTicketApp.Validation.Attributes;

namespace SearchTicketApp.Data.Models.Abstract
{
    public abstract class TicketCommand : Entity
    {

        [MaxLength(200), Required]
        public string Title { get; set; } = default!;

        [Required]
        public TravelTransportationType TravelTransportationType { get; set; } = default!;

        [ForeignKey(nameof(Destination)), Required]
        public int DestinationId { get; set; }

        public LocationCommand Destination { get; set; } = default!;

        [ForeignKey(nameof(Origin)), Required]
        public int OriginId { get; set; }

        public LocationCommand Origin { get; set; } = default!;

        [Range(0.0f, float.PositiveInfinity), Required]
        public float Price { get; set; }

        [Required]
        public DateTime DepartureTime { get; set; }

        [Required, GreaterThanDate(nameof(ArrivalTime))]
        public DateTime ArrivalTime { get; set; }

        [Required, MaxLength(200)]
        public string DepartureLocalTimeZone { get; set; } = default!;
    }
}
