using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StudentManagement.Models
{
    public class Group
    {
        public Group() { }


        public Group(string name) : this()
        {
            this.Roles = new List<ApplicationRolegroup>();
            this.Name = name;
        }


        [Key]
        [Required]
        public virtual int Id { get; set; }

        public virtual string Name { get; set; }
        public virtual ICollection<ApplicationRolegroup> Roles { get; set; }
    }
}