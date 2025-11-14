using SearchTicketApp.Helpers;
using SearchTicketApp.Models.Abstracts;
using SearchTicketApp.Models.User;

namespace SearchTicketApp.Services
{
    public static class LocalAndUserTimeTicketSetter
    {
        public static void SetLocalAndUserTime(TicketQuery query, UserContext userContext)
        {
            query.DepartureTimeUser = DateTimeConverter.ToLocal(query.DepartureTimeUtc, userContext.TimeZone);
            query.ArrivalTimeUser = DateTimeConverter.ToLocal(query.ArrivalTimeUtc, userContext.TimeZone);

            query.DepartureTimeLocal = DateTimeConverter.ToLocal(query.DepartureTimeUtc, query.DepartureLocalTimeZone);
            query.ArrivalTimeLocal = DateTimeConverter.ToLocal(query.ArrivalTimeUtc, query.DepartureLocalTimeZone);
        }
    }
}
