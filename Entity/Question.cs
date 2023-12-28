using System.ComponentModel.DataAnnotations;

namespace iDEA.Entity{

    public class Question{
        [Key]
        public int QuestionID { get; set; }
        public int AssignmentID { get; set; }
        public string? Text { get; set; }
        public string? Options { get; set; }
        public string? CorrectAnswer { get; set; }
    }

}

