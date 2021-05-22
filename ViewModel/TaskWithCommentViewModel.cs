using Lab3AgendaV2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab3AgendaV2.ViewModel
{
    public class TaskWithCommentViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DateTimeAdded { get; set; }
        public DateTime DateTimeDeadline { get; set; }
        //low, medium, high
        public string Importance { get; set; }
        //open, in progress, closed
        public string State { get; set; }
        public DateTime DateTimeClosedAt { get; set; }

        public IEnumerable<CommentViewModel> Comments { get; set; }

    }
}
