using Microsoft.AspNetCore.Identity;
using SearchTicketApp.Data;
using SearchTicketApp.Data.Models;
using SearchTicketApp.Interfaces;
using SearchTicketApp.Models.Query;

namespace SearchTicketApp.Services
{
    public class OnSaleTicketSearchService : IOnSaleTicketSearchService
    {
        private readonly TicketDbContext dbContext;
        private readonly IUserContextAccessor userContextAccessor;

        public OnSaleTicketSearchService(TicketDbContext dbContext, IUserContextAccessor userContextAccessor)
        {
            this.dbContext = dbContext;
            this.userContextAccessor = userContextAccessor;
        }
        public Task GetOnSaleTicketsAsync(OnSaleSearchQuery query)
        {
            throw new NotImplementedException();
        }

        public Task GetOnSaleTicketsAsync(OnSaleContextSearchQuery query)
        {
            throw new NotImplementedException();
        }
    }
}
