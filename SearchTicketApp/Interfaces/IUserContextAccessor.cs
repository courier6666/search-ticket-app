using SearchTicketApp.Models.User;

namespace SearchTicketApp.Interfaces
{
    public interface IUserContextAccessor
    { 
        UserContext? GetUserContext();
    }
}
