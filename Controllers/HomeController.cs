using Microsoft.AspNetCore.Mvc;

namespace iDEA.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}