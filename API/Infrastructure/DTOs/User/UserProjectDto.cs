using Infrastructure.DTOs.User;
using TaskManagerAPI.Enums;

namespace TaskManagerAPI.DTOs.User
{
    public class UserProjectDto : UserDto
    {
        public ProjectUserRolesEnum? ProjectRole { get; set; }
    }
}
