using CompanySystem.Interfaces;
using CompanySystem.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CompanySystem.Controllers
{
    [ApiController]
    [Route("api/proj")]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpPost]
        public async Task<ActionResult<Project>> CreateProject([FromBody] Project project)
        {
            if (project == null)
            {
                return BadRequest("Project data is null.");
            }

            var createdProject = await _projectService.CreateProjAsync(project);
            return CreatedAtAction(nameof(GetProjectById), new { id = createdProject.Projno }, createdProject);
        }

        [HttpGet]
        public ActionResult<IEnumerable<Project>> GetAllProjects()
        {
            var projects = _projectService.GetAllProj();
            return Ok(projects);
        }

        [HttpGet("{id}")]
        public ActionResult<Project> GetProjectById(int id)
        {
            var project = _projectService.GetProjById(id);
            if (project == null)
            {
                return NotFound("Project not found.");
            }
            return Ok(project);
        }

        [HttpPut("{id}")]
        public ActionResult<string> UpdateProject(int id, [FromBody] Project project)
        {
            if (project == null)
            {
                return BadRequest("Project data is null.");
            }

            if (id != project.Projno)
            {
                return BadRequest("Project ID mismatch.");
            }

            var result = _projectService.UpdateProj(project);
            if (result == "Project not found")
            {
                return NotFound(result);
            }

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public ActionResult<string> DeleteProject(int id)
        {
            var result = _projectService.DeleteProj(id);
            if (result == "Project not found")
            {
                return NotFound(result);
            }

            return Ok(result);
        }
    }
}
