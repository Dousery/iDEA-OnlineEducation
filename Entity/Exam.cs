using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace iDEA.Entity {

    public class Exam {
        [Key]
        public int ID { get; set; }

        public int CourseID { get; set; }

        public DateTime Time { get; set; }

        public string? Info { get; set; }
    }

}
