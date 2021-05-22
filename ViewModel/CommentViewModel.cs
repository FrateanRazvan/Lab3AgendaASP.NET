﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab3AgendaV2.ViewModel
{
    public class CommentViewModel
    {
        public long Id { get; set; }
        public string Text { get; set; }
        public bool Important { get; set; }
        public DateTime CommentDatetime { get; set; }
    }
}
