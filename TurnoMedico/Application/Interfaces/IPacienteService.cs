using Application.Models.Dtos;
using Application.Models.Request;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IPacienteService
    {
        IEnumerable<PacienteDto> GetAllPacientes();
        PacienteDto GetPacienteById(int id);
        void AddPaciente(PacienteSaveRequest pacienteSaveRequest);
        void UpdatePaciente(PacienteSaveRequest pacienteSaveRequest);
        void DeletePaciente(int id);

    }

}
