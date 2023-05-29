using Infrastructure.DTOs.Task;

namespace ApplicationCore.Contracts.Manager
{
    public interface ITaskManager
    {
        Task<List<TaskDto>> GetTasksOfProjectAsync(int projectId);
        Task<List<TaskDto>> GetUserTasks(TaskUserDto taskUserDto);
        Task<TaskDto> GetTaskByIdAsync(TaskRequestDto taskRequestDto);
        Task<TaskDto> CreateTaskAsync(TaskCreateDto taskCreateDto);
        Task<TaskDto> UpdateTaskAsync(TaskUpdateDto taskUpdateDto);
        Task DeleteTaskAsync(int taskId);
        Task<TaskDto> ChangeTaskStatusAsync(TaskStatusDto taskStatusDto);
        Task<TaskDto> AddUserToTask(TaskAddUserDto taskAddUserDto);
    }
}
