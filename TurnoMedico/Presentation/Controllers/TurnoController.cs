using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TurnoController : ControllerBase
    {
        private readonly ITurnoService _turnoService;

        public TurnoController(ITurnoService turnoService)
        {
            _turnoService = turnoService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Turno>> GetAllTurnos()
        {
            var turnos = _turnoService.GetAllTurnos();
            return Ok(turnos);
        }

        [HttpGet("{id}")]
        public ActionResult<Turno> GetTurnoById(int id)
        {
            var turno = _turnoService.GetTurnoById(id);

            if (turno == null)
            {
                return NotFound();
            }

            return Ok(turno);
        }

        [HttpPost]
        public IActionResult AddTurno([FromBody] Turno turno)
        {
            _turnoService.AddTurno(turno);
            return CreatedAtAction(nameof(GetTurnoById), new { id = turno.Id }, turno);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateTurno(int id, [FromBody] Turno turno)
        {
            if (id != turno.Id)
            {
                return BadRequest();
            }

            try
            {
                _turnoService.UpdateTurno(turno);
            }
            catch (Exception)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTurno(int id)
        {
            var turno = _turnoService.GetTurnoById(id);

            if (turno == null)
            {
                return NotFound();
            }

            _turnoService.DeleteTurno(id);
            return NoContent();
        }

    }

}
