using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace StudentManagement.Models
{
    [Table("Student")]
    public class Student
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StudentID { get; set; }

        [DisplayName("Họ")]
        [MaxLength(50)]
        [MinLength(3)]
        [Required(ErrorMessage = "Không được để trống.")]
        public string FirstName { get; set; }

        [DisplayName("Tên")]
        [MaxLength(50)]
        [Required(ErrorMessage = "Không được để trống.")]
        public string LastName { get; set; }

        [DisplayName("Giới tính")]

        public string Gender { get; set; }
        [Display]
        [Required(ErrorMessage = "Không được để trống.")]

        [DisplayName("Ngày sinh")]
        public DateTime? Birthday { get; set; }

        [MaxLength(50)]
        [Required(ErrorMessage = "Không được để trống.")]
        [EmailAddress(ErrorMessage = "Mời nhập Email vào.")]
        public string Email { get; set; }


        [DisplayName("Số điện thoại")]
        [Required(ErrorMessage = "Không được để trống.")]
        [MaxLength(20)]
        public string PhoneNumber { get; set; }

        public virtual ICollection<RegisterCourse> RegisterCourses { get; set; }

        [NotMapped]
        public string FullName { get; set; }
    }

}