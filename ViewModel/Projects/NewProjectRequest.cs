using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab3AgendaV2.ViewModel.Projects
{
    public class NewProjectRequest
    {
        public List<int> TasksOfProjectIds { get; set; }

        public DateTime? ProjectAddDateTime { get; set; }
        public string Name { get; set; }
    }
}
