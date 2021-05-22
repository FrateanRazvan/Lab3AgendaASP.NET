using FluentValidation;
using Lab3AgendaV2.Data;
using Lab3AgendaV2.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab3AgendaV2.Validators
{
    public class TaskValidator : AbstractValidator<TaskViewModel>
    {

       


        public TaskValidator(){
            RuleFor(x => x.Title).MaximumLength(80);
            RuleFor(x => x.Description).MaximumLength(2000);
            RuleFor(x => x.DateTimeAdded).NotEmpty();
            RuleFor(x => x.DateTimeDeadline).NotEmpty();
            RuleFor(x => x.DateTimeClosedAt).NotEmpty();
            RuleFor(x => x.Importance).NotEmpty();
            RuleFor(x => x.DateTimeClosedAt).NotEmpty();
        }
    }
}
