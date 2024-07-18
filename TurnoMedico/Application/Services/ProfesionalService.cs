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
                Especialidad = p.Especialidad,
                Matricula = p.Matricula,
                DNI = p.DNI,
                Telefono = p.Telefono,
                Email = p.Email,
                
            });
        }

        public ProfesionalDto GetProfesionalById(int id)
        {
            var profesional = _profesionalRepository.GetById(id);

            return new ProfesionalDto
            {
                Id = profesional.Id,
                Nombre = profesional.Nombre,
                Apellido = profesional.Apellido,
                Especialidad = profesional.Especialidad,
                Matricula = profesional.Matricula,
                DNI = profesional.DNI,
                Telefono = profesional.Telefono,
                Email = profesional.Email,
                
            };
        }

        public void AddProfesional(ProfesionalSaveRequest profesionalSaveRequest)
        {
            // Verificando si el profesional ya existe por DNI
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
                Especialidad = profesionalSaveRequest.Especialidad,
                Matricula = profesionalSaveRequest.Matricula,
                DNI = profesionalSaveRequest.DNI,
                Telefono = profesionalSaveRequest.Telefono,
                Email = profesionalSaveRequest.Email,
                  
            };

            _profesionalRepository.Add(profesional);
        }

        public void UpdateProfesional(ProfesionalDto profesionaldto, ProfesionalSaveRequest profesionalSaveRequest)
        {
            var profesional = _profesionalRepository.GetById(profesionaldto.Id);

            if (profesional == null)
            {
                throw new InvalidOperationException("Profesional no encontrado.");
            }

            // Verificando si otro profesional con el mismo DNI ya existe y está activo
            var existingProfesional = _profesionalRepository.FindActive(p => p.DNI == profesionalSaveRequest.DNI && p.Id != profesionaldto.Id).FirstOrDefault();

            if (existingProfesional != null)
            {
                throw new InvalidOperationException("Ya existe otro profesional con el mismo DNI.");
            }

            profesional.Nombre = profesionalSaveRequest.Nombre;
            profesional.Apellido = profesionalSaveRequest.Apellido;
            profesional.Password = profesionalSaveRequest.Password;
            profesional.Especialidad = profesionalSaveRequest.Especialidad;
            profesional.Matricula = profesionalSaveRequest.Matricula;
            profesional.DNI = profesionalSaveRequest.DNI;
            profesional.Telefono = profesionalSaveRequest.Telefono;
            profesional.Email = profesionalSaveRequest.Email;
     
            _profesionalRepository.Update(profesional);

        }

        public void DeleteProfesional(int id)
        {
            try
            {
                _profesionalRepository.Delete(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al intentar eliminar el profesional.", ex);
            }

        }

    }

}

