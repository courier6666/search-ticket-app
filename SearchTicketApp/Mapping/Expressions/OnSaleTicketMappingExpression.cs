using System.Linq.Expressions;
using SearchTicketApp.Data.Models;
using SearchTicketApp.Models.Result;

namespace SearchTicketApp.Mapping.Expressions
{
    public static class OnSaleTicketMappingExpression
    {
        public static Expression<Func<OnSaleTicket, OnSaleTicketResult>> GetOnSaleTicketQuery()
            => (ticket) => new OnSaleTicketResult()
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
                PurchaseCount = ticket.PurchaseCount,
                ViewsCount = ticket.ViewsCount,
                DepartureTimeUtc = ticket.DepartureTimeUtc,
                ArrivalTimeUtc = ticket.ArrivalTimeUtc,
                DepartureLocalTimeZone = ticket.DepartureLocalTimeZone,
            };

        public static Expression<Func<OnSaleTicket, OnSaleTicketResult>> GetOnSaleTicketWithPurchaseStatusByUserQuery(int userId)
            => (ticket) => new OnSaleTicketResult()
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
                PurchaseCount = ticket.PurchaseCount,
                ViewsCount = ticket.ViewsCount,
                DepartureTimeUtc = ticket.DepartureTimeUtc,
                ArrivalTimeUtc = ticket.ArrivalTimeUtc,
                DepartureLocalTimeZone = ticket.DepartureLocalTimeZone,
                IsBought = ticket.PurchasedTickets
                    .Where(pt => pt.PurchaserId == userId)
                    .Select(pt => pt.Id)
                    .Any(),
            };
    }
}
