using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace StudentManagement.Models
{
    [Table("RegisterCourse")]
    public class RegisterCourse
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StudentCourseID { get; set; }

        public int StudentID { get; set; }

        public int ClassRoomID { get; set; }

        public DateTime Created { get; set; }

        public bool Deleted { get; set; }

        public virtual Student Student { get; set; }
        public virtual ClassRoom ClassRoom { get; set; }
    }

}