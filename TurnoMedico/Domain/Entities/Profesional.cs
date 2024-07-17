using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Profesional : User
    {
        public string Matricula { get; set; } = string.Empty; 

        // Relación uno a muchos con Turno
        public ICollection<Turno> Turnos { get; set; } = new List<Turno>();

    }
}
