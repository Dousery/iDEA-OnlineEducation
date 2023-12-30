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

            var query = _context.Courses.FirstOrDefault(x => x.ID == id);

            var takenCourse = _context.TakenCourses.FirstOrDefault(x => x.PersonID == AccountID && x.CourseID == id);

            var takenAssignment = _context.TakenAssignments
            .Where(x => x.PersonID == AccountID)
            .Join(_context.Assignments, ta => ta.AssignmentID, a => a.AssignmentID, (a ,ta) => new {a, ta})
            .Where(x => x.ta.CourseID == id);

            var takenExam = _context.TakenExams
            .Where(x => x.PersonID == AccountID)
            .Join(_context.Exams, te => te.ExamID, e => e.ID, (e ,te) => new {e, te})
            .Where(x => x.te.CourseID == id);

            CourseInfoModel model = new CourseInfoModel(){
                CourseID = id,
                CourseName = query.Name,
                Credit = query.Credit,
                CourseInfo = "Placeholder text. TODO: Add a column to Courses for Info.",
                Assignments = new List<Models.Assignment>(),
                Exams = new List<Models.Exam>(),
                Sessions = new List<Models.Session>(),
                Score = takenCourse.Point,
                Grade = takenCourse.Grade
            };

            foreach(var assignment in takenAssignment) {
                model.Assignments.Add(new Models.Assignment{
                    ID = assignment.a.AssignmentID,
                    Name = assignment.ta.Name,
                    Point = assignment.a.Point,
                    Deadline = assignment.ta.Deadline
                });
            }
            foreach(var exam in takenExam) {
                model.Exams.Add(new Models.Exam{
                    ID = exam.e.ExamID,
                    Time = exam.te.Time,
                    Info = exam.te.Info,
                    Point = exam.e.Point 
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

            return View();
        }

    }

}