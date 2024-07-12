using Application.Interfaces;
using Application.Models.Dtos;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class PacienteService : IPacienteService
    {
        private readonly IPacienteRepository _pacienteRepository;

        public PacienteService(IPacienteRepository pacienteRepository)
        {
            _pacienteRepository = pacienteRepository;
        }

        public IEnumerable<PacienteDto> GetAllPacientes()
        {
            var pacientes = _pacienteRepository.GetAll();
            return pacientes.Select(p => new PacienteDto
            {
                Id = p.Id,
                Nombre = p.Nombre,
                Apellido = p.Apellido,
                Email = p.Email,
                Telefono = p.Telefono,
                FechaNacimiento = p.FechaNacimiento,
                DNI = p.DNI
            });
        }

        public PacienteDto GetPacienteById(int id)
        {
            var paciente = _pacienteRepository.GetById(id);

            if (paciente == null)
                throw new InvalidOperationException("El paciente no existe.");

            return new PacienteDto
            {
                Id = paciente.Id,
                Nombre = paciente.Nombre,
                Apellido = paciente.Apellido,
                FechaNacimiento = paciente.FechaNacimiento,
                Diagnostico = paciente.Diagnostico,
                DNI = paciente.DNI,
                Telefono = paciente.Telefono,
                Email = paciente.Email,
                
            };
        }

        public void AddPaciente(PacienteDto pacienteDto)
        {
            // Verifica si el paciente ya existe por DNI y está activo
            var existingPaciente = _pacienteRepository.FindActive(p => p.DNI == pacienteDto.DNI).FirstOrDefault();

            if (existingPaciente != null)
            {
                throw new InvalidOperationException("El paciente ya existe.");
            }

            var paciente = new Paciente
            {
                Id = pacienteDto.Id,
                Nombre = pacienteDto.Nombre,
                Apellido = pacienteDto.Apellido,
                FechaNacimiento = pacienteDto.FechaNacimiento,
                Diagnostico = pacienteDto.Diagnostico,
                DNI = pacienteDto.DNI,
                Telefono = pacienteDto.Telefono,
                Email = pacienteDto.Email,
            };

            _pacienteRepository.Add(paciente);
        }

        public void UpdatePaciente(PacienteDto pacienteDto)
        {
            var paciente = _pacienteRepository.GetById(pacienteDto.Id);

            if (paciente == null)
            {
                throw new InvalidOperationException("Paciente no encontrado.");
            }

            // Verifica si otro paciente con el mismo DNI ya existe y está activo
            var existingPaciente = _pacienteRepository.FindActive(p => p.DNI == pacienteDto.DNI && p.Id != pacienteDto.Id).FirstOrDefault();

            if (existingPaciente != null)
            {
                throw new InvalidOperationException("Ya existe otro paciente con el mismo DNI.");
            }

            paciente.Nombre = pacienteDto.Nombre;
            paciente.Apellido = pacienteDto.Apellido;
            paciente.FechaNacimiento = pacienteDto.FechaNacimiento;
            paciente.Diagnostico = pacienteDto.Diagnostico;
            paciente.Email = pacienteDto.Email;
            paciente.DNI = pacienteDto.DNI;
            paciente.Telefono = pacienteDto.Telefono;
            paciente.Email = pacienteDto.Email;
           
            _pacienteRepository.Update(paciente);
        }

        public void DeletePaciente(int id)
        {
            _pacienteRepository.Delete(id);
        }
    }
}

