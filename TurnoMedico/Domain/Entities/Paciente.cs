using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Paciente : User
    {
        public DateTime FechaNacimiento { get; set; }
        public string Diagnostico { get; set; } = string.Empty;
        public string Role { get; set; } = "paciente";

        // Relación uno a muchos con Turno
        public ICollection<Turno> Turnos { get; set; } = new List<Turno>();
    }
}
