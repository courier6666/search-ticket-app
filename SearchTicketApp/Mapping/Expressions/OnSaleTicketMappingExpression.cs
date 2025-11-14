using System.Linq.Expressions;
using SearchTicketApp.Data.Models;
using SearchTicketApp.Models.Query;

namespace SearchTicketApp.Mapping.Expressions
{
    public static class OnSaleTicketMappingExpression
    {
        public static Expression<Func<OnSaleTicket, OnSaleTicketQuery>> ToOnSaleTicketQuery
            => (ticket) => new OnSaleTicketQuery()
            {
                Id = ticket.Id,
                Title = ticket.Title,
                TravelTransportationType = ticket.TravelTransportationType,
                DestinationId = ticket.DestinationId,
                Destination = new LocationQuery()
                {
                    Id = ticket.Destination.Id,
                    Latitude = ticket.Destination.Latitude,
                    Longitude = ticket.Destination.Longitude,
                    Settlement = ticket.Destination.Settlement,
                },
                Origin = new LocationQuery()
                {
                    Id = ticket.Origin.Id,
                    Latitude = ticket.Origin.Latitude,
                    Longitude = ticket.Origin.Longitude,
                    Settlement = ticket.Origin.Settlement,
                },
                Price = ticket.Price,
                PurchaseCount = ticket.PurchaseCount,
                ViewsCount = ticket.ViewsCount,
                DepartureTimeUtc = ticket.DepartureTimeUtc,
                ArrivalTimeUtc = ticket.ArrivalTimeUtc,
                DepartureLocalTimeZone = ticket.DepartureLocalTimeZone,
            }; 
    }
}
