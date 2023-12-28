namespace iDEA.Models {

    public class Course {
        public int CourseID { get; set; }
        public String? CourseName { get; set; }
        public int Credit { get; set; }
        public int AssignmentCount { get; set; }
        public int SessionCount { get; set; }
        public int ExamCount { get; set; }
        public float Score { get; set; }
        public String? Grade { get; set; }
    }
    public class CoursesModel{
        public IList<Course>? Courses { get; set; }
    }
}

