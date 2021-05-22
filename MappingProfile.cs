using AutoMapper;
using Lab3AgendaV2.ViewModel;
using Lab3AgendaV2.Models;

namespace Lab3AgendaV2
{
    public class MappingProfile: Profile
    {

        public MappingProfile()
        {
            CreateMap<Task, TaskViewModel>();
            CreateMap<CommentViewModel, CommentViewModel>();
            CreateMap<Task, TaskWithCommentViewModel>().ReverseMap();
        }
    }
}
