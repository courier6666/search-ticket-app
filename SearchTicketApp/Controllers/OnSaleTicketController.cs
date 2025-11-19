using Microsoft.AspNetCore.Mvc;
using SearchTicketApp.Interfaces;
using SearchTicketApp.Models.Query;
using SearchTicketApp.Models.ViewModels;

namespace SearchTicketApp.Controllers
{
    public class OnSaleTicketController : Controller
    {
        private readonly IOnSaleTicketService onSaleTicketService;
        private readonly IOnSaleTicketContextSearchService onSaleTicketContextSearchService;
        public OnSaleTicketController(IOnSaleTicketService onSaleTicketService, IOnSaleTicketContextSearchService onSaleTicketContextSearchService)
        {
            this.onSaleTicketService = onSaleTicketService;
            this.onSaleTicketContextSearchService = onSaleTicketContextSearchService;
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
        [HttpGet("ContextSearch/{page}")]

        public async Task<IActionResult> ContextSearch([FromQuery] OnSaleContextSearchQuery? query, [FromRoute] int page, int pageSize = 6)
        {
            var pagedTickets = await this.onSaleTicketContextSearchService.GetAllPagedBasedOnQueryAsync(query, page, pageSize);
            return View(new TicketContextSearchViewModel()
            {
                Tickets = pagedTickets,
                Query = query,
            });
        }

        public async Task<IActionResult> Purchase()
        {
            throw new NotImplementedException();
        }
    }
}
