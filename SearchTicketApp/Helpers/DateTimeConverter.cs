using System;

namespace SearchTicketApp.Helpers
{
    public static class DateTimeConverter
    {
        private static string GetTimeZoneWindowsId(string timeZoneIaanId)
        {
            if (!TimeZoneInfo.TryConvertIanaIdToWindowsId(timeZoneIaanId, out string? windowsId))
            {
                throw new InvalidOperationException($"Cannot convert such Iaan id '{timeZoneIaanId}' to windows id.");
            }

            return windowsId!;
        }

        public static DateTime ToLocal(DateTime utcTime, string timeZoneId)
        {
            return TimeZoneInfo.ConvertTimeFromUtc(utcTime,TimeZoneInfo.FindSystemTimeZoneById(GetTimeZoneWindowsId(timeZoneId)));
        }

        public static DateTime ToUtc(DateTime localTime, string timeZoneId)
        {
            return TimeZoneInfo.ConvertTimeToUtc(localTime, TimeZoneInfo.FindSystemTimeZoneById(GetTimeZoneWindowsId(timeZoneId)));
        }
    }
}
