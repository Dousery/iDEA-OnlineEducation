namespace iDEA.Models {
    public class StudentDataModel{
        public string? Name { get; set; }
        public int ID { get; set; }
        public string? Department { get; set; }
        public float GPA { get; set; }
        public int Credit { get; set; }
    }

    public class StudentsDataModel{
        public IList<StudentDataModel>? Students { get; set; }
    }
}

