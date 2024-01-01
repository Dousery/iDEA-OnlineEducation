
using System.ComponentModel.DataAnnotations;

namespace iDEA.Models{
    public class AddStudentModel{

        [Required]
        public String Name { get; set; }

        [Required]
        public String Surname { get; set; }

        [Required]
        [EmailAddress]
        public String Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public String Password { get; set; }
        
        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password),ErrorMessage ="Parolanız eşleşmiyor.")]
        public String ConfirmPassword { get; set; }




    }
}