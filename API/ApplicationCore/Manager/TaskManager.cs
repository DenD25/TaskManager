using ApplicationCore.Contracts.Manager;
using ApplicationCore.Contracts.Repository;
using AutoMapper;
using Infrastructure.DTOs.Task;
using Infrastructure.Enums;
using Infrastructure.Models;

namespace ApplicationCore.Manager
{
    public class TaskManager : ITaskManager
    {
        private readonly IMapper _mapper;
        private readonly IProjectManager _projectManager;
        private readonly ITaskRepository _taskRepository;

        public TaskManager(IMapper mapper, IProjectManager projectManager, ITaskRepository taskRepository)
        {
            _mapper = mapper;
            _projectManager = projectManager;
            _taskRepository = taskRepository;
        }
        public async Task<TaskDto> GetTaskByIdAsync(TaskRequestDto taskRequestDto)
        {
            var existingUser = await _projectManager.GetUserExistAsync(taskRequestDto.ProjectId, null);

            if (existingUser == false)
            {
                throw new ArgumentException("User is not a member of the project");
            }

            var task = await _taskRepository.TaskByIdAsync(taskRequestDto.TaskId);

            var taskDto = _mapper.Map<TaskDto>(task);

            return taskDto;
        }

        public async Task<List<TaskDto>> GetTasksOfProjectAsync(int projectId)
        {
            var existingUser = await _projectManager.GetUserExistAsync(projectId, null);

            if (existingUser == false)
            {
                throw new ArgumentException("User is not a member of the project");
            }

            var tasks = await _taskRepository.TasksOfProjectAsync(projectId);

            var tasksDto = _mapper.Map<List<TaskDto>>(tasks);

            return tasksDto;
        }

        public async Task<List<TaskDto>> GetUserTasks(TaskUserDto taskUserDto)
        {
            var existingUser = await _projectManager.GetUserExistAsync(taskUserDto.ProjectId, taskUserDto.UsertId);

            if (existingUser == false)
            {
                throw new ArgumentException("User is not a member of the project");
            }

            var tasks = await _taskRepository.UserTasks(taskUserDto.ProjectId, taskUserDto.UsertId);

            var tasksDto = _mapper.Map<List<TaskDto>>(tasks);

            return tasksDto;
        }

        public async Task<TaskDto> CreateTaskAsync(TaskCreateDto taskCreateDto)
        {
            var role = await _projectManager.GetUserProjectRoleAsync(taskCreateDto.ProjectId);

            if (role == ProjectUserRolesEnum.Member) 
            {
                throw new ArgumentException("User have`t permission");
            }

            var taskCreate = _mapper.Map<TaskModel>(taskCreateDto);

            var task = await _taskRepository.CreateAsync(taskCreate);

            var taskDto = _mapper.Map<TaskDto>(task);

            return taskDto;

        }
        public async Task<TaskDto> UpdateTaskAsync(TaskUpdateDto taskUpdateDto)
        {
            var role = await _projectManager.GetUserProjectRoleAsync(taskUpdateDto.ProjectId);

            if (role == ProjectUserRolesEnum.Member)
            {
                throw new ArgumentException("User have`t permission");
            }

            var taskUpdate = _mapper.Map<TaskModel>(taskUpdateDto);

            await _taskRepository.UpdateAsync(taskUpdate);

            var task = await _taskRepository.TaskByIdAsync(taskUpdateDto.Id);

            var taskDto = _mapper.Map<TaskDto>(task);

            return taskDto;
        }

        public async Task DeleteTaskAsync(int taskId)
        {
            var role = await _projectManager.GetUserProjectRoleAsync(taskId);

            if (role == ProjectUserRolesEnum.Member)
            {
                throw new ArgumentException("User have`t permission");
            }

            await _taskRepository.DeleteAsync(taskId);
        }

        public async Task<TaskDto> ChangeTaskStatusAsync(TaskStatusDto taskStatusDto)
        {
            var role = await _projectManager.GetUserProjectRoleAsync(taskStatusDto.ProjectId);

            if (role == ProjectUserRolesEnum.Member)
            {
                throw new ArgumentException("User have`t permission");
            }

            var task = await _taskRepository.ChangeStatusAsync(taskStatusDto.TaskId, taskStatusDto.StatusId);

            var taskDto = _mapper.Map<TaskDto>(task);

            return taskDto;
        }

        public async Task<TaskDto> AddUserToTask(TaskAddUserDto taskUserDto)
        {

            var role = await _projectManager.GetUserProjectRoleAsync(taskUserDto.ProjectId);

            if (role == ProjectUserRolesEnum.Member)
            {
                throw new ArgumentException("User have`t permission");
            }

            var task = await _taskRepository.AddUser(taskUserDto.TaskId, taskUserDto.UsertId);

            var taskDto = _mapper.Map<TaskDto>(task);

            return taskDto;
        }
    }
}
