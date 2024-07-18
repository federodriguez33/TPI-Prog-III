using Application.Models.Dtos;
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
        IEnumerable<TurnoGetDto> GetAllTurnos();
        TurnoGetDto GetTurnoById(int id);
        void AddTurno(TurnoDto turnoDto);
        void UpdateTurno(int turnoId, TurnoDto turnoDto);
        void DeleteTurno(int id);
    }

}
