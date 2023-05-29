using Infrastructure.Enums;
using Infrastructure.Models;

namespace ApplicationCore.Contracts.Repository
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
