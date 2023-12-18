using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace iDEA.Entity {

    public class Feedback {
        [Key]
        public int ID { get; set; }

        public int CourseID { get; set; }

        public int Rate { get; set; }
        
        public string? Comment { get; set; }
    }

}
