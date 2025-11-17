using SearchTicketApp.Data.Models;
using SearchTicketApp.Models.Command;

namespace SearchTicketApp.Mapping.Extensions
{
    public static class LocationMappingExtension
    {
        public static Location Map(this LocationCommand locationCommand)
        {
            return new Location()
            {
                Id = locationCommand.Id,
                Latitude = locationCommand.Latitude,
                Longitude = locationCommand.Longitude,
                Settlement = locationCommand.Settlement,
            };
        }

        public static void MapToExisting(this LocationCommand locationCommand, Location location)
        {
            location.Latitude = locationCommand.Latitude;
            location.Longitude = locationCommand.Longitude;
            location.Settlement = locationCommand.Settlement;
        }
    }
}
