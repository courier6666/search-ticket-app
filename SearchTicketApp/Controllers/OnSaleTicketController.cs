using Microsoft.AspNetCore.Mvc;
using SearchTicketApp.Extensions;
using SearchTicketApp.Interfaces;
using SearchTicketApp.Models.Query;
using SearchTicketApp.Models.ViewModels;

namespace SearchTicketApp.Controllers
{
    public class OnSaleTicketController : Controller
    {
        private readonly IOnSaleTicketService onSaleTicketService;
        private readonly IOnSaleTicketContextSearchService onSaleTicketContextSearchService;
        private readonly IHttpContextAccessor httpContextAccessor;
        public OnSaleTicketController(
            IOnSaleTicketService onSaleTicketService,
            IOnSaleTicketContextSearchService onSaleTicketContextSearchService,
            IHttpContextAccessor httpContextAccessor)
        {
            this.onSaleTicketService = onSaleTicketService;
            this.onSaleTicketContextSearchService = onSaleTicketContextSearchService;
            this.httpContextAccessor = httpContextAccessor;
        }

        [HttpGet("Search/{page}")]
        public async Task<IActionResult> Search([FromQuery] OnSaleSearchQuery? query, [FromRoute] int page, int pageSize = 6)
        {
            
            var pagedTickets = await this.onSaleTicketService.GetAllPagedBasedOnQueryAsync(query, page, pageSize);
            return View(new TicketSearchViewModel()
            {
                Tickets = pagedTickets,
                Query = query,
            });
        }

        [HttpGet("ContextSearch/{page:int}")]

        public async Task<IActionResult> ContextSearch([FromQuery] OnSaleContextSearchQuery? query, [FromRoute] int page, int pageSize = 6)
        {
            var pagedTickets = await this.onSaleTicketContextSearchService.GetAllPagedBasedOnQueryAsync(query, page, pageSize);
            return View(new TicketContextSearchViewModel()
            {
                Tickets = pagedTickets,
                Query = query,
            });
        }

        [HttpGet("Detail/{onSaleTicketId:int}")]
        public async Task<IActionResult> Detail([FromRoute] int onSaleTicketId, [FromQuery] string? returnUrl = null)
        {
            var ticket = await this.onSaleTicketService.GetByIdAsync(onSaleTicketId);

            if (ticket == null)
            {
                return NotFound();
            }

            this.ViewData["returnUrl"] = returnUrl ?? "/";

            await this.onSaleTicketService.ViewTicketAsync(onSaleTicketId);
            return View(ticket);
        }
    }
}
