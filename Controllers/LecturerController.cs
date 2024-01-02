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
                    .FirstOrDefault(),
                Courses = new List<Models.Course>(),
            };

            var query = _context.LectureCourses.Where(x => x.PersonID == AccountID);

            foreach (var course in query)
            {
                var qcourse = _context.Courses.FirstOrDefault(x => x.ID == course.CourseID);

                model.Courses.Add(new Models.Course
                {
                    CourseName = qcourse.Name,
                    CourseID = course.CourseID,
                    Credit = (int)qcourse.Credit
                });
            }

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

        public async Task<IActionResult> ExamManager(int id)
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

            IList<StudentExamDataModel> model = new List<StudentExamDataModel>();

            var query = _context.TakenExams.Join(_context.Students, te => te.PersonID, s => s.ID, (te, s) => new { te, s }).Join(_context.People, te => te.te.PersonID, p => p.ID, (te, p) => new { te, p });

            if (id != 0) {
                if (!_context.LectureCourses.Any(x => x.CourseID == _context.Exams.FirstOrDefault(x => x.ID == id).CourseID && x.PersonID == AccountID)) {
                    return RedirectToAction("Index", "Lecturer");
                }
                query = query.Where(x => x.te.te.ExamID == id);
            }

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
        [HttpGet]
        public IActionResult AddStudent()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            if (User.FindFirstValue(ClaimTypes.Role) != "admin")
            {
                return RedirectToAction("Index", "Account");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddStudent(AddStudentModel model)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            if (User.FindFirstValue(ClaimTypes.Role) != "admin")
            {
                return RedirectToAction("Index", "Account");
            }

            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index", "Lecturer");
            }


            int id = _context.People.Count() + 1;

            if (model.Password != model.ConfirmPassword)
            {
                return View(model);
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            _context.People.Add(new Person
            {
                ID = id,
                Name = model.Name,
                Surname = model.Surname,
                Username = model.Email,
                Password = model.Password
            });

            int AccountID = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            _context.Students.Add(new Student
            {
                ID = id,
                GPA = 0,
                Department = _context.Lecturers.FirstOrDefault(x => x.ID == AccountID).Department
            });

            _context.SaveChanges();

            return RedirectToAction("StudentManager", "Lecturer");
        }

        public async Task<IActionResult> CourseManager(int id)
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

            if (!_context.LectureCourses.Any(x => x.PersonID == AccountID && x.CourseID == id))
            {
                return RedirectToAction("Index", "Home");
            }

            var course = _context.Courses.FirstOrDefault(x => x.ID == id);

            LectureCourseDataModel model = new LectureCourseDataModel
            {
                CourseID = id,
                CourseName = course.Name,
                Credit = course.Credit,
                Students = new List<StudentDataModel>(),
                Exams = new List<Models.Exam>(),
                Assignments = new List<Models.Assignment>(),
            };

            var takenAssignment = _context.Assignments.Where(x => x.CourseID == id);

            var takenExam = _context.Exams.Where(x => x.CourseID == id);

            var Students = _context.TakenCourses
            .Where(x => x.CourseID == id)
            .Join(_context.Students, tc => tc.PersonID, s => s.ID, (tc, s) => new { tc, s })
            .Join(_context.People, s => s.s.ID, p => p.ID, (s, p) => new { s, p });

            foreach (var assignment in takenAssignment)
            {
                model.Assignments.Add(new Models.Assignment
                {
                    ID = assignment.AssignmentID,
                    Name = assignment.Name,
                    Deadline = assignment.Deadline
                });
            }
            foreach (var exam in takenExam)
            {
                model.Exams.Add(new Models.Exam
                {
                    ID = exam.ID,
                    Time = exam.Time,
                    Info = exam.Info,
                });
            }

            foreach (var student in Students)
            {
                model.Students.Add(new StudentDataModel
                {
                    ID = student.p.ID,
                    Name = student.p.Name + " " + student.p.Surname,
                    Department = student.s.s.Department,
                    GPA = student.s.s.GPA,
                });
            }

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> CourseManager(int id, LectureCourseDataModel model)
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

            if (!_context.LectureCourses.Any(x => x.PersonID == AccountID && x.CourseID == id))
            {
                return RedirectToAction("Index", "Home");
            }

            if (!_context.Students.Any(x => x.ID == model.InputStudentID) || _context.TakenCourses.Any(x => x.PersonID == model.InputStudentID && x.CourseID == id))
            {
                return RedirectToAction("CourseManager", "Lecturer");
            }

            _context.TakenCourses.Add(new TakenCourse
            {
                CourseID = id,
                PersonID = model.InputStudentID,
                Point = 0
            });

            _context.SaveChanges();

            return RedirectToAction("CourseManager", "Lecturer");
        }

    }

}
