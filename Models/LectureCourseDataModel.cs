namespace iDEA.Models {

    public class LectureCourseDataModel{
        public String? CourseName { get; set; }
        public float Credit { get; set; }
        public int CourseID { get; set; }
        public IList<Assignment>? Assignments { get; set; }
        public IList<Exam>? Exams { get; set; }
        public IList<StudentDataModel>? Students { get; set; }
        public int InputStudentID { get; set; }
    }
}

