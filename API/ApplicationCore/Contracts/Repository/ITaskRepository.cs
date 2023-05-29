using Infrastructure.Models;

namespace ApplicationCore.Contracts.Repository
{
    public interface ITaskRepository
    {
        Task<List<TaskModel>> TasksOfProjectAsync(int projectId);
        Task<List<TaskModel>> UserTasks(int projectId, int userId);
        Task<TaskModel> TaskByIdAsync(int taskId);
        Task<TaskModel> CreateAsync(TaskModel task);
        Task UpdateAsync(TaskModel task);
        Task DeleteAsync(int taskId);
        Task<TaskModel> ChangeStatusAsync(int taskId, int statusId);
        Task<TaskModel> AddUser(int taskId, int userId);
    }
}
