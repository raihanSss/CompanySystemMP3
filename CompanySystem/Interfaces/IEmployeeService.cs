using CompanySystem.Models;

namespace CompanySystem.Interfaces
{
    public interface IEmployeeService
    {
        Task<Employee> AddEmployeeAsync(Employee employee);
        IEnumerable<Employee> GetAllEmployee();
        Employee GetEmployeeById(int id);
        string UpdateEmployee(Employee employee);
        string DeleteEmployee(int id);

        IEnumerable<int> GetFemaleEmployeeBirthYearsAfter1990();

        IEnumerable<Employee> GetEmployeesFromBrics();

        IEnumerable<Employee> GetEmployeesBornBetween1980And1990();

        IEnumerable<Employee> GetEmployeesNotManagers();

        IEnumerable<Employee> GetEmployeesInITDepartment();

        IEnumerable<object> GetEmployeesWithAge();
    }
}
