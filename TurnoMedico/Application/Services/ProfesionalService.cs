using Application.Interfaces;
using Application.Models.Dtos;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Application.Services
{
    public class ProfesionalService : IProfesionalService
    {
        private readonly IProfesionalRepository _profesionalRepository;

        public ProfesionalService(IProfesionalRepository profesionalRepository)
        {
            _profesionalRepository = profesionalRepository;
        }

        public IEnumerable<ProfesionalDto> GetAllProfesionales()
        {
            var profesionales = _profesionalRepository.GetAll();

            return profesionales.Select(p => new ProfesionalDto
            {
                Id = p.Id,
                Nombre = p.Nombre,
                Apellido = p.Apellido,
                Matricula = p.Matricula,
                DNI = p.DNI,
                Telefono = p.Telefono,
                Email = p.Email,
                
            });
        }

        public ProfesionalDto GetProfesionalById(int id)
        {
            var profesional = _profesionalRepository.GetById(id);

            if (profesional == null)
                throw new InvalidOperationException("El profesional no existe.");

            return new ProfesionalDto
            {
                Id = profesional.Id,
                Nombre = profesional.Nombre,
                Apellido = profesional.Apellido,
                Matricula = profesional.Matricula,
                DNI = profesional.DNI,
                Telefono = profesional.Telefono,
                Email = profesional.Email,
                
            };
        }

        public void AddProfesional(ProfesionalDto profesionalDto)
        {
            // Verifica si el profesional ya existe por DNI y está activo
            var existingProfesional = _profesionalRepository.FindActive(p => p.DNI == profesionalDto.DNI).FirstOrDefault();

            if (existingProfesional != null)
            {
                throw new InvalidOperationException("El profesional ya existe.");
            }

            var profesional = new Profesional
            {
                Id = profesionalDto.Id,
                Nombre = profesionalDto.Nombre,
                Apellido = profesionalDto.Apellido,
                Matricula = profesionalDto.Matricula,
                DNI = profesionalDto.DNI,
                Telefono = profesionalDto.Telefono,
                Email = profesionalDto.Email,
                  
            };

            _profesionalRepository.Add(profesional);
        }

        public void UpdateProfesional(ProfesionalDto profesionalDto)
        {
            var profesional = _profesionalRepository.GetById(profesionalDto.Id);

            if (profesional == null)
            {
                throw new InvalidOperationException("Profesional no encontrado.");
            }

            // Verifica si otro profesional con el mismo DNI ya existe y está activo
            var existingProfesional = _profesionalRepository.FindActive(p => p.DNI == profesionalDto.DNI && p.Id != profesionalDto.Id).FirstOrDefault();

            if (existingProfesional != null)
            {
                throw new InvalidOperationException("Ya existe otro profesional con el mismo DNI.");
            }

            profesional.Nombre = profesionalDto.Nombre;
            profesional.Apellido = profesionalDto.Apellido;
            profesional.Matricula = profesionalDto.Matricula;
            profesional.DNI = profesionalDto.DNI;
            profesional.Telefono = profesionalDto.Telefono;
            profesional.Email = profesionalDto.Email;
     
            _profesionalRepository.Update(profesional);

        }

        public void DeleteProfesional(int id)
        {
            _profesionalRepository.Delete(id);
        }
    }
}

