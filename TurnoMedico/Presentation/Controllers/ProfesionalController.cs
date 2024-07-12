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
    public class ProfesionalController : ControllerBase
    {
        private readonly IProfesionalService _profesionalService;

        public ProfesionalController(IProfesionalService profesionalService)
        {
            _profesionalService = profesionalService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ProfesionalDto>> GetAllProfesionales()
        {
            var profesionales = _profesionalService.GetAllProfesionales();
            return Ok(profesionales);
        }

        [HttpGet("{id}")]
        public ActionResult<ProfesionalDto> GetProfesionalById(int id)
        {
            var profesionalDto = _profesionalService.GetProfesionalById(id);

            if (profesionalDto == null)
            {
                return NotFound();
            }

            return Ok(profesionalDto);
        }

        [HttpPost]
        public IActionResult AddProfesional([FromBody] ProfesionalDto profesionalDto)
        {
            try
            {
                _profesionalService.AddProfesional(profesionalDto);
                return CreatedAtAction(nameof(GetProfesionalById), new { id = profesionalDto.Id }, profesionalDto);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }

        }

        [HttpPut("{id}")]
        public IActionResult UpdateProfesional(int id, [FromBody] ProfesionalDto profesionalDto)
        {
            if (id != profesionalDto.Id)
            {
                return BadRequest();
            }

            try
            {
                _profesionalService.UpdateProfesional(profesionalDto);
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
            var profesionalDto = _profesionalService.GetProfesionalById(id);

            if (profesionalDto == null)
            {
                return NotFound();
            }

            _profesionalService.DeleteProfesional(id);
            return NoContent();
        }
    }
}


