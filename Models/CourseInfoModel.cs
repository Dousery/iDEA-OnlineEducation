namespace iDEA.Models {

    public class Assignment {

    }
    public class Exam {
        
    }
    public class Session {
        
    }
    public class CourseInfoModel{
        public String? CourseName { get; set; }
        public int CourseID { get; set; }
        public String? CourseInfo { get; set; }
        public IList<Assignment>? Assignments { get; set; }
        public IList<Exam>? Exams { get; set; }
        public IList<Exam>? Sessions { get; set; }
        public float Score { get; set; }
    }
}

