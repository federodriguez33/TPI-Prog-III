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
        IEnumerable<Paciente> GetAllPacientes();
        Paciente GetPacienteById(int id);
        void AddPaciente(Paciente paciente);
        void UpdatePaciente(Paciente paciente);
        void DeletePaciente(int id);

    }

}
