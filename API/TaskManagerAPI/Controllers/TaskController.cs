using Infrastructure.DTOs.Task;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagerAPI.Contracts.Manager;
using TaskManagerAPI.DTOs.Task;

namespace TaskManagerAPI.Controllers
{
    [Authorize]
    public class TaskController : BaseController
    {
        private readonly ITaskManager _taskManager;

        public TaskController(ITaskManager taskManager)
        {
            _taskManager = taskManager;
        }

        // GET: api/Task/{projectId}
        [HttpGet]
        [Route("{projectId}")]
        public async Task<ActionResult<List<TaskDto>>> GetTasksOfProject(int projectId)
        {
            try
            {
                var tasks = await _taskManager.GetTasksOfProjectAsync(projectId);

                return Ok(tasks);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: api/Task/user
        [HttpPost]
        [Route("user")]
        public async Task<ActionResult<List<TaskDto>>> GetUserTasks([FromBody] TaskUserDto taskUserDto)
        {
            try
            {
                var tasks = await _taskManager.GetUserTasks(taskUserDto);

                return Ok(tasks);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: api/Task/id
        [HttpPost]
        [Route("id")]
        public async Task<ActionResult<TaskDto>> GetTaskById([FromBody] TaskRequestDto taskRequestDto)
        {
            try
            {
                var task = await _taskManager.GetTaskByIdAsync(taskRequestDto);

                return Ok(task);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }

        // POST: api/Task
        [HttpPost]
        public async Task<ActionResult<TaskDto>> CreateTask([FromBody] TaskCreateDto taskCreateDto)
        {
            try
            {
                var task = await _taskManager.CreateTaskAsync(taskCreateDto);

                return Ok(task);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/Task
        [HttpPut]
        public async Task<ActionResult<TaskDto>> UpdateTask([FromBody] TaskUpdateDto taskUpdateDto)
        {
            try
            {
                var task = await _taskManager.UpdateTaskAsync(taskUpdateDto);

                return Ok(task);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/Task/{taskId}
        [HttpDelete]
        [Route("{taskId}")]
        public async Task<ActionResult> DeleteTask(int taskId)
        {
            try
            {
                await _taskManager.DeleteTaskAsync(taskId);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: api/Task/status
        [HttpPost]
        [Route("status")]
        public async Task<ActionResult> ChangeTaskStatus([FromBody] TaskStatusDto taskStatusDto)
        {
            try
            {
                var task = await _taskManager.ChangeTaskStatusAsync(taskStatusDto);

                return Ok(task);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: api/Task/addUser
        [HttpPost]
        [Route("addUser")]
        public async Task<ActionResult> AddUserToTask([FromBody] TaskAddUserDto taskUserDto)
        {
            try
            {
                var task = await _taskManager.AddUserToTask(taskUserDto);

                return Ok(task);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
