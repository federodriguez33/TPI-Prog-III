using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfesionalController : ControllerBase
    {
        private readonly IProfesionalService _profesionalService;

        public ProfesionalController(IProfesionalService profesionalService)
        {
            _profesionalService = profesionalService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Profesional>> GetAllProfesionales()
        {
            var profesionales = _profesionalService.GetAllProfesionales();
            return Ok(profesionales);
        }

        [HttpGet("{id}")]
        public ActionResult<Profesional> GetProfesionalById(int id)
        {
            var profesional = _profesionalService.GetProfesionalById(id);

            if (profesional == null)
            {
                return NotFound();
            }

            return Ok(profesional);
        }

        [HttpPost]
        public IActionResult AddProfesional([FromBody] Profesional profesional)
        {
            try
            {
                _profesionalService.AddProfesional(profesional);
                return CreatedAtAction(nameof(GetProfesionalById), new { id = profesional.Id }, profesional);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }

        }

        [HttpPut("{id}")]
        public IActionResult UpdateProfesional(int id, [FromBody] Profesional profesional)
        {

            if (id != profesional.Id)
            {
                return BadRequest();
            }

            try
            {
                _profesionalService.UpdateProfesional(profesional);
            }
            catch (Exception)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProfesional(int id)
        {
            var profesional = _profesionalService.GetProfesionalById(id);

            if (profesional == null)
            {
                return NotFound();
            }

            _profesionalService.DeleteProfesional(id);
            return NoContent();
        }

    }

}

