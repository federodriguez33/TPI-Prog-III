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
            if (_turnoRepository.IsTurnoAvailable(turno))
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
