using Microsoft.AspNetCore.Mvc;

namespace SearchTicketApp.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
