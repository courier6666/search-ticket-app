using System.Linq.Expressions;
using SearchTicketApp.Data.Models;
using SearchTicketApp.Helpers;
using SearchTicketApp.Models.Result;

namespace SearchTicketApp.Mapping.Expressions
{
    public static class OnSaleTicketMappingExpression
    {
        public static Expression<Func<OnSaleTicket, OnSaleTicketResult>> OnSaleTicketQuery()
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

        public static Expression<Func<OnSaleTicket, OnSaleTicketResult>> OnSaleTicketWithUserStatus(int userId)
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

        public static Expression<Func<OnSaleTicket, OnSaleTicketResult>> OnSaleTicketWithUserStatusAndDistance(
            int userId, double userLat, double userLon)
        {
            double toRad = Math.PI / 180f;

            return (ticket) => new OnSaleTicketResult()
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
                DistanceFromUserKm =
                    2f * DistanceCalculator.EarthRadius *
                    Math.Asin(Math.Sqrt(
                        Math.Pow(Math.Sin(((ticket.Origin.Latitude - userLat) * toRad) / 2f), 2f) +
                        Math.Cos(userLat * toRad) *
                        Math.Cos(ticket.Origin.Latitude * toRad) *
                        Math.Pow(Math.Sin(((ticket.Origin.Longitude - userLon) * toRad) / 2f), 2f)
                    ))

            };
        }

        public static Expression<Func<OnSaleTicket, OnSaleTicketResult>> OnSaleTicketWithUserDistance(double userLat, double userLon)
        {
            double toRad = Math.PI / 180f;

            return (ticket) => new OnSaleTicketResult()
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
                DistanceFromUserKm =
                    2f * DistanceCalculator.EarthRadius *
                    Math.Asin(Math.Sqrt(
                        Math.Pow(Math.Sin(((ticket.Origin.Latitude - userLat) * toRad) / 2f), 2f) +
                        Math.Cos(userLat * toRad) *
                        Math.Cos(ticket.Origin.Latitude * toRad) *
                        Math.Pow(Math.Sin(((ticket.Origin.Longitude - userLon) * toRad) / 2f), 2f)
                    ))

            };
        }
    }
}
