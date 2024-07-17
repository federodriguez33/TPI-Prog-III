using Application.Interfaces;
using Application.Models.Dtos;
using Application.Models.Request;
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

        public void AddProfesional(ProfesionalSaveRequest profesionalSaveRequest)
        {
            // Verifica si el profesional ya existe por DNI
            var existingProfesional = _profesionalRepository.FindActive(p => p.DNI == profesionalSaveRequest.DNI).FirstOrDefault();

            if (existingProfesional != null)
            {
                throw new InvalidOperationException("El profesional ya existe.");
            }

            var profesional = new Profesional
            {
                Nombre = profesionalSaveRequest.Nombre,
                Apellido = profesionalSaveRequest.Apellido,
                Password = profesionalSaveRequest.Password,
                Matricula = profesionalSaveRequest.Matricula,
                DNI = profesionalSaveRequest.DNI,
                Telefono = profesionalSaveRequest.Telefono,
                Email = profesionalSaveRequest.Email,
                  
            };

            _profesionalRepository.Add(profesional);
        }

        public void UpdateProfesional(ProfesionalSaveRequest profesionalSaveRequest)
        {
            var profesional = _profesionalRepository.GetById(profesionalSaveRequest.Id);

            if (profesional == null)
            {
                throw new InvalidOperationException("Profesional no encontrado.");
            }

            // Verifica si otro profesional con el mismo DNI ya existe y está activo
            var existingProfesional = _profesionalRepository.FindActive(p => p.DNI == profesionalSaveRequest.DNI && p.Activo != profesionalSaveRequest.Activo).FirstOrDefault();

            if (existingProfesional != null)
            {
                throw new InvalidOperationException("Ya existe otro profesional con el mismo DNI.");
            }

            profesional.Nombre = profesionalSaveRequest.Nombre;
            profesional.Apellido = profesionalSaveRequest.Apellido;
            profesional.Password = profesionalSaveRequest.Password;
            profesional.Matricula = profesionalSaveRequest.Matricula;
            profesional.DNI = profesionalSaveRequest.DNI;
            profesional.Telefono = profesionalSaveRequest.Telefono;
            profesional.Email = profesionalSaveRequest.Email;
     
            _profesionalRepository.Update(profesional);

        }

        public void DeleteProfesional(int id)
        {
            _profesionalRepository.Delete(id);
        }
    }
}

