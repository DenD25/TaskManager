using Microsoft.EntityFrameworkCore;
using TaskManagerAPI.Contracts.Repository;
using TaskManagerAPI.Data;
using TaskManagerAPI.DTOs.Task;
using TaskManagerAPI.Enums;
using TaskManagerAPI.Models;

namespace TaskManagerAPI.Repository
{
    public class TaskRepository : ITaskRepository
    {
        private readonly DataContext _context;

        public TaskRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<List<TaskModel>> TasksOfProjectAsync(int projectId)
        {
            var tasks = await _context.Tasks
                .Where(x => x.ProjectId == projectId)
                .ToListAsync();

            if (tasks == null)
            {
                throw new NullReferenceException("Tasks list is empty!");
            }

            return tasks;
        }

        public async Task<List<TaskModel>> UserTasks(int projectId, int userId)
        {
            var tasks = await _context.Tasks
                .Where(x => x.ProjectId == projectId)
                .Where(x => x.AssignedToId == userId)
                .ToListAsync();

            if (tasks == null)
            {
                throw new NullReferenceException("Tasks list is empty!");
            }

            return tasks;
        }

        public async Task<TaskModel> TaskByIdAsync(int taskId)
        {
            var task =  await _context.Tasks
                .SingleOrDefaultAsync(x => x.Id == taskId);

            if (task == null)
            {
                throw new NullReferenceException("Wrong task id!");
            }

            return task;
        }

        public async Task<TaskModel> CreateAsync(TaskModel task)
        {
            await _context.Tasks.AddAsync(task);
            await SaveAllAsync();

            return task;
        }

        public async Task UpdateAsync(TaskModel task)
        {
            _context.Tasks.Update(task);
            await SaveAllAsync();
        }

        public async Task DeleteAsync(int taskId)
        {
            var task = await TaskByIdAsync(taskId);

            _context.Tasks.Remove(task);
            await SaveAllAsync();
        }

        public async Task<TaskModel> ChangeStatusAsync(int taskId, int statusId)
        {
            var task = await TaskByIdAsync(taskId);

            task.Status = (TaskStatusEnum)statusId;

            await UpdateAsync(task);

            return task;
        }

        public async Task<TaskModel> AddUser(int taskId, int userId)
        {
            var task = await TaskByIdAsync(taskId);

            task.AssignedToId = userId;

            await UpdateAsync(task);

            return task;
        }

        private async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
