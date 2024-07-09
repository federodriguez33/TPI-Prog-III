using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ITurnoService
    {
        IEnumerable<Turno> GetAllTurnos();
        Turno GetTurnoById(int id);
        void AddTurno(Turno turno);
        void UpdateTurno(Turno turno);
        void DeleteTurno(int id);

    }

}
