using Microsoft.EntityFrameworkCore;

namespace iDEA.Entity {

    public class DataContext: DbContext {

        public DataContext(DbContextOptions<DataContext> options): base(options) {

        } 
        public DbSet<Assignment> Assignments => Set<Assignment>();
        public DbSet<Attendance> Attendances => Set<Attendance>();
        public DbSet<Course> Courses => Set<Course>();
        public DbSet<Exam> Exams => Set<Exam>();
        public DbSet<Feedback> Feedbacks => Set<Feedback>();
        public DbSet<LectureCourse> LectureCourses => Set<LectureCourse>();
        public DbSet<Lecturer> Lecturers => Set<Lecturer>();
        public DbSet<Person> People => Set<Person>();
        public DbSet<Record> Records => Set<Record>();
        public DbSet<Session> Sessions => Set<Session>();
        public DbSet<Student> Students => Set<Student>();
        public DbSet<TakenAssignment> TakenAssignments => Set<TakenAssignment>();
        public DbSet<TakenCourse> TakenCourses => Set<TakenCourse>();
        public DbSet<TakenExam> TakenExams => Set<TakenExam>();

        public DbSet<Question> Questions => Set<Question>();
    }

}
