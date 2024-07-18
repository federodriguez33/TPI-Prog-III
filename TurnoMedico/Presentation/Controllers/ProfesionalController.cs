using Application.Interfaces;
using Application.Models.Dtos;
using Application.Models.Request;
using Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

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

        [HttpGet("MostrarTodosLosProfesionales")]
        public ActionResult<IEnumerable<ProfesionalDto>> GetAllProfesionales()
        {
            var profesionales = _profesionalService.GetAllProfesionales();
            return Ok(profesionales);
        }

        [HttpGet("MostrarProfesionalConID{id}")]
        public ActionResult<ProfesionalDto> GetProfesionalById(int id)
        {
            try
            {
                var profesionalDto = _profesionalService.GetProfesionalById(id);
                return Ok(profesionalDto);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPost("CrearProfesional")]
        public IActionResult AddProfesional([FromBody] ProfesionalSaveRequest profesionalSaveRequest)
        {
            try
            {
                _profesionalService.AddProfesional(profesionalSaveRequest);
                return Ok(profesionalSaveRequest.Nombre);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }

        }

        [HttpPut("ModificarProfesionalConID{id}")]
        public IActionResult UpdateProfesional(int id, [FromBody] ProfesionalSaveRequest profesionalSaveRequest)
        {

            var updatedProfesional = _profesionalService.GetProfesionalById(id);

            if (updatedProfesional == null)
            {
                return BadRequest();
            }

            try
            {
                _profesionalService.UpdateProfesional(updatedProfesional, profesionalSaveRequest);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, "Error interno al actualizar el profesional.");
            }

            return NoContent();
        }

        [HttpDelete("EliminarProfesionalConID{id}")]
        public IActionResult DeleteProfesional(int id)
        {
            var profesionalDto = _profesionalService.GetProfesionalById(id);

            if (profesionalDto == null)
            {
                return NotFound();
            }

            try
            {
                _profesionalService.DeleteProfesional(id);
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


