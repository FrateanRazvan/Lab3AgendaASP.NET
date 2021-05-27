﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab3AgendaV2.Models
{
    public class ApplicationUser : IdentityUser
    {
        public List<Project> Projects { get; set; }
    }
}
