using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class TurnoService : ITurnoService
    {
        private readonly ITurnoRepository _turnoRepository;

        public TurnoService(ITurnoRepository turnoRepository)
        {
            _turnoRepository = turnoRepository;
        }

        public IEnumerable<Turno> GetAllTurnos()
        {
            return _turnoRepository.GetAll();
        }

        public Turno GetTurnoById(int id)
        {
            return _turnoRepository.GetById(id);
        }

        public void AddTurno(Turno turno)
        {
            if (IsTurnoAvailable(turno))
            {
                _turnoRepository.Add(turno);
            }
            else
            {
                throw new InvalidOperationException("El turno ya está reservado.");
            }
        }

        public void UpdateTurno(Turno turno)
        {
            if (IsTurnoAvailable(turno))
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

        private bool IsTurnoAvailable(Turno turno)
        {
            // Lógica para verificar si el turno ya está reservado
            var existingTurnos = _turnoRepository.Find(t => t.ProfesionalId == turno.ProfesionalId && t.FechaHora == turno.FechaHora);
            return !existingTurnos.Any();
        }

    }

}
