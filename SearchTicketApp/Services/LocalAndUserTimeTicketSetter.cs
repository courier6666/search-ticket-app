using SearchTicketApp.Helpers;
using SearchTicketApp.Models.Abstracts;
using SearchTicketApp.Models.User;

namespace SearchTicketApp.Services
{
    public static class LocalAndUserTimeTicketSetter
    {
        public static void SetLocalTime(TicketResult result)
        {
            result.DepartureTimeLocal = DateTimeConverter.ToLocal(result.DepartureTimeUtc, result.DepartureLocalTimeZone);
            result.ArrivalTimeLocal = DateTimeConverter.ToLocal(result.ArrivalTimeUtc, result.DepartureLocalTimeZone);
        }

        public static void SetUserTime(TicketResult result, UserContext userContext)
        {
            result.UserLocalTimeZone = userContext.TimeZone;
            result.DepartureTimeUser = DateTimeConverter.ToLocal(result.DepartureTimeUtc, userContext.TimeZone);
            result.ArrivalTimeUser = DateTimeConverter.ToLocal(result.ArrivalTimeUtc, userContext.TimeZone);
        }
    }
}
