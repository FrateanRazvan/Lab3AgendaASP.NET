﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab3AgendaV2.Models
{
    public class Project
    {
        public int Id { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public IEnumerable<Task> Tasks { get; set; }
        public string Name { get; set; }
        public DateTime DateTimeProject { get; set; }

    }
}
