using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace StudentManagement.Models
{
    [Table("RegisterClassRoom")]
    public class RegisterClassRoom
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RegisterClassRoomID { get; set; }

        public int TeacherID { get; set; }

        public int ClassRoomID { get; set; }

        [Display]
        public DateTime Created { get; set; }

        public bool Deleted { get; set; }

        public virtual ClassRoom ClassRoom { get; set; }
        public virtual Teacher Teacher { get; set; }
    }

}