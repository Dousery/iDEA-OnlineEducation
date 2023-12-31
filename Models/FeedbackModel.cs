using System.ComponentModel.DataAnnotations;

namespace iDEA.Models {
    public class FeedbackModel{
        [Required]
        public String? Name { get; set; }
        public String? Email { get; set; }
        public String? Text { get; set; }
        public String? PhoneNumber { get; set; }
    }
}

