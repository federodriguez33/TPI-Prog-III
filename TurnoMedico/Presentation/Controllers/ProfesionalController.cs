using Application.Interfaces;
using Application.Models.Dtos;
using Application.Models.Request;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Policy = "AdminOrProfesional")]
        public ActionResult<IEnumerable<ProfesionalDto>> GetAllProfesionales()
        {
            var profesionales = _profesionalService.GetAllProfesionales();
            return Ok(profesionales);
        }

        [HttpGet("MostrarProfesionalConID{id}")]
        [Authorize(Policy = "AdminOrProfesional")]
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
        [Authorize(Policy = "AdminOnly")]
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
        [Authorize(Policy = "AdminOrProfesional")]
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
        [Authorize(Policy = "AdminOnly")]
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


