using CompanySystem.Models;

namespace CompanySystem.Interfaces
{
    public interface IDepartementService
    {
        Task<Departement> CreateDept(Departement departement);
        IEnumerable<Departement> GetAllDept();
        Departement GetDeptById(int id);
        string UpdateDept(Departement departement);
        string DeleteDept(int id);

        IEnumerable<Employee> GetFemaleManagers();

        int CountFemaleManagers();

        IEnumerable<Employee> GetManagersRetire();

        IEnumerable<Employee> GetManagersUnder40();


    }
}
