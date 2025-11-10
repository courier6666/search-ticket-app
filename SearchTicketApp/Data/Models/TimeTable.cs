namespace SearchTicketApp.Data.Models
{
    public class TimeTable
    {
        public int Id { get; set; }

        public DayOfWeek DayOfWeek { get; set; }

        public TimeOnly Time { get; set; }

        public TimeSpan TravelDuration { get; set; }
    }
}
