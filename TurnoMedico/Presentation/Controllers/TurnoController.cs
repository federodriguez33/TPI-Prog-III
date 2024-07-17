using Application.Interfaces;
using Application.Models.Dtos;
using Application.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

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
        public ActionResult<IEnumerable<TurnoDto>> GetAllTurnos()
        {
            var turnos = _turnoService.GetAllTurnos();
            return Ok(turnos);
        }

        [HttpGet("{id}")]
        public ActionResult<TurnoDto> GetTurnoById(int id)
        {
            var turnoDto = _turnoService.GetTurnoById(id);

            if (turnoDto == null)
            {
                return NotFound();
            }

            return Ok(turnoDto);
        }

        [HttpPost]
        public IActionResult AddTurno([FromBody] TurnoDto turnoDto)
        {
            try
            {
                _turnoService.AddTurno(turnoDto);
                return Ok("Turno confirmado con éxito");
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocurrió un error al agregar el turno.");
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateTurno(int id, [FromBody] Turno turnoD)
        {

            var updatedPaciente = _turnoService.GetTurnoById(id);

            if (updatedPaciente == null)
            {
                return BadRequest();
            }

            try
            {
                _turnoService.UpdateTurno(turnoD);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocurrió un error al actualizar el turno.");
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTurno(int id)
        {
            var turnoDto = _turnoService.GetTurnoById(id);

            if (turnoDto == null)
            {
                return NotFound();
            }

            try
            {
                _turnoService.DeleteTurno(id);
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocurrió un error al eliminar el turno.");
            }
        }
    }
}

