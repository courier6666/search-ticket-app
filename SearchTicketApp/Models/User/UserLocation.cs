using System.Text.Json.Serialization;

namespace SearchTicketApp.Models.User
{
    public class UserLocation
    {
        [JsonPropertyName("latitude")]
        public double Latitude { get; set; }

        [JsonPropertyName("longitude")]
        public double Longitude { get; set; }
    }
}
