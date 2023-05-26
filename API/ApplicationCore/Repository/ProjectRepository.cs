using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TaskManagerAPI.Contracts.Repository;
using TaskManagerAPI.Contracts.Service;
using TaskManagerAPI.Data;
using TaskManagerAPI.Enums;
using TaskManagerAPI.Models;

namespace TaskManagerAPI.Repository
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly DataContext _context;
        private readonly IUserService _userService;

        public ProjectRepository(DataContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        public async Task<List<Project>> GetProjecsAsync()
        {

            var userClaimId = int.Parse(_userService.GetUserId());

            var roles = _userService.GetUserRoles();

            bool isAdmin = roles.Contains("Admin");

            if (isAdmin)
            {
                var projects = await _context.Projects
                    .Include(x => x.Tasks)
                    .Include(x => x.Users)
                        .ThenInclude(x => x.User)
                    .ToListAsync();

                foreach (var project in projects)
                {
                    foreach (var user in project.Users)
                    {
                        var projectUser = project.Users.FirstOrDefault(pu => pu.UserId == user.User.Id);
                        if (projectUser != null)
                        {
                            user.User.ProjectRole = projectUser.Role;
                        }
                    }
                }

                return projects;
            }
            else
            {
                var projects = await _context.Projects
                    .Include(x => x.Tasks)
                    .Include(x => x.Users)
                        .ThenInclude(x => x.User)
                    .Where(x => x.Users.Any(u => u.User.Id == userClaimId))
                    .ToListAsync();

                foreach (var project in projects)
                {
                    foreach (var user in project.Users)
                    {
                        var projectUser = project.Users.FirstOrDefault(pu => pu.UserId == user.User.Id);
                        if (projectUser != null)
                        {
                            user.User.ProjectRole = projectUser.Role;
                        }
                    }
                }

                return projects;
            }
        }
        public async Task<Project> ProjectByIdAsync(int projectId)
        {
            var project = await _context.Projects
                .Include(x => x.Tasks)
                .Include(x => x.Users)
                    .ThenInclude(x => x.User)
                .SingleOrDefaultAsync(x => x.Id == projectId);


            if (project == null)
            {
                throw new ArgumentException("Project is null");
            }

            foreach (var user in project.Users)
            {
                var projectUser = project.Users.FirstOrDefault(pu => pu.UserId == user.User.Id);
                if (projectUser != null)
                {
                    user.User.ProjectRole = projectUser.Role;
                }
            }

            return project;
        }

        public async Task<Project> CreateAsync(Project project)
        {
            await _context.Projects.AddAsync(project);
            await SaveAllAsync();

            return project;
        }

        public async Task<Project> UpdateAsync(Project projectUpdate)
        {

            _context.Projects.Update(projectUpdate);
            await SaveAllAsync();

            var project = await ProjectByIdAsync(projectUpdate.Id);

            return project;
        }

        public async Task DeleteAsync(int projectId)
        {
            var project = await ProjectByIdAsync(projectId);

            _context.Projects.Remove(project);
            await SaveAllAsync();
        }

        public async Task<Project> AddUserAsync(Project project, User user)
        {
            var existingUser = await UserExistAsync(project.Id, user.Id);

            if (existingUser == true)
            {
                throw new ArgumentException("User is already a member of the project");
            }

            var projectUser = new ProjectUser
            {
                User = user,
                Role = ProjectUserRolesEnum.Member
            };

            project.Users.Add(projectUser);

            var projectUpdated = await UpdateAsync(project);

            await SaveAllAsync();

            return projectUpdated;
        }

        public async Task<Project> RemoveUserAsync(Project project, User user)
        {
            var existingUser = await UserExistAsync(project.Id, user.Id);

            if (existingUser == false)
            {
                throw new ArgumentException("User is not a member of the project");
            }

            var projectUser = project.Users.SingleOrDefault(x => x.UserId == user.Id);

            project.Users.Remove(projectUser);

            var projectUpdated = await UpdateAsync(project);

            await SaveAllAsync();

            return projectUpdated;
        }

        public async Task<ProjectUserRolesEnum?> UserProjectRoleAsync(int projectId)
        {
            var project = await ProjectByIdAsync(projectId);

            var userClaimId = int.Parse(_userService.GetUserId());

            var existingUser = await UserExistAsync(projectId, userClaimId);

            if (existingUser == false)
            {
                throw new ArgumentException("User is not a member of the project");
            }

            var user = project.Users.FirstOrDefault(pu => pu.UserId == userClaimId);

            return user.User.ProjectRole;
        }
        public async Task<bool> UserExistAsync(int projectId, int? userId)
        {
            if (userId == null || userId == 0)
                userId = int.Parse(_userService.GetUserId());

            var project = await ProjectByIdAsync(projectId);

            var existingUser = project?.Users?.Any(u => u.UserId == userId);

            return existingUser ?? false;
        }

        private async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
