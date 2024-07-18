using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Dtos
{
    public class TurnoGetDto
    {
        public int Id { get; set; }
        public DateTime FechaHora { get; set; }

        // Relación con Paciente
        public int PacienteId { get; set; }
        public string NombrePaciente { get; set; } = string.Empty;
        public string ApellidoPaciente { get; set; } = string.Empty;
        public string DiagnosticoPaciente { get; set; } = string.Empty;
        public string DNIPaciente { get; set; } = string.Empty;

        // Relación con Profesional
        public int ProfesionalId { get; set; }
        public string NombreProfesional { get; set; } = string.Empty;
        public string ApellidoProfesional { get; set; } = string.Empty;
        public string EspecialidadProfesional { get; set; } = string.Empty;
        public string MatriculaProfesional { get; set; } = string.Empty;
    }

}
