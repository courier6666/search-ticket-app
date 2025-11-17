using SearchTicketApp.Data.Models.Abstract;

namespace SearchTicketApp.Models.Result
{
    public class UserResult : Entity
    {
        public string Email { get; set; } = default!;
    }
}
