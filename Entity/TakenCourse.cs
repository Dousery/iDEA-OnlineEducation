using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace iDEA.Entity {
    [PrimaryKey(nameof(PersonID), nameof(CourseID))]
    public class TakenCourse {
        [Column(Order = 0)]
        public int PersonID { get; set; }
        [Column(Order = 1)]
        public int CourseID { get; set; }

        public float Point { get; set; }

        public string? Grade { get; set; }
    }

}
