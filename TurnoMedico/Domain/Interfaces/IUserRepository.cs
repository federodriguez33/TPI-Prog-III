using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Paciente GetPacienteByDNI(string DNI);
        Profesional GetProfesionalByDNI(string DNI);
        Admin GetAdminByDNI(string DNI);
    }
}
