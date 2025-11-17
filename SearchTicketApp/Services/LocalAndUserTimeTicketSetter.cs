using SearchTicketApp.Helpers;
using SearchTicketApp.Models.Abstracts;
using SearchTicketApp.Models.User;

namespace SearchTicketApp.Services
{
    public static class LocalAndUserTimeTicketSetter
    {
        public static void SetLocalAndUserTime(TicketResult result, UserContext userContext)
        {
            result.DepartureTimeUser = DateTimeConverter.ToLocal(result.DepartureTimeUtc, userContext.TimeZone);
            result.ArrivalTimeUser = DateTimeConverter.ToLocal(result.ArrivalTimeUtc, userContext.TimeZone);

            result.DepartureTimeLocal = DateTimeConverter.ToLocal(result.DepartureTimeUtc, result.DepartureLocalTimeZone);
            result.ArrivalTimeLocal = DateTimeConverter.ToLocal(result.ArrivalTimeUtc, result.DepartureLocalTimeZone);
        }
    }
}
