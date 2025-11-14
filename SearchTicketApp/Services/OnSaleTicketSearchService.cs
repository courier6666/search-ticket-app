using System.Linq.Expressions;
using Microsoft.AspNetCore.Identity;
using SearchTicketApp.Data;
using SearchTicketApp.Data.Models;
using SearchTicketApp.Interfaces;
using SearchTicketApp.Models.Query;
using SearchTicketApp.Shared;

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

        public Expression<Func<OnSaleTicketQuery, bool>> GetOnSaleTicketFilterExpression(OnSaleSearchQuery query)
        {
            throw new NotImplementedException();
        }

        public Task<PagingInfo<OnSaleTicketQuery>> GetAllTicketsPagedAsync(OnSaleSearchQuery query)
        {
            throw new NotImplementedException();
        }

        public Task<PagingInfo<OnSaleTicketQuery>> GetAllTicketsWithUserContextPagedAsync(OnSaleSearchQuery query)
        {
            throw new NotImplementedException();
        }
    }
}
