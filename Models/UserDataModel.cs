namespace iDEA.Models {
    public class UserDataModel{
        public string? Name { get; set; }
        public int ID { get; set; }
        public string? Department { get; set; }
        public bool IsStudent { get; set; }
        public bool IsLecturer { get; set; }
        public float GPA { get; set; }
        public int Credit { get; set; }
        public IList<Exam> Exams { get; set; }
        public IList<Assignment> Assignments { get; set; } 
    }
}

