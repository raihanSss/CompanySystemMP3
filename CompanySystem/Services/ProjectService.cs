using CompanySystem.Interfaces;
using CompanySystem.Models;

namespace CompanySystem.Services
{
    public class ProjectService : IProjectService
    {
        private readonly CompanyContext _context;

        public ProjectService(CompanyContext context)
        {
            _context = context;
        }

        public async Task<Project> CreateProjAsync(Project project)
        {
            if (project == null)
            {
                throw new ArgumentNullException(nameof(project));
            }

            await _context.Projects.AddAsync(project);
            await _context.SaveChangesAsync();
            return project;
        }

        public IEnumerable<Project> GetAllProj()
        {
            return _context.Projects.ToList();
        }

        public Project GetProjById(int id)
        {
            return _context.Projects.FirstOrDefault(p => p.Projno == id);
        }

        public string UpdateProj(Project project)
        {
            var existingProject = _context.Projects.FirstOrDefault(p => p.Projno == project.Projno);
            if (existingProject == null)
            {
                return "Project not found";
            }

            existingProject.Projname = project.Projname;
            existingProject.Deptno = project.Deptno;

            _context.Projects.Update(existingProject);
            _context.SaveChanges();

            return "Project updated successfully";
        }

        public string DeleteProj(int id)
        {
            var project = _context.Projects.FirstOrDefault(p => p.Projno == id);
            if (project == null)
            {
                return "Project not found";
            }

            _context.Projects.Remove(project);
            _context.SaveChanges();

            return "Project deleted successfully";
        }
    }
}
