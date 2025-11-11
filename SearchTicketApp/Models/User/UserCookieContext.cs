namespace SearchTicketApp.Models.User
{
    public class UserCookieContext
    {
        public string TimeZone { get; set; }

        public DateTime TimeUTC { get; set; }

        public UserLocation Location { get; set; }
    }
}
