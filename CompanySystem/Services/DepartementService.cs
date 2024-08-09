using CompanySystem.Interfaces;
using CompanySystem.Models;

namespace CompanySystem.Services
{
    public class DepartementService : IDepartementService
    {
        private readonly CompanyContext _context;

        public DepartementService(CompanyContext context)
        {
            _context = context;
        }

        public async Task<Departement> CreateDept(Departement departement)
        {
            await _context.Departements.AddAsync(departement);
            await _context.SaveChangesAsync();
            return departement;
        }

        public IEnumerable<Departement> GetAllDept()
        {
            return _context.Departements.ToList();
        }

        public Departement GetDeptById(int id)
        {
            return _context.Departements.FirstOrDefault(d => d.Deptno == id);
        }

        public string UpdateDept(Departement departement)
        {
            var existingDept = _context.Departements.FirstOrDefault(d => d.Deptno == departement.Deptno);
            if (existingDept == null)
            {
                return "Departement not found";
            }

            existingDept.Deptname = departement.Deptname;
            existingDept.Mgrempno = departement.Mgrempno;

            _context.Departements.Update(existingDept);
            _context.SaveChanges();
            return "Departement updated successfully";
        }

        public string DeleteDept(int id)
        {
            var departement = _context.Departements.FirstOrDefault(d => d.Deptno == id);
            if (departement == null)
            {
                return "Departement not found";
            }

            _context.Departements.Remove(departement);
            _context.SaveChanges();
            return "Departement deleted successfully";
        }

        public IEnumerable<Employee> GetFemaleManagers()
        {
            var femaleManagers = _context.Departements
                .Where(d => d.Mgrempno.HasValue)
                .Select(d => d.MgrempnoNavigation)
                .Where(e => e != null && e.Sex == 'F')
                .OrderBy(e => e.Lname)
                .ThenBy(e => e.Fname)
                .ToList();

            return femaleManagers!;
        }

        public int CountFemaleManagers()
        {
            // Get count of female managers
            var count = _context.Departements
                .Where(d => d.Mgrempno.HasValue)
                .Select(d => d.MgrempnoNavigation)
                .Count(e => e != null && e.Sex == 'F');

            return count;
        }

        public IEnumerable<Employee> GetManagersRetire()
        {
            var tahunsekarang = DateTime.Now.Year;

            var managersRetire = _context.Departements
                .Where(d => d.Mgrempno.HasValue)
                .Select(d => d.MgrempnoNavigation)
                .Where(e => e != null && e.Dob.HasValue && (tahunsekarang - e.Dob.Value.Year) == 40)
                .OrderBy(e => e.Lname)
                .ToList();

            return managersRetire!;
        }

        public IEnumerable<Employee> GetManagersUnder40()
        {
            var tahunsekarang = DateTime.Now.Year;

            var managersUnder40 = _context.Departements
                .Where(d => d.Mgrempno.HasValue)
                .Select(d => d.MgrempnoNavigation)
                .Where(e => e != null && e.Dob.HasValue && (tahunsekarang - e.Dob.Value.Year) < 40)
                .OrderBy(e => e.Lname)
                .ToList();

            return managersUnder40!;
        }

    }
}
