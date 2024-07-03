using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Profesional : User
    {
        public int Matricula { get; set; }
        public ICollection<Paciente> Pacientes { get; set; } = new List<Paciente>();
        public ICollection<Turno> Turnos { get; set; } = new List<Turno>();

    }
}
