using CompanySystem.Models;

namespace CompanySystem.Interfaces
{
    public interface IProjectService
    {
        Task<Project> CreateProjAsync(Project project);
        IEnumerable<Project> GetAllProj();
        Project GetProjById(int id);
        string UpdateProj(Project project);
        string DeleteProj(int id);
    }
}
