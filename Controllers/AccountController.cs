using System.Security.Claims;
using iDEA.Entity;
using iDEA.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;

namespace iDEA.Controllers
{
    public class AccountController : Controller
    {

        private readonly DataContext _context;

        public AccountController(DataContext context) {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            if(User.Identity!.IsAuthenticated) {
                return RedirectToAction("Index", "Account");
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if(User.Identity!.IsAuthenticated) {
                return RedirectToAction();
            }

            if (!ModelState.IsValid) {
                ModelState.AddModelError("", "Invalid username or password.");
                return View(model);
            }
            
            var isUser = _context.People.FirstOrDefault(x => x.Username == model.Username && x.Password == model.Password);
            if (isUser == null)
            {
                ModelState.AddModelError("", "Invalid username or password.");
                return View(model);
            }

            var userClaims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, isUser.ID.ToString()),
                new(ClaimTypes.Name, isUser.Username ?? ""),
                new(ClaimTypes.GivenName, isUser.Name ?? "")
            };

            var claimsIdentity = new ClaimsIdentity(userClaims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties {
                IsPersistent = true
            };

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

            return RedirectToAction("Index", "Account");
        }

        public async Task<IActionResult> Logout() {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index","Home");
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}