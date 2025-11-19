using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SearchTicketApp.Data;
using SearchTicketApp.Data.Models;
using SearchTicketApp.Extensions;
using SearchTicketApp.Interfaces;
using SearchTicketApp.Models.Query;
using SearchTicketApp.Models.Result;
using System.Linq.Expressions;
using static SearchTicketApp.Services.LocalAndUserTimeTicketSetter;
using static SearchTicketApp.Mapping.Expressions.OnSaleTicketMappingExpression;

namespace SearchTicketApp.Services
{
    public class OnSaleTicketQueryFactory
    {
        private const double DistRelevancyModifier = 2;
        private const double TimeZoneRelevancyModifier = 1;
        private const double PopularRelevancyModifier = 1;
        private const double BasedOnPreferenceModifier = 1.5;

        private readonly TicketDbContext dbContext;
        private readonly IUserContextAccessor userContextAccessor;
        private readonly IHttpContextAccessor httpContextAccessor;

        public OnSaleTicketQueryFactory(TicketDbContext dbContext,
            IUserContextAccessor userContextAccessor,
            IHttpContextAccessor httpContextAccessor)
        {
            this.dbContext = dbContext;
            this.userContextAccessor = userContextAccessor;
            this.httpContextAccessor = httpContextAccessor;
        }

        protected void SetLocalAndUserTime(OnSaleTicketResult onSaleTicket)
        {
            var userContext = this.userContextAccessor.GetUserContext();

            SetLocalTime(onSaleTicket);

            if (userContext != null)
                SetUserTime(onSaleTicket, userContext);
        }

        protected IQueryable<OnSaleTicketResult> GetOnSaleTicketQuery(Expression<Func<OnSaleTicket, OnSaleTicketResult>> mappingExpression)
        {
            return this.dbContext.Tickets.
                Include(t => t.Destination).
                Include(t => t.Origin).
                Include(t => t.PurchasedTickets).
                Select(mappingExpression);
        }

        protected Expression<Func<OnSaleTicket, OnSaleTicketResult>> GetQueryMappingExpressionBasedOnContext()
        {
            var userContext = this.userContextAccessor.GetUserContext();
            var userClaims = this.httpContextAccessor.HttpContext!.User;

            //if user authenticated, include purchase status of sale ticket
            if (userClaims.IsAuthenticated())
            {
                if (userContext?.Location != null)
                {
                    return OnSaleTicketWithUserStatusAndDistance(
                        userClaims.GetUserId() ?? 0,
                        userContext.Location.Latitude,
                        userContext.Location.Longitude);
                }
                else
                {
                    return OnSaleTicketWithUserStatus(userClaims.GetUserId() ?? 0);
                }
            }

            //if user is not authenticated, do not include ticket purchase status
            if (userContext?.Location != null)
            {
                return OnSaleTicketWithUserDistance(userContext.Location.Latitude, userContext.Location.Longitude);
            }

            return OnSaleTicketQuery();
        }

        protected IQueryable<OnSaleTicketResult> FilterOnSaleTickets(IQueryable<OnSaleTicketResult> ticketsQuery, OnSaleSearchQuery query)
        {
            if (!string.IsNullOrWhiteSpace(query.Title))
            {
                var title = query.Title.Trim();
                ticketsQuery = ticketsQuery.Where(t => t.Title.Contains(title));
            }

            if (!string.IsNullOrWhiteSpace(query.Settlement))
            {
                var settlement = query.Settlement.Trim();
                ticketsQuery = ticketsQuery.Where(t => t.Origin.Settlement.Contains(settlement));
            }

            if (query.PriceLower.HasValue)
            {
                ticketsQuery = ticketsQuery.Where(t => t.Price >= query.PriceLower.Value);
            }

            if (query.PriceUpper.HasValue)
            {
                ticketsQuery = ticketsQuery.Where(t => t.Price <= query.PriceUpper.Value);
            }

            if (query.TravelTransportationType.HasValue)
            {
                ticketsQuery = ticketsQuery.Where(t => t.TravelTransportationType == query.TravelTransportationType.Value);
            }

            return ticketsQuery;
        }


        protected async Task<IQueryable<OnSaleTicketResult>> FilterOnSaleTicketsWithContextAsync(IQueryable<OnSaleTicketResult> ticketsQuery,
            OnSaleContextSearchQuery query)
        {

            var userContext = this.userContextAccessor.GetUserContext();
            ticketsQuery = FilterOnSaleTickets(ticketsQuery, query);

            ticketsQuery = ticketsQuery.Where(t => t.DepartureTimeUtc >= DateTime.UtcNow);

            if (query.MyTimeZone)
            {
                ticketsQuery = ticketsQuery.Where(t => t.DepartureLocalTimeZone == userContext!.TimeZone);
            }

            if (query.ClosestToMe)
            {
                return ticketsQuery.OrderBy(t => t.DistanceFromUserKm);
            }

            if (query.MostPopular)
            {
                return ticketsQuery.OrderByDescending(t => t.ViewsCount).ThenBy(t => t.PurchaseCount);
            }

            if (query.MostRelevant)
            {
                var maxViews = await this.dbContext.Tickets.MaxAsync(t => t.ViewsCount);
                var maxPurchases = await this.dbContext.Tickets.MaxAsync(t => t.PurchaseCount);
                var maxDistance = await GetOnSaleTicketQuery(GetQueryMappingExpressionBasedOnContext())
                    .MaxAsync(t => t.DistanceFromUserKm);

                var viewedTicketsQuery = this.dbContext.Users.Include(u => u.ViewedTickets)
                    .SelectMany(u => u.ViewedTickets);

                var busTicketsCount = await viewedTicketsQuery
                    .CountAsync(t => t.TravelTransportationType == TravelTransportationType.Bus);

                var trainTicketsCount = await viewedTicketsQuery
                    .CountAsync(t => t.TravelTransportationType == TravelTransportationType.Train);

                var planeTicketsCount = await viewedTicketsQuery
                    .CountAsync(t => t.TravelTransportationType == TravelTransportationType.Plane);

                var viewedTicketsCount = await viewedTicketsQuery.CountAsync();

                var busCoef = viewedTicketsCount > 0 ? busTicketsCount * 1.0 / viewedTicketsCount : 0;
                var trainCoef = viewedTicketsCount > 0 ? trainTicketsCount * 1.0 / viewedTicketsCount : 0;
                var planeCoef = viewedTicketsCount > 0 ? planeTicketsCount * 1.0 / viewedTicketsCount : 0;

                return ticketsQuery.OrderByDescending(t =>
                    (1 - t.DistanceFromUserKm / maxDistance) * DistRelevancyModifier
                    + (t.DepartureLocalTimeZone == userContext!.TimeZone ? 1 : 0) * TimeZoneRelevancyModifier
                    + (1 - (t.PurchaseCount + t.ViewsCount) / (maxPurchases + maxViews)) * PopularRelevancyModifier
                    + (
                        t.TravelTransportationType == TravelTransportationType.Bus ? busCoef
                        : t.TravelTransportationType == TravelTransportationType.Train ? trainCoef
                        : t.TravelTransportationType == TravelTransportationType.Plane ? planeCoef
                        : 0
                    ) * BasedOnPreferenceModifier);
            }

            return ticketsQuery;
        }
    }
}
