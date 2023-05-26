using TaskManagerAPI.DTOs.Project;
using TaskManagerAPI.DTOs.Task;
using TaskManagerAPI.Enums;
using TaskManagerAPI.Models;

namespace TaskManagerAPI.Contracts.Repository
{
    public interface IProjectRepository
    {
        Task<List<Project>> GetProjecsAsync();
        Task<Project> CreateAsync(Project project);
        Task<Project> UpdateAsync(Project project);
        Task DeleteAsync(int projectId);
        Task<Project> AddUserAsync(Project project, User user);
        Task<Project> RemoveUserAsync(Project project, User user);
        Task<Project> ProjectByIdAsync(int projectId);
        Task<ProjectUserRolesEnum?> UserProjectRoleAsync(int projectId);
        Task<bool> UserExistAsync(int projectId, int? userId);
    }
}
