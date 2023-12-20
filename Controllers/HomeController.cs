using Microsoft.AspNetCore.Mvc;

namespace iDEA.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            if(User.Identity!.IsAuthenticated) {
                return RedirectToAction("Index", "Account");
            }

            return View();
        }
    }
}