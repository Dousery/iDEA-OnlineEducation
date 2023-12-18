using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace iDEA.Entity {

    public class Session {
        [Key]
        public int ID { get; set; }

        public int CourseID { get; set; }
    }

}
