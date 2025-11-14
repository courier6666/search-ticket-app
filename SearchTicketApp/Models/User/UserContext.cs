namespace SearchTicketApp.Models.User
{
    public class UserContext
    {
        public string TimeZone { get; set; }

        public DateTime LocalTime { get; set; }

        public UserLocation Location { get; set; }
    }
}
