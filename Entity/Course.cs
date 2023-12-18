using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace iDEA.Entity {

    public class Course {
        [Key]
        public int ID { get; set; }

        public string? Name { get; set; }

        public float Credit { get; set; }
        
        public string? ResourcePath { get; set; }
    }

}
