﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Profesional : User
    {
        public string Especialidad { get; set; } = string.Empty;
        public string Matricula { get; set; } = string.Empty;
        public string Role { get; set; } = "profesional";

        // Relación uno a muchos con Turno
        public ICollection<Turno> Turnos { get; set; } = new List<Turno>();

    }
}
