using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PacienteController : ControllerBase
    {
        private readonly IPacienteService _pacienteService;

        public PacienteController(IPacienteService pacienteService)
        {
            _pacienteService = pacienteService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Paciente>> GetAllPacientes()
        {
            var pacientes = _pacienteService.GetAllPacientes();
            return Ok(pacientes);
        }

        [HttpGet("{id}")]
        public ActionResult<Paciente> GetPacienteById(int id)
        {
            var paciente = _pacienteService.GetPacienteById(id);

            if (paciente == null)
            {
                return NotFound();
            }

            return Ok(paciente);
        }

        [HttpPost]
        public IActionResult AddPaciente([FromBody] Paciente paciente)
        {

            try
            {
                _pacienteService.AddPaciente(paciente);
                return CreatedAtAction(nameof(GetPacienteById), new { id = paciente.Id }, paciente);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }

        }

        [HttpPut("{id}")]
        public IActionResult UpdatePaciente(int id, [FromBody] Paciente paciente)
        {

            if (id != paciente.Id)
            {
                return BadRequest();
            }

            try
            {
                _pacienteService.UpdatePaciente(paciente);
            }
            catch (Exception)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePaciente(int id)
        {
            var paciente = _pacienteService.GetPacienteById(id);

            if (paciente == null)
            {
                return NotFound();
            }

            _pacienteService.DeletePaciente(id);
            return NoContent();
        }

    }

}

