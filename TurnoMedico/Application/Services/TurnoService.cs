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

        public TurnoService(ITurnoRepository turnoRepository)
        {
            _turnoRepository = turnoRepository;
        }

        public IEnumerable<TurnoDto> GetAllTurnos()
        {
            var turnos = _turnoRepository.GetAll();

            return turnos.Select(t => new TurnoDto
            {
                Id = t.Id,
                PacienteId = t.PacienteId,
                ProfesionalId = t.ProfesionalId,
                FechaHora = t.FechaHora,
                
            });
        }

        public TurnoDto GetTurnoById(int id)
        {
            var turno = _turnoRepository.GetById(id);

            if (turno == null)
                throw new InvalidOperationException("El turno no existe.");

            return new TurnoDto
            {
                Id = turno.Id,
                PacienteId = turno.PacienteId,
                ProfesionalId = turno.ProfesionalId,
                FechaHora = turno.FechaHora,
                
            };
        }

        public void AddTurno(TurnoDto turnoDto)
        {
    
            var paciente = new Paciente
            {
                Id = turnoDto.Paciente.Id,
                Nombre = turnoDto.Paciente.Nombre,
                Apellido = turnoDto.Paciente.Apellido,
            };

            var profesional = new Profesional
            {
                Id = turnoDto.Profesional.Id,
                Nombre = turnoDto.Profesional.Nombre,
                Apellido = turnoDto.Profesional.Apellido,
            };

            var turno = new Turno
            {
                Id = turnoDto.Id,
                PacienteId = turnoDto.PacienteId,
                ProfesionalId = turnoDto.ProfesionalId,
                FechaHora = turnoDto.FechaHora,

                Paciente = paciente,
                Profesional = profesional
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

        public void UpdateTurno(TurnoDto turnoDto)
        {
            var turno = _turnoRepository.GetById(turnoDto.Id);

            if (turno == null)
            {
                throw new InvalidOperationException("Turno no encontrado.");
            }

            turno.PacienteId = turnoDto.PacienteId;
            turno.ProfesionalId = turnoDto.ProfesionalId;
            turno.FechaHora = turnoDto.FechaHora;
            

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

