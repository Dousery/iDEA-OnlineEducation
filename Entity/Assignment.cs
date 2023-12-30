using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace iDEA.Entity {

    public class Assignment {
        [Key]
        public int AssignmentID { get; set; }

        public int CourseID { get; set; }

        public String? Name { get; set; }

        public DateTime Deadline { get; set; }
    }

}
