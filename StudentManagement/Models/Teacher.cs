using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace StudentManagement.Models
{
    [Table("Teacher")]
    public class Teacher
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TeacherID { get; set; }

        [MaxLength(50)]
        [DisplayName("Họ")]
        public string FirstName { get; set; }

        [MaxLength(50)]
        [DisplayName("Tên")]
        public string LastName { get; set; }

        [MaxLength(50)]
        [DisplayName("Chuyên Môn")]
        public string Qualification { get; set; }

        //public int? ClassRoomID { get; set; }

        [MaxLength(50)]
        public string Email { get; set; }

        [MaxLength(20)]
        [DisplayName("Số Điện Thoại")]
        public string PhoneNumber { get; set; }


        [DisplayName("Giới Tính")]
        public string Gender { get; set; }
        public virtual ICollection<RegisterClassRoom> RegisterClassRooms { get; set; }
    }

}