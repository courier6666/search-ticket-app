using System.Text.Json.Serialization;

namespace SearchTicketApp.Models.User
{
    public class UserContext
    {
        [JsonPropertyName("timeZone")]
        public string TimeZone { get; set; }

        [JsonPropertyName("location")]
        public UserLocation Location { get; set; }
    }
}
