using System.ComponentModel.DataAnnotations;

namespace iDEA.Models{
    public class Question {
        public String? Text { get; set; }
        public String? Options { get; set; }
        public String? CorrectAnswer { get; set; }
    }
    public class QuestionAnswerModel {
        public int AssignmentID { get; set; }
        public IList<Question> Questions { get; set; }
        public IList<String> Answers { get; set; }
    }
}