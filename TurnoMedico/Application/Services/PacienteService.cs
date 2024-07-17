using Application.Interfaces;
using Application.Models.Dtos;
using Application.Models.Request;
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

        public void AddPaciente(PacienteSaveRequest pacienteSaveRequest)
        {
            // Verifica si el paciente ya existe por DNI
            var existingPaciente = _pacienteRepository.FindActive(p => p.DNI == pacienteSaveRequest.DNI).FirstOrDefault();

            if (existingPaciente != null)
            {
                throw new InvalidOperationException("El paciente ya existe.");
            }

            var paciente = new Paciente
            {
                Nombre = pacienteSaveRequest.Nombre,
                Apellido = pacienteSaveRequest.Apellido,
                Password = pacienteSaveRequest.Password,
                FechaNacimiento = pacienteSaveRequest.FechaNacimiento,
                Diagnostico = pacienteSaveRequest.Diagnostico,
                DNI = pacienteSaveRequest.DNI,
                Telefono = pacienteSaveRequest.Telefono,
                Email = pacienteSaveRequest.Email,
            };

            _pacienteRepository.Add(paciente);
        }

        public void UpdatePaciente(PacienteSaveRequest pacienteSaveRequest)
        {
            var paciente = _pacienteRepository.GetByDNI(pacienteSaveRequest.DNI);

            if (paciente == null)
            {
                throw new InvalidOperationException("Paciente no encontrado.");
            }

            // Verifica si otro paciente con el mismo DNI ya existe y está activo
            var existingPaciente = _pacienteRepository.FindActive(p => p.DNI == pacienteSaveRequest.DNI && p.Activo != pacienteSaveRequest.Activo).FirstOrDefault();

            if (existingPaciente != null)
            {
                throw new InvalidOperationException("Ya existe otro paciente con el mismo DNI.");
            }

            paciente.Nombre = pacienteSaveRequest.Nombre;
            paciente.Apellido = pacienteSaveRequest.Apellido;
            paciente.Password = pacienteSaveRequest.Password;
            paciente.FechaNacimiento = pacienteSaveRequest.FechaNacimiento;
            paciente.Diagnostico = pacienteSaveRequest.Diagnostico;
            paciente.DNI = pacienteSaveRequest.DNI;
            paciente.Telefono = pacienteSaveRequest.Telefono;
            paciente.Email = pacienteSaveRequest.Email;
           
            _pacienteRepository.Update(paciente);
        }

        public void DeletePaciente(int id)
        {
            _pacienteRepository.Delete(id);
        }
    }
}

