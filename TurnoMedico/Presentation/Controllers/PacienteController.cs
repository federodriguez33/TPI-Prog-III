using Application.Interfaces;
using Application.Models.Dtos;
using Application.Models.Request;
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
    public class PacienteController : ControllerBase
    {
        private readonly IPacienteService _pacienteService;

        public PacienteController(IPacienteService pacienteService)
        {
            _pacienteService = pacienteService;
        }

        [HttpGet("GetAll")]
        public ActionResult<IEnumerable<PacienteDto>> GetAllPacientes()
        {
            var pacientes = _pacienteService.GetAllPacientes();
            return Ok(pacientes);
        }

        [HttpGet("{id}")]
        public ActionResult<PacienteDto> GetPacienteById(int id)
        {
            var pacienteDto = _pacienteService.GetPacienteById(id);

            if (pacienteDto == null)
            {
                return NotFound();
            }

            return Ok(pacienteDto);
        }

        [HttpPost]
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

        [HttpPut("{id}")]
        public IActionResult UpdatePaciente(int id, [FromBody] PacienteSaveRequest pacienteSaveRequest)
        {

            var updatedPaciente = _pacienteService.GetPacienteById(id);

            if (updatedPaciente == null)
            {
                return BadRequest();
            }

            try
            {
                _pacienteService.UpdatePaciente(pacienteSaveRequest);
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
            var pacienteDto = _pacienteService.GetPacienteById(id);

            if (pacienteDto == null)
            {
                return NotFound();
            }

            _pacienteService.DeletePaciente(id);
            return NoContent();
        }
    }
}


