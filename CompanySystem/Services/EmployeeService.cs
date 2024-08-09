using CompanySystem.Interfaces;
using CompanySystem.Models;
using Microsoft.EntityFrameworkCore;

namespace CompanySystem.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly CompanyContext _context;

        public EmployeeService(CompanyContext context)
        {
            _context = context;
        }

        public async Task<Employee> AddEmployeeAsync(Employee employee)
        {
            

            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();

            return employee;
        }

        public IEnumerable <Employee> GetAllEmployee()
        {
            return _context.Employees.ToList();
        }

        public Employee GetEmployeeById(int id)
        {
            return _context.Employees.FirstOrDefault(emp => emp.Empno == id);
        }

        public string UpdateEmployee( Employee employee)
        {
            var existingEmployee = _context.Employees.FirstOrDefault(emp => emp.Empno == employee.Empno);
            if (existingEmployee != null)

            existingEmployee.Fname = employee.Fname;
            existingEmployee.Lname = employee.Lname;
            existingEmployee.Address = employee.Address;
            existingEmployee.Dob = employee.Dob;
            existingEmployee.Sex = employee.Sex;
            existingEmployee.Position = employee.Position;
            existingEmployee.Deptno = employee.Deptno;

            _context.SaveChanges();
            return "Data employee berhasil diubah";
        }

        public string DeleteEmployee(int id)
        {
            var employee = _context.Employees.FirstOrDefault(emp => emp.Empno == id);
            if (employee != null)
            {
                _context.Employees.Remove(employee);
                 _context.SaveChanges();
                return "Data employee berhasil dihapus";
            }
            return "Employee tidak ditemukan";
        }


        public IEnumerable<int> GetFemaleEmployeeBirthYearsAfter1990()
        {
            var tahunLahir = _context.Employees
                .Where(e => e.Sex == 'F' && e.Dob.HasValue && e.Dob.Value.Year > 1990)
                .Select(e => e.Dob.Value.Year)
                .ToList();

            return tahunLahir;
        }


        public IEnumerable<Employee> GetEmployeesFromBrics()
        {
           
            var bricsCountries = new[] { "Brazil", "Russia", "India", "China", "South Africa" };

            var employees = _context.Employees
                .Where(e => bricsCountries.Any(country => e.Address != null && e.Address.Contains(country)))
                .OrderBy(e => e.Lname)
                .ToList();

            return employees;
        }

        public IEnumerable<Employee> GetEmployeesBornBetween1980And1990()
        {
            var employees = _context.Employees
                .Where(e => e.Dob.HasValue && e.Dob.Value.Year >= 1980 && e.Dob.Value.Year <= 1990)
                .ToList();

            return employees;
        }

        public IEnumerable<Employee> GetEmployeesNotManagers()
        {
            
            var managerIds = _context.Departements
                .Where(d => d.Mgrempno.HasValue)
                .Select(d => d.Mgrempno.Value)
                .ToList();

            
            var employees = _context.Employees
                .Where(e => !managerIds.Contains(e.Empno))
                .ToList();

            return employees;
        }

        public IEnumerable<Employee> GetEmployeesInITDepartment()
        {
            
            var employees = _context.Employees
                .Where(e => e.Deptno == 6)
                .Select(e => new Employee
                {
                    Fname = e.Fname,
                    Lname = e.Lname,
                    Address = e.Address
                })
                .ToList();

            return employees;
        }

        public IEnumerable<object> GetEmployeesWithAge()
        {
            var currentYear = DateTime.Now.Year;

    
            var employeesWithAge = _context.Employees
                .Where(e => e.Dob.HasValue && e.Deptno.HasValue)
                .Select(e => new
                {
                    Name = $"{e.Fname} {e.Lname}",
                    Department = e.DeptnoNavigation.Deptname,
                    Age = currentYear - e.Dob.Value.Year
                })
                .ToList();

            return employeesWithAge;
        }
    }
}
