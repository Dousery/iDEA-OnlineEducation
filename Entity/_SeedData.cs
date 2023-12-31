using Microsoft.EntityFrameworkCore;

namespace iDEA.Entity
{
    public static class SeedData
    {
        public static void InitTestValues(IApplicationBuilder app)
        {
            var context = app.ApplicationServices.CreateScope().ServiceProvider.GetService<DataContext>();

            if (context == null) return;

            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }

            if (!context.People.Any())
            {
                int count = 0;
                context.People.Add(new Person
                {
                    ID = ++count,
                    Name = "Alice",
                    Surname = "Johnson",
                    Username = "alice.johnson@example.com",
                    Password = "Password123"
                });
                context.People.Add(new Person
                {
                    ID = ++count,
                    Name = "Bob",
                    Surname = "Smith",
                    Username = "bob.smith@example.com",
                    Password = "SecurePass456"
                });
                context.People.Add(new Person
                {
                    ID = ++count,
                    Name = "Clara",
                    Surname = "Rodriguez",
                    Username = "clara.rodriguez@example.com",
                    Password = "StrongPwd789"
                });
                context.People.Add(new Person
                {
                    ID = ++count,
                    Name = "David",
                    Surname = "Taylor",
                    Username = "david.taylor@example.com",
                    Password = "Secret123"
                });
                context.People.Add(new Person
                {
                    ID = ++count,
                    Name = "Emily",
                    Surname = "Brown",
                    Username = "emily.brown@example.com",
                    Password = "ProtectedPwd"
                });
                context.People.Add(new Person
                {
                    ID = ++count,
                    Name = "Frank",
                    Surname = "Davis",
                    Username = "frank.davis@example.com",
                    Password = "Access456"
                });
                context.People.Add(new Person
                {
                    ID = ++count,
                    Name = "Grace",
                    Surname = "Miller",
                    Username = "grace.miller@example.com",
                    Password = "PrivatePwd789"
                });
                context.People.Add(new Person
                {
                    ID = ++count,
                    Name = "Henry",
                    Surname = "Anderson",
                    Username = "henry.anderson@example.com",
                    Password = "Classified123"
                });
                context.People.Add(new Person
                {
                    ID = ++count,
                    Name = "Irene",
                    Surname = "White",
                    Username = "irene.white@example.com",
                    Password = "ConfidentialPwd"
                });
                context.People.Add(new Person
                {
                    ID = ++count,
                    Name = "Jack",
                    Surname = "Martinez",
                    Username = "jack.martinez@example.com",
                    Password = "SecureAccess789"
                });
            }

            if (!context.Students.Any())
            {
                int count = 0;
                context.Students.Add(new Student
                {
                    ID = ++count,
                    Department = "Computer Engineering"
                });
                context.Students.Add(new Student
                {
                    ID = ++count,
                    Department = "Computer Engineering"
                });
                context.Students.Add(new Student
                {
                    ID = ++count,
                    Department = "Mechanical Engineering"
                });
                context.Students.Add(new Student
                {
                    ID = ++count,
                    Department = "Mechanical Engineering"
                });
                context.Students.Add(new Student
                {
                    ID = ++count,
                    Department = "Electrical and Electronics Engineering"
                });
                context.Students.Add(new Student
                {
                    ID = ++count,
                    Department = "Electrical and Electronics Engineering"
                });
            }

            if (!context.Lecturers.Any())
            {
                context.Lecturers.Add(new Lecturer
                {
                    ID = 7,
                    Department = "Mathematics"
                });
                context.Lecturers.Add(new Lecturer
                {
                    ID = 8,
                    Department = "Mechanical Engineering"
                });
                context.Lecturers.Add(new Lecturer
                {
                    ID = 9,
                    Department = "Electrical and Electronics Engineering"
                });
                context.Lecturers.Add(new Lecturer
                {
                    ID = 10,
                    Department = "Computer Engineering"
                });
            }

            if (!context.Courses.Any())
            {
                int count = 0;
                context.Courses.Add(new Course
                {
                    ID = ++count,
                    Name = "Data Structures",
                    Credit = 4,
                    ResourcePath = "~/CourseResources/1",
                });
                context.Courses.Add(new Course
                {
                    ID = ++count,
                    Name = "Algorithms",
                    Credit = 4,
                    ResourcePath = "~/CourseResources/2"
                });
                context.Courses.Add(new Course
                {
                    ID = ++count,
                    Name = "Semiconductors",
                    Credit = 4,
                    ResourcePath = "~/CourseResources/1"
                });
                context.Courses.Add(new Course
                {
                    ID = ++count,
                    Name = "Circuits",
                    Credit = 4,
                    ResourcePath = "~/CourseResources/2"
                });
                context.Courses.Add(new Course
                {
                    ID = ++count,
                    Name = "Thermodynamics",
                    Credit = 4,
                    ResourcePath = "~/CourseResources/1"
                });
                context.Courses.Add(new Course
                {
                    ID = ++count,
                    Name = "Fluid Mechanics",
                    Credit = 4,
                    ResourcePath = "~/CourseResources/2"
                });
                context.Courses.Add(new Course
                {
                    ID = ++count,
                    Name = "Calculus",
                    Credit = 4,
                    ResourcePath = "~/CourseResources/1"
                });
                context.Courses.Add(new Course
                {
                    ID = ++count,
                    Name = "Differential Equations",
                    Credit = 4,
                    ResourcePath = "~/CourseResources/2"
                });
            }

            if (!context.TakenCourses.Any())
            {
                context.TakenCourses.Add(new TakenCourse
                {
                    PersonID = 1,
                    CourseID = 1,
                });
                context.TakenCourses.Add(new TakenCourse
                {
                    PersonID = 1,
                    CourseID = 8,
                });
                context.TakenCourses.Add(new TakenCourse
                {
                    PersonID = 2,
                    CourseID = 2,
                });
                context.TakenCourses.Add(new TakenCourse
                {
                    PersonID = 2,
                    CourseID = 7,
                });
                context.TakenCourses.Add(new TakenCourse
                {
                    PersonID = 3,
                    CourseID = 5,
                });
                context.TakenCourses.Add(new TakenCourse
                {
                    PersonID = 3,
                    CourseID = 7,
                });
                context.TakenCourses.Add(new TakenCourse
                {
                    PersonID = 4,
                    CourseID = 6,
                });
                context.TakenCourses.Add(new TakenCourse
                {
                    PersonID = 4,
                    CourseID = 8,
                });
                context.TakenCourses.Add(new TakenCourse
                {
                    PersonID = 5,
                    CourseID = 4,
                });
                context.TakenCourses.Add(new TakenCourse
                {
                    PersonID = 5,
                    CourseID = 7,
                });
                context.TakenCourses.Add(new TakenCourse
                {
                    PersonID = 6,
                    CourseID = 3,
                });
                context.TakenCourses.Add(new TakenCourse
                {
                    PersonID = 6,
                    CourseID = 8,
                });
            }

            if (!context.LectureCourses.Any())
            {
                context.LectureCourses.Add(new LectureCourse
                {
                    PersonID = 7,
                    CourseID = 7
                });
                context.LectureCourses.Add(new LectureCourse
                {
                    PersonID = 7,
                    CourseID = 8
                });
                context.LectureCourses.Add(new LectureCourse
                {
                    PersonID = 8,
                    CourseID = 5
                });
                context.LectureCourses.Add(new LectureCourse
                {
                    PersonID = 8,
                    CourseID = 6
                });
                context.LectureCourses.Add(new LectureCourse
                {
                    PersonID = 9,
                    CourseID = 3
                });
                context.LectureCourses.Add(new LectureCourse
                {
                    PersonID = 9,
                    CourseID = 4
                });
                context.LectureCourses.Add(new LectureCourse
                {
                    PersonID = 10,
                    CourseID = 1
                });
                context.LectureCourses.Add(new LectureCourse
                {
                    PersonID = 10,
                    CourseID = 2
                });
            }

            if (!context.Assignments.Any())
            {
                int count = 0;

                context.Assignments.Add(new Assignment
                {
                    AssignmentID = ++count,
                    CourseID = 1,                           //data structure
                    Deadline = DateTime.Now.AddDays(7),
                    Name = "Hash Tables"
                });

                context.Assignments.Add(new Assignment
                {
                    AssignmentID = ++count,
                    CourseID = 2,                           //algorithm
                    Deadline = DateTime.Now.AddDays(10),
                    Name = "Big O Notation"
                });

                context.Assignments.Add(new Assignment
                {
                    AssignmentID = ++count,
                    CourseID = 4,                           //circuits assignmentID=3
                    Deadline = DateTime.Now.AddDays(10),
                    Name = "Kirchoff's Principles"
                });

                context.Assignments.Add(new Assignment
                {
                    AssignmentID = ++count,
                    CourseID = 5,                           //thermodynamıc  assignmentID=4
                    Deadline = DateTime.Now.AddDays(5),
                    Name = "Combustion"
                });

            }

            if (!context.Questions.Any())
            {
                int count = 0;
                context.Questions.Add(new Question
                {
                    QuestionID = ++count,
                    AssignmentID = 1,
                    Text = "Elemanları key-value çiftleri halinde depolanan bir veri yapısı hangisidir?",
                    Options = "a) Heap b) Queue c) Set d) Hash Table",
                    CorrectAnswer = "d"
                });

                context.Questions.Add(new Question
                {
                    QuestionID = ++count,
                    AssignmentID = 1,
                    Text = "Her elemanının kendisinden önceki ve sonraki elemanı gösteren bir bağlantıya sahip olan veri yapısı hangisidir?",
                    Options = "a) Stack b) Array c) Linked List d) Hash Table",
                    CorrectAnswer = "c"
                });

                context.Questions.Add(new Question
                {
                    QuestionID = ++count,
                    AssignmentID = 1,
                    Text = "Bir düğümün en fazla iki çocuğa sahip olabildiği, her düğümün sol alt ağaçtaki düğümlerinden daha küçük ve sağ alt ağaçtaki düğümlerden daha büyük olduğu bir veri yapısı hangisidir?",
                    Options = "a) Heap b) Graph c) Binary Search Tree d) Array",
                    CorrectAnswer = "c"
                });

                context.Questions.Add(new Question
                {
                    QuestionID = ++count,
                    AssignmentID = 1,
                    Text = "Bir elemanın birden fazla yöne bağlı olabileceği ve döngüler içerebilecek şekilde ilişkilendirildiği bir veri yapısı hangisidir?",
                    Options = "a) Graph b) Heap c) Tree d) Hash Table",
                    CorrectAnswer = "a"
                });

                context.Questions.Add(new Question
                {
                    QuestionID = ++count,
                    AssignmentID = 2,
                    Text = "Bir graf üzerinde en kısa yol hesaplamak için hangi algoritma kullanılır?",
                    Options = "a) Dijkstra's Algorithm b) Merge Sort c) Bubble Sort d) Insertion Sort",
                    CorrectAnswer = "a"
                });

                context.Questions.Add(new Question
                {
                    QuestionID = ++count,
                    AssignmentID = 2,
                    Text = "Veri sıkıştırma işlemlerinde kullanılan bir algoritma aşağıdakilerden hangisidir?",
                    Options = "a) Prim's Algorithm b) Huffman Coding c) Kruskal's Algorithm d) DFS (Depth-First Search)",
                    CorrectAnswer = "b"
                });

                context.Questions.Add(new Question
                {
                    QuestionID = ++count,
                    AssignmentID = 2,
                    Text = "İki sıralı liste birleştirilirken hangi algoritma kullanılır?",
                    Options = "a) Merge Sort b) Quick Sort c) Bubble Sort d) Insertion Sort",
                    CorrectAnswer = "a"
                });


                context.Questions.Add(new Question
                {
                    QuestionID = ++count,
                    AssignmentID = 3,
                    Text = "Elektrik devrelerinde kullanılan bir düğme anahtarın temel görevi nedir?",
                    Options = "a) Akımı artırmak b) Devreyi kapatmak ve açmak c) Gerilimi ölçmek d) Direnci değiştirmek",
                    CorrectAnswer = "b"
                });

                context.Questions.Add(new Question
                {
                    QuestionID = ++count,
                    AssignmentID = 3,
                    Text = "Bir kondansatör ne işe yarar?",
                    Options = "a) Elektrik akımını düşürür b) Elektrik enerjisini depolar c) Direnci artırır d) Işıma yapar",
                    CorrectAnswer = "b"
                });

                context.Questions.Add(new Question
                {
                    QuestionID = ++count,
                    AssignmentID = 3,
                    Text = "Transformatörlerin temel görevi nedir?",
                    Options = "a) Akımı kontrol etmek b) Işıma Yapmak c) Gerilimi düşürmek veya yükseltmek d) Direnci artırmak",
                    CorrectAnswer = "c"
                });

                context.Questions.Add(new Question
                {
                    QuestionID = ++count,
                    AssignmentID = 5,
                    Text = "Bir buzdolabı içerisindeki bir madde, içeriden dışarı doğru ısı transferi sonucu donarak katı hale geçer. Bu süreç aşağıdakilerden hangisine örnektir?",
                    Options = "a) İzobarik süreç b) İzotermal süreç c) İzokorik süreç d) Adiabatik süreç",
                    CorrectAnswer = "c"
                });

                context.Questions.Add(new Question
                {
                    QuestionID = ++count,
                    AssignmentID = 5,
                    Text = "Bir buhar makinesinde, buharın basıncı sabit tutularak yapılan bir süreçte, makineye verilen ısı miktarı ile yapılan iş arasındaki ilişki nasıldır?",
                    Options = "a) İki miktar birbirinden bağımsızdır. b) İki miktar doğru orantılıdır. c) İki miktar ters orantılıdır. d) İki miktar arasında belirli bir ilişki yoktur.",
                    CorrectAnswer = "a"
                });


            }

            if (!context.TakenAssignments.Any())
            {

                context.TakenAssignments.Add(new TakenAssignment
                {
                    PersonID = 1,
                    AssignmentID = 1,
                    Point = -1
                });

                context.TakenAssignments.Add(new TakenAssignment
                {
                    PersonID = 2,
                    AssignmentID = 2,
                    Point = -1
                });

                context.TakenAssignments.Add(new TakenAssignment
                {
                    PersonID = 5,
                    AssignmentID = 3,
                    Point = -1
                });

                context.TakenAssignments.Add(new TakenAssignment
                {
                    PersonID = 3,
                    AssignmentID = 4,
                    Point = -1
                });

            }

            if (!context.Exams.Any())
            {

                int count = 0;

                context.Exams.Add(new Exam
                {
                    ID = ++count,
                    CourseID = 1,
                    Time = DateTime.Now.AddDays(-80),
                    Info = "Data Structure Midterm"
                });

                context.Exams.Add(new Exam
                {
                    ID = ++count,
                    CourseID = 1,
                    Time = DateTime.Now.AddDays(-10),
                    Info = "Data Structure Final"
                });

                context.Exams.Add(new Exam
                {
                    ID = ++count,
                    CourseID = 2,
                    Time = DateTime.Now.AddDays(-79),
                    Info = "Algorithm Midterm"
                });

                context.Exams.Add(new Exam
                {
                    ID = ++count,
                    CourseID = 2,
                    Time = DateTime.Now.AddDays(-9),
                    Info = "Algorithm Final"
                });

                context.Exams.Add(new Exam
                {
                    ID = ++count,
                    CourseID = 3,
                    Time = DateTime.Now.AddDays(-78),
                    Info = "Semiconductors Midterm"
                });

                context.Exams.Add(new Exam
                {
                    ID = ++count,
                    CourseID = 3,
                    Time = DateTime.Now.AddDays(-8),
                    Info = "Semiconductors Final"
                });

                context.Exams.Add(new Exam
                {
                    ID = ++count,
                    CourseID = 4,
                    Time = DateTime.Now.AddDays(-77),
                    Info = "Circuits Midterm"
                });

                context.Exams.Add(new Exam
                {
                    ID = ++count,
                    CourseID = 4,
                    Time = DateTime.Now.AddDays(-7),
                    Info = "Circuits Final"
                });

                context.Exams.Add(new Exam
                {
                    ID = ++count,
                    CourseID = 5,
                    Time = DateTime.Now.AddDays(-76),
                    Info = "Thermodynamics Midterm"
                });

                context.Exams.Add(new Exam
                {
                    ID = ++count,
                    CourseID = 5,
                    Time = DateTime.Now.AddDays(-6),
                    Info = "Thermodynamics Final"
                });

                context.Exams.Add(new Exam
                {
                    ID = ++count,
                    CourseID = 6,
                    Time = DateTime.Now.AddDays(-75),
                    Info = "Fluid Mechanics Midterm"
                });

                context.Exams.Add(new Exam
                {
                    ID = ++count,
                    CourseID = 6,
                    Time = DateTime.Now.AddDays(-5),
                    Info = "Fluid Mechanics Final"
                });

                context.Exams.Add(new Exam
                {
                    ID = ++count,
                    CourseID = 7,
                    Time = DateTime.Now.AddDays(-74),
                    Info = "Calculus Midterm"
                });

                context.Exams.Add(new Exam
                {
                    ID = ++count,
                    CourseID = 7,
                    Time = DateTime.Now.AddDays(-4),
                    Info = "Calculus Final"
                });

                context.Exams.Add(new Exam
                {
                    ID = ++count,
                    CourseID = 8,
                    Time = DateTime.Now.AddDays(-73),
                    Info = "Differential Equations Midterm"
                });

                context.Exams.Add(new Exam
                {
                    ID = ++count,
                    CourseID = 8,
                    Time = DateTime.Now.AddDays(-3),
                    Info = "Differential Equations Final"
                });

            }

            if (!context.TakenExams.Any())
            {
                context.TakenExams.Add(new TakenExam
                {
                    PersonID = 1,
                    ExamID = 1,
                    Point = 93
                });
                context.TakenExams.Add(new TakenExam
                {
                    PersonID = 1,
                    ExamID = 2,
                    Point = 72
                });
                context.TakenExams.Add(new TakenExam
                {
                    PersonID = 2,
                    ExamID = 3,
                    Point = 88
                });
                context.TakenExams.Add(new TakenExam
                {
                    PersonID = 2,
                    ExamID = 4,
                    Point = 42
                });
                context.TakenExams.Add(new TakenExam
                {
                    PersonID = 6,
                    ExamID = 5,
                    Point = 75
                });
                context.TakenExams.Add(new TakenExam
                {
                    PersonID = 6,
                    ExamID = 6,
                    Point = 98
                });
                context.TakenExams.Add(new TakenExam
                {
                    PersonID = 5,
                    ExamID = 7,
                    Point = 88
                });
                context.TakenExams.Add(new TakenExam
                {
                    PersonID = 5,
                    ExamID = 8,
                    Point = 42
                });
                context.TakenExams.Add(new TakenExam
                {
                    PersonID = 3,
                    ExamID = 9,
                    Point = 88
                });
                context.TakenExams.Add(new TakenExam
                {
                    PersonID = 3,
                    ExamID = 10,
                    Point = 42
                });
                context.TakenExams.Add(new TakenExam
                {
                    PersonID = 4,
                    ExamID = 11,
                    Point = 88
                });
                context.TakenExams.Add(new TakenExam
                {
                    PersonID = 4,
                    ExamID = 12,
                    Point = 42
                });
                context.TakenExams.Add(new TakenExam
                {
                    PersonID = 2,
                    ExamID = 13,
                    Point = 62
                });
                context.TakenExams.Add(new TakenExam
                {
                    PersonID = 3,
                    ExamID = 13,
                    Point = 41
                });
                context.TakenExams.Add(new TakenExam
                {
                    PersonID = 5,
                    ExamID = 13,
                    Point = 88
                });
                context.TakenExams.Add(new TakenExam
                {
                    PersonID = 2,
                    ExamID = 14,
                    Point = 88
                });
                context.TakenExams.Add(new TakenExam
                {
                    PersonID = 3,
                    ExamID = 14,
                    Point = 93
                });
                context.TakenExams.Add(new TakenExam
                {
                    PersonID = 5,
                    ExamID = 14,
                    Point = 67
                });
                context.TakenExams.Add(new TakenExam
                {
                    PersonID = 1,
                    ExamID = 15,
                    Point = 88
                });
                context.TakenExams.Add(new TakenExam
                {
                    PersonID = 4,
                    ExamID = 15,
                    Point = 93
                });
                context.TakenExams.Add(new TakenExam
                {
                    PersonID = 6,
                    ExamID = 15,
                    Point = 67
                });
                context.TakenExams.Add(new TakenExam
                {
                    PersonID = 1,
                    ExamID = 16,
                    Point = 62
                });
                context.TakenExams.Add(new TakenExam
                {
                    PersonID = 4,
                    ExamID = 16,
                    Point = 41
                });
                context.TakenExams.Add(new TakenExam
                {
                    PersonID = 6,
                    ExamID = 16,
                    Point = 88
                });
            }

            var coursesToUpdate = context.TakenCourses.ToList();

            foreach (var course in coursesToUpdate)
            {
                var averagePoint = context.TakenExams
                    .Where(exam => exam.PersonID == course.PersonID)
                    .Join(context.Exams, exam => exam.ExamID, e => e.ID, (exam, e) => new { exam, e })
                    .Where(joined => joined.e.CourseID == course.CourseID)
                    .Select(joined => joined.exam.Point)
                    .Union(context.TakenAssignments
                        .Join(context.Assignments, ta => ta.AssignmentID, a => a.AssignmentID, (ta, a) => new { ta, a })
                        .Where(x => x.a.CourseID == course.CourseID && x.ta.PersonID == course.PersonID && x.ta.Point != -1)
                        .Select(x => x.ta.Point)).DefaultIfEmpty()
                    .Average();

                course.Point = averagePoint;
            }
            context.SaveChanges();
            
            var students = context.Students.ToList();

            foreach (var student in students)
            {
                var GPA = (float)context.TakenCourses.Where(x => x.PersonID == student.ID).DefaultIfEmpty()
                .Average(x => x.Point >= 90 ? 4.0 :
                x.Point >= 80 ? 3.0 :
                x.Point >= 70 ? 2.0 :
                x.Point >= 60 ? 1.0 : 0.0);

                student.GPA = GPA;
            }

            context.SaveChanges();
        }
    }
}
