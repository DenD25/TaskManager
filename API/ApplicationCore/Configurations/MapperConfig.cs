using AutoMapper;
using Infrastructure.DTOs.User;
using Infrastructure.Models;
using TaskManagerAPI.DTOs.Auth;
using TaskManagerAPI.DTOs.Project;
using TaskManagerAPI.DTOs.Task;
using TaskManagerAPI.DTOs.User;
using TaskManagerAPI.Models;

namespace TaskManagerAPI.Configurations
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<User, RegisterDto>().ReverseMap();
            CreateMap<AuthUserDto, User>().ReverseMap();
            CreateMap<UserDto, User>().ReverseMap();
            CreateMap<UserProjectDto, User>().ReverseMap();
            CreateMap<UpdateUserDto, User>().ReverseMap();
            CreateMap<ProjectCreateDto, Project>().ReverseMap();
            CreateMap<ProjectUpdateDto, Project>().ReverseMap();
            CreateMap<ProjectDto, Project>().ReverseMap()
                   .ForMember(x => x.Users, opt => opt.MapFrom(x => x.Users.Select(x => x.User)))
                   .ForMember(x => x.Tasks, opt => opt.MapFrom(x => x.Tasks));
            CreateMap<TaskDto, TaskModel>().ReverseMap();
            CreateMap<TaskCreateDto, TaskModel>().ReverseMap();
            CreateMap<TaskUpdateDto, TaskModel>().ReverseMap();
            CreateMap<PhotoDto, Photo>().ReverseMap();
        }
    }
}
