using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Dtos
{
    public class TurnoDto
    {
        public int Id { get; set; }
        public int PacienteId { get; set; }
        public int ProfesionalId { get; set; }
        public DateTime FechaHora { get; set; }
        public PacienteDto Paciente { get; set; } = new PacienteDto();
        public ProfesionalDto Profesional { get; set; } = new ProfesionalDto();
    }
}
