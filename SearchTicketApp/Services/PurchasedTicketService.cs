using Microsoft.EntityFrameworkCore;
using SearchTicketApp.Data;
using SearchTicketApp.Data.Models.Abstract;
using SearchTicketApp.Interfaces;
using SearchTicketApp.Mapping.Expressions;
using SearchTicketApp.Models.Result;
using SearchTicketApp.Shared;
using static SearchTicketApp.Services.LocalAndUserTimeTicketSetter;

namespace SearchTicketApp.Services
{
    public class PurchasedTicketService : IPurchasedTicketService
    {
        private readonly TicketDbContext dbContext;
        private readonly IUserContextAccessor userContextAccessor;

        public PurchasedTicketService(TicketDbContext dbContext, IUserContextAccessor userContextAccessor)
        {
            this.dbContext = dbContext;
            this.userContextAccessor = userContextAccessor;
        }

        private IQueryable<PurchasedTicketResult> GetPurchasedTicketQuery()
        {
            return this.dbContext.PurchasedTickets.
                Include(t => t.Destination).
                Include(t => t.Origin).
                Select(PurchasedTicketMappingExpression.ToPurchasedTicketQuery);
        }

        public async Task<ICollection<PurchasedTicketResult>> GetAllAsync()
        {
            var tickets = await GetPurchasedTicketQuery().
                ToListAsync();

            var userContext = userContextAccessor.GetUserContext();

            if (userContext != null)
                foreach (var ticket in tickets)
                {
                    SetLocalAndUserTime(ticket, userContext);
                }

            return tickets;
        }

        public async Task<ICollection<PurchasedTicketResult>> GetAllForUserAsync(int userId)
        {
            var usersTickets = await GetPurchasedTicketQuery().
                Where(t => t.PurchaserId == userId).
                ToListAsync();

            var userContext = userContextAccessor.GetUserContext();

            if (userContext != null)
                foreach (var ticket in usersTickets)
                {
                    SetLocalAndUserTime(ticket, userContext);
                }

            return usersTickets;
        }

        public async Task<PagingInfo<PurchasedTicketResult>> GetAllForUserPagedAsync(int userId, int page, int pageSize)
        {
            var ticketsQuery = GetPurchasedTicketQuery().
                Where(t => t.PurchaserId == userId);

            var ticketsCount = await ticketsQuery.CountAsync();

            var pagedTickets = await ticketsQuery.
                Skip((page - 1) * pageSize).
                Take(pageSize).
                ToListAsync();

            return PagingInfo<PurchasedTicketResult>.Create(pagedTickets, ticketsCount, page, pageSize);
        }

        public async Task<PurchasedTicketResult?> GetByIdAsync(int id)
        {
            var foundTicket = await GetPurchasedTicketQuery().
                FirstOrDefaultAsync(t => t.Id == id);

            var userContext = userContextAccessor.GetUserContext();

            if (foundTicket != null && userContext != null)
            {
                SetLocalAndUserTime(foundTicket, userContext);
            }

            return foundTicket;
        }

        public async Task<PagingInfo<PurchasedTicketResult>> GetAllPagedAsync(int page, int pageSize)
        {
            var ticketsQuery = GetPurchasedTicketQuery();

            var ticketsCount = await ticketsQuery.CountAsync();

            var pagedTickets = await ticketsQuery.
                Skip((page - 1) * pageSize).
                Take(pageSize).
                ToListAsync();

            return PagingInfo<PurchasedTicketResult>.Create(pagedTickets, ticketsCount, page, pageSize);
        }
    }
}
