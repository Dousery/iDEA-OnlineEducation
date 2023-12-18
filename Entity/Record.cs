using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace iDEA.Entity {

    public class Record {
        [Key]
        public int ID { get; set; }

        public string? Chat { get; set; }

        public string? VideoPath { get; set; }
    }

}
