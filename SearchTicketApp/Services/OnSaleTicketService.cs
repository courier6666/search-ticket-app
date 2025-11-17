using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SearchTicketApp.Data;
using SearchTicketApp.Data.Models;
using SearchTicketApp.Data.Models.Abstract;
using SearchTicketApp.Extensions;
using SearchTicketApp.Interfaces;
using SearchTicketApp.Mapping.Expressions;
using SearchTicketApp.Mapping.Extensions;
using SearchTicketApp.Models.Command;
using SearchTicketApp.Models.Result;
using SearchTicketApp.Models.User;
using SearchTicketApp.Shared;
using static SearchTicketApp.Services.LocalAndUserTimeTicketSetter;

namespace SearchTicketApp.Services
{
    public class OnSaleTicketService : IOnSaleTicketService
    {
        private readonly TicketDbContext dbContext;
        private readonly UserManager<User> userManager;
        private readonly IUserContextAccessor userContextAccessor;
        private readonly IHttpContextAccessor httpContextAccessor;

        public OnSaleTicketService(TicketDbContext dbContext,
            UserManager<User> userManager,
            IUserContextAccessor userContextAccessor,
            IHttpContextAccessor httpContextAccessor)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
            this.userContextAccessor = userContextAccessor;
            this.httpContextAccessor = httpContextAccessor;
        }

        private IQueryable<OnSaleTicketResult> GetOnSaleTicketQuery()
        {
            if (this.httpContextAccessor.HttpContext?.User.IsAuthenticated() ?? false)
            {
                return this.dbContext.Tickets.Include(t => t.Destination).Include(t => t.Origin)
                    .Include(t => t.PurchasedTickets)
                    .Select(OnSaleTicketMappingExpression.GetOnSaleTicketWithPurchaseStatusByUserQuery(
                        this.httpContextAccessor.HttpContext.User.GetUserId() ?? 0));
            }
            else
            {

                return this.dbContext.Tickets.
                    Include(t => t.Destination).
                    Include(t => t.Origin).
                    Select(OnSaleTicketMappingExpression.GetOnSaleTicketQuery());
            }
        }

        public async Task AddAsync(OnSaleTicketCommand entity)
        {
            await this.dbContext.Tickets.AddAsync(entity.Map());
            await this.dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(OnSaleTicketCommand entity)
        {
            await DeleteByIdAsync(entity.Id);
        }

        public async Task DeleteByIdAsync(int id)
        {
            var foundTicket = await this.dbContext.Tickets.FindAsync(id);
            if (foundTicket == null)
            {
                throw new InvalidOperationException("Cannot delete unexisting ticket.");
            }

            this.dbContext.Tickets.Remove(foundTicket);
            await this.dbContext.SaveChangesAsync();
        }

        #region PurchaseTicket

        private static void ValidateUser(User? user, int userId)
        {
            if (user == null)
            {
                throw new InvalidOperationException($"User does not exist by such id '{userId}'!");
            }
        }

        private static void ValidateTicket(OnSaleTicket? ticket, int ticketId)
        {
            if (ticket == null)
            {
                throw new InvalidOperationException($"Ticket with id '{ticketId}' has not been found!");
            }
        }

        public async Task PurchaseTicketAsync(int onSaleTicketId, int userId)
        {
            var foundTicket = await this.dbContext.Tickets.
                Include(t => t.Origin).
                Include(t => t.Destination).
                FirstOrDefaultAsync(t => t.Id == onSaleTicketId);

            var user = await this.dbContext.Users.FindAsync(userId);

            ValidateUser(user, onSaleTicketId);
            ValidateTicket(foundTicket, userId);

            await AddAndSavePurchasedTicketAsync(CreatePurchaseTicket(foundTicket!, user!));
        }

        private static PurchasedTicket CreatePurchaseTicket(OnSaleTicket ticket, User purchaser)
        {
            return new PurchasedTicket()
            {
                Id = ticket.Id,
                Title = ticket.Title,
                TravelTransportationType = ticket.TravelTransportationType,
                DestinationId = ticket.DestinationId,
                Destination = new Location()
                {
                    Id = ticket.Destination.Id,
                    Latitude = ticket.Destination.Latitude,
                    Longitude = ticket.Destination.Longitude,
                    Settlement = ticket.Destination.Settlement,
                },
                Origin = new Location()
                {
                    Id = ticket.Origin.Id,
                    Latitude = ticket.Origin.Latitude,
                    Longitude = ticket.Origin.Longitude,
                    Settlement = ticket.Origin.Settlement,
                },
                Price = ticket.Price,
                DepartureTimeUtc = ticket.DepartureTimeUtc,
                ArrivalTimeUtc = ticket.ArrivalTimeUtc,
                DepartureLocalTimeZone = ticket.DepartureLocalTimeZone,
                PurchaserId = purchaser.Id
            };
        }

        private async Task AddAndSavePurchasedTicketAsync(PurchasedTicket ticket)
        {
            await this.dbContext.PurchasedTickets.AddAsync(ticket);
            await this.dbContext.SaveChangesAsync();
        }
        #endregion

        public async Task UpdateAsync(OnSaleTicketCommand entity)
        {
            var foundTicket = await this.dbContext.Tickets.FirstOrDefaultAsync(t => t.Id == entity.Id);
            if (foundTicket == null)
            {
                throw new InvalidOperationException("Cannot update unexisting ticket.");
            }

            entity.MapToExisting(foundTicket);
            await this.dbContext.SaveChangesAsync();
        }

        public async Task<OnSaleTicketResult?> GetByIdAsync(int id)
        {
            var foundTicket = await GetOnSaleTicketQuery().
                FirstOrDefaultAsync(t => t.Id == id);

            var userContext = userContextAccessor.GetUserContext();

            if (foundTicket != null && userContext != null)
            {
                SetLocalAndUserTime(foundTicket, userContext);
            }

            return foundTicket;
        }

        public async Task<ICollection<OnSaleTicketResult>> GetAllAsync()
        {
            var tickets = await GetOnSaleTicketQuery().
                ToListAsync();

            var userContext = userContextAccessor.GetUserContext();

            if (userContext != null)
                foreach (var ticket in tickets)
                {
                    SetLocalAndUserTime(ticket, userContext);
                }

            return tickets;
        }

        public async Task<PagingInfo<OnSaleTicketResult>> GetAllPagedAsync(int page, int pageSize)
        {
            var ticketsQuery = GetOnSaleTicketQuery();

            var ticketsCount = await ticketsQuery.CountAsync();

            var pagedTickets = await ticketsQuery.
                Skip((page - 1) * pageSize).
                Take(pageSize).
                ToListAsync();

            return PagingInfo<OnSaleTicketResult>.Create(pagedTickets, ticketsCount, page, pageSize);
        }
    }
}
