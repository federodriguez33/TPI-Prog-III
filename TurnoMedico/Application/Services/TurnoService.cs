using Application.Interfaces;
using Application.Models.Dtos;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Application.Services
{
    public class TurnoService : ITurnoService
    {
        private readonly ITurnoRepository _turnoRepository;
        private readonly IPacienteRepository _pacienteRepository;
        private readonly IProfesionalRepository _profesionalRepository;

        public TurnoService(
            ITurnoRepository turnoRepository,
            IPacienteRepository pacienteRepository,
            IProfesionalRepository profesionalRepository)
        {
            _turnoRepository = turnoRepository;
            _pacienteRepository = pacienteRepository;
            _profesionalRepository = profesionalRepository;
        }

        public IEnumerable<TurnoGetDto> GetAllTurnos()
        {
            var turnos = _turnoRepository.GetAll();

            return turnos.Select(t => new TurnoGetDto
            {
                Id = t.Id,
                FechaHora = t.FechaHora,
                PacienteId = t.PacienteId,
                NombrePaciente = t.Paciente.Nombre,
                ApellidoPaciente = t.Paciente.Apellido,
                DiagnosticoPaciente = t.Paciente.Diagnostico,
                DNIPaciente = t.Paciente.DNI,
                ProfesionalId = t.ProfesionalId,
                NombreProfesional = t.Profesional.Nombre,
                ApellidoProfesional = t.Profesional.Apellido,
                EspecialidadProfesional = t.Profesional.Especialidad,
                MatriculaProfesional = t.Profesional.Matricula,
            });
        }

        public TurnoGetDto GetTurnoById(int id)
        {
            var turno = _turnoRepository.GetById(id);

            return new TurnoGetDto
            {
                Id = turno.Id,
                FechaHora = turno.FechaHora,
                PacienteId = turno.PacienteId,
                NombrePaciente = turno.Paciente.Nombre,
                ApellidoPaciente = turno.Paciente.Apellido,
                DiagnosticoPaciente = turno.Paciente.Diagnostico,
                DNIPaciente = turno.Paciente.DNI,
                ProfesionalId = turno.ProfesionalId,
                NombreProfesional = turno.Profesional.Nombre,
                ApellidoProfesional = turno.Profesional.Apellido,
                EspecialidadProfesional = turno.Profesional.Especialidad,
                MatriculaProfesional = turno.Profesional.Matricula,
            };
        }

        public void AddTurno(TurnoDto turnoDto)
        {
            // Buscando paciente por ID
            var paciente = _pacienteRepository.GetById(turnoDto.PacienteId);

            if (paciente == null)
            {
                throw new Exception("El paciente no existe.");
            }

            // Buscando profesional por ID
            var profesional = _profesionalRepository.GetById(turnoDto.ProfesionalId);

            if (profesional == null)
            {
                throw new Exception("El profesional no existe.");
            }

            // Creando el turno y asignandole el paciente y el profesional
            var turno = new Turno
            {
                PacienteId = turnoDto.PacienteId,
                Paciente = paciente,
                ProfesionalId = turnoDto.ProfesionalId,
                Profesional = profesional,
                FechaHora = turnoDto.FechaHora
            };

            if (_turnoRepository.TurnoDisponible(turno))
            {
                _turnoRepository.Add(turno);
            }
            else
            {
                throw new InvalidOperationException("El turno ya está reservado.");
            }
        }

        public void UpdateTurno(int turnoId, TurnoDto turnoDto)
        {
            var turno = _turnoRepository.GetById(turnoId);

            if (turno == null)
            {
                throw new InvalidOperationException("Turno no encontrado.");
            }

            // Buscando paciente por ID
            var paciente = _pacienteRepository.GetById(turnoDto.PacienteId);
            if (paciente == null)
            {
                throw new Exception("El paciente no existe.");
            }

            // Buscando profesional por ID
            var profesional = _profesionalRepository.GetById(turnoDto.ProfesionalId);
            if (profesional == null)
            {
                throw new Exception("El profesional no existe.");
            }

            turno.PacienteId = turnoDto.PacienteId;
            turno.ProfesionalId = turnoDto.ProfesionalId;
            turno.FechaHora = turnoDto.FechaHora;

            if (_turnoRepository.TurnoDisponible(turno))
            {
                _turnoRepository.Update(turno);
            }
            else
            {
                throw new InvalidOperationException("El turno ya está reservado.");
            }
        }

        public void DeleteTurno(int id)
        {
            try
            {
                _turnoRepository.Delete(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al intentar eliminar el turno.", ex);
            }

        }

    }

}


