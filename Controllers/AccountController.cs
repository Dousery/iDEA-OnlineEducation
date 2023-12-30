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

            var model = new UserDataModel
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

                IsStudent = _context.Students.Any(x => x.ID == AccountID),

                IsLecturer = _context.Lecturers.Any(x => x.ID == AccountID),

                GPA = (float)_context.TakenCourses.Where(x => x.PersonID == AccountID).DefaultIfEmpty()
                .Average(x => x.Point >= 90 ? 4.0 :
                x.Point >= 80 ? 3.0 :
                x.Point >= 70 ? 2.0 :
                x.Point >= 60 ? 1.0 : 0.0),

                Credit = (int)_context.TakenCourses.Where(x => x.PersonID == AccountID)
                .Join(_context.Courses,
                takenCourse => takenCourse.CourseID,
                course => course.ID,
                (takenCourse, course) => course.Credit).DefaultIfEmpty()
                .Sum(),

                Assignments = new List<Models.Assignment>(),
                Exams = new List<Models.Exam>(),

            };

            var assignments = _context.TakenAssignments.Where(x => x.PersonID == AccountID).Take(5);

            foreach (var row in assignments) {
                var assignment = _context.Assignments.FirstOrDefault(x => x.AssignmentID == row.AssignmentID);

                model.Assignments.Add(new Models.Assignment{
                    Course = _context.Courses.FirstOrDefault(x => x.ID == assignment.CourseID).Name,
                    ID = row.AssignmentID,
                    Name = assignment.Name,
                    Point = row.Point,
                    Deadline = assignment.Deadline
                });
            }

            var exams = _context.TakenExams.Where(x => x.PersonID == AccountID).Take(5);

            foreach (var row in exams) {
                var exam = _context.Exams.FirstOrDefault(x => x.ID == row.ExamID);

                model.Exams.Add(new Models.Exam{
                    Course = _context.Courses.FirstOrDefault(x => x.ID == exam.CourseID).Name,
                    ID = row.ExamID,
                    Info = exam.Info,
                    Point = row.Point,
                    Time = exam.Time,
                });
            }

            return View(model);
        }

        public async Task<IActionResult> Assignment() {

            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            int AccountID = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            IList<Models.Assignment> assignments = new List<Models.Assignment>();

            var query = _context.TakenAssignments.Where(x => x.PersonID == AccountID);

            foreach (var row in query) {
                var assignment = _context.Assignments.FirstOrDefault(x => x.AssignmentID == row.AssignmentID);

                assignments.Add(new Models.Assignment{
                    Course = _context.Courses.FirstOrDefault(x => x.ID == assignment.CourseID).Name,
                    ID = row.AssignmentID,
                    Name = assignment.Name,
                    Point = row.Point,
                    Deadline = assignment.Deadline
                });
            }

            return View(assignments);
        }

        public async Task<IActionResult> Exam() {

            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            int AccountID = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            IList<Models.Exam> exams = new List<Models.Exam>();

            var query = _context.TakenExams.Where(x => x.PersonID == AccountID);

            foreach (var row in query) {
                var exam = _context.Exams.FirstOrDefault(x => x.ID == row.ExamID);

                exams.Add(new Models.Exam{
                    Course = _context.Courses.FirstOrDefault(x => x.ID == exam.CourseID).Name,
                    ID = row.ExamID,
                    Info = exam.Info,
                    Point = row.Point,
                    Time = exam.Time,
                });
            }

            return View(exams);
        }
    }
}