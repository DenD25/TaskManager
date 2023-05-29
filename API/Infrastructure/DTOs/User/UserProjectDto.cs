using Infrastructure.Enums;

namespace Infrastructure.DTOs.User
{
    public class UserProjectDto : UserDto
    {
        public ProjectUserRolesEnum? ProjectRole { get; set; }
    }
}
