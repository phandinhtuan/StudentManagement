using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace StudentManagement.Models
{
    
    public class usp_AspNetUserRoles_GetList
    {
        public string UserId { get; set; }

        public string UserName { get; set; }

        public string Name { get; set; }

        public string RoleId { get; set; }
    }

}