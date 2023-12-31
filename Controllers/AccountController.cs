using System.Numerics;
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

        public AccountController(DataContext context)
        {
            _context = context;
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (User.Identity!.IsAuthenticated)
            {
                return RedirectToAction();
            }

            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Invalid username or password.");
                ViewBag.Invalid = true;
                return View("~/Views/Home/Index.cshtml", model);
            }

            var isUser = _context.People.FirstOrDefault(x => x.Username == model.Username && x.Password == model.Password);
            if (isUser == null)
            {
                ModelState.AddModelError("", "Invalid username or password.");
                ViewBag.Invalid = true;
                return View("~/Views/Home/Index.cshtml", model);
            }

            var userClaims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, isUser.ID.ToString()),
                new(ClaimTypes.Name, isUser.Username ?? ""),
                new(ClaimTypes.GivenName, isUser.Name + " " + isUser.Surname ?? "")
            };

            if (_context.Lecturers.FirstOrDefault(x => x.ID == isUser.ID) is null)
            {
                userClaims.Add(new Claim(ClaimTypes.Role, "admin"));
            }

            var claimsIdentity = new ClaimsIdentity(userClaims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true
            };

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);
            if (_context.Lecturers.FirstOrDefault(x => x.ID == isUser.ID) is not null)
            {
                return RedirectToAction("Index", "Lecturer");
            }
            return RedirectToAction("Index", "Account");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);  //delete cookie info
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            int AccountID = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var model = new StudentDataModel
            {
                Name = User.FindFirstValue(ClaimTypes.GivenName),

                ID = AccountID,

                Department = _context.Students
                .Where(student => student.ID == AccountID)
                .Select(student => new { ID = student.ID, Department = student.Department })
                .Union(
                    _context.Lecturers
                        .Where(lecturer => lecturer.ID == AccountID)
                        .Select(lecturer => new { ID = lecturer.ID, Department = lecturer.Department })
                )
                .Select(combined => combined.Department)
                .FirstOrDefault(),

                GPA = (float)_context.TakenCourses.Where(x => x.PersonID == AccountID)
            .Average(x => x.Point >= 90 ? 4.0 :
            x.Point >= 80 ? 3.0 :
            x.Point >= 70 ? 2.0 :
            x.Point >= 60 ? 1.0 : 0.0),

                Credit = (int)_context.TakenCourses.Where(x => x.PersonID == AccountID)
            .Join(_context.Courses,
            takenCourse => takenCourse.CourseID,
            course => course.ID,
            (takenCourse, course) => course.Credit)
            .Sum()
            };

            return View(model);

        }
    }
}