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

        public IActionResult StudentManager()
        {
            /*
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            var model2 = new StudentsDataModel {
                Students = new List<Models.StudentDataModel>()
            };


            int lecturerID = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var lecturerCourses = _context.LectureCourses
       .Where(lc => lc.PersonID == lecturerID)
       .Select(lc => lc.CourseID)
       .ToList();

            // Bu kurslara kayıtlı öğrencileri bulalım
            var students = _context.TakenCourses
                .Where(tc => lecturerCourses.Contains(tc.CourseID))
                .Select(tc => tc.PersonID)
                .ToList();

            foreach(var studentID in students) {
                var model2 = new StudentDataModel
                {
                    Name = _context.People
            .Where(student => students.Contains(student.ID))
            .Select(student => student.Name)
            .FirstOrDefault(),

                Department = _context.Students
            .Where(student => students.Contains(student.ID))
            .Select(student => student.Department)
            .FirstOrDefault(),

                GPA = (float)_context.TakenCourses
            .Where(x => students.Contains(x.PersonID))
            .Average(x => x.Point >= 90 ? 4.0 :
                          x.Point >= 80 ? 3.0 :
                          x.Point >= 70 ? 2.0 :
                          x.Point >= 60 ? 1.0 : 0.0),

                Credit = (int)_context.TakenCourses
            .Where(x => students.Contains(x.PersonID))
            .Join(_context.Courses,
                  takenCourse => takenCourse.CourseID,
                  course => course.ID,
                  (takenCourse, course) => course.Credit)
            .Sum()
                };
                
                model2.Students.Add(model2);
            }
*/
            return View();
        }
         


    }

}
