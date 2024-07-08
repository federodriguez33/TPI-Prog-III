using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class ProfesionalRepository : BaseRepository<Profesional>, IProfesionalRepository
    {
        public ProfesionalRepository(TurnoMedicoDbContext context) : base(context)
        {
        }

        // Aquí puedes añadir métodos específicos de Profesional, si es necesario
    }
}
