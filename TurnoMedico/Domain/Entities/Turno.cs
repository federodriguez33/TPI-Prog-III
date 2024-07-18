using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Swashbuckle.AspNetCore.Annotations;

namespace Domain.Entities
{
    public class Turno
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime FechaHora { get; set; }

        // Relación con Paciente
        public int PacienteId { get; set; }
        public string NombrePaciente { get; set; } = string.Empty;
        public string ApellidoPaciente { get; set; } = string.Empty;
        public string DNIPaciente { get; set; } = string.Empty;
        public Paciente Paciente { get; set; }

        // Relación con Profesional
        public int ProfesionalId { get; set; }
        public string NombreProfesional { get; set; } = string.Empty;
        public string ApellidoProfesional { get; set; } = string.Empty;
        public string MatriculaProfesional { get; set; } = string.Empty;
        public Profesional Profesional { get; set; }

        public Turno()
        {
        }

        public Turno(int pacienteId, int profesionalId, DateTime fechaHora)
        {
            PacienteId = pacienteId;
            ProfesionalId = profesionalId;
            FechaHora = fechaHora;

        }

    }

}
