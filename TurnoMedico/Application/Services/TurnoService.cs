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

        public IEnumerable<Turno> GetAllTurnos()
        {
            var turnos = _turnoRepository.GetAll();

            return turnos.Select(t => new Turno
            {
                Id = t.Id,
                PacienteId = t.PacienteId,
                ProfesionalId = t.ProfesionalId,
                FechaHora = t.FechaHora,
            });
        }

        public Turno GetTurnoById(int id)
        {
            var turno = _turnoRepository.GetById(id);

            if (turno == null)
                throw new InvalidOperationException("El turno no existe.");

            return new Turno
            {
                Id = turno.Id,
                PacienteId = turno.PacienteId,
                ProfesionalId = turno.ProfesionalId,
                FechaHora = turno.FechaHora,
            };
        }

        public void AddTurno(TurnoDto turnoDto)
        {
            // Buscar el paciente por ID
            var paciente = _pacienteRepository.GetById(turnoDto.PacienteId);

            if (paciente == null)
            {
                throw new Exception("El paciente no existe.");
            }

            // Buscar el profesional por ID
            var profesional = _profesionalRepository.GetById(turnoDto.ProfesionalId);

            if (profesional == null)
            {
                throw new Exception("El profesional no existe.");
            }

            // Crear el turno y asignar el paciente y el profesional
            var turno = new Turno
            {
                PacienteId = turnoDto.PacienteId,
                Paciente = paciente,
                ProfesionalId = turnoDto.ProfesionalId,
                Profesional = profesional,
                FechaHora = turnoDto.FechaHora
            };

            if (_turnoRepository.IsTurnoAvailable(turno))
            {
                _turnoRepository.Add(turno);
            }
            else
            {
                throw new InvalidOperationException("El turno ya está reservado.");
            }
        }

        public void UpdateTurno(Turno turnoD)
        {
            var turno = _turnoRepository.GetById(turnoD.Id);

            if (turno == null)
            {
                throw new InvalidOperationException("Turno no encontrado.");
            }

            // Buscar el paciente por ID
            var paciente = _pacienteRepository.GetById(turnoD.PacienteId);
            if (paciente == null)
            {
                throw new Exception("El paciente no existe.");
            }

            // Buscar el profesional por ID
            var profesional = _profesionalRepository.GetById(turnoD.ProfesionalId);
            if (profesional == null)
            {
                throw new Exception("El profesional no existe.");
            }

            turno.PacienteId = turnoD.PacienteId;
            turno.Paciente = paciente;
            turno.ProfesionalId = turnoD.ProfesionalId;
            turno.Profesional = profesional;
            turno.FechaHora = turnoD.FechaHora;

            if (_turnoRepository.IsTurnoAvailable(turno))
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
            _turnoRepository.Delete(id);
        }
    }
}


