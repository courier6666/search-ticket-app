using SearchTicketApp.Data.Models;
using SearchTicketApp.Data.Models.Abstract;
using SearchTicketApp.Models.Dto;
using SearchTicketApp.Validation.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SearchTicketApp.Models.Result;

namespace SearchTicketApp.Models.Abstracts
{
    public class TicketResult : Entity
    {
        public string Title { get; set; } = default!;

        public TravelTransportationType TravelTransportationType { get; set; } = default!;

        public int DestinationId { get; set; }

        public LocationResult Destination { get; set; } = default!;

        public int OriginId { get; set; }

        public LocationResult Origin { get; set; } = default!;

        public float Price { get; set; }

        public DateTime DepartureTimeUtc { get; set; }

        public DateTime ArrivalTimeUtc { get; set; }

        /// <summary>
        /// Departure time local to the area of departure.
        /// </summary>

        public DateTime DepartureTimeLocal { get; set; }

        /// <summary>
        /// Arrival time local to the area of departure.
        /// </summary>
        public DateTime ArrivalTimeLocal { get; set; }

        /// <summary>
        /// Departure time based on client's (user's) local time.
        /// </summary>
        public DateTime DepartureTimeUser { get; set; }

        /// <summary>
        /// Arrival time based on client's (user's) local time.
        /// </summary>
        public DateTime ArrivalTimeUser { get; set; }

        /// <summary>
        /// Time zone for the area of departure.
        /// </summary>

        public string DepartureLocalTimeZone { get; set; } = default!;

        /// <summary>
        /// Time zone of client's (user's) area.
        /// </summary>
        public string UserLocalTimeZone { get; set; } = default;
    }
}
