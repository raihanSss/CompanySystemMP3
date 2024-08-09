using CompanySystem.Models;

namespace CompanySystem.Interfaces
{
    public interface IWorksOnService
    {
        Task<Workson> CreateWorkAsync(Workson worksOn);
        IEnumerable <Workson> GetAllWorks();
        Workson GetWorkById(int id);
        string UpdateWork(Workson worksOn);
        string DeleteWork(int id);
    }
}
