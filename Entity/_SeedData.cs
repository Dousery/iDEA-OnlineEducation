using Microsoft.EntityFrameworkCore;

namespace iDEA.Entity {
    public static class SeedData {
        public static void InitTestValues(IApplicationBuilder app) {
            var context = app.ApplicationServices.CreateScope().ServiceProvider.GetService<DataContext>();

            if (context == null) return;

            if (context.Database.GetPendingMigrations().Any()) {
                context.Database.Migrate();
            }

            if (!context.People.Any()) {
                int count = 0;
                context.People.Add(new Person{
                    ID = ++count,
                    Name = "Alice",
                    Surname = "Johnson",
                    Username = "alice.johnson@example.com",
                    Password = "Password123"
                });
                context.People.Add(new Person{
                    ID = ++count,
                    Name = "Bob",
                    Surname = "Smith",
                    Username = "bob.smith@example.com",
                    Password = "SecurePass456"
                });
                context.People.Add(new Person{
                    ID = ++count,
                    Name = "Clara",
                    Surname = "Rodriguez",
                    Username = "clara.rodriguez@example.com",
                    Password = "StrongPwd789"
                });
                context.People.Add(new Person{
                    ID = ++count,
                    Name = "David",
                    Surname = "Taylor",
                    Username = "david.taylor@example.com",
                    Password = "Secret123"
                });
                context.People.Add(new Person{
                    ID = ++count,
                    Name = "Emily",
                    Surname = "Brown",
                    Username = "emily.brown@example.com",
                    Password = "ProtectedPwd"
                });
                context.People.Add(new Person{
                    ID = ++count,
                    Name = "Frank",
                    Surname = "Davis",
                    Username = "frank.davis@example.com",
                    Password = "Access456"
                });
                context.People.Add(new Person{
                    ID = ++count,
                    Name = "Grace",
                    Surname = "Miller",
                    Username = "grace.miller@example.com",
                    Password = "PrivatePwd789"
                });
                context.People.Add(new Person{
                    ID = ++count,
                    Name = "Henry",
                    Surname = "Anderson",
                    Username = "henry.anderson@example.com",
                    Password = "Classified123"
                });
                context.People.Add(new Person{
                    ID = ++count,
                    Name = "Irene",
                    Surname = "White",
                    Username = "irene.white@example.com",
                    Password = "ConfidentialPwd"
                });
                context.People.Add(new Person{
                    ID = ++count,
                    Name = "Jack",
                    Surname = "Martinez",
                    Username = "jack.martinez@example.com",
                    Password = "SecureAccess789"
                });
            }

            if (!context.Students.Any()) {
                int count = 0;
                context.Students.Add(new Student{
                    ID = ++count,
                    Department = "Computer Engineering"
                });
                context.Students.Add(new Student{
                    ID = ++count,
                    Department = "Computer Engineering"
                });
                context.Students.Add(new Student{
                    ID = ++count,
                    Department = "Mechanical Engineering"
                });
                context.Students.Add(new Student{
                    ID = ++count,
                    Department = "Mechanical Engineering"
                });
                context.Students.Add(new Student{
                    ID = ++count,
                    Department = "Electrical and Electronics Engineering"
                });
                context.Students.Add(new Student{
                    ID = ++count,
                    Department = "Electrical and Electronics Engineering"
                });
            }

            if (!context.Lecturers.Any()) {
                context.Lecturers.Add(new Lecturer{
                    ID = 7,
                    Department = "Mathematics"
                });
                context.Lecturers.Add(new Lecturer{
                    ID = 8,
                    Department = "Mechanical Engineering"
                });
                context.Lecturers.Add(new Lecturer{
                    ID = 9,
                    Department = "Electrical and Electronics Engineering"
                });
                context.Lecturers.Add(new Lecturer{
                    ID = 10,
                    Department = "Computer Engineering"
                });
            }

            if (!context.Courses.Any()) {
                int count = 0;
                context.Courses.Add(new Course{
                    ID = ++count,
                    Name = "Data Structures",
                    Credit = 4,
                    ResourcePath = "~/CourseResources/1"
                });
                context.Courses.Add(new Course{
                    ID = ++count,
                    Name = "Algorithms",
                    Credit = 4,
                    ResourcePath = "~/CourseResources/2"
                });
                context.Courses.Add(new Course{
                    ID = ++count,
                    Name = "Semiconductors",
                    Credit = 4,
                    ResourcePath = "~/CourseResources/1"
                });
                context.Courses.Add(new Course{
                    ID = ++count,
                    Name = "Circuits",
                    Credit = 4,
                    ResourcePath = "~/CourseResources/2"
                });
                context.Courses.Add(new Course{
                    ID = ++count,
                    Name = "Thermodynamics",
                    Credit = 4,
                    ResourcePath = "~/CourseResources/1"
                });
                context.Courses.Add(new Course{
                    ID = ++count,
                    Name = "Fluid Mechanics",
                    Credit = 4,
                    ResourcePath = "~/CourseResources/2"
                });
                context.Courses.Add(new Course{
                    ID = ++count,
                    Name = "Calculus",
                    Credit = 4,
                    ResourcePath = "~/CourseResources/1"
                });
                context.Courses.Add(new Course{
                    ID = ++count,
                    Name = "Differential Equations",
                    Credit = 4,
                    ResourcePath = "~/CourseResources/2"
                });
            }

            if (!context.TakenCourses.Any()) {
                context.TakenCourses.Add(new TakenCourse{
                    PersonID = 1,
                    CourseID = 1,
                });
                context.TakenCourses.Add(new TakenCourse{
                    PersonID = 1,
                    CourseID = 8,
                });
                context.TakenCourses.Add(new TakenCourse{
                    PersonID = 2,
                    CourseID = 2,
                });
                context.TakenCourses.Add(new TakenCourse{
                    PersonID = 2,
                    CourseID = 7,
                });
                context.TakenCourses.Add(new TakenCourse{
                    PersonID = 3,
                    CourseID = 5,
                });
                context.TakenCourses.Add(new TakenCourse{
                    PersonID = 3,
                    CourseID = 7,
                });
                context.TakenCourses.Add(new TakenCourse{
                    PersonID = 4,
                    CourseID = 6,
                });
                context.TakenCourses.Add(new TakenCourse{
                    PersonID = 4,
                    CourseID = 8,
                });
                context.TakenCourses.Add(new TakenCourse{
                    PersonID = 5,
                    CourseID = 4,
                });
                context.TakenCourses.Add(new TakenCourse{
                    PersonID = 5,
                    CourseID = 7,
                });
                context.TakenCourses.Add(new TakenCourse{
                    PersonID = 6,
                    CourseID = 3,
                });
                context.TakenCourses.Add(new TakenCourse{
                    PersonID = 6,
                    CourseID = 8,
                });
            }

            if (!context.LectureCourses.Any()) {
                context.LectureCourses.Add(new LectureCourse{
                    PersonID = 7,
                    CourseID = 7
                });
                context.LectureCourses.Add(new LectureCourse{
                    PersonID = 7,
                    CourseID = 8
                });
                context.LectureCourses.Add(new LectureCourse{
                    PersonID = 8,
                    CourseID = 5
                });
                context.LectureCourses.Add(new LectureCourse{
                    PersonID = 8,
                    CourseID = 6
                });
                context.LectureCourses.Add(new LectureCourse{
                    PersonID = 9,
                    CourseID = 3
                });
                context.LectureCourses.Add(new LectureCourse{
                    PersonID = 9,
                    CourseID = 4
                });
                context.LectureCourses.Add(new LectureCourse{
                    PersonID = 10,
                    CourseID = 1
                });
                context.LectureCourses.Add(new LectureCourse{
                    PersonID = 10,
                    CourseID = 2
                });
            }

            context.SaveChanges();
        }
    }
}
