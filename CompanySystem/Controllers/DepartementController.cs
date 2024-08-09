using CompanySystem.Interfaces;
using CompanySystem.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CompanySystem.Controllers
{
    [ApiController]
    [Route("api/dept")]
    public class DepartementController : ControllerBase
    {
        private readonly IDepartementService _departementService;

        public DepartementController(IDepartementService departementService)
        {
            _departementService = departementService;
        }

        [HttpPost]
        public async Task<ActionResult<Departement>> CreateDept([FromBody] Departement departement)
        {
            if (departement == null)
            {
                return BadRequest("Departement tidak ada");
            }

            var createdDept = await _departementService.CreateDept(departement);
            return CreatedAtAction(nameof(GetDeptById), new { id = createdDept.Deptno }, createdDept);
        }

        [HttpGet]
        public ActionResult<IEnumerable<Departement>> GetAllDept()
        {
            var departements = _departementService.GetAllDept();
            return Ok(departements);
        }

        [HttpGet("{id}")]
        public ActionResult<Departement> GetDeptById(int id)
        {
            var departement = _departementService.GetDeptById(id);
            if (departement == null)
            {
                return NotFound("Departement tidak ditemukan");
            }
            return Ok(departement);
        }

        [HttpPut("{id}")]
        public ActionResult<string> UpdateDept(int id, [FromBody] Departement departement)
        {
            if (departement == null || id != departement.Deptno)
            {
                return BadRequest("Invalid input data");
            }

            var result = _departementService.UpdateDept(departement);
            if (result == "Departement tidak ada")
            {
                return NotFound(result);
            }

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public ActionResult<string> DeleteDept(int id)
        {
            var result = _departementService.DeleteDept(id);
            if (result == "Departement tidak ada")
            {
                return NotFound(result);
            }

            return Ok(result);
        }

        [HttpGet("female-managers")]
        public ActionResult<IEnumerable<Employee>> GetFemaleManagers()
        {
            var femaleManagers = _departementService.GetFemaleManagers();
            return Ok(femaleManagers);
        }

        [HttpGet("count-female-managers")]
        public ActionResult<int> CountFemaleManagers()
        {
            var count = _departementService.CountFemaleManagers();
            return Ok(count);
        }

        [HttpGet("managers-retire")]
        public ActionResult<IEnumerable<Employee>> GetManagersDueToRetireThisYear()
        {
            var managersDueToRetire = _departementService.GetManagersRetire();
            return Ok(managersDueToRetire);
        }

        [HttpGet("managers-under-40")]
        public ActionResult<IEnumerable<Employee>> GetManagersUnder40()
        {
            var managersUnder40 = _departementService.GetManagersUnder40();
            return Ok(managersUnder40);
        }
    }
}
