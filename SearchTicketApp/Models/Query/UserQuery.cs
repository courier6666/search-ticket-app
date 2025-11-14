using SearchTicketApp.Data.Models.Abstract;

namespace SearchTicketApp.Models.Query
{
    public class UserQuery : Entity
    {
        public string Email { get; set; } = default!;
    }
}
