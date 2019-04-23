using Microsoft.AspNetCore.Mvc;

namespace Lab_4.Controllers
{
    public class SessionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}