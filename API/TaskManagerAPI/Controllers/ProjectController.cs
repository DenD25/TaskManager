using ApplicationCore.Contracts.Manager;
using Infrastructure.DTOs.Project;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TaskManagerAPI.Controllers
{
    [Authorize]
    public class ProjectController : BaseController
    {
        private readonly IProjectManager _projectManager;

        public ProjectController(IProjectManager projectManager)
        {
            _projectManager = projectManager;
        }

        // GET: api/Project
        [HttpGet]
        public async Task<ActionResult<List<ProjectDto>>> GetProjects()
        {
            try
            {
                var projects = await _projectManager.GetProjectsAsync();

                return Ok(projects);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/Project/{projectId}
        [HttpGet]
        [Route("{projectId}")]
        public async Task<ActionResult<ProjectDto>> GetProject(int projectId)
        {
            try
            {
                var project = await _projectManager.GetProjectByIdAsync(projectId);

                return Ok(project);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: api/Project
        [HttpPost]
        public async Task<ActionResult<ProjectDto>> CreateProject(ProjectCreateDto projectCreateDto)
        {
            try
            {
                var project = await _projectManager.CreateProjectAsync(projectCreateDto);

                return Ok(project);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/Project
        [HttpPut]
        public async Task<ActionResult<ProjectDto>> UpdateProject(ProjectUpdateDto projectUpdateDto)
        {
            try
            {
                var project = await _projectManager.UpdateProjectAsync(projectUpdateDto);

                return Ok(project);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/Project
        [HttpDelete]
        [Route("{projectId}")]
        public async Task<ActionResult> DeleteProject(int projectId)
        {
            try
            {
                await _projectManager.DeleteProjectAsync(projectId);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: api/Project/add
        [HttpPost]
        [Route("add/{projectId}")]
        public async Task<ActionResult<ProjectDto>> AddUser(int projectId,[FromBody] int userId)
        {
            try
            {
                var project = await _projectManager.AddUserToProjectAsync(projectId, userId);

                return Ok(project);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: api/Project/remove
        [HttpPost]
        [Route("remove/{projectId}")]
        public async Task<ActionResult<ProjectDto>> RemoveUser(int projectId,[FromBody] int userId)
        {
            try
            {
                var project = await _projectManager.RemoveUserFromProjectAsync(projectId, userId);

                return Ok(project);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
