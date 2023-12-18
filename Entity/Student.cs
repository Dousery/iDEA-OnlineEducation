using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace iDEA.Entity {

    public class Student {
        [Key]
        public int ID { get; set; }

        public float GPA { get; set; }

        public string? Department { get; set; }
    }

}
