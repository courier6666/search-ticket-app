using SearchTicketApp.Data.Models.Abstract;
using SearchTicketApp.Helpers;
using SearchTicketApp.Models.Abstract;
using SearchTicketApp.Models.Abstracts;

namespace SearchTicketApp.Mapping.Extensions
{
    public static class TicketMappingExtension
    {
        public static void MapToExisting(this TicketCommand ticketCommand, Ticket ticket)
        {
            ticket.Title = ticketCommand.Title;
            ticket.TravelTransportationType = ticketCommand.TravelTransportationType;
            ticket.ArrivalTimeUtc = DateTimeConverter.ToUtc(ticketCommand.ArrivalTime, ticketCommand.DepartureLocalTimeZone);
            ticket.DepartureTimeUtc = DateTimeConverter.ToUtc(ticketCommand.DepartureTime, ticketCommand.DepartureLocalTimeZone);
            ticket.Price = ticketCommand.Price;
            ticket.DepartureLocalTimeZone = ticketCommand.DepartureLocalTimeZone;

            ticketCommand.Origin.MapToExisting(ticket.Origin);
            ticketCommand.Destination.MapToExisting(ticket.Destination);
        }
    }
}
