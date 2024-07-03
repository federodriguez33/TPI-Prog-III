using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Paciente : User
    {
        public int Edad { get; set; }
        public string Diagnostico { get; set; }

        public ICollection<Turno> Turnos { get; set; } = new List<Turno>();
    }
}
