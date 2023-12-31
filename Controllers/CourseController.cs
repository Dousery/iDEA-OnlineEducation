using System.Security.Claims;
using iDEA.Entity;
using iDEA.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

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

            if (User.FindFirstValue(ClaimTypes.Role) == "admin") {
                return RedirectToAction("Index", "Lecturer");
            }

            int AccountID = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var model = new CoursesModel
            {
                Courses = new List<Models.Course>()
            };

            var query = _context.TakenCourses
            .Where(x => x.PersonID == AccountID)
            .Join(
                _context.Courses, x => x.CourseID,
                x => x.ID,
                (takenCourse, course) => new
                {
                    CourseID = course.ID,
                    CourseName = course.Name,
                    Credit = course.Credit,
                    Point = takenCourse.Point,
                    Grade = takenCourse.Grade
                }
            );


            foreach (var qcourse in query)
            {
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

        public async Task<IActionResult> Info(int id)
        {

            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            if (User.FindFirstValue(ClaimTypes.Role) == "admin") {
                return RedirectToAction("Index", "Lecturer");
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
            .Join(_context.Assignments, ta => ta.AssignmentID, a => a.AssignmentID, (a, ta) => new { a, ta })
            .Where(x => x.ta.CourseID == id);

            var takenExam = _context.TakenExams
            .Where(x => x.PersonID == AccountID)
            .Join(_context.Exams, te => te.ExamID, e => e.ID, (e, te) => new { e, te })
            .Where(x => x.te.CourseID == id);

            var sessions = _context.Sessions.Where(x => x.CourseID == id);

            CourseInfoModel model = new CourseInfoModel()
            {
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

            foreach (var assignment in takenAssignment)
            {
                model.Assignments.Add(new Models.Assignment
                {
                    ID = assignment.a.AssignmentID,
                    Name = assignment.ta.Name,
                    Point = assignment.a.Point,
                    Deadline = assignment.ta.Deadline
                });
            }
            foreach (var exam in takenExam)
            {
                model.Exams.Add(new Models.Exam
                {
                    ID = exam.e.ExamID,
                    Time = exam.te.Time,
                    Info = exam.te.Info,
                    Point = exam.e.Point
                });
            }
            foreach (var session in sessions)
            {
                model.Sessions.Add(new Models.Session
                {
                    SessionID = session.ID,
                });
            }

            return View(model);
        }

        public async Task<IActionResult> Assignment(int id)
        {

            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            if (User.FindFirstValue(ClaimTypes.Role) == "admin") {
                return RedirectToAction("Index", "Lecturer");
            }

            int AccountID = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            if (!_context.TakenAssignments.Any(x => x.PersonID == AccountID && x.AssignmentID == id))
            {
                return RedirectToAction("Index", "Home");
            }

            if (_context.TakenAssignments.FirstOrDefault(x => x.PersonID == AccountID && x.AssignmentID == id).Point != -1)
            {
                return RedirectToAction("Index", "Home");
            }

            var query = _context.Questions.Where(x => x.AssignmentID == id);

            QuestionAnswerModel model = new QuestionAnswerModel()
            {
                Questions = new List<Models.Question>(),
                Answers = new List<string>(),
                AssignmentID = id
            };

            foreach (var question in query)
            {
                model.Questions.Add(new Models.Question
                {
                    Text = question.Text,
                    Options = question.Options,
                });
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Answer(int id, QuestionAnswerModel model)
        {

            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            if (User.FindFirstValue(ClaimTypes.Role) == "admin") {
                return RedirectToAction("Index", "Lecturer");
            }

            int AccountID = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            if (!_context.TakenAssignments.Any(x => x.PersonID == AccountID && x.AssignmentID == id))
            {
                return RedirectToAction("Index", "Home");
            }

            var assignment = _context.TakenAssignments.FirstOrDefault(x => x.PersonID == AccountID && x.AssignmentID == id);

            if (assignment.Point != -1)
            {
                return RedirectToAction("Index", "Home");
            }

            var questions = _context.Questions.Where(x => x.AssignmentID == id).ToList();


            int correct = 0;
            for (int i = 0; i < questions.Count; i++)
            {
                if (questions[i].CorrectAnswer == model.Answers[i])
                {
                    correct++;
                }
            }

            assignment.Point = (int)(((float)correct / questions.Count) * 100);

            _context.SaveChanges();

            var courseID = _context.Assignments.FirstOrDefault(x => x.AssignmentID == assignment.AssignmentID).CourseID;

            var course = _context.TakenCourses.FirstOrDefault(x => x.CourseID == courseID && x.PersonID == AccountID);

            var averagePoint = _context.TakenExams
                    .Where(exam => exam.PersonID == course.PersonID)
                    .Join(_context.Exams, exam => exam.ExamID, e => e.ID, (exam, e) => new { exam, e })
                    .Where(joined => joined.e.CourseID == course.CourseID)
                    .Select(joined => joined.exam.Point)
                    .Union(_context.TakenAssignments
                        .Join(_context.Assignments, ta => ta.AssignmentID, a => a.AssignmentID, (ta, a) => new { ta, a })
                        .Where(x => x.a.CourseID == course.CourseID && x.ta.PersonID == course.PersonID && x.ta.Point != -1)
                        .Select(x => x.ta.Point))
                    .Average();

            course.Point = averagePoint;

            _context.SaveChanges();

            var GPA = (float)_context.TakenCourses.Where(x => x.PersonID == AccountID).DefaultIfEmpty()
                .Average(x => x.Point >= 90 ? 4.0 :
                x.Point >= 80 ? 3.0 :
                x.Point >= 70 ? 2.0 :
                x.Point >= 60 ? 1.0 : 0.0);

            _context.Students.FirstOrDefault(x => x.ID == AccountID).GPA = GPA;

            _context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }


        public async Task<IActionResult> Session(int id)
        {

            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            if (User.FindFirstValue(ClaimTypes.Role) == "admin") {
                return RedirectToAction("Index", "Lecturer");
            }

            int AccountID = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            if (!_context.TakenCourses.Where(x => x.PersonID == AccountID)
            .Join(_context.Sessions, tc => tc.CourseID, s => s.CourseID, (tc, s) => new {tc, s})
            .Any(x => x.s.CourseID == id)) {
                return RedirectToAction("Index", "Account");
            }

            if (!_context.Attendances.Any(x => x.StudentID == AccountID && x.SessionID == id)) {
                _context.Attendances.Add(new Attendance{
                    StudentID = AccountID,
                    SessionID = id 
                });
                _context.SaveChanges();
            }

            return View();
        }
    }

}