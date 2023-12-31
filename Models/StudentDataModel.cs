namespace iDEA.Models {
    public class StudentDataModel{
        public string? Name { get; set; }
        public int ID { get; set; }
        public string? Department { get; set; }
        public float GPA { get; set; }
        public int Credit { get; set; }
        public IList<Exam> Exams { get; set; }
        public IList<Assignment> Assignments { get; set; } 
    }

    public class StudentsDataModel{
        public IList<StudentDataModel>? Students { get; set; }
    }

    public class StudentExamDataModel{
        public string? StudentName { get; set; }
        public int StudentID { get; set; }
        public string? ExamName { get; set; }
        public float Point { get; set; }
    }
}

