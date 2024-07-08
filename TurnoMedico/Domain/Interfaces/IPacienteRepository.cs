using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IPacienteRepository : IBaseRepository<Paciente>
    {
        // Aquí puedes definir métodos específicos para Paciente, si es necesario
    }
}
