using Infrastructure.DTOs.Project;
using Infrastructure.Enums;

namespace ApplicationCore.Contracts.Manager
{
    public interface IProjectManager
    {
        Task<List<ProjectDto>> GetProjectsAsync(); 
        Task<ProjectDto> GetProjectByIdAsync(int projectId);
        Task<ProjectDto> CreateProjectAsync(ProjectCreateDto projectCreateDto);
        Task<ProjectDto> UpdateProjectAsync(ProjectUpdateDto projectUpdateDto);
        Task DeleteProjectAsync(int projectId);
        Task<ProjectDto> AddUserToProjectAsync(int projectId, int userId);
        Task<ProjectDto> RemoveUserFromProjectAsync(int projectId, int userId);
        Task<ProjectUserRolesEnum?> GetUserProjectRoleAsync(int projectId);
        Task<bool> GetUserExistAsync(int projectId, int? userId);
    }
}
