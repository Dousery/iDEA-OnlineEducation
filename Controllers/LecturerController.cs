using System.Security.Claims;
using iDEA.Entity;
using iDEA.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace iDEA.Controllers
{
    public class LecturerController : Controller
    {

        private readonly DataContext _context;

        public LecturerController(DataContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            if (User.FindFirstValue(ClaimTypes.Role) != "admin")
            {
                return RedirectToAction("Index", "Account");
            }


            int AccountID = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var model = new LecturerDataModel
            {
                Name = User.FindFirstValue(ClaimTypes.GivenName),
                Surname = User.FindFirstValue(ClaimTypes.Surname),
                ID = AccountID,
                Department = _context.Lecturers
                    .Where(lecturer => lecturer.ID == AccountID)
                    .Select(lecturer => lecturer.Department)
                    .FirstOrDefault()
            };

            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);  //delete cookie info
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> StudentManager()
        {

            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            if (User.FindFirstValue(ClaimTypes.Role) != "admin")
            {
                return RedirectToAction("Index", "Account");
            }

            var query = _context.Students.Join(_context.People, s => s.ID, p => p.ID, (s, p) => new { s, p });

            StudentsDataModel model = new StudentsDataModel
            {
                Students = new List<StudentDataModel>()
            };

            foreach (var student in query)
            {
                model.Students.Add(new StudentDataModel
                {
                    Name = student.p.Name + " " + student.p.Surname,
                    ID = student.p.ID,
                    Department = student.s.Department,
                    GPA = student.s.GPA,
                    Credit = (int)_context.TakenCourses.Where(x => x.PersonID == student.s.ID)
                        .Join(_context.Courses,
                        takenCourse => takenCourse.CourseID,
                        course => course.ID,
                        (takenCourse, course) => course.Credit).DefaultIfEmpty().
                        Sum(),
                });
            }

            return View(model);
        }

        public async Task<IActionResult> ExamManager()
        {

            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            if (User.FindFirstValue(ClaimTypes.Role) != "admin")
            {
                return RedirectToAction("Index", "Account");
            }

            int AccountID = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var query = _context.TakenExams.Join(_context.Students, te => te.PersonID, s => s.ID, (te, s) => new {te, s}).Join(_context.People, te => te.te.PersonID, p => p.ID, (te, p) => new {te, p});



            IList<StudentExamDataModel> model = new List<StudentExamDataModel>();

            foreach (var student in query)
            {
                model.Add(new StudentExamDataModel
                {
                    StudentName = student.p.Name + " " + student.p.Surname,
                    StudentID = student.p.ID,
                    ExamName = _context.Exams.FirstOrDefault(x => x.ID == student.te.te.ExamID).Info,
                    Point = student.te.te.Point
                });
            }


            return View(model);
        }

    }

}
