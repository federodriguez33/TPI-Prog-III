using Application.Interfaces;
using Application.Models.Dtos;
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
        public IActionResult AddPaciente([FromBody] PacienteDto pacienteDto)
        {
            try
            {
                _pacienteService.AddPaciente(pacienteDto);
                return CreatedAtAction(nameof(GetPacienteById), new { id = pacienteDto.Id }, pacienteDto);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdatePaciente(int id, [FromBody] PacienteDto pacienteDto)
        {
            if (id != pacienteDto.Id)
            {
                return BadRequest();
            }

            try
            {
                _pacienteService.UpdatePaciente(pacienteDto);
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


