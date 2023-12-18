using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace iDEA.Entity {
    [PrimaryKey(nameof(StudentID), nameof(SessionID))]
    public class Attendance {
        [Column(Order = 0)]
        public int StudentID { get; set; }
        [Column(Order = 1)]
        public int SessionID { get; set; }
    }

}
