using AutoMapper;
using Data.Entities;
using Core.DTO;

namespace Core.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<TaskItem, ReturnTaskItemDTO>();

            CreateMap<ReturnTaskItemDTO, TaskItem>();

            CreateMap<GetTaskItemDTO, TaskItem>();

            CreateMap<TodoList, ReturnTodoListDTO>();

            CreateMap<GetTodoListDTO, TodoList>();

            CreateMap<User, ReturnUserDTO>();

            CreateMap<GetUserDTO, User>();

            CreateMap<LoginUserDTO, User>();

            CreateMap<ReturnTodoListDTO, TodoList>()
                .ForMember(dest => dest.Id, src => src.MapFrom(x => x.Id))
                .ForMember(dest => dest.Name, src => src.MapFrom(x => x.Name))
                .ForMember(dest => dest.Tasks, src => src.MapFrom(x => x.Tasks));

        }
    
    }
}
