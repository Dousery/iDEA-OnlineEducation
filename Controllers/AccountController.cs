using Microsoft.AspNetCore.Mvc;

namespace iDEA.Controllers
{
    public class AccountController : Controller
    {
        [HttpPost]
        public IActionResult Login(string Username, string Password)
        {
            Console.WriteLine(Username);
            Console.WriteLine(Password);
            return RedirectToAction("Index", "Account");
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}