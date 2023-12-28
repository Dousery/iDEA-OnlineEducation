using System.Security.Claims;
using iDEA.Entity;
using iDEA.Models;
using Microsoft.AspNetCore.Mvc;

namespace iDEA.Controllers
{

    public class CourseController : Controller
    {

        private readonly DataContext _context;

        public CourseController(DataContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            int AccountID = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var model = new CoursesModel {
                Courses = new List<Models.Course>()
            };

            var query = _context.TakenCourses
            .Where(x => x.PersonID == AccountID)
            .Join(
                _context.Courses, x => x.CourseID,
                x => x.ID,
                (takenCourse, course) => new {
                    CourseID = course.ID,
                    CourseName = course.Name,
                    Credit = course.Credit,
                    Point = takenCourse.Point,
                    Grade = takenCourse.Grade
                }
            );

            
            foreach(var qcourse in query) {
                Models.Course course = new()
                {
                    CourseID = qcourse.CourseID,
                    CourseName = qcourse.CourseName,
                    Credit = (int)qcourse.Credit,
                    Score = qcourse.Point,
                    Grade = qcourse.Grade
                };
                model.Courses.Add(course);
            }
            

            return View(model);
        }

        public async Task<IActionResult> Info(int id) {

            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            int AccountID = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            if (!_context.TakenCourses.Any(x => x.PersonID == AccountID && x.CourseID == id))
            {
                return RedirectToAction("Index", "Home");
            }



            return View();
        }

        public async Task<IActionResult> Assignment() {

            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            int AccountID = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            return View();
        }

    }

}