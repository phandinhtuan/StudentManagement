﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentManagement.Models
{
    public class Administrator
    {

        public int Id { get; set; }
        public string Name { get; set; }

        public ApplicationUser User { get; set; }
    }
}