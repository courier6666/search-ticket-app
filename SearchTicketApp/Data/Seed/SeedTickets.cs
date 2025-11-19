using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using SearchTicketApp.Data.Models;
using SearchTicketApp.Options;

namespace SearchTicketApp.Data.Seed
{
    public static class SeedTickets
    {
        private static List<OnSaleTicket> GenerateTickets(int count = 100)
        {
            var tickets = new List<OnSaleTicket>();
            var random = new Random(123); // fixed seed for reproducibility

            var locations = new[]
            {
                // Ukraine
                new {City="Kyiv", Country="Ukraine", Lat=50.4501f, Lon=30.5234f, TimeZone="Europe/Kiev"},
                new {City="Lviv", Country="Ukraine", Lat=49.8397f, Lon=24.0297f, TimeZone="Europe/Kiev"},
                new {City="Odesa", Country="Ukraine", Lat=46.4825f, Lon=30.7233f, TimeZone="Europe/Kiev"},
                new {City="Kharkiv", Country="Ukraine", Lat=50.0017f, Lon=36.2310f, TimeZone="Europe/Kiev"},
                // Europe
                new {City="Berlin", Country="Germany", Lat=52.5200f, Lon=13.4050f, TimeZone="Europe/Berlin"},
                new {City="Paris", Country="France", Lat=48.8566f, Lon=2.3522f, TimeZone="Europe/Paris"},
                new {City="London", Country="UK", Lat=51.5074f, Lon=-0.1278f, TimeZone="Europe/London"},
                // America
                new {City="New York", Country="USA", Lat=40.7128f, Lon=-74.0060f, TimeZone="America/New_York"},
                new {City="Los Angeles", Country="USA", Lat=34.0522f, Lon=-118.2437f, TimeZone="America/Los_Angeles"},
                new {City="Chicago", Country="USA", Lat=41.8781f, Lon=-87.6298f, TimeZone="America/Chicago"},
                // Asia
                new {City="Tokyo", Country="Japan", Lat=35.6895f, Lon=139.6917f, TimeZone="Asia/Tokyo"},
                new {City="Beijing", Country="China", Lat=39.9042f, Lon=116.4074f, TimeZone="Asia/Shanghai"},
                new {City="Dubai", Country="UAE", Lat=25.276987f, Lon=55.296249f, TimeZone="Asia/Dubai"}
            };

            TravelTransportationType[] transportTypes =
                { TravelTransportationType.Bus, TravelTransportationType.Train, TravelTransportationType.Plane };

            for (int i = 1; i <= count; i++)
            {
                var origin = locations[random.Next(locations.Length)];
                var dest = locations[random.Next(locations.Length)];
                while (dest.City == origin.City) dest = locations[random.Next(locations.Length)];

                var departure = DateTime.UtcNow.AddDays(random.Next(1, 30)).AddHours(random.Next(0, 24));
                var arrival = departure.AddHours(random.Next(1, 12));

                var ticket = new OnSaleTicket
                {
                    Title = $"Ticket {i}: {origin.City} → {dest.City}",
                    TravelTransportationType = transportTypes[random.Next(transportTypes.Length)],
                    Origin = new Location
                    {
                        Settlement = origin.City,
                        Latitude = origin.Lat,
                        Longitude = origin.Lon
                    },
                    Destination = new Location
                    {
                        Settlement = dest.City,
                        Latitude = dest.Lat,
                        Longitude = dest.Lon
                    },
                    Price = (float)(50 + random.NextDouble() * 150),
                    DepartureTimeUtc = departure,
                    ArrivalTimeUtc = arrival,
                    DepartureLocalTimeZone = origin.TimeZone,
                    ViewsCount = random.Next(0, 500),
                    PurchaseCount = random.Next(0, 100)
                };

                tickets.Add(ticket);
            }

            return tickets;
        }

        public static async Task SeedOnSaleTicketsAsync(WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            using var dbContext = scope.ServiceProvider.GetService<TicketDbContext>();

            if (dbContext.Tickets.Any())
                return;

            var generatedTickets = GenerateTickets();
            dbContext.Tickets.AddRange(generatedTickets);
            await dbContext.SaveChangesAsync();
        }
    }
}
