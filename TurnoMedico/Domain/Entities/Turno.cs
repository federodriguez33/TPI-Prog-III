using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Turno
    {
        public DateTime FechaHora { get; set; }
        public ICollection<Paciente> Pacientes { get; set; } = new List<Paciente>();
        public ICollection<Profesional> Profesionales { get; set; } = new List<Profesional>();
    }
}
