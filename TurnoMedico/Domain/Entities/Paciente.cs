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
        public string Diagnostico { get; set; } = string.Empty;
        public Paciente()
        {
            UserType = "paciente";
        }

        // Relación muchos a muchos con Profesional
        public ICollection<Profesional> Profesionales { get; set; } = new List<Profesional>();

        // Relación uno a muchos con Turno
        public ICollection<Turno> Turnos { get; set; } = new List<Turno>();
    }
}
