using AutoMapper;
using Infrastructure.DTOs.Auth;
using Infrastructure.DTOs.Message;
using Infrastructure.DTOs.Project;
using Infrastructure.DTOs.Task;
using Infrastructure.DTOs.User;
using Infrastructure.Models;

namespace ApplicationCore.Configurations
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
            CreateMap<CreateMessageDto, Message>().ReverseMap();
            CreateMap<MessageDto, Message>().ReverseMap()
                .ForMember(x => x.SenderUsername, opt => opt.MapFrom(x => x.Sender.UserName))
                .ForMember(x => x.SenderPhotoUrl, opt => opt.MapFrom(x => x.Sender.Photo.Url));
        }
    }
}
