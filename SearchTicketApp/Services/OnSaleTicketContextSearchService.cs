using SearchTicketApp.Data;
using SearchTicketApp.Factories;
using SearchTicketApp.Interfaces;
using SearchTicketApp.Models.Query;
using SearchTicketApp.Models.Result;
using SearchTicketApp.Shared;

namespace SearchTicketApp.Services
{
    public class OnSaleTicketContextSearchService : OnSaleTicketQueryFactory, IOnSaleTicketContextSearchService
    {
        public OnSaleTicketContextSearchService(TicketDbContext dbContext,
            IUserContextAccessor userContextAccessor,
            IHttpContextAccessor httpContextAccessor)
            : base(dbContext, userContextAccessor, httpContextAccessor)
        {
        }

        public async Task<PagingInfo<OnSaleTicketResult>> GetAllPagedBasedOnQueryAsync(OnSaleContextSearchQuery query, int page, int pageSize)
        {
            var ticketsQuery = GetOnSaleTicketQuery(GetQueryMappingExpressionBasedOnContext());
            ticketsQuery = await FilterOnSaleTicketsWithContextAsync(ticketsQuery, query);

            var pagedTickets = await PagingInfoFactory.CreateFromQueryable(ticketsQuery, page, pageSize);

            foreach (var ticket in pagedTickets.Items)
            {
                SetLocalAndUserTime(ticket);
            }

            return pagedTickets;
        }
    }
}
