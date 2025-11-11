using Microsoft.AspNetCore.Mvc;

namespace SearchTicketApp.Controllers
{
    public class OnSaleTicketController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
