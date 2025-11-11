using Microsoft.AspNetCore.Mvc;

namespace SearchTicketApp.Controllers
{
    public class PurchasedTicketController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
