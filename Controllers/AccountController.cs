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

            var userClaims = new List<Claim>();

            userClaims.Add(new Claim(ClaimTypes.NameIdentifier, isUser.ID.ToString()));
            userClaims.Add(new Claim(ClaimTypes.Name, isUser.Username ?? ""));
            userClaims.Add(new Claim(ClaimTypes.GivenName, isUser.Name ?? ""));

            if(isUser.ID == 0) {
                userClaims.Add(new Claim(ClaimTypes.Role, "root"));
            }

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