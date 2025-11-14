using SearchTicketApp.Data.Models;
using SearchTicketApp.Data.Models.Abstract;
using System.Net.Sockets;
using SearchTicketApp.Helpers;

namespace SearchTicketApp.Mapping.Extensions
{
    public static class OnSaleTicketMappingExtension
    {
        private static void SetTimeOfTicketFromLocalToUtc(OnSaleTicketCommand ticketCommand, OnSaleTicket ticket)
        {
            ticket.ArrivalTimeUtc = DateTimeConverter.ToUtc(ticketCommand.ArrivalTime, ticketCommand.DepartureLocalTimeZone);
            ticket.DepartureTimeUtc = DateTimeConverter.ToUtc(ticketCommand.DepartureTime, ticketCommand.DepartureLocalTimeZone);
        }

        public static void MapToExisting(this OnSaleTicketCommand ticketCommand, OnSaleTicket ticket)
        {
            ticket.Title = ticketCommand.Title;
            ticket.TravelTransportationType = ticketCommand.TravelTransportationType;
            ticket.Price = ticketCommand.Price;
            ticket.DepartureLocalTimeZone = ticketCommand.DepartureLocalTimeZone;
            ticketCommand.Origin.MapToExisting(ticket.Origin);
            ticketCommand.Destination.MapToExisting(ticket.Destination);

            SetTimeOfTicketFromLocalToUtc(ticketCommand, ticket);
        }

        public static OnSaleTicket Map(this OnSaleTicketCommand ticketCommand)
        {
            var onSaleTicket = new OnSaleTicket()
            {
                Id = ticketCommand.Id,
                Title = ticketCommand.Title,
                TravelTransportationType = ticketCommand.TravelTransportationType,
                Price = ticketCommand.Price,
                DepartureLocalTimeZone = ticketCommand.DepartureLocalTimeZone,
                Origin = ticketCommand.Origin.Map(),
                Destination = ticketCommand.Destination.Map(),
            };

            SetTimeOfTicketFromLocalToUtc(ticketCommand, onSaleTicket);
            return onSaleTicket;
        }
    }
}
