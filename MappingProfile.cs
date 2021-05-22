using AutoMapper;
using Lab3AgendaV2.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab3AgendaV2
{
    public class MappingProfile: Profile
    {

        public MappingProfile()
        {

            CreateMap<Task, TaskViewModel>();
            CreateMap<CommentViewModel, CommentViewModel>();
            CreateMap<Task, TaskWithCommentViewModel>();
        }
    }
}
