using ApplicationCore.Contracts.Manager;
using ApplicationCore.Contracts.Repository;
using ApplicationCore.Contracts.Service;
using AutoMapper;
using Infrastructure.DTOs.Project;
using Infrastructure.Enums;
using Infrastructure.Models;

namespace ApplicationCore.Manager
{
    public class ProjectManager : IProjectManager
    {

        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly IUserRepository _userRepository;
        private readonly IProjectRepository _projectRepository;

        public IProjectRepository ProjectRepository => _projectRepository;

        public ProjectManager(IProjectRepository projectRepository, IUserRepository userRepository, IMapper mapper, IUserService userService)
        {
            _mapper = mapper;
            _userService = userService;
            _userRepository = userRepository;
            _projectRepository = projectRepository;
        }

        public async Task<ProjectDto> GetProjectByIdAsync(int projectId)
        {
            var project = await ProjectRepository.ProjectByIdAsync(projectId);

            var projectDto = _mapper.Map<ProjectDto>(project);

            return projectDto;
        }

        public async Task<List<ProjectDto>> GetProjectsAsync()
        {
            var projects =  await ProjectRepository.GetProjecsAsync();

            var projectsDto = _mapper.Map<List<ProjectDto>>(projects);

            return projectsDto;
        }

        public async Task<ProjectDto> CreateProjectAsync(ProjectCreateDto projectCreateDto)
        {
            var projectCreate = _mapper.Map<Project>(projectCreateDto);

            var userClaimId = int.Parse(_userService.GetUserId());

            var user = await _userRepository.UserByIdAsync(userClaimId);

            var projectUser = new ProjectUser
            {
                User = user,
                Role = ProjectUserRolesEnum.Owner
            };

            projectCreate.Users = new List<ProjectUser> { projectUser };

            var project = await ProjectRepository.CreateAsync(projectCreate);

            var projectDto = _mapper.Map<ProjectDto>(project);

            return projectDto;
        }

        public async Task<ProjectDto> UpdateProjectAsync(ProjectUpdateDto projectUpdateDto)
        {
            var role = await GetUserProjectRoleAsync(projectUpdateDto.Id);

            if (role == ProjectUserRolesEnum.Member)
            {

                throw new ArgumentException("User have`t permission");
            }

            var projectUpdate = _mapper.Map<Project>(projectUpdateDto);

            var project = await ProjectRepository.UpdateAsync(projectUpdate);

            var projectDto = _mapper.Map<ProjectDto>(project);

            return projectDto;
        }

        public async Task DeleteProjectAsync(int projectId)
        {
            var role = await ProjectRepository.UserProjectRoleAsync(projectId);

            if (role != ProjectUserRolesEnum.Owner)
            {
                throw new ArgumentException("User have`t permission");
            }

            await ProjectRepository.DeleteAsync(projectId);
        }

        public async Task<ProjectDto> AddUserToProjectAsync(int projectId, int userId)
        {
            var role = await GetUserProjectRoleAsync(projectId);

            if (role == ProjectUserRolesEnum.Member)
            {

                throw new ArgumentException("User have`t permission");
            }

            var project = await ProjectRepository.ProjectByIdAsync(projectId);

            var user = await _userRepository.UserByIdAsync(userId);

            var response = await ProjectRepository.AddUserAsync(project, user);

            var projectDto = _mapper.Map<ProjectDto>(project);

            return projectDto;
        }

        public async Task<ProjectDto> RemoveUserFromProjectAsync(int projectId, int userId)
        {
            var role = await GetUserProjectRoleAsync(projectId);

            if (role == ProjectUserRolesEnum.Member)
            {

                throw new ArgumentException("User have`t permission");
            }

            var project = await ProjectRepository.ProjectByIdAsync(projectId);

            var user = await _userRepository.UserByIdAsync(userId);

            var response = await ProjectRepository.AddUserAsync(project, user);

            var projectDto = _mapper.Map<ProjectDto>(project);

            return projectDto;
        }

        public async Task<ProjectUserRolesEnum?> GetUserProjectRoleAsync(int projectId)
        {
            var role = await ProjectRepository.UserProjectRoleAsync(projectId);

            return role;
        }

        public async Task<bool> GetUserExistAsync(int projectId, int? userId)
        {
            var existingUser = await ProjectRepository.UserExistAsync(projectId, userId);

            return existingUser;
        }
    }
}
