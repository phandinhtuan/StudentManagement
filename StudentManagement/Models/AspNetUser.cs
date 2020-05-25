using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace StudentManagement.Models
{
    [Table("AspNetUsers")]
    public class AspNetUser
    {
        [Key]
        [MaxLength(128)]
        public string Id { get; set; }

        [MaxLength(256)]
        public string Email { get; set; }

        public bool EmailConfirmed { get; set; }

        [MaxLength]
        public string PasswordHash { get; set; }

        [MaxLength]
        public string SecurityStamp { get; set; }

        [MaxLength]
        public string PhoneNumber { get; set; }

        public bool PhoneNumberConfirmed { get; set; }

        public bool TwoFactorEnabled { get; set; }

        public DateTime? LockoutEndDateUtc { get; set; }

        public bool LockoutEnabled { get; set; }

        public int AccessFailedCount { get; set; }

        [MaxLength(256)]
        public string UserName { get; set; }
    }

}