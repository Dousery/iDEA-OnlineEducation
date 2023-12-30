namespace iDEA.Models {

    public class Assignment {
        public String? Course { get; set; }
        public int ID { get; set; }
        public String? Name { get; set; }
        public float Point { get; set; }
        public DateTime Deadline { get; set; }
    }
    public class Exam {
        public String? Course { get; set; }
        public int ID { get; set; }
        public DateTime Time { get; set; }
        public String? Info { get; set; }
        public float Point { get; set; }
    }
    public class Session {
        
    }
    public class CourseInfoModel{
        public String? CourseName { get; set; }
        public float Credit { get; set; }
        public int CourseID { get; set; }
        public String? CourseInfo { get; set; }
        public IList<Assignment>? Assignments { get; set; }
        public IList<Exam>? Exams { get; set; }
        public IList<Session>? Sessions { get; set; }
        public float Score { get; set; }
        public String? Grade { get; set; }
    }
}

