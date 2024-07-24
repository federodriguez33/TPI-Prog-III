using Application.Interfaces;
using Application.Models.Dtos;
using Application.Models.Request;
using Application.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

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

        [HttpGet("MostrarTodosLosPaciente")]
        [Authorize(Policy = "AdminOrPaciente")]
        public ActionResult<IEnumerable<PacienteDto>> GetAllPacientes()
        {
            var pacientes = _pacienteService.GetAllPacientes();
            return Ok(pacientes);
        }

        [HttpGet("MostrarPacienteConID{id}")]
        [Authorize(Policy = "AdminOrPaciente")]
        public ActionResult<PacienteDto> GetPacienteById(int id)
        {
            try
            {
                var pacienteDto = _pacienteService.GetPacienteById(id);
                return Ok(pacienteDto);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPost("CrearPaciente")]
        [Authorize(Policy = "AdminOnly")]
        public IActionResult AddPaciente([FromBody] PacienteSaveRequest pacienteSaveRequest)
        {
            try
            {
                _pacienteService.AddPaciente(pacienteSaveRequest);
                return Ok(pacienteSaveRequest.Nombre);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("ModificarPacienteConID{id}")]
        [Authorize(Policy = "AdminOrPaciente")]
        public IActionResult UpdatePaciente(int id, [FromBody] PacienteSaveRequest pacienteSaveRequest)
        {

            var updatedPaciente = _pacienteService.GetPacienteById(id);

            if (updatedPaciente == null)
            {
                return BadRequest("El paciente no existe");
            }

            try
            {
                _pacienteService.UpdatePaciente(updatedPaciente, pacienteSaveRequest);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, "Error interno al actualizar el paciente.");
            }

            return Ok("Paciente actualizado con éxito");
        }

        [HttpDelete("EliminarPacienteConID{id}")]
        [Authorize(Policy = "AdminOnly")]
        public IActionResult DeletePaciente(int id)
        {
            var pacienteDto = _pacienteService.GetPacienteById(id);

            if (pacienteDto == null)
            {
                return NotFound("El paciente no existe");
            }

            try
            {
                _pacienteService.DeletePaciente(id);
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }

        }
    }
}


