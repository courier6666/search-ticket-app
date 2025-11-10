using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using SearchTicketApp.Data.Models;

namespace SearchTicketApp.Data
{
    public class TicketDbContext : IdentityDbContext<User>
    {
    }
}
