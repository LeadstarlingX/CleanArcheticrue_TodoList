using AutoMapper;
using Data.Entities;
using Core.DTO;

namespace Core.Mappers
{
    internal class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<TaskItem, ReturnTaskItemDTO>().ForMember(dest => dest.Name, src => src.MapFrom(x => x.Name))
                .ForMember(dest => dest.Id, src => src.MapFrom(x => x.Id))
                .ForMember(dest => dest.IsCompleted, src => src.MapFrom(x => x.IsCompleted));

            CreateMap<GetTaskItemDTO, TaskItem>().ForMember(dest => dest.Name, src => src.MapFrom(x => x.Name))
                .ForMember(dest => dest.IsCompleted, src => src.MapFrom(x => x.IsCompleted))
                .ForMember(dest => dest.TodoListID, src => src.MapFrom(x => x.TodoListId))
                .ForMember(dest => dest.Id, src => src.MapFrom(x => x.Id));

            CreateMap<TodoList, ReturnTodoListDTO>()
                .ForMember(dest => dest.Id, src => src.MapFrom(x => x.Id))
                .ForMember(dest => dest.Name, src => src.MapFrom(x => x.Name))
                .ForMember(dest => dest.Tasks, src => src.MapFrom(x => x.Tasks));

            CreateMap<ReturnTodoListDTO, TodoList>()
                .ForMember(dest => dest.Id, src => src.MapFrom(x => x.Id))
                .ForMember(dest => dest.Name, src => src.MapFrom(x => x.Name))
                .ForMember(dest => dest.Tasks, src => src.MapFrom(x => x.Tasks));

        }
    
    }
}
