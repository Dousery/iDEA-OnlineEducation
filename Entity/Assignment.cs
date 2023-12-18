using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace iDEA.Entity {

    public class Assignment {
        [Key]
        public int ID { get; set; }

        public int CourseID { get; set; }

        public DateTime Deadline { get; set; }
        
        public string? Info { get; set; }
    }

}
