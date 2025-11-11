using System.Reflection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SearchTicketApp.Data.Models;

namespace SearchTicketApp.Data
{
    public class TicketDbContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public TicketDbContext(DbContextOptions<TicketDbContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(Assembly.GetAssembly(typeof(TicketDbContext))!);
        }

        public DbSet<OnSaleTicket> Tickets { get; set; }

        public DbSet<PurchasedTicket> PurchasedTickets { get; set; }

        public DbSet<TimeTable> TimeTables { get; set; }

        public DbSet<Location> Locations { get; set; }
    }
}
