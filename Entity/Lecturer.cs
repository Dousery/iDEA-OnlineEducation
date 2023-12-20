using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace iDEA.Entity {

    public class Lecturer {
        [Key]
        public int ID { get; set; }

        public string? Department { get; set; }
    }

}
