using SearchTicketApp.Models.Result;

namespace SearchTicketApp.Models.ViewModels
{
    public class ProfileViewModel
    {
        public UserResult UserResult { get; set; }

        public int BusCount { get; set; }

        public int TrainCount { get; set; }

        public int PlaneCount { get; set; }
    }
}
