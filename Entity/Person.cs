using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace iDEA.Entity {

    public class Person {
        [Key]
        public int ID { get; set; }

        public string? Name { get; set; }

        public string? Surname { get; set; }

        public string? Username { get; set; }
        [PasswordPropertyText]
        public byte? Password { get; set; }
    }

}
