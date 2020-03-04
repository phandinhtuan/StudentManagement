using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace StudentManagement.Models
{
    [Table("ClassRoom")]
    public class ClassRoom
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ClassRoomID { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }

        public DateTime TimeStart { get; set; }

        public DateTime? TimeEnd { get; set; }

        public int? CourseID { get; set; }

        public virtual ICollection<RegisterCourse> RegisterCourses { get; set; }
        public virtual ICollection<RegisterClassRoom> RegisterClassRooms { get; set; }
    }

}