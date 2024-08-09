using CompanySystem.Interfaces;
using CompanySystem.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CompanySystem.Services
{
    public class WorksOnService : IWorksOnService
    {
        private readonly CompanyContext _context;

        public WorksOnService(CompanyContext context)
        {
            _context = context;
        }

        public async Task<Workson> CreateWorkAsync(Workson worksOn)
        {
            if (worksOn == null)
            {
                throw new ArgumentNullException(nameof(worksOn));
            }

            await _context.Worksons.AddAsync(worksOn);
            await _context.SaveChangesAsync();
            return worksOn;
        }

        public IEnumerable<Workson> GetAllWorks()
        {
            return _context.Worksons
                .Include(w => w.EmpnoNavigation)
                .Include(w => w.ProjnoNavigation)
                .ToList();
        }

        public Workson GetWorkById(int id)
        {
            return _context.Worksons
                .Include(w => w.EmpnoNavigation)
                .Include(w => w.ProjnoNavigation)
                .FirstOrDefault(w => w.Id == id);
        }

        public string UpdateWork(Workson worksOn)
        {
            var existingWork = _context.Worksons.FirstOrDefault(w => w.Id == worksOn.Id);
            if (existingWork == null)
            {
                return "Workson entry not found";
            }

            existingWork.Empno = worksOn.Empno;
            existingWork.Projno = worksOn.Projno;
            existingWork.Dateworked = worksOn.Dateworked;
            existingWork.Hoursworked = worksOn.Hoursworked;

            _context.Worksons.Update(existingWork);
            _context.SaveChanges();

            return "Workson entry updated successfully";
        }

        public string DeleteWork(int id)
        {
            var worksOn = _context.Worksons.FirstOrDefault(w => w.Id == id);
            if (worksOn == null)
            {
                return "Workson entry not found";
            }

            _context.Worksons.Remove(worksOn);
            _context.SaveChanges();

            return "Workson entry deleted successfully";
        }
    }
}
