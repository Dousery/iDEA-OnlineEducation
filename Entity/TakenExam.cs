using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace iDEA.Entity {
    [PrimaryKey(nameof(PersonID), nameof(ExamID))]
    public class TakenExam {
        [Column(Order = 0)]
        public int PersonID { get; set; }
        [Column(Order = 1)]
        public int ExamID { get; set; }

        public float Point { get; set; }
        
        public string? AttachmentPath { get; set; }
    }

}
