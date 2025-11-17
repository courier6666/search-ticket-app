using System.Linq.Expressions;
using SearchTicketApp.Data.Models;
using SearchTicketApp.Models.Result;

namespace SearchTicketApp.Mapping.Expressions
{
    public static class PurchasedTicketMappingExpression
    {
        public static Expression<Func<PurchasedTicket, PurchasedTicketResult>> ToPurchasedTicketQuery
            => (ticket) => new PurchasedTicketResult()
            {
                Id = ticket.Id,
                Title = ticket.Title,
                TravelTransportationType = ticket.TravelTransportationType,
                DestinationId = ticket.DestinationId,
                Destination = new LocationResult()
                {
                    Id = ticket.Destination.Id,
                    Latitude = ticket.Destination.Latitude,
                    Longitude = ticket.Destination.Longitude,
                    Settlement = ticket.Destination.Settlement,
                },
                Origin = new LocationResult()
                {
                    Id = ticket.Origin.Id,
                    Latitude = ticket.Origin.Latitude,
                    Longitude = ticket.Origin.Longitude,
                    Settlement = ticket.Origin.Settlement,
                },
                Price = ticket.Price,
                PurchaserId = ticket.PurchaserId,
                Purchaser = new UserResult()
                {
                    Id = ticket.PurchaserId,
                    Email = ticket.Purchaser.Email!,
                },
                DepartureTimeUtc = ticket.DepartureTimeUtc,
                ArrivalTimeUtc = ticket.ArrivalTimeUtc,
                DepartureLocalTimeZone = ticket.DepartureLocalTimeZone,
            };
    }
}
