using CompanySystem.Interfaces;
using CompanySystem.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CompanySystem.Controllers
{
    [ApiController]
    [Route("api/W")]
    public class WorksOnController : ControllerBase
    {
        private readonly IWorksOnService _worksOnService;

        public WorksOnController(IWorksOnService worksOnService)
        {
            _worksOnService = worksOnService;
        }

       
        [HttpPost]
        public async Task<ActionResult<Workson>> CreateWork([FromBody] Workson worksOn)
        {
            if (worksOn == null)
            {
                return BadRequest("Workson object is null.");
            }

            var createdWork = await _worksOnService.CreateWorkAsync(worksOn);
            return CreatedAtAction(nameof(GetWorkById), new { id = createdWork.Id }, createdWork);
        }

        
        [HttpGet]
        public ActionResult<IEnumerable<Workson>> GetAllWorks()
        {
            var works = _worksOnService.GetAllWorks();
            return Ok(works);
        }

        
        [HttpGet("{id}")]
        public ActionResult<Workson> GetWorkById(int id)
        {
            var work = _worksOnService.GetWorkById(id);
            if (work == null)
            {
                return NotFound("Workson entry not found.");
            }
            return Ok(work);
        }

        
        [HttpPut("{id}")]
        public ActionResult<string> UpdateWork(int id, [FromBody] Workson worksOn)
        {
            if (worksOn == null)
            {
                return BadRequest("Workson object is null.");
            }

            if (id != worksOn.Id)
            {
                return BadRequest("Workson ID mismatch.");
            }

            var result = _worksOnService.UpdateWork(worksOn);
            if (result == "Workson entry not found")
            {
                return NotFound(result);
            }

            return Ok(result);
        }

     
        [HttpDelete("{id}")]
        public ActionResult<string> DeleteWork(int id)
        {
            var result = _worksOnService.DeleteWork(id);
            if (result == "Workson entry not found")
            {
                return NotFound(result);
            }

            return Ok(result);
        }
    }
}
